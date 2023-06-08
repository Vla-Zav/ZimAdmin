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
    /// Логика взаимодействия для ChangePasswordPage.xaml
    /// </summary>
    public partial class ChangePasswordPage : Page
    {
        CharsBlocker blocker = new CharsBlocker();
        private Admins currentAdmin = new Admins();
        public ChangePasswordPage()
        {
            InitializeComponent();
            currentAdmin = GetDbContext.GetContext().Admins.Find(CurrentAdmin.GetAdmin().Id_Admin);
            DataContext = currentAdmin;
        }

        private void btnSaveChange_Click(object sender, RoutedEventArgs e)
        {
            bool rightPass = false;

            try
            {
                if(currentAdmin.Password == pbOldPassAdmin.Password)
                    rightPass = true;
                if (rightPass)
                {
                    currentAdmin.Password = pbNewPassAdmin.Password;
                    GetDbContext.GetContext().SaveChanges();
                    MessageBox.Show("Пароль изменён", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
                    ManageClass.getFrame.GoBack();
                }
                else
                    MessageBox.Show("Старый пароль неверный", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
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

        private void specialCharsBlocker_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            blocker.SpecialCharsBlocker(e);
            blocker.NoRussianLetters(e);
        }
        private void spaceBlocker_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            blocker.SpaceBlocker(e);
        }
    }
}
