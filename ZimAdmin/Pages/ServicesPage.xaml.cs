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
using ZimAdmin.Pages.AddPages;

namespace ZimAdmin.Pages
{
    /// <summary>
    /// Логика взаимодействия для ServicesPage.xaml
    /// </summary>
    public partial class ServicesPage : Page
    {
        public ServicesPage()
        {
            InitializeComponent();
            dgServices.ItemsSource = GetDbContext.GetContext().Types_of_services.ToList();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            ManageClass.getFrame.Navigate(new EditServicesPage((sender as Button).DataContext as Types_of_services));
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                GetDbContext.GetContext().ChangeTracker.Entries().ToList().ForEach(entry => entry.Reload());
                dgServices.ItemsSource = GetDbContext.GetContext().Types_of_services.ToList();
            }
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            var delService = dgServices.SelectedItems.Cast<Types_of_services>().ToList();
            if (MessageBox.Show("Вы уверены, что хотите удалить назначение?", "Уведомлние", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    GetDbContext.GetContext().Types_of_services.RemoveRange(delService);
                    GetDbContext.GetContext().SaveChanges();
                    dgServices.ItemsSource = GetDbContext.GetContext().Types_of_services.ToList();
                }
                catch (Exception ex)
                { MessageBox.Show(ex.Message, "Ошибка базы данных", MessageBoxButton.OK, MessageBoxImage.Error); }
            }
        }

        private void btnAddService_Click(object sender, RoutedEventArgs e)
        {
            ManageClass.getFrame.Navigate(new AddServicePage());
        }
    }
}
