using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для EditServicesPage.xaml
    /// </summary>
    public partial class EditServicesPage : Page
    {
        CharsBlocker blocker = new CharsBlocker();
        Types_of_services services = new Types_of_services();
        public EditServicesPage(Types_of_services selectedService)
        {
            InitializeComponent();
            services = selectedService;
            DataContext = services;
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            int cost = 0;

            int.TryParse(tbxCostService.Text, out cost);

            services.Cost = cost;

            if (services.Cost <= 0)
            {
                errors.AppendLine("Некорректная стоимость");
            }

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString(), "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            try
            {
                GetDbContext.GetContext().SaveChanges();
                MessageBox.Show("Изменения сохранены", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                ManageClass.getFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка базы данных", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            ManageClass.getFrame.GoBack();
        }

        private void tbxCostService_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            blocker.OnlyNumbers(e);
        }


        private void tbxCostService_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            blocker.ZeroFirstBlocker(tbxCostService.Text, e);
            blocker.SpaceBlocker(e);
        }
    }
}
