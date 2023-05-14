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
    /// Логика взаимодействия для DoctorsPage.xaml
    /// </summary>
    public partial class DoctorsPage : Page
    {
        public DoctorsPage()
        {
            Privat_HospitalEntities hospitalEntities = new Privat_HospitalEntities();
            
            InitializeComponent();

            List<Types_of_services> services = hospitalEntities.Types_of_services.ToList();
            List<Work_shift> shifts = hospitalEntities.Work_shift.ToList();

            services.Insert(0, new Types_of_services{ Name = "Все специальности"});
            shifts.Insert(0, new Work_shift { Number = "Все смены"});


            cbShift.ItemsSource = shifts;
            cbSpecialty.ItemsSource = services;
            cbShift.SelectedIndex = 0;
            cbSpecialty.SelectedIndex = 0;
            
            GetDbContext.GetContext().ChangeTracker.Entries().ToList().ForEach(entry => entry.Reload());
            
            searchDoctors();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            ManageClass.getFrame.Navigate(new AddAndEditDoctors((sender as Button).DataContext as Doctors));
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            ManageClass.getFrame.Navigate(new AddAndEditDoctors(null));
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                GetDbContext.GetContext().ChangeTracker.Entries().ToList().ForEach(entry => entry.Reload());
                dgDoctors.ItemsSource = GetDbContext.GetContext().Doctors.ToList();
            }
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            var delDoctor = dgDoctors.SelectedItems.Cast<Doctors>().ToList();
            if (MessageBox.Show("Вы уверены, что хотите удалить доктора", "Уведомлние", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    GetDbContext.GetContext().Doctors.RemoveRange(delDoctor);
                    GetDbContext.GetContext().SaveChanges();
                    dgDoctors.ItemsSource = GetDbContext.GetContext().Doctors.ToList();
                }
                catch (Exception ex)
                { MessageBox.Show(ex.Message, "Ошибка базы данных", MessageBoxButton.OK, MessageBoxImage.Error); }
            }
        }

        private void searchDoctors()
        {
            List<Doctors> doctors = GetDbContext.GetContext().Doctors.ToList();
            Types_of_services idSpetialty = (Types_of_services)cbSpecialty.SelectedItem;

            if (cbSpecialty.SelectedIndex > 0)
                doctors = doctors.Where(d => d.Specialty.Equals(idSpetialty.id_Type)).ToList();
            if (cbShift.SelectedIndex > 0)
                doctors = doctors.Where(d => d.Shift.Equals(cbShift.SelectedIndex)).ToList();
            
            if(doctors.Count == 0)
            {
                dgDoctors.Visibility = Visibility.Collapsed;
                tbNotFound.Visibility = Visibility.Visible;
            }
            else
            {
                dgDoctors.Visibility = Visibility.Visible;
                tbNotFound.Visibility = Visibility.Collapsed;
            }

            dgDoctors.ItemsSource = doctors;
        }

        private void search_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            searchDoctors();
        }
    }
}
