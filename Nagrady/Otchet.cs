using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Linq;
using ОДБ = System.Data.OleDb;
using Ворд = Microsoft.Office.Interop;

namespace Nagrady
{
    public partial class Otchet : Form
    {
        public Otchet()
        {
            InitializeComponent();
        }

        // DataSet rewards;
        ОДБ.OleDbConnection con = new ОДБ.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source= rewards.mdb");
        ОДБ.OleDbCommand ucommand = new ОДБ.OleDbCommand();
        // ОДБ.OleDbDataAdapter Adapter;

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int q;
                int w;
                int a;
                int s;
                int t;
                String qwt;//начало даты
                String ast;//конец даты

                ////Проверка на ввод года, картала, месяца с формы
                //if (checkBox1.Checked == true && checkBox2.Checked == false && checkBox2.Checked == false && checkBox3.Checked == false)//если выбираем год
                //{
                //    q = 1;//месяц
                //    w = 1;//день
                //    a = 12;//месяц
                //    s = 31;//день
                //    t = int.Parse(textBox1.Text);
                //    qwt = q.ToString() + "/" + w.ToString() + "/" + t.ToString();
                //    ast = s.ToString() + "/" + a.ToString() + "/" + t.ToString();
                //}

                //else
                //if (checkBox2.Checked == true && checkBox1.Checked == false && checkBox3.Checked == false)//если выбираем квартал
                //{
                //    if (comboBox1.Text.ToString() == "1")
                //    {
                //        q = 1;//месяц
                //        w = 1;//день
                //        a = 3;//месяц
                //        s = 31;//день
                //        t = int.Parse(textBox3.Text);
                //        qwt = q.ToString() + "/" + w.ToString() + "/" + t.ToString();
                //        ast = s.ToString() + "/" + a.ToString() + "/" + t.ToString();
                //    }
                //    else if (comboBox1.Text.ToString() == "2")
                //    {
                //        q = 1;//месяц
                //        w = 4;//день
                //        a = 6;//месяц
                //        s = 30;//день
                //        t = int.Parse(textBox3.Text);
                //        qwt = q.ToString() + "/" + w.ToString() + "/" + t.ToString();
                //        ast = s.ToString() + "/" + a.ToString() + "/" + t.ToString();
                //    }
                //    else if (comboBox1.Text.ToString() == "3")
                //    {
                //        q = 1;//месяц
                //        w = 7;//день
                //        a = 9;//месяц
                //        s = 30;//день
                //        t = int.Parse(textBox3.Text);
                //        qwt = q.ToString() + "/" + w.ToString() + "/" + t.ToString();
                //        ast = s.ToString() + "/" + a.ToString() + "/" + t.ToString();
                //    }
                //    else if (comboBox1.Text.ToString() == "4")
                //    {
                //        q = 1;//месяц
                //        w = 10;//день
                //        a = 12;//месяц
                //        s = 31;//день
                //        t = int.Parse(textBox3.Text);
                //        qwt = q.ToString() + "/" + w.ToString() + "/" + t.ToString();
                //        ast = s.ToString() + "/" + a.ToString() + "/" + t.ToString();
                //    }



                //}//если выбираем месяц
                //else if (checkBox3.Checked == true && checkBox2.Checked == false && checkBox1.Checked == false && checkBox3.Checked == false)//если выбираем год
                //{

                //q = int.Parse(comboBox2.Text);//месяц
                //w = 1;//день
                //a = int.Parse(comboBox2.Text);//месяц
                //s = 31;//день
                //t = int.Parse(textBox1.Text);
                //qwt = q.ToString() + "/" + w.ToString() + "/" + t.ToString();
                //ast = s.ToString() + "/" + a.ToString() + "/" + t.ToString();

                //}
                

                    q = 1;//месяц
                w = 1;//день
                a = 12;//месяц
                s = 31;//день

                if (checkBox1.Enabled = true) t = int.Parse(textBox1.Text);
               else if (checkBox2.Enabled = true) t = int.Parse(textBox3.Text);
                else
                t = int.Parse(textBox5.Text);
                


                qwt = q.ToString() + "/" + w.ToString() + "/" + t.ToString();
                ast = s.ToString() + "/" + a.ToString() + "/" + t.ToString();



