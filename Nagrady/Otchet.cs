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

        ОДБ.OleDbConnection con = new ОДБ.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source= rewards.mdb");
        ОДБ.OleDbCommand ucommand = new ОДБ.OleDbCommand();
        int check = 0;// счетчик, указывает на дипазон времени выбранного пользователем
        void makeQuery()
        {

        }
        void func(int q, int w, int t, int s, int a, string data_otchet) //   String data_otchet - переменная в которой записывается выбранный диапазон дат, для вывода в отчете
        { 
            con.Open();
            try
            {
                String qwt;//начало даты
                String ast;//конец даты
                // месяц день год
                //   q    w    t
                //    a    s    t
                qwt = q.ToString() + "/" + w.ToString() + "/" + t.ToString();
                ast = s.ToString() + "/" + a.ToString() + "/" + t.ToString();

                var comand1 = new ОДБ.OleDbCommand("(SELECT s.n, s.c, s1.c1 from (select reward_types.type_name as n, Count(*) as c " +
                     "FROM awardemps, rewards, reward_types  " +
                     "WHERE rewards.id=awardemps.reward_id and reward_types.id = rewards.id_type  " +
                    " AND awardemps.date_get>#" + qwt + "# And awardemps.date_get<#" + ast + "# " +
                     "GROUP BY  reward_types.type_name) as s  left join " +
               "(select  reward_types.type_name as n, Count(*) as  c1 " +
                              "   FROM awardemps, rewards, reward_types  " +
                               "  WHERE rewards.id=awardemps.reward_id and reward_types.id = rewards.id_type  " +
                              "   AND awardemps.date_award>#" + qwt + "# And awardemps.date_award<#" + ast + "#  " +
               "GROUP BY reward_types.type_name)  as s1 on s1.n = s.n)  union " +
               "(SELECT s1.n, s.c, s1.c1 from (select  reward_types.type_name as n, Count(*) as c " +
                            "     FROM awardemps, rewards, reward_types  " +
                             "    WHERE rewards.id=awardemps.reward_id and reward_types.id = rewards.id_type  " +
                              "   AND awardemps.date_get>#" + qwt + "# And awardemps.date_get<#" + ast + "#  " +
                               "  GROUP BY reward_types.type_name) as s right join " +
               "(select  reward_types.type_name as n, Count(*) as  c1 " +
                          "       FROM awardemps, rewards, reward_types  " +
                            "     WHERE rewards.id=awardemps.reward_id and reward_types.id = rewards.id_type  " +
                             "    AND awardemps.date_award>#" + qwt + "# And awardemps.date_award<#" + ast + "#  " +
               "GROUP BY reward_types.type_name) as s1 on s1.n = s.n) ", con);

                // подсчёт количества строк таблицы в отчете --->
                ОДБ.OleDbDataReader выборка1 = comand1.ExecuteReader();
                DataTable Rows1 = new DataTable();
                Rows1.Columns.Add(выборка1.GetName(0));
                while (выборка1.Read() == true)
                    Rows1.Rows.Add(new object[] { выборка1.GetValue(0) });
                выборка1.Close();

                var comand2 = new ОДБ.OleDbCommand("SELECT COUNT(*) FROM (SELECT DISTINCT awardemps.reward_id" +
                    " FROM awardemps, rewards, reward_types " +
                     "WHERE rewards.id=awardemps.reward_id and reward_types.id = rewards.id_type " +
                     "AND ((awardemps.date_award>#" + qwt + "# And awardemps.date_award<#" + ast + "# )" +
                     "or (awardemps.date_get>#" + qwt + "# And awardemps.date_get<#" + ast + "# )))", con);
                ОДБ.OleDbDataReader выборка2 = comand2.ExecuteReader();
                int col2=0;
                while (выборка2.Read() == true)
                {
                    col2 = Int16.Parse(выборка2.GetValue(0).ToString());
                }
                выборка2.Close();
                int f = Rows1.Rows.Count + col2 + 1;//количество строк таблицы в отчете , + 1 - это верхняя строка в которой содержатся названия столбцов
                MessageBox.Show(f.ToString());
                //   <---- подсчёт количества строк таблицы в отчете


                //  Создание документа, вывод текста ---->
                ОДБ.OleDbDataReader reader = comand1.ExecuteReader();
                var Word1 = new Ворд.Word.Application();
                Word1.Visible = true;
                Word1.Documents.Add();
                Word1.Selection.TypeText("СВЕДЕНИЯ О НАГРАДНОЙ ДЕЯТЕЛЬНОСТИ\r\n");
                Word1.Selection.TypeText("Министерство сельского хозяйства и рыбной промышленности Астраханской области\r\n");
                Word1.Selection.TypeText(data_otchet + "\r\n");
                Word1.ActiveDocument.Tables.Add(Word1.Selection.Range, f, 3, Ворд.Word.WdDefaultTableBehavior.wdWord9TableBehavior, Ворд.Word.WdAutoFitBehavior.wdAutoFitContent);
                //    <--------  Создание документа, вывод текста


                // Вывод названия столбцов ---->
                Word1.ActiveDocument.Tables[1].Cell(1, 1).Range.Font.Size = 14;
                Word1.ActiveDocument.Tables[1].Cell(1, 1).Range.Font.Name = "Times New Roman";
                Word1.ActiveDocument.Tables[1].Cell(1, 2).Range.Font.Size = 14;
                Word1.ActiveDocument.Tables[1].Cell(1, 2).Range.Font.Name = "Times New Roman";
                Word1.ActiveDocument.Tables[1].Cell(1, 3).Range.Font.Size = 14;
                Word1.ActiveDocument.Tables[1].Cell(1, 3).Range.Font.Name = "Times New Roman";
                Word1.ActiveDocument.Tables[1].Cell(1, 1).Range.InsertAfter("Вид награды");
                Word1.ActiveDocument.Tables[1].Cell(1, 2).Range.InsertAfter("Количество представленных к награждению");
                Word1.ActiveDocument.Tables[1].Cell(1, 3).Range.InsertAfter("Количество награжденных");
                //  <------ Вывод названия столбцов


                int j = 2; int i = 2;

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
                    Word1.ActiveDocument.Tables[1].Cell(j, 3).Range.Font.Size = 14;
                    Word1.ActiveDocument.Tables[1].Cell(j, 3).Range.Font.Bold = 3;
                    Word1.ActiveDocument.Tables[1].Cell(j, 3).Range.Font.Name = "Times New Roman";
                    Word1.ActiveDocument.Tables[1].Cell(j, 3).Range.InsertAfter(reader.GetValue(2).ToString());
                    //var comanda = new ОДБ.OleDbCommand("SELECT rewards.reward_name, Count(*)  " +
                    //" FROM awardemps, rewards, reward_types " +
                    // "WHERE rewards.id=awardemps.reward_id and reward_types.id = rewards.id_type " +
                    // "AND awardemps.date_award>#" + qwt + "# And awardemps.date_award<#" + ast + "# and  reward_types.type_name = '" + reader.GetValue(0) +
                    // "' GROUP BY rewards.reward_name", con);


                    var comanda = new ОДБ.OleDbCommand("(SELECT s.n, s.c, s1.c1 from (select rewards.reward_name as n, Count(*) as c " +
                "  FROM awardemps, rewards, reward_types WHERE rewards.id=awardemps.reward_id and reward_types.id = rewards.id_type   " +
                  " AND awardemps.date_get>#" + qwt + "# And awardemps.date_get<#" + ast + "#  and  reward_types.type_name = '" + reader.GetValue(0) +
                  "' GROUP BY rewards.reward_name) as s  left join  (select rewards.reward_name as n, Count(*) as  c1  FROM awardemps, rewards, reward_types  WHERE rewards.id=awardemps.reward_id and reward_types.id = rewards.id_type   " +
                 "  AND awardemps.date_award>#" + qwt + "# And awardemps.date_award<#" + ast + "#  and  reward_types.type_name = '" + reader.GetValue(0) +
"' GROUP BY rewards.reward_name)  as s1 on s1.n = s.n)  union (SELECT s1.n, s.c, s1.c1 from (select rewards.reward_name as n, Count(*) as c  " +
                "   FROM awardemps, rewards, reward_types WHERE rewards.id=awardemps.reward_id and reward_types.id = rewards.id_type   " +
                "    AND awardemps.date_get>#" + qwt + "# And awardemps.date_get<#" + ast + "#  and  reward_types.type_name = '" + reader.GetValue(0) +
                "'  GROUP BY rewards.reward_name) as s right join (select rewards.reward_name as n, Count(*) as  c1  FROM awardemps, rewards, reward_types   " +
                "   WHERE rewards.id=awardemps.reward_id and reward_types.id = rewards.id_type   " +
               "    AND awardemps.date_award>#" + qwt + "# And awardemps.date_award<#" + ast + "#  and  reward_types.type_name = '" + reader.GetValue(0) +
              "'   GROUP BY rewards.reward_name)  as s1 on s1.n = s.n)", con);


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
                        Word1.ActiveDocument.Tables[1].Cell(i, 3).Range.Font.Size = 14;
                        Word1.ActiveDocument.Tables[1].Cell(i, 3).Range.Font.Name = "Times New Roman";
                        Word1.ActiveDocument.Tables[1].Cell(i, 3).Range.InsertAfter(выполнение.GetValue(2).ToString());
                        i++; j++;
                    }
                    выполнение.Close();
                }
                Word1.Selection.MoveDown(Ворд.Word.WdUnits.wdLine, 15);
                reader.Close();
                
            }

            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Ошибка БД");
                con.Close();
            } con.Close();
        }
        private void checkSelect(int orderType)
        {
            try
            {
                //диапазон дат
                int q;//месяц1
                int w = 1;//день1
                int a;//месяц2
                int s;//день2
                int t; //год
                string header;

                //Проверка на ввод года, картала, месяца с формы
                if (check == 1)//если выбираем год
                {
                    if (int.Parse(textBox1.Text) > 2100 || int.Parse(textBox1.Text) < 1940)
                    {
                        textBox1.Text = "";
                    }
                    q = 1;
                    t = int.Parse(textBox1.Text);
                    s = 31;
                    a = 12;
                    header = "За " + textBox1.Text.ToString() + " год";

                }
                else if (check == 2)//если выбираем квартал
                {
                    if (int.Parse(textBox3.Text) > 2100 || int.Parse(textBox3.Text) < 1940)
                    {
                        textBox3.Text = "";
                    }
                    t = int.Parse(textBox3.Text);
                    int quarter = comboBox1.SelectedIndex;
                    a = (quarter + 1) * 3;
                    q = 1 + quarter * 3;
                    s = DateTime.DaysInMonth(t, q);
                    header = "За " + (quarter + 1) + " квартал " + t + " года";
                }
                else
                {
                    if (int.Parse(textBox5.Text) > 2100 || int.Parse(textBox5.Text) < 1940)
                    {
                        textBox5.Text = "";
                    }

                    q = comboBox2.SelectedIndex + 1;
                    a = q;
                    t = int.Parse(textBox5.Text);
                    s = DateTime.DaysInMonth(t, q);
                    header = "За " + comboBox2.SelectedIndex + textBox5.Text + " года";

                }
                if(orderType==1)
                    func(q, w, t, s, a, header);
                else
                    func2(q, w, t, s, a, header);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка ввода! Введите корректные значения!");
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            checkSelect(1);
        }

        void func2(int q, int w, int t, int s, int a, string data_otchet)
        {
            //try            {
                String qwt;//начало даты
                String ast;//конец даты
                // месяц день год
                //   q    w    t
                //    a    s    t
                qwt = q.ToString() + "/" + w.ToString() + "/" + t.ToString();
                ast = s.ToString() + "/" + a.ToString() + "/" + t.ToString();
                con.Open();

                DataTable mytable = new DataTable();
                ОДБ.OleDbDataReader выполнение;
                var comand1 = new ОДБ.OleDbCommand(" select reward_types.type_name, ' ', ' ', ' ', ' ', ' ', ' '" +
                     " FROM awardemps, rewards, reward_types " +
                     " WHERE rewards.id=awardemps.reward_id and reward_types.id = rewards.id_type " +
                     " AND awardemps.date_award>#"+qwt+"# And awardemps.date_award<#"+ast+"#" +
                     " GROUP BY reward_types.type_name ", con);
                ОДБ.OleDbDataReader reader = comand1.ExecuteReader();
                mytable.Columns.Add("Фамилия");
                mytable.Columns.Add("Имя");
                mytable.Columns.Add("Отчество");
                mytable.Columns.Add("Должность");
                mytable.Columns.Add("Дата рождения");
                mytable.Columns.Add("Вид награды");
                mytable.Columns.Add("Документ о награждении");
                ОДБ.OleDbCommand comanda;

                while (reader.Read() == true)
                {
                    mytable.Rows.Add(new object[] { reader.GetValue(0).ToString(), reader.GetValue(1).ToString(), reader.GetValue(2).ToString(),
                        reader.GetValue(3).ToString(), reader.GetValue(4).ToString(), reader.GetValue(5).ToString(), reader.GetValue(6).ToString() });

                    comanda = new ОДБ.OleDbCommand("select employees.lname, employees.fname, employees.patre, employees.pos, employees.birth, " +
                    " rewards.reward_name, awardEmps.act_id from employees, awardemps, rewards, reward_types " +
                    " where awardemps.reward_id = rewards.id and reward_types.id = rewards.id_type " + 
                     " AND awardemps.date_award>#" + qwt + "# And awardemps.date_award<#" + ast + "#" +
                    "AND employees.id = awardemps.emp_id and reward_types.type_name = '" + reader.GetValue(0) + "'", con);
                    выполнение = comanda.ExecuteReader();

                    while (выполнение.Read() == true)
                        mytable.Rows.Add(new object[]

                        { выполнение.GetValue(0).ToString(), выполнение.GetValue(1).ToString(), выполнение.GetValue(2).ToString(),
                выполнение.GetValue(3).ToString(), выполнение.GetValue(4).ToString(), выполнение.GetValue(5).ToString(),  выполнение.GetValue(6).ToString()
                        });

                    выполнение.Close();
                }

                reader.Close();
                con.Close();
                dataGridView1.DataSource = mytable;



/*            }

            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Ошибка БД");
            }*/
        }

        private void button2_Click(object sender, EventArgs e)
        {
            /*try
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
                    func2(q = 1, w = 1, t = int.Parse(textBox1.Text), s = 31, a = 12, "За " + textBox1.Text.ToString() + " год");

                }
                if (check == 2)//если выбираем квартал
                {
                    if (int.Parse(textBox3.Text) > 2100 || int.Parse(textBox3.Text) < 1940)
                    {
                        textBox3.Text = "";
                    }
                 
                    t = int.Parse(textBox3.Text);
                    int quarter =comboBox1.SelectedIndex;
                    func2(q = 1 + quarter * 3, w = 1, t, s = DateTime.DaysInMonth(t, q), a = (quarter + 1) * 3, "За 4 квартал " + t + " года");
                }
                if (check == 3)//если выбираем месяц
                {
                    if (int.Parse(textBox5.Text) > 2100 || int.Parse(textBox5.Text) < 1940)
                    {
                        textBox5.Text = "";
                    }
                   
                    q=comboBox2.SelectedIndex+1;
                    t=int.Parse(textBox5.Text);
                    func2(q, w = 1, t, s = DateTime.DaysInMonth(t, q), q, "За Декабрь " + textBox5.Text + " года");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка ввода! Введите корректные значения!");
            }*/
            checkSelect(2);
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


    }
}
