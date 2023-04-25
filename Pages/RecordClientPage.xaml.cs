using LearnSchoolApp.Model;
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

namespace LearnSchoolApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для RecordClientPage.xaml
    /// </summary>
    public partial class RecordClientPage : Page
    {
        Service contextService;
        ClientService contextClientService;
        public RecordClientPage(Service service)
        {
            InitializeComponent();
            contextService = service;
            TBTitle.Text = contextService.Title;
            TBDuration.Text = $", длительность: {contextService.DurationInSeconds / 60} минут";
            var recordClient = new ClientService();
            contextClientService = recordClient;
            DataContext = contextClientService;
            CBClient.ItemsSource = App.DB.Clients.ToList();
            App.MainWindowInstance.BBack.Visibility = Visibility.Visible;
        }

        private void BSave_Click(object sender, RoutedEventArgs e)
        {
            string error = "";
            if (CBClient.SelectedItem == null)
                error += "Выберите клиента\n";
            if (DPDate.SelectedDate == null)
                error += "Выберите дату\n";
            if (string.IsNullOrWhiteSpace(TBTime.Text))
                error += "Введите время оказания услуги\n";
            if (!new Regex("^(0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$").IsMatch(TBTime.Text))
                error += "Время оказания услуги не соответствует формату ЧЧ:ММ\n";
            if (contextService.MainImagePath == null)
                error += "Загрузите главное фото\n";
            if (string.IsNullOrWhiteSpace(error) == false)
            {
                MessageBox.Show(error);
                return;
            }
            var selectedClient = CBClient.SelectedItem as Client;
            contextClientService.StartTime = DPDate.SelectedDate.Value + DateTime.Parse(TBTime.Text).TimeOfDay;
            contextClientService.ServiceID = contextService.ID;
            contextClientService.Client = selectedClient;
            if (string.IsNullOrWhiteSpace(TBComment.Text) == false)
            {
                contextClientService.Comment = TBComment.Text;
            }
            App.DB.ClientServices.Add(contextClientService);
            App.DB.SaveChanges();
            NavigationService.GoBack();
        }
    }
}
