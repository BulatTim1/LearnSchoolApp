using LearnSchoolApp.AppWindows;
using LearnSchoolApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace LearnSchoolApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для ListServicePage.xaml
    /// </summary>
    public partial class ListServicePage : Page
    {
        public ListServicePage()
        {
            InitializeComponent();
            Refresh(0);
            CBSale.SelectedIndex = 0;
        }
        
        private void BEdit_Click(object sender, RoutedEventArgs e)
        {
            var service = (sender as Button).DataContext as Service;
            new EditServiceWindow(service).ShowDialog();
            Refresh(0);
        }

        private void BDelete_Click(object sender, RoutedEventArgs e)
        {
            var service = (sender as Button).DataContext as Service;
			if (App.DB.ClientServices.Where(cs => cs.ServiceID == service.ID).Count() > 0)
            {
                MessageBox.Show("Есть информация о записях на услугу, удаление запрещено", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Hand);
                return;
            }
            else
            {
                new DeleteServiceDialog(service).ShowDialog();
                Refresh(0);
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh(0);
        }

        public void Reload()
        {
            Refresh(0);
        }

        private void Refresh(int i)
        {
            App.MainWindowInstance.BBack.Visibility = Visibility.Collapsed;
            if (App.MainWindowInstance.TBCode.Text == "0000")
            {
                BViewRecordingService.Visibility = Visibility.Visible;
                BAddSerice.Visibility = Visibility.Visible;
            }
            
            else
            {
                BViewRecordingService.Visibility = Visibility.Collapsed;
                BAddSerice.Visibility = Visibility.Collapsed;

            }

			App.DB = new Model.LearnSchoolDBEntities();
			var allService = App.DB.Services.ToList();
			var filtred = new List<Service>(allService);
            var searchText = TBSearch.Text.ToLower();
            if (CBSale != null)
            {
                switch (CBSale.SelectedIndex)
                {
                    case 1:
						filtred = filtred.Where(f => Convert.ToString(f.Discount.Value) == f.Discount0).ToList();
                        break;
                    case 2:
						filtred = filtred.Where(f => Convert.ToString(f.Discount.Value) == f.Discount5).ToList();
                        break;
                    case 3:
						filtred = filtred.Where(f => Convert.ToString(f.Discount.Value) == f.Discount15).ToList();
						break;
                    case 4:
						filtred = filtred.Where(f => Convert.ToString(f.Discount.Value) == f.Discount30).ToList();
						break;
                    case 5:
						filtred = filtred.Where(f => Convert.ToString(f.Discount.Value) == f.Discount70).ToList();
						break;
				}
            }
            if (string.IsNullOrWhiteSpace(searchText) == false)
                filtred = filtred.Where(f => f.Title.ToLower().Contains(searchText) || (f.Description != null && f.Description.ToLower().Contains(searchText))).ToList();

            if(i == 1)
                filtred = filtred.OrderBy(f => f.NewCost).ToList();
            if(i == 2)
                filtred = filtred.OrderByDescending(f => f.NewCost).ToList();
            LVService.ItemsSource = filtred.ToList();
            TBAllProducts.Text = $"{filtred.Count} из {allService.Count}";
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh(0);
        }

        private void TBSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Refresh(0);
        }

        private void BAddService_Click(object sender, RoutedEventArgs e)
        {
            new EditServiceWindow(new Service()).ShowDialog();
            Refresh(0);
        }

        private void BRecordClient_Click(object sender, RoutedEventArgs e)
        {
            var service = (sender as Button).DataContext as Service;
            NavigationService.Navigate(new RecordClientPage(service));
        }

        private void BAscending_Click(object sender, RoutedEventArgs e)
        {
            Refresh(1);
        }

        private void BDecreasing_Click(object sender, RoutedEventArgs e)
        {
            Refresh(2);
        }

        private void BViewRecordingService_Click(object sender, RoutedEventArgs e)
        {
            new ListRecordedClientsWindow().ShowDialog();
        }
        
    }
}
