using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnSchoolApp.Model
{
    public partial class ClientService
    {
        public string StartTimes
        {
            get
            {
                string result = "";             
                if ((Convert.ToDateTime(StartTime) - DateTime.Now).Hours < 0)
				{
                    var minutes = Convert.ToDateTime(StartTime).Minute.ToString().Length > 1 ? Convert.ToDateTime(StartTime).Minute.ToString() : "0" + Convert.ToDateTime(StartTime).Minute.ToString();
					result = $"Началось {Convert.ToDateTime(StartTime).ToShortDateString()} в {Convert.ToDateTime(StartTime).Hour}:{minutes}";
				} 
                else
				{
					//var startTimeDays = (Convert.ToDateTime(StartTime) - DateTime.Now).Days;
					var startTimeHours = (Convert.ToDateTime(StartTime) - DateTime.Now).Hours;
                    var startTimeMinutes = (Convert.ToDateTime(StartTime) - DateTime.Now).Minutes;
                    result = $"Начнется через {startTimeHours} часов {startTimeMinutes} минут";
				}
                return result ;
            }
        }
        public string Color
        {
            get
            {
                string result = "";
                if ((Convert.ToDateTime(StartTime) - DateTime.Now).Hours < 1 && (Convert.ToDateTime(StartTime) - DateTime.Now).Days == 0)
                {
                    result = "#ff6666";
                }
                else
                {
                    result = "#000000";
                }
                return result;
            }
        }

        public string BGColor
        {
            get
			{
				string result = "";
				if ((Convert.ToDateTime(StartTime) - DateTime.Now).Hours < 1 && (Convert.ToDateTime(StartTime) - DateTime.Now).Days == 0)
				{
					result = "#E7FABF";
				}
				else
				{
					result = "#FFFFFF";
				}
				return result;
			}
        }

        public string FullName
        {
            get
            {               
                return $"{Client.LastName} {Client.FirstName} {Client.Patronymic}";
            }
        }
    }
}