                con = new ОДБ.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0; Data Source = rewards.mdb");
                con.Open();
                DataTable mytable = new DataTable();
                var comand1 = new ОДБ.OleDbCommand("SELECT  reward_types.type_name, count(*) " +
                    "FROM awardemps, rewards, reward_types " +
                    "WHERE rewards.id=awardemps.reward_id AND awardemps.date_award>#" + qwt + "# And awardemps.date_award<#" + ast + "# " +
                    "and reward_types.id = rewards.id_type " +
                    "GROUP BY reward_types.type_name ", con);
                ОДБ.OleDbDataReader reader = comand1.ExecuteReader();
                var Word1 = new Ворд.Word.Application();
                Word1.Visible = true;
                Word1.Documents.Add();
                Word1.Selection.TypeText("                                    СВЕДЕНИЯ О НАГРАДНОЙ ДЕЯТЕЛЬНОСТИ\r\n");
                Word1.Selection.TypeText("         Министерство сельского хозяйства и рыбной промышленности Астраханской области\r\n");
                Word1.ActiveDocument.Tables.Add(Word1.Selection.Range, 20, 3, Ворд.Word.WdDefaultTableBehavior.wdWord9TableBehavior, Ворд.Word.WdAutoFitBehavior.wdAutoFitContent);
                int j = 1; int i = 1;
                while (reader.Read() == true)
                {
                    Word1.ActiveDocument.Tables[1].Cell(j, 1).Range.Font.Size = 14;
                    Word1.ActiveDocument.Tables[1].Cell(j, 1).Range.Font.Bold = 3;
                    Word1.ActiveDocument.Tables[1].Cell(j, 1).Range.Font.Name = "Times New Roman";
                    Word1.ActiveDocument.Tables[1].Cell(j, 1).Range.InsertAfter(reader.GetValue(0).ToString());
                    Word1.ActiveDocument.Tables[1].Cell(j, 2).Range.Font.Size = 14;
                    Word1.ActiveDocument.Tables[1].Cell(j, 2).Range.Font.Bold = 3;
                    Word1.ActiveDocument.Tables[1].Cell(j, 2).Range.Font.Name = "Times New Roman";
                    Word1.ActiveDocument.Tables[1].Cell(j, 2).Range.InsertAfter(reader.GetValue(1).ToString());

                    var comanda = new ОДБ.OleDbCommand("SELECT rewards.reward_name, Count(*)  " +
                      " FROM awardemps, rewards, reward_types " +
                       "WHERE rewards.id=awardemps.reward_id and reward_types.id = rewards.id_type " +
                       "AND awardemps.date_award>#" + qwt + "# And awardemps.date_award<#" + ast + "# and  reward_types.type_name = '" + reader.GetValue(0) +
                       "' GROUP BY rewards.reward_name", con);
                    j++; i++;
                    ОДБ.OleDbDataReader выполнение = comanda.ExecuteReader();


                    while (выполнение.Read() == true)
                    {
                        Word1.ActiveDocument.Tables[1].Cell(i, 1).Range.Font.Size = 14;
                        Word1.ActiveDocument.Tables[1].Cell(i, 1).Range.Font.Name = "Times New Roman";
                        Word1.ActiveDocument.Tables[1].Cell(i, 1).Range.InsertAfter(выполнение.GetValue(0).ToString());
                        Word1.ActiveDocument.Tables[1].Cell(i, 2).Range.Font.Size = 14;
                        Word1.ActiveDocument.Tables[1].Cell(i, 2).Range.Font.Name = "Times New Roman";
                        Word1.ActiveDocument.Tables[1].Cell(i, 2).Range.InsertAfter(выполнение.GetValue(1).ToString());

                        i++; j++;
                    }
                    выполнение.Close();
                }
                Word1.Selection.MoveDown(Ворд.Word.WdUnits.wdLine, 15);
                reader.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            //Word1.Selection.TypeText("                                    Сведения\r\n");
            //Word1.Selection.TypeText("                        о награжденных работниках сельского хозяйства, представленных\r\n");
            //Word1.Selection.TypeText("         Министерством сельского хозяйства и рыбной промышленности Астраханской области\r\n");
        }

        private void checkBox1_CheckStateChanged(object sender, EventArgs e)
        {

            if (checkBox1.Checked == true)
            {
                label2.Enabled = true;
                textBox1.Enabled = true;
                checkBox2.Enabled = false;
                checkBox3.Enabled = false;
                button1.Enabled = true;
                button2.Enabled = true;
            }
            else
            {
                label2.Enabled = false;
                textBox1.Enabled = false;
                checkBox2.Enabled = true;
                checkBox3.Enabled = true;
                button1.Enabled = false;
                button2.Enabled = false;

            }
        }

        private void checkBox2_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                label5.Enabled = true;
                label3.Enabled = true;
                comboBox1.Enabled = true;
                textBox3.Enabled = true;
                checkBox1.Enabled = false;
                checkBox3.Enabled = false;
                button1.Enabled = true;
                button2.Enabled = true;
            }
            else
            {
                label3.Enabled = false;
                label5.Enabled = false;
                comboBox1.Enabled = false;
                textBox3.Enabled = false;
                checkBox1.Enabled = true;
                checkBox3.Enabled = true;
                button1.Enabled = false;
                button2.Enabled = false;
            }
        }

        private void checkBox3_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
            {
                label6.Enabled = true;
                label4.Enabled = true;
                comboBox2.Enabled = true;
                textBox5.Enabled = true;
                checkBox1.Enabled = false;
                checkBox2.Enabled = false;
                button1.Enabled = true;
                button2.Enabled = true;
            }
            else
            {
                label4.Enabled = false;
                label6.Enabled = false;
                comboBox2.Enabled = false;
                textBox5.Enabled = false;
                checkBox1.Enabled = true;
                checkBox2.Enabled = true;
                button1.Enabled = false;
                button2.Enabled = false;
            }
        }

        private void Otchet_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("1");
            comboBox1.Items.Add("2");
            comboBox1.Items.Add("3");
            comboBox1.Items.Add("4");

            comboBox2.Items.Add("1");
            comboBox2.Items.Add("2");
            comboBox2.Items.Add("3");
            comboBox2.Items.Add("4");
            comboBox2.Items.Add("5");
            comboBox2.Items.Add("6");
            comboBox2.Items.Add("7");
            comboBox2.Items.Add("8");
            comboBox2.Items.Add("9");
            comboBox2.Items.Add("10");
            comboBox2.Items.Add("11");
            comboBox2.Items.Add("12");

        }
    }
}
