using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApplication10
{
    public partial class Statistic : Form
    {
     public   string[] mass;
        public Statistic()
        {
            InitializeComponent();
        }

        private void Statistic_Shown(object sender, EventArgs e)
        {
            listView1.SmallImageList = imageList1;
            // listView1.SmallImageList = imageList1;


            string bufs = mass[2] + "|" + mass[1] + "|" + mass[0] + "|" + mass[3];
            StreamWriter sw = new StreamWriter(DataProfil.PathForProfil, true, Encoding.Default);
            sw.WriteLine(bufs);
            sw.Close();

            label1.Text = "Тема: " + mass[0];
            label2.Text = "Раздел: " + mass[1];
            label3.Text = "Предмет: " + mass[2];
            label4.Text = "Процент правильных ответов: " + mass[3] +"%";
            
            for (int sch = 4; sch <(mass.Length) ; sch++)
            {
                ListViewItem lv = new ListViewItem();

                if (Convert.ToBoolean(mass[sch]))
                {
                    lv.Text = "Вопрос "+(sch-3)+": Правильно";
                    lv.ImageIndex = (1);
                }
                else
                {
                    lv.Text = "Вопрос " + (sch - 3) + ":Не правильно";
                    lv.ImageIndex = (0);
                }
                listView1.Items.Add(lv);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            menu m = new menu();
            m.Show();
            this.Close();

        }
    }
}
