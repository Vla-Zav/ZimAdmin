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
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        CharsBlocker blocker = new CharsBlocker();
        
        Privat_HospitalEntities hospitalEntities = new Privat_HospitalEntities();
        public static Admins admins;
        Authorization_history history;

        public static int unsuccessfulAttempts = 0;
        public AuthPage()
        {
            InitializeComponent();
            history = new Authorization_history();
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            if (IsAuth(tbxLogin.Text, passBox.Password))
            {
                try
                {
                    DataContext = admins;
                    history.Date_Auth = DateTime.Now;
                    history.Id_Admin = admins.Id_Admin;
                    GetDbContext.GetContext().Authorization_history.Add(history);
                    GetDbContext.GetContext().SaveChanges();
                }
                catch (Exception ex) { MessageBox.Show(ex.Message, "Ошибка записи в историю", MessageBoxButton.OK, MessageBoxImage.Information); }

                ManageClass.getFrame.Navigate(new ProfilePage());
            }
            else
            {
                unsuccessfulAttempts++;
                if (unsuccessfulAttempts == 3)
                {
                    MessageBox.Show("Вы превысили количество попыток", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
                    Application.Current.Shutdown();
                }
                MessageBox.Show("Не верный логин или пароль", "Ошибка авторизации", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void btnReg_Click(object sender, RoutedEventArgs e)
        {
            ManageClass.getFrame.Navigate(new RegPage());
        }


        public bool IsAuth(string currentLogin, string currentPassword)
        {
            bool isAuth;
            try
            {
                admins = hospitalEntities.Admins.FirstOrDefault(user => user.Login == currentLogin && user.Password == currentPassword);
                if (admins != null)
                {
                    isAuth = true;
                }
                else isAuth = false;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                isAuth = false;
            }
            return isAuth;
        }

        private void specialCharsBlocker_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            blocker.SpecialCharsBlocker(e);
        }
        private void spaceBlocker_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            blocker.SpaceBlocker(e);
        }

        private void passBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                btnEnter_Click(sender, e);
        }
    }
}
