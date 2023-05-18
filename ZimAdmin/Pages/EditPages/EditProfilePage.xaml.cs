using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Логика взаимодействия для EditProfilePage.xaml
    /// </summary>
    public partial class EditProfilePage : Page
    {
        CharsBlocker blocker = new CharsBlocker();
        private Admins currentAdmin = new Admins();
        private Admins originalAdmin = null;
        public EditProfilePage()
        {
            InitializeComponent();
            currentAdmin = GetDbContext.GetContext().Admins.Find(AuthPage.admins.id_Admin);
            originalAdmin = CloneAdmin(currentAdmin);
            DataContext = currentAdmin;
        }

        private void btnChangePassword_Click(object sender, RoutedEventArgs e)
        {
            ManageClass.getFrame.Navigate(new ChangePasswordPage());
        }

        private void btnSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(currentAdmin.Last_Name))
                errors.AppendLine("Фамилия не указана");
            else if (currentAdmin.Last_Name.Length < 2)
                errors.AppendLine("Фамилия слишком короткая (минимум 2 буквы)");
            if (string.IsNullOrWhiteSpace(currentAdmin.First_Name))
                errors.AppendLine("Имя не указано");
            else if (currentAdmin.First_Name.Length < 2)
                errors.AppendLine("Имя слишком короткое (минимум 2 буквы)");
            if (string.IsNullOrWhiteSpace(currentAdmin.Middle_Name))
                errors.AppendLine("Отчество не указано");
            if (string.IsNullOrEmpty(currentAdmin.Login))
                errors.AppendLine("Логин не может бить пустым");
            else if (currentAdmin.Login.Length < 3)
                        errors.AppendLine("Логин слигком короткий (минимум 3 символа)");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString(), "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            try
            {
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
            currentAdmin = CloneAdmin(originalAdmin);
            DataContext = currentAdmin;
            GetDbContext.GetContext().ChangeTracker.Entries().ToList().ForEach(entire => entire.Reload());
            ManageClass.getFrame.GoBack();
        }

        private Admins CloneAdmin(Admins admin)
        {
            // Создаем копию сущности Admins
            return new Admins
            {
                Last_Name = admin.Last_Name,
                First_Name = admin.First_Name,
                Middle_Name = admin.Middle_Name,
                Login = admin.Login,
                Password = admin.Password
            };
        }


        private void specialCharsBlocker_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            blocker.specialCharsBlocker(e);
            blocker.noRussianLetters(e);
        }

        private void onlyLetters_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            blocker.onlyLetters(e);
            blocker.russianLetters(e);
        }

        private void spaceBlocker_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            blocker.spaceBlocker(e);
        }
    }
}
