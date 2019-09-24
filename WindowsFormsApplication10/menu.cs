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
    public partial class menu : Form
    {
        private string ProfilName;
        private string ProfillPass;
        private int counter = 0;
        private List<string> dataFromFileWithData = new List<string>();
        private string _predmet,_razd,_tema,_path;
        int perem = 0;
        public void GetData(string nameProfil, string pathToProfil)
        {
            ProfilName = nameProfil;
            ProfillPass = pathToProfil;
        }
        bool listChek(string somestring)
        {
            for(int i = 0;i<listBox1.Items.Count;i++)
            {
                if(listBox1.Items[i].ToString()==somestring)
                {
                    return false;
                }
            }
            return true;
        }
        void ListAddPredmet()
        {
            foreach (var bufs in dataFromFileWithData)
            {
                string[] words = bufs.Split(new char[] { '|' });
               if(listChek(words[0]))
                {
                    listBox1.Items.Add(words[0]);
                }
            }
        }
        void readFileWithPath()
        {        

        System.IO.StreamReader file =
    new System.IO.StreamReader(@"Pathes\pathes.txt", Encoding.Default);
            file.ReadLine();

            string line;
                while ((line = file.ReadLine()) != null)
                {
                dataFromFileWithData.Add(line);
                    counter++;
                }
            

            file.Close();
        }
        public menu()
        {
            InitializeComponent();
            ChekDir();
            ChekPathes();

        }
        void ChekDir()
        {
            string subpath = @"Pathes";
            DirectoryInfo dirInfo = new DirectoryInfo(subpath);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }
        }

        void ChekPathes()
        {
            string path = @"Pathes\pathes.txt";
            FileInfo fileInf = new FileInfo(path);
            if (!fileInf.Exists)
            {
                StreamWriter sw = new StreamWriter(path, true, Encoding.Default);
                sw.WriteLine("This file with tests path");
                sw.Close();
            }
        }
        private void menu_Shown(object sender, EventArgs e)
        {
            label2.Text = ProfilName;
            perem = 0;
            readFileWithPath();
            ListAddPredmet();
            
        }

        void ListAddRazd()
        {
            listBox1.Items.Clear();
            foreach (var bufs in dataFromFileWithData)
            {
                string[] words = bufs.Split(new char[] { '|' });
                if (listChek(words[1])&&(words[0]==_predmet))
                {
                    listBox1.Items.Add(words[1]);
                }
            }
        }

        void ListAddTema()
        {
            listBox1.Items.Clear();
            foreach (var bufs in dataFromFileWithData)
            {
                string[] words = bufs.Split(new char[] { '|' });
                if (listChek(words[2]) && (words[0] == _predmet) && (words[1] == _razd))
                {
                    listBox1.Items.Add(words[2]);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            var fr = new StatisticProfil(this);
            fr.Show();
        }

        private void menu_Load(object sender, EventArgs e)
        {
            label2.Text = ProfilName;
            perem = 0;
            readFileWithPath();
            ListAddPredmet();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog()==DialogResult.Cancel)
            {
                return;
            }
            string pathfileFunc = openFileDialog1.FileName;
            Form1 fr = new Form1(pathfileFunc,true,this);
            this.Hide();
            fr.Show();
        }

        void SearchFile()
        {
           
            foreach (var bufs in dataFromFileWithData)
            {
                string[] words = bufs.Split(new char[] { '|' });
           
                if ((words[2]==_tema)&& (words[0] == _predmet) && (words[1] == _razd))
                {
                   _path = words[3];
                }
            }
        }
        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            int i = 0;
            switch (perem)
            {
                case 0:
                    i = listBox1.SelectedIndex;
                    _predmet = listBox1.Items[i].ToString();
                    ListAddRazd();
                    break;
                case 1:
                   i = listBox1.SelectedIndex;
                    _razd = listBox1.Items[i].ToString();
                    ListAddTema();
                    break;

                case 2:
                  
                   i = listBox1.SelectedIndex;
                    _tema = listBox1.Items[i].ToString();
                    try
                    {
                        SearchFile();
                        perem = -1;
                        listBox1.Items.Clear();                      
                        ListAddPredmet();
                        Form1 fr = new Form1(_path, false, this);


                        this.Hide();
                        fr.Show();
                    }
                    catch
                    {

                    }
                    break;

            }

            perem++;
        }
    }
}
