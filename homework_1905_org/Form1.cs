using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace homework_1905_org
{


    public partial class Tasks : Form
    {
        Timer timer;

        void LoadTasks()
        {
            try
            {
                using (var fs = File.OpenRead("Tasks.bin"))
                {
                    if (fs.Length != 0)
                    {
                        _tasklist = (List<NewTask>)bf.Deserialize(fs);
                    }
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
                using (var fs = File.Create("Tasks.bin")) ;
            }
        }
        void SaveTasks()
        {
            using (var fs = File.OpenWrite("Tasks.bin"))
            {
                bf.Serialize(fs, _tasklist);
            }
        }
        BinaryFormatter bf = new BinaryFormatter();
        List<NewTask> _tasklist = new List<NewTask>();

        public Tasks()
        {
            InitializeComponent();
            LoadTasks();
            timer = new Timer();
            timer.Interval = 58000;
            timer.Tick += (s, e) =>
            {
                var now = DateTime.Now;
              //  Event.Text = $"Time: {now.Hour:00}:{now.Minute:00}:{now.Second:00}";
                for (int i = 0; i < _tasklist.Count; i++)
                {
                    if (now.ToShortDateString() == _tasklist[i].Date.ToShortDateString() && now.ToShortTimeString() == _tasklist[i].Date.ToShortTimeString())
                    {
                        MessageBox.Show(_tasklist[i].Tasktext, "Deal Now", MessageBoxButtons.OK, MessageBoxIcon.Information) ;
                    }

                }
            };
            for (int i = 0; i < _tasklist.Count; i++)
            {
                monthCalendar1.AddBoldedDate(_tasklist[i].Date);
                flowLayoutPanel1.Controls.Add(new Label() { AutoSize = true, TextAlign = System.Drawing.ContentAlignment.TopLeft, BorderStyle = BorderStyle.FixedSingle, Size = new Size(70, 40), Text = $"{_tasklist[i].Date.ToShortDateString()}\n{_tasklist[i].Date.ToShortTimeString()}\n{_tasklist[i].Tasktext}" });
            }
            monthCalendar1.UpdateBoldedDates();
            timer.Start();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Label win = new Label();
            win.BorderStyle = BorderStyle.FixedSingle;
            win.Size=new Size(70,40);
            win.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            win.AutoSize = true;
            monthCalendar1.AddBoldedDate(monthCalendar1.SelectionStart);
            NewTask task = new NewTask(monthCalendar1.SelectionStart, textBox1.Text);
            task.Date= task.Date.AddHours(dateTimePicker1.Value.Hour);
            task.Date = task.Date.AddMinutes(dateTimePicker1.Value.Minute);
            win.Text = $"{task.Date.ToShortDateString()}\n{task.Date.ToShortTimeString()}\n{task.Tasktext}";
            flowLayoutPanel1.Controls.Add(win); 
            _tasklist.Add(task);
            textBox1.Text = "";
            monthCalendar1.UpdateBoldedDates();
            SaveTasks();
        }


    }
}
