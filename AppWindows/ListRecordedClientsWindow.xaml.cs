using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
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
    /// Логика взаимодействия для ListRecordedClientsWindow.xaml
    /// </summary>
    public partial class ListRecordedClientsWindow : Window
	{
		System.Timers.Timer timer;
		public ListRecordedClientsWindow()
        {
            InitializeComponent();
			Refresh();
			timer = new System.Timers.Timer(30000);
			timer.Elapsed += OnTimerElapsed;
			timer.AutoReset = true;
			timer.Start();
		}
		private void OnTimerElapsed(object sender, ElapsedEventArgs e)
		{
			Dispatcher.Invoke(() => Refresh());
		}
        private void Refresh()
		{
			App.DB = new Model.LearnSchoolDBEntities();
			var filtred = App.DB.ClientServices.ToList();
            filtred = filtred.OrderBy(f => f.StartTime).ToList();
			filtred = filtred.Where((f => f.StartTime.Date == DateTime.Now.Date || f.StartTime.Date == (DateTime.Now.Date.AddDays(1)))).ToList();
            LVRecordingClient.ItemsSource = filtred;
        }
		private void Window_Closed(object sender, EventArgs e)
		{
			timer.Stop();
		}
	}
}
