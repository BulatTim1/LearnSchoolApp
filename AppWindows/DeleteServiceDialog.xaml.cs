using LearnSchoolApp.Model;
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
using System.Windows.Shapes;

namespace LearnSchoolApp.AppWindows
{
    /// <summary>
    /// Логика взаимодействия для DeleteServiceDialog.xaml
    /// </summary>
    public partial class DeleteServiceDialog : Window
    {
        Service contextService;
        public DeleteServiceDialog(Service service)
        {
            InitializeComponent();
            contextService = service;
            DataContext = contextService;
            
        }

        private void BAgree_Click(object sender, RoutedEventArgs e)
        {
            App.DB.Services.Remove(contextService);
            if (CBDeleteImages.IsChecked == true)
            {
                App.DB.ServicePhotoes.RemoveRange(
                    App.DB.ServicePhotoes.Where(sp => sp.ServiceID == contextService.ID).ToList()
                );
            }
            App.DB.SaveChanges();
            this.DialogResult = true;
        }

        private void BCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
