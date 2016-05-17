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

        int check = 0;// счетчик, указывает на дипазон времени выбранного пользователем

        void func(int q, int w, int t, int s, int a)
        {
            try
            {
                String qwt;//начало даты
                String ast;//конец даты
                // месяц день год
                //   q    w    t
                //    a    s    t
                qwt = q.ToString() + "/" + w.ToString() + "/" + t.ToString();
                ast = s.ToString() + "/" + a.ToString() + "/" + t.ToString();


                con = new ОДБ.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0; Data Source = rewards.mdb");
                con.Open();
             var comand1 = new ОДБ.OleDbCommand("SELECT reward_types.type_name, count(*) " +
                    "FROM awardemps, rewards, reward_types " +
                    "WHERE rewards.id=awardemps.reward_id AND awardemps.date_award>#" + qwt + "# And awardemps.date_award<#" + ast + "# " +
                    "and reward_types.id = rewards.id_type " +
                    "GROUP BY reward_types.type_name ", con);


               // подсчёт количества строк таблицы в отчете --->
                ОДБ.OleDbDataReader выборка1 = comand1.ExecuteReader();
                DataTable Rows1 = new DataTable();
                Rows1.Columns.Add(выборка1.GetName(0));
                while (выборка1.Read() == true)
                    Rows1.Rows.Add(new object[] { выборка1.GetValue(0) });
                выборка1.Close();

                var comand2 = new ОДБ.OleDbCommand("SELECT rewards.reward_name, Count(*)  " +
                    " FROM awardemps, rewards, reward_types " +
                     "WHERE rewards.id=awardemps.reward_id and reward_types.id = rewards.id_type " +
                     "AND awardemps.date_award>#" + qwt + "# And awardemps.date_award<#" + ast + "# " +
                     " GROUP BY rewards.reward_name", con);
                ОДБ.OleDbDataReader выборка2 = comand2.ExecuteReader();
                DataTable Rows2 = new DataTable();
                Rows2.Columns.Add(выборка2.GetName(0));
                while (выборка2.Read() == true)
                    Rows2.Rows.Add(new object[] { выборка2.GetValue(0) });
                выборка2.Close();
                 int f = Rows1.Rows.Count + Rows2.Rows.Count;
                //MessageBox.Show(f.ToString());
                //   <---- подсчёт количества строк таблицы в отчете



                ОДБ.OleDbDataReader reader = comand1.ExecuteReader();
                var Word1 = new Ворд.Word.Application();
                Word1.Visible = true;
                Word1.Documents.Add();
                Word1.Selection.TypeText("                                    СВЕДЕНИЯ О НАГРАДНОЙ ДЕЯТЕЛЬНОСТИ\r\n");
                Word1.Selection.TypeText("         Министерство сельского хозяйства и рыбной промышленности Астраханской области\r\n");
                Word1.ActiveDocument.Tables.Add(Word1.Selection.Range, f, 3, Ворд.Word.WdDefaultTableBehavior.wdWord9TableBehavior, Ворд.Word.WdAutoFitBehavior.wdAutoFitContent);
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
        
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //диапазон дат
                int q;//месяц1
                int w;//день1
                int a;//месяц2
                int s;//день2
                int t; //год


                //Проверка на ввод года, картала, месяца с формы
                if (check == 1)//если выбираем год
                {
                    if (int.Parse(textBox1.Text) > 2100 || int.Parse(textBox1.Text) < 1940)
                    {
                        textBox1.Text = "";
                    }
                    func(q = 1, w = 1, t = int.Parse(textBox1.Text), s = 31, a = 12);
                }
                if (check == 2)//если выбираем квартал
                {
                    if (int.Parse(textBox3.Text) > 2100 || int.Parse(textBox3.Text) < 1940)
                    {
                        textBox3.Text = "";
                    }
                    if (comboBox1.Text.ToString() == "1")
                    {
                        func(q = 1, w = 1, t = int.Parse(textBox3.Text), s = 31, a = 3);
                    }
                    else if (comboBox1.Text.ToString() == "2")
                    {
                        func(q = 4, w = 1, t = int.Parse(textBox3.Text), s = 30, a = 6);
                    }
                    else if (comboBox1.Text.ToString() == "3")
                    {
                        func(q = 7, w = 1, t = int.Parse(textBox3.Text), s = 30, a = 9);
                    }
                    else if (comboBox1.Text.ToString() == "4")
                    {
                        func(q = 10, w = 1, t = int.Parse(textBox3.Text), s = 31, a = 12);
                    }
                }
                if (check == 3)//если выбираем месяц
                {
                    if (int.Parse(textBox5.Text) > 2100 || int.Parse(textBox5.Text) < 1940)
                    {
                        textBox5.Text = "";
                    }
                    switch (comboBox2.Text)
                    {
                        case "Январь":
                            func(q = 1, w = 1, t = int.Parse(textBox5.Text), s = 31, a = 1);
                            break;
                        case "Февраль":
                            double proverka = Double.Parse(textBox5.Text) % 4; //проверка на високосный год
                            if (proverka == 0)
                            {
                                func(q = 2, w = 1, t = int.Parse(textBox5.Text), s = 29, a = 2);
                            }
                            else
                            {
                                func(q = 2, w = 1, t = int.Parse(textBox5.Text), s = 28, a = 2); 
                            }
                            
                            break;
                        case "Март":
                            func(q = 3, w = 1, t = int.Parse(textBox5.Text), s = 31, a = 3);
                            break;
                        case "Апрель":
                            func(q = 4, w = 1, t = int.Parse(textBox5.Text), s = 30, a = 4);
                            break;
                        case "Май":
                            func(q = 5, w = 1, t = int.Parse(textBox5.Text), s = 31, a = 5);
                            break;
                        case "Июнь":
                            func(q = 6, w = 1, t = int.Parse(textBox5.Text), s = 30, a = 6);
                            break;
                        case "Июль":
                            func(q = 7, w = 1, t = int.Parse(textBox5.Text), s = 31, a = 7);
                            break;
                        case "Август":
                            func(q = 8, w = 1, t = int.Parse(textBox5.Text), s = 31, a = 8);
                            break;
                        case "Сентябрь":
                            func(q = 9, w = 1, t = int.Parse(textBox5.Text), s = 30, a = 9);
                            break;
                        case "Октябрь":
                            func(q = 10, w = 1, t = int.Parse(textBox5.Text), s = 31, a = 10);
                            break;
                        case "Ноябрь":
                            func(q = 11, w = 1, t = int.Parse(textBox5.Text), s = 30, a = 11);
                            break;
                        case "Декабрь":
                            func(q = 12, w = 1, t = int.Parse(textBox5.Text), s = 31, a = 12);
                            break;
                        default:
                            MessageBox.Show("В году всего 12 месяцев!");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка ввода! Введите корректные значения!");
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
                check = 1;
                label2.Enabled = true;
                textBox1.Enabled = true;
                checkBox2.Enabled = false;
                checkBox3.Enabled = false;
                button1.Enabled = true;
                button2.Enabled = true;
            }
            else
            {
                check = 0;
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
                check = 2;
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
                check = 0;
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
                check = 3;
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
                check = 0;
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
            //кварталы
            comboBox1.Items.Add("1");
            comboBox1.Items.Add("2");
            comboBox1.Items.Add("3");
            comboBox1.Items.Add("4");
            //месяцы
            comboBox2.Items.Add("Январь");
            comboBox2.Items.Add("Февраль");
            comboBox2.Items.Add("Март");
            comboBox2.Items.Add("Апрель");
            comboBox2.Items.Add("Май");
            comboBox2.Items.Add("Июнь");
            comboBox2.Items.Add("Июль");
            comboBox2.Items.Add("Август");
            comboBox2.Items.Add("Сентябрь");
            comboBox2.Items.Add("Октябрь");
            comboBox2.Items.Add("Ноябрь");
            comboBox2.Items.Add("Декабрь");

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
