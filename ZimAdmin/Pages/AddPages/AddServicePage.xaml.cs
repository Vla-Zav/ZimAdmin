﻿using System;
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

namespace ZimAdmin.Pages.AddPages
{
    /// <summary>
    /// Логика взаимодействия для AddServicePage.xaml
    /// </summary>
    public partial class AddServicePage : Page
    {
        CharsBlocker blocker = new CharsBlocker();
        private Types_of_services services = new Types_of_services();
        public AddServicePage()
        {
            InitializeComponent();
            DataContext = services;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(services.Name))
                errors.AppendLine("Название услуги не указано");
            else if (services.Name.Length < 3)
                errors.AppendLine("Название слишком короткое(минимум 3 буквы)");
            if (services.Cost <= 0)
                errors.AppendLine("Некорректная стоимость");

            if(errors.Length > 0)
            {
                MessageBox.Show(errors.ToString(), "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            try
            {
                GetDbContext.GetContext().Types_of_services.Add(services);
                GetDbContext.GetContext().SaveChanges();
                MessageBox.Show("Устуга сохранена", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                ManageClass.getFrame.GoBack();
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка базы данных", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void specialCharsBlocker_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            blocker.specialCharsBlocker(e);
        }

        private void onlyNumbers_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            blocker.onlyNumbers(e);
        }

        private void spaceAndZeroBlocker_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            blocker.spaceBlocker(e);
            blocker.zeroFirstBlocker(tbxCostService.Text, e);
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            ManageClass.getFrame.GoBack();
        }
    }
}