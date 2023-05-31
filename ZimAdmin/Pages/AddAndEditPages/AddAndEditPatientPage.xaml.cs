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
using ZimAdmin.Entitys;
using ZimAdmin.Classes;

namespace ZimAdmin.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddAndEditPatientPage.xaml
    /// </summary>
    public partial class AddAndEditPatientPage : Page
    {
        CharsBlocker blocker = new CharsBlocker();
        private Patients currentPatient = new Patients();
        public AddAndEditPatientPage(Patients selectedPatient)
        {
            InitializeComponent();
            if (selectedPatient != null)
                currentPatient = selectedPatient;
            else
                currentPatient.Bithday = DateTime.Now.Date;
            DataContext = currentPatient;
        }


        private void btnSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            LFMTemplate.LFMComplite(currentPatient.Last_Name, currentPatient.First_Name, currentPatient.Middle_Name, errors);
            
            if (!tbxPatientPhone.IsMaskFull)
                errors.AppendLine("Номер телефона не указан");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString(), "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            try
            {
                if (currentPatient.id_Patient == 0)
                {
                    GetDbContext.GetContext().Patients.Add(currentPatient);
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
        private void btnBack_Click(object sender, RoutedEventArgs e)
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

        private void calBDPatient_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
        }
    }
}
