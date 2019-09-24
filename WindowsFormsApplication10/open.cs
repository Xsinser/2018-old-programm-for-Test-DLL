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
    public partial class open : Form
    {
        string[] filesname;
        public open()
        {
            InitializeComponent();
            ChekDir();
            ProfilAdd();
        }

        void ChekDir()
        {
            string subpath = @"Profils";
            DirectoryInfo dirInfo = new DirectoryInfo(subpath);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }
        }
        void ProfilAdd()
        {
            filesname = Directory.GetFiles(@"Profils");

            if (filesname.Length != 0)
            {
                foreach (var s in filesname)
                {
                    StreamReader sr = new StreamReader(s);
                    sr.ReadLine();
                   comboBox1.Items.Add(  sr.ReadLine());
                    sr.Close();
                                                           
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            panel1.Visible = !panel1.Visible;
        }

        private void button3_Click(object sender, EventArgs e)
        {

            ChekDir();
            int i = comboBox1.Items.Count + 1;
            string path = @"Profils\" + i + ".txt";
            FileInfo fileInf = new FileInfo(path);
            if (!fileInf.Exists)
            {
                StreamWriter sw = new StreamWriter(path, true, Encoding.Default);
                sw.WriteLine("Test Profil next line - profil info");
                sw.WriteLine(textBox1.Text);
                sw.Close();
            }
            panel1.Visible = !panel1.Visible;
            comboBox1.Items.Clear();
            ProfilAdd();
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

                e.Handled = true;
        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text.Length>0)
            {
                this.Hide();
                menu m = new menu();
                m.GetData(comboBox1.Text, filesname[comboBox1.SelectedIndex]);
                DataProfil.Name = comboBox1.Text;
                DataProfil.PathForProfil = filesname[comboBox1.SelectedIndex];
                m.Show();

            }
        }
    }
}
