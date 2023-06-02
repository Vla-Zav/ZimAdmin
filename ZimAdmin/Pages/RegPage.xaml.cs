using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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
    /// Логика взаимодействия для RegPage.xaml
    /// </summary>
    public partial class RegPage : Page
    {
        CharsBlocker blocker = new CharsBlocker();
        Admins newAdmin = new Admins();
        public RegPage()
        {
            InitializeComponent();
            DataContext = newAdmin;
        }

        private void btnReg_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            LFMTemplate.LFMComplite(newAdmin.Last_Name, newAdmin.First_Name, newAdmin.Middle_Name, errors);

            if (string.IsNullOrWhiteSpace(tbxLogin.Text))
                errors.AppendLine("Логин не указан");
            else if (tbxLogin.Text.Length < 4)
                errors.AppendLine("Логин слишком короткий (минимум 4 символа)");
            if(string.IsNullOrWhiteSpace(pbPassword.Password))
                errors.AppendLine("Пароль не введён");
            else if (pbPassword.Password.Length < 4)
                errors.AppendLine("Пароль слишком короткий (минимум 4 символа)");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString(), "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            Admins existsAdmin = GetDbContext.GetContext().Admins.FirstOrDefault(a => a.Login == tbxLogin.Text);
            if(existsAdmin != null)
            {
                MessageBox.Show($"Пользователь с логином {existsAdmin.Login} уже существует\nВыберете другой",
                    "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            try
            {
                newAdmin.Password = pbPassword.Password;
                GetDbContext.GetContext().Admins.Add(newAdmin);
                GetDbContext.GetContext().SaveChanges();
                MessageBox.Show("Регистрация прошла успешно", "Зарегистрирован", MessageBoxButton.OK, MessageBoxImage.Information);
                ManageClass.getFrame.Navigate(new AuthPage());
            }
            catch (DbEntityValidationException ex)
            {
                StringBuilder error = new StringBuilder();
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        error.AppendLine($"Ошибка проверки: {validationError.ErrorMessage}");
                    }
                }
                MessageBox.Show(error.ToString(), "Ошибка базы данных!", MessageBoxButton.OK, MessageBoxImage.Error);
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
