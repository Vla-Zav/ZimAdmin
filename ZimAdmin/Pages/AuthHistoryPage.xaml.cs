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
    /// Логика взаимодействия для AuthHistoryPage.xaml
    /// </summary>
    public partial class AuthHistoryPage : Page
    {
        List<Authorization_history> historyList = GetDbContext.GetContext().Authorization_history.ToList();
        public AuthHistoryPage()
        {
            InitializeComponent();
            dgAuthHistory.ItemsSource = GetDbContext.GetContext().Authorization_history.ToList();
            GetDbContext.GetContext().ChangeTracker.Entries().ToList().ForEach(entry => entry.Reload());
            if (historyList.Count == 0)
            {
                dgAuthHistory.Visibility = Visibility.Collapsed;
                tbNotFound.Visibility = Visibility.Visible;
            }
            else
            {
                dgAuthHistory.Visibility= Visibility.Visible;
                tbNotFound.Visibility= Visibility.Collapsed;
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            ManageClass.getFrame.GoBack();
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                GetDbContext.GetContext().ChangeTracker.Entries().ToList().ForEach(entry => entry.Reload());
                dgAuthHistory.ItemsSource = GetDbContext.GetContext().Authorization_history.ToList();
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            GetDbContext.GetContext().Authorization_history.RemoveRange(historyList);
            GetDbContext.GetContext().SaveChanges();
            MessageBox.Show("История очищена", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            ManageClass.getFrame.GoBack();
        }
    }
}
