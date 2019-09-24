using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LiberufortestgoTW;
using System.IO;

namespace WindowsFormsApplication10
{
    public partial class Form1 : Form
    {
        int schv = 1;
        C_interf a;
        private string filename;
        private bool typeConstructor = false;

        menu pathToClass;
     public   string[] ar;
        public Form1(string pathFileTest,bool bufb, menu objectClass)
        {
            InitializeComponent();
            typeConstructor = bufb;
            filename = pathFileTest;
            richTextBox1.Enabled = true;
            richTextBox2.Enabled = true;
            richTextBox3.Enabled = true;
            richTextBox4.Enabled = true;
            pathToClass = objectClass;
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            //if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
            //    return;
            //// получаем выбранный файл
            //string filename = openFileDialog1.FileName;
            bool chekP = true;
            try
            {
                StreamReader rd = new StreamReader(filename);
                rd.ReadToEnd();
                rd.Close();
            }
            catch
            {
                chekP = false;
                this.Close();
                pathToClass.Show();
            }
            if (chekP)
            {
                a = new C_interf();

                a.GettingDataFromAFile(filename);
                if (typeConstructor)
                {
                    string strv = a.GetTestSubject() + "|" + a.GetTestSection() + "|" + a.GetTestName() + "|" + filename;
                    string path = @"Pathes\pathes.txt";

                    StreamWriter sw = new StreamWriter(path, true, Encoding.Default);
                    sw.WriteLine(strv);
                    sw.Close();

                }
                label4.Text = schv + " из " + a.GetQuestionCount();
                label1.Text = "Тема " + a.GetTestName();
                label2.Text = "Раздел " + a.GetTestSection();
                label3.Text = "Предмет " + a.GetTestSubject();
                richTextBox5.Text = a.GetQuestionText();

                if (a.GetAnswerType(0) == "text")
                    richTextBox1.Text = a.GetTextAnswers(0);
                else
                {
                    op1.Visible = !op1.Visible;

                    op1.Image = StrToImg(a.GetTextAnswers(0));
                }

                if (a.GetAnswerType(1) == "text")
                    richTextBox2.Text = a.GetTextAnswers(1);
                else
                {
                    op2.Visible = !op2.Visible;

                    op2.Image = StrToImg(a.GetTextAnswers(1));
                }

                if (a.GetAnswerType(2) == "text")
                    richTextBox3.Text = a.GetTextAnswers(2);
                else
                {
                    op3.Visible = !op3.Visible;

                    op3.Image = StrToImg(a.GetTextAnswers(2));
                }
                if (a.GetAnswerType(3) == "text")
                    richTextBox4.Text = a.GetTextAnswers(3);
                else
                {
                    op4.Visible = !op4.Visible;

                    op4.Image = StrToImg(a.GetTextAnswers(3));
                }

            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            op1.Visible = false; op2.Visible = false; op3.Visible = false; op4.Visible = false;
            string str = "";
            if (checkBox1.Checked)
                str += "0 ";
            if (checkBox2.Checked)
                str += "1 ";
            if (checkBox3.Checked)
                str += "2 ";
            if (checkBox4.Checked)
                str += "3 ";

            a.AnswerCheck(str).ToString();
            a.NextQuestion();
            checkBox1.Checked = false; checkBox2.Checked = false; checkBox3.Checked = false; checkBox4.Checked = false;
            checkBox1.Visible = false; checkBox2.Visible = false; checkBox3.Visible = false; checkBox4.Visible = false;
            richTextBox5.Text = a.GetQuestionText();

            if (a.GetAnswerType(0) == "text")
                richTextBox1.Text = a.GetTextAnswers(0);
            else
            {
                op1.Visible = !op1.Visible;

                op1.Image = StrToImg(a.GetTextAnswers(0));
            }

            if (a.GetAnswerType(1) == "text")
                richTextBox2.Text = a.GetTextAnswers(1);
            else
            {
                op2.Visible = !op2.Visible;

                op2.Image = StrToImg(a.GetTextAnswers(1));
            }

            if (a.GetAnswerType(2) == "text")
                richTextBox3.Text = a.GetTextAnswers(2);
            else
            {
                op3.Visible = !op3.Visible;

                op3.Image = StrToImg(a.GetTextAnswers(2));
            }
            if (a.GetAnswerType(3) == "text")
                richTextBox4.Text = a.GetTextAnswers(3);
            else
            {
                op4.Visible = !op4.Visible;

                op4.Image = StrToImg(a.GetTextAnswers(3));
            }
            schv++;
            label4.Text = schv+" из " + a.GetQuestionCount();
            if (schv == a.GetQuestionCount())
                pictureBox1.Visible = false;
        }

        public Image StrToImg(string StrImg)
        {
            byte[] arrayimg = Convert.FromBase64String(StrImg);
            Image imageStr = Image.FromStream(new MemoryStream(arrayimg));
            return imageStr;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            op1.Visible = false; op2.Visible = false; op3.Visible = false; op4.Visible = false;
            string str = "";
            if (checkBox1.Checked)
                str += "0 ";
            if (checkBox2.Checked)
                str += "1 ";
            if (checkBox3.Checked)
                str += "2 ";
            if (checkBox4.Checked)
                str += "3 ";

            a.AnswerCheck(str).ToString();
            richTextBox1.Text = ""; richTextBox2.Text = ""; richTextBox3.Text = ""; richTextBox4.Text = "";

             ar = new string[a.GetQuestionCount()+4];
            ar[0] = a.GetTestName();
            ar[1] =  a.GetTestSection();
            ar[2] =  a.GetTestSubject();
            ar[3] = a.GlobalAnswerCheck().ToString();
            for( int sch=4;sch< a.GetQuestionCount()+4;sch++)
            {
                ar[sch] = a.QuestionCheck(sch - 4).ToString();

            }
            Statistic st = new Statistic();
            st.Show();
            this.Hide();
            st.mass = ar;
            a.TestClean();
            //   Application.Exit();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            checkBox1.Checked = !checkBox1.Checked;
        }

        private void op1_Click(object sender, EventArgs e)
        {
            checkBox1.Checked = !checkBox1.Checked;
            if (checkBox1.Checked)
                checkBox1.Visible = true;
            else
                checkBox1.Visible = !true;
        }

        private void op2_Click(object sender, EventArgs e)
        {
            checkBox2.Checked = !checkBox2.Checked;
            if (checkBox2.Checked)
                checkBox2.Visible = true;
            else
                checkBox2.Visible = !true;
        }

        private void op3_Click(object sender, EventArgs e)
        {
            checkBox3.Checked = !checkBox3.Checked;
            if (checkBox3.Checked)
                checkBox3.Visible = true;
            else
                checkBox3.Visible = !true;
        }

        private void op4_Click(object sender, EventArgs e)
        {
            checkBox4.Checked = !checkBox4.Checked;
            if (checkBox4.Checked)
                checkBox4.Visible = true;
            else
                checkBox4.Visible = !true;
        }
    }
}
