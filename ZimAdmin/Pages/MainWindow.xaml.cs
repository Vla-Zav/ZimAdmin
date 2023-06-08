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
using ZimAdmin.Pages;
using ZimAdmin.Classes;

namespace ZimAdmin
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ManageClass.getFrame = mainFrame;
            ManageClass.getFrame.Navigate(new AuthPage());
        }

        private void mainFrame_ContentRendered(object sender, EventArgs e)
        {
            if (CurrentAdmin.GetAdmin().Id_Admin != 0)
            {
                navigate.Visibility = Visibility.Visible;
                myInfo.Visibility = Visibility.Visible;
            }
            else { 
                navigate.Visibility = Visibility.Collapsed;
                myInfo.Visibility = Visibility.Collapsed;
            }
        }

        private void miDoctors_Click(object sender, RoutedEventArgs e)
        {
            ManageClass.getFrame.Navigate(new DoctorsPage());
        }

        private void miProfile_Click(object sender, RoutedEventArgs e)
        {
            ManageClass.getFrame.Navigate(new ProfilePage());
        }

        private void miExit_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Вы точно хотите выйти из аккаунта?", "Вы уверены?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                ManageClass.getFrame.Navigate(new AuthPage());
                CurrentAdmin.SetAdmin(null);
                AuthPage.unsuccessfulAttempts = 0;
            }
        }

        private void miPatients_Checked(object sender, RoutedEventArgs e)
        {
            ManageClass.getFrame.Navigate(new PatientsPage());
        }

        private void miAppointments_Click(object sender, RoutedEventArgs e)
        {
            ManageClass.getFrame.Navigate(new AppointmentsPage());
        }

        private void miPatients_Click(object sender, RoutedEventArgs e)
        {
            ManageClass.getFrame.Navigate(new PatientsPage());
        }

        private void miAnalytics_Click(object sender, RoutedEventArgs e)
        {
            ManageClass.getFrame.Navigate(new AnalyticsPage());
        }

        private void miServices_Click(object sender, RoutedEventArgs e)
        {
            ManageClass.getFrame.Navigate(new ServicesPage());
        }

        private void aboutMe_Click(object sender, RoutedEventArgs e)
        {
            ManageClass.getFrame.Navigate(new MyInfoPage());
        }
    }
}