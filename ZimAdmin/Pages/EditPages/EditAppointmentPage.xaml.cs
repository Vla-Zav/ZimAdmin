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
    /// Логика взаимодействия для EditAppointmentPage.xaml
    /// </summary>
    public partial class EditAppointmentPage : Page
    {
        private Appointments currentAppointment = new Appointments();
        public EditAppointmentPage(Appointments selectedAppointment)
        {
            InitializeComponent();
            currentAppointment = selectedAppointment;
            DataContext = currentAppointment;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            ManageClass.getFrame.GoBack();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (dtpEditDateTime.Value.Value.Date < DateTime.Now.Date)
                errors.AppendLine("Дата не может быть прошедшей");
            else if (dtpEditDateTime.Value.Value.Hour < DateTime.Now.Hour && dtpEditDateTime.Value.Value.Date == DateTime.Now.Date)
                errors.AppendLine("Время не может быть прошедшим");
            else if (!IsDoctorTime())
                errors.AppendLine("Доктор не работает в это время");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString(), "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            currentAppointment.DataTime_Appointment = dtpEditDateTime.Value.Value;

            try
            {
                MessageBox.Show("Приём назначен", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                GetDbContext.GetContext().SaveChanges();
                ManageClass.getFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка базы данных", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool IsDoctorTime()
        {
            int shift = currentAppointment.Doctors.Shift;
            if (shift == 1 && dtpEditDateTime.Value.Value.Hour > 14 || dtpEditDateTime.Value.Value.Hour < 8)
                return false;
            else if (shift == 2 && dtpEditDateTime.Value.Value.Hour < 14 || dtpEditDateTime.Value.Value.Hour > 19)
                return false;
            else if (shift == 3 && dtpEditDateTime.Value.Value.Hour > 19 || dtpEditDateTime.Value.Value.Hour < 8)
                return false;
            return true;
        }

        private void dtpEditDateTime_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
        }
    }
}
