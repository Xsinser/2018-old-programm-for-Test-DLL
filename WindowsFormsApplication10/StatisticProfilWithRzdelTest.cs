using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication10
{
 
    public partial class StatisticProfilWithRzdelTest : Form
    {
        List<string> massiv;
        string Razdel;
        StatisticProfil bufer;
        public StatisticProfilWithRzdelTest(List<string> dataFromFile,string _Razdel, StatisticProfil st)
        {
            InitializeComponent();
          massiv=  dataFromFile;
       Razdel=_Razdel;
            bufer = st;
            label4.Text = "Предмет: " + Razdel;
            loadData();
        }
        bool listChek(string somestring)
        {
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                if (listBox1.Items[i].ToString() == somestring)
                {
                    return false;
                }
            }
            return true;
        }
        void loadData()
        {
            foreach(var bufs in massiv)
            {
                string[] words = bufs.Split(new char[] { '|' });
                if((words[0]==Razdel)&& listChek(words[1]))
                {
                    listBox1.Items.Add(words[1]);
                }
            } 

            
        }


        void MetodStatistic(string PredmetName)
        {

            int i = 0;
            double _procent = 0;
            foreach (var bufs in massiv)
            {
                string[] words = bufs.Split(new char[] { '|' });

                if ((words[1] == PredmetName)&&(words[0]==Razdel))
                {
                    _procent += Convert.ToDouble(words[3]);
                    i++;

                }
            }
            _procent = _procent / (i);

            progressBar1.Maximum = 100;
            progressBar1.Minimum = 0;
            progressBar1.Value = Convert.ToInt16(_procent);
            if (_procent >= 70)
            {
                ModifyProgressBarColor.SetState(progressBar1, 1);
            }
            else if (_procent >= 50)
            {
                ModifyProgressBarColor.SetState(progressBar1, 2);
            }
            else
            {
                ModifyProgressBarColor.SetState(progressBar1, 3);
            }
            label3.Text = "Выполнено: " + _procent.ToString();
            label1.Text = "Выполнено тестов по предмету: " + (i);
            panel1.Visible = true;
        }
        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
            int i;
            i = listBox1.SelectedIndex;
        string  selectedRazd = listBox1.Items[i].ToString();
            MetodStatistic(selectedRazd);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            bufer.Show();
        }
    }
}
