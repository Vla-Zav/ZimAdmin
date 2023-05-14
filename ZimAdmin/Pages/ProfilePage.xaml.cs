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
    /// Логика взаимодействия для ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        private Admins currentAdmin = new Admins();
        public ProfilePage(Admins actualAdmin)
        {
            InitializeComponent();
            currentAdmin = actualAdmin;
            DataContext = currentAdmin;
        }

        private void btnEditProfile_Click(object sender, RoutedEventArgs e)
        {
            ManageClass.getFrame.Navigate(new EditProfilePage());
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                GetDbContext.GetContext().ChangeTracker.Entries().ToList().ForEach(entry => entry.Reload());
                DataContext = currentAdmin;
            }
        }

        private void btnAuthHistory_Click(object sender, RoutedEventArgs e)
        {
            ManageClass.getFrame.Navigate(new AuthHistoryPage());
        }
    }
}