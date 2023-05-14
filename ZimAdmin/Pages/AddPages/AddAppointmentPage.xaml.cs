using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ZimAdmin.Classes;
using ZimAdmin.Entitys;

namespace ZimAdmin.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddAndEditAppointmentPage.xaml
    /// </summary>
    public partial class AddAppointmentPage : Page
    {
        private Appointments currentAppointment = new Appointments();
        CharsBlocker blocker = new CharsBlocker();
        public AddAppointmentPage()
        {
            InitializeComponent();
            DataContext = currentAppointment;
            cbDoctors.ItemsSource = GetDbContext.GetContext().Doctors.ToList();
            cbPatients.ItemsSource = GetDbContext.GetContext().Patients.ToList();
        }

        private void tpAppointment_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (cbDoctors.SelectedItem == null)
                errors.AppendLine("Доктор не выбран");
            if (cbPatients.SelectedItem == null)
                errors.AppendLine("Пациент не выбран");
            if (dtpAppointmentDate.Value == null)
                errors.AppendLine("Дата не назначена");
            else if (dtpAppointmentDate.Value.Value.Date < DateTime.Now.Date)
                errors.AppendLine("Дата не может быть прошедшей");
            else if (dtpAppointmentDate.Value.Value.Hour <= DateTime.Now.Hour && dtpAppointmentDate.Value.Value.Date == DateTime.Now.Date)
                errors.AppendLine("Время не может быть прошедшим");
            else if (!isDoctorTime())
                errors.AppendLine("Доктор не работает в это время");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString(), "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            currentAppointment.Patients = (Patients)cbPatients.SelectedItem;
            currentAppointment.id_Patient = currentAppointment.Patients.id_Patient;
            currentAppointment.id_Doctor = currentAppointment.Doctors.id_Doctor;
            currentAppointment.DataTime_Appointment = dtpAppointmentDate.Value.Value;

            try
            {
                GetDbContext.GetContext().Appointments.Add(currentAppointment);
                MessageBox.Show("Приём назначен", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                GetDbContext.GetContext().SaveChanges();
                ManageClass.getFrame.GoBack();
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка базы данных", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            ManageClass.getFrame.GoBack();
        }

        private void cbDoctors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            currentAppointment.Doctors = (Doctors)cbDoctors.SelectedItem;
        }

        private bool isDoctorTime()
        {
            int shift = currentAppointment.Doctors.Shift;
            if (shift == 1 && dtpAppointmentDate.Value.Value.Hour > 14 || dtpAppointmentDate.Value.Value.Hour < 8)
                return false;
            else if (shift == 2 && dtpAppointmentDate.Value.Value.Hour < 14 || dtpAppointmentDate.Value.Value.Hour > 19)
                return false;
            else if(shift == 3 && dtpAppointmentDate.Value.Value.Hour > 19 || dtpAppointmentDate.Value.Value.Hour < 8)
                return false;
            return true;
        }

        private void dtpAppointmentDate_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            blocker.spaceBlocker(e);
            blocker.backSpaceBlocker(e);
            e.Handled = true;
        }
    }
}