using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace WindowsFormsApplication10
{
    public partial class StatisticProfil : Form
    {

        private List<string> dataFromFileWithData = new List<string>();
        int counter = 0;
        string selectedTema;
        menu Menu;
        public StatisticProfil(menu objectMenu)
        {
            InitializeComponent();
            ModifyProgressBarColor.SetState(progressBar1, 1);
            readFileWithPath();
            Menu = objectMenu;
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {
          
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

        void readFileWithPath()
        {
           
            System.IO.StreamReader file =
        new System.IO.StreamReader(DataProfil.PathForProfil, Encoding.Default);
            file.ReadLine();
            file.ReadLine();
            string line;
            while ((line = file.ReadLine()) != null)
            {
                dataFromFileWithData.Add(line);
                string[] words = line.Split(new char[] { '|' });
                if (listChek(words[0]))
                    {
                    listBox1.Items.Add(words[0]);
                }
                    counter++;
            }
            label2.Text = "Пройдено тестов всего: " + counter.ToString();

            file.Close();
        }

        void MetodStatistic(string PredmetName)
        {
            
            int i = 0;
            double _procent = 0;
            foreach (var bufs in dataFromFileWithData)
            {
                string[] words = bufs.Split(new char[] { '|' });
             
                if(words[0]==PredmetName)
                {
                    _procent +=Convert.ToDouble( words[3]);
                    i++;

                }
            }
            _procent = _procent / (i );

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
            label3.Text = "Выполнено: "+_procent.ToString() ;
            label1.Text = "Выполнено тестов по предмету: " + (i);
            panel1.Visible = true;
        }

        private void StatisticProfil_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int i;
            i = listBox1.SelectedIndex;
            selectedTema = listBox1.Items[i].ToString();
          MetodStatistic(selectedTema);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            var fr = new StatisticProfilWithRzdelTest(dataFromFileWithData, selectedTema,this);
            fr.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            Menu.Show();
        }
    }
}
