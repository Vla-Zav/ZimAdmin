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
    /// Логика взаимодействия для AppointmentsPage.xaml
    /// </summary>
    public partial class AppointmentsPage : Page
    {
        public AppointmentsPage()
        {
            InitializeComponent();
        }

        private void btnAddAppointment_Click(object sender, RoutedEventArgs e)
        {
            ManageClass.getFrame.Navigate(new AddAppointmentPage());
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                HasData();
                GetDbContext.GetContext().ChangeTracker.Entries().ToList().ForEach(entry => entry.Reload());
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            ManageClass.getFrame.Navigate(new EditAppointmentPage((sender as Button).DataContext as Appointments));
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            var delAppoint = dgAppointments.SelectedItems.Cast<Appointments>().ToList();
            if (MessageBox.Show("Вы уверены, что хотите удалить назначение?", "Уведомлние", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    GetDbContext.GetContext().Appointments.RemoveRange(delAppoint);
                    GetDbContext.GetContext().SaveChanges();
                    dgAppointments.ItemsSource = GetDbContext.GetContext().Appointments.ToList();
                }
                catch (Exception ex)
                { MessageBox.Show(ex.Message, "Ошибка базы данных", MessageBoxButton.OK, MessageBoxImage.Error); }
            }
        }
        private void HasData()
        {
            dgAppointments.ItemsSource = GetDbContext.GetContext().Appointments.ToList();
            if (dgAppointments.Items.Count == 0)
            {
                dgAppointments.Visibility = Visibility.Collapsed;
                tbNotFound.Visibility = Visibility.Visible;
            }
            else
            {
                dgAppointments.Visibility = Visibility.Visible;
                tbNotFound.Visibility = Visibility.Collapsed;
            }
        }
    }
}
