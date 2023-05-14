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
    /// Логика взаимодействия для PatientsPage.xaml
    /// </summary>
    public partial class PatientsPage : Page
    {
        CharsBlocker blocker = new CharsBlocker();
        public PatientsPage()
        {
            InitializeComponent();
            searchPatient();
        }

        private void btnLookConclution_Click(object sender, RoutedEventArgs e)
        {
            ManageClass.getFrame.Navigate(new PatientConclusionsPage((sender as Button).DataContext as Patients));
        }

        private void btnEditPatient_Click(object sender, RoutedEventArgs e)
        {
            ManageClass.getFrame.Navigate(new AddAndEditPatientPage((sender as Button).DataContext as Patients));
        }

        private void tbnAddPatient_Click(object sender, RoutedEventArgs e)
        {
            ManageClass.getFrame.Navigate(new AddAndEditPatientPage(null));
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                GetDbContext.GetContext().ChangeTracker.Entries().ToList().ForEach(entry => entry.Reload());
                dgPatients.ItemsSource = GetDbContext.GetContext().Patients.ToList();
            }
        }

        private void searchPatient()
        {
            List<Patients> patients = GetDbContext.GetContext().Patients.ToList();

            patients = patients.Where(p => p.Last_Name.ToLower().Contains(tbxSearchLName.Text.ToLower())).ToList();
            
            if(patients.Count == 0)
            {
                dgPatients.Visibility = Visibility.Collapsed;
                tbNotFound.Visibility = Visibility.Visible;
            }
            else
            {
                dgPatients.Visibility = Visibility.Visible;
                tbNotFound.Visibility = Visibility.Collapsed;
            }

            dgPatients.ItemsSource = patients;
        }

        private void SearchPatient(object sender, TextChangedEventArgs e)
        {
            searchPatient();
        }

        private void tbxSearchLName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            blocker.onlyLetters(e);
        }

        private void tbxSearchLName_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            blocker.spaceBlocker(e);
        }
    }
}
