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
    /// Логика взаимодействия для AddAndEditDoctors.xaml
    /// </summary>
    public partial class AddAndEditDoctors : Page
    {
        CharsBlocker blocker = new CharsBlocker();
        private Doctors currentDoctor = new Doctors();
        public AddAndEditDoctors(Doctors selectedDoctor)
        {
            InitializeComponent();
            if (selectedDoctor != null)
                currentDoctor = selectedDoctor;
            DataContext = currentDoctor;
            cbSpecialty.ItemsSource = GetDbContext.GetContext().Types_of_services.ToList();
            cbShift.ItemsSource = GetDbContext.GetContext().Work_shift.ToList();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            LFMTemplate.LFMComplite(currentDoctor.Last_Name, currentDoctor.First_Name, currentDoctor.Middle_Name, errors);
            
            if (cbSpecialty.SelectedItem == null)
                errors.AppendLine("Специальность не указана");
            if (cbShift.SelectedItem == null)
                errors.AppendLine("Смена работы не указана");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString(), "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            try
            {
                if (currentDoctor.Id_Doctor == 0)
                {
                    GetDbContext.GetContext().Doctors.Add(currentDoctor);
                    MessageBox.Show("Данные сохранены", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else 
                    MessageBox.Show("Данные изменены", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                GetDbContext.GetContext().SaveChanges();
                ManageClass.getFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            ManageClass.getFrame.GoBack();
        }

        private void onlyLetters_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            blocker.RussianLetters(e);
        }

        private void spaceBlocker_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            blocker.SpaceBlocker(e);
        }
    }
}
