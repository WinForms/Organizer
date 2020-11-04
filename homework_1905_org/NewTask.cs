using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace homework_1905_org
{
    [Serializable]

    public class NewTask 
    {
        string _tasktext;
        DateTime _date;


        public string Tasktext { get => _tasktext; set => _tasktext = value; }
        public DateTime Date { get => _date; set => _date = value; }

        public NewTask(DateTime date, string tasktext)
        {
            _date = date;
            _tasktext = tasktext;
        }

        public override string ToString()
        {
            return $"Date: {_date.ToShortDateString()}\n" +
                   $"Time: {_date.ToShortTimeString()}\n" +
                   $"Task: {_tasktext}\n";
        }

    }
}
