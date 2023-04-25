using LearnSchoolApp.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace LearnSchoolApp
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static LearnSchoolDBEntities DB = new LearnSchoolDBEntities();
        public static MainWindow MainWindowInstance;
        public App()
        {
            DispatcherUnhandledException += App_DispatcherUnhadledExeption;
        }
        private void App_DispatcherUnhadledExeption(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message);
            e.Handled = true;
        }
    }
}
