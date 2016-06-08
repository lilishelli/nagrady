using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Nagrady
{
    public partial class AddAwardEmp : Form
    {
        public AddAwardEmp()
        {
            InitializeComponent();
        }
        string newdoc="";
        string olddoc = "";
        string sourcedoc="";
        string formatdoc = "";
        void update()
        {
            try
            {
                if (comboBox5.SelectedIndex >= 0)
                {
                    Database.execute("Update Awardemps SET reward_id = " + comboBox4.Items[comboBox2.SelectedIndex].ToString() + ", emp_id = " + Data.empId + ", date_get = '" + dateTimePicker1.Value.Date
                        + "',  date_award = '" + dateTimePicker2.Value.Date + "',act_id = " + comboBox6.Items[comboBox5.SelectedIndex].ToString()
                        + ", act_num = '" + textBox1.Text + "', act_date = '" + dateTimePicker3.Value.Date + "', comment = '" + textBox3.Text + "', doc = '"+newdoc+"' WHERE (id = " + Data.awardEmpId + ")");
                    
                }
                else
                {
                    Database.execute("Update Awardemps SET reward_id = " + comboBox4.Items[comboBox2.SelectedIndex].ToString() + ", emp_id = " + Data.empId + ", date_get = '" + dateTimePicker1.Value.Date
                        + "', date_award=null, act_id=null, act_num=null, act_date=null, comment = '" + textBox3.Text + "',doc = '"+newdoc+"' WHERE (id = " + Data.awardEmpId + ")");
                }
                MessageBox.Show("Запись обновлена");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка ввода данных");
            }
        }
        void add()
        {
           if (comboBox5.SelectedIndex > 0)
            {
                Database.execute("INSERT INTO Awardemps (reward_id, emp_id, date_get, date_award,act_id,act_num, act_date, comment, doc) VALUES (" + comboBox4.Items[comboBox2.SelectedIndex].ToString() + ", " + Data.empId
                    + ", '" + dateTimePicker1.Value.Date.ToString("dd.MM.yyyy") + "', '" + dateTimePicker2.Value.Date.ToString("dd.MM.yyyy") + "', " + Int32.Parse(comboBox6.Items[comboBox5.SelectedIndex].ToString()) + ", '" + textBox1.Text + "', '" + dateTimePicker3.Value.Date.ToString("dd.MM.yyyy") + "', '" + textBox3.Text + "', '"+newdoc+"')");
               
            }
            else
            {
                Database.execute("INSERT INTO Awardemps (reward_id, emp_id, date_get, comment, doc) VALUES (" + comboBox4.Items[comboBox2.SelectedIndex].ToString() + ", " + Data.empId + ", '" + dateTimePicker1.Value.Date.ToString("dd.MM.yyyy") + "', '" + textBox3.Text + "', '"+newdoc+"')");
                    
            }
           MessageBox.Show("В таблицу добавлена запись");
                      
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
               if (Data.isAddAwardBtn==false)
                {
                    update();
                }
                else
                {
                    add();
                }
                if(newdoc!="" && sourcedoc!="")
                {
                    File.Copy(sourcedoc, AppDomain.CurrentDomain.BaseDirectory + "docs\\" + Data.empId+"."+formatdoc, true); 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка ввода данных");
               
            }
            loadaddawardemp();
        }
        void loadaddawardemp()
        {
            newdoc = "";
            sourcedoc = "";
            olddoc = "";
            formatdoc = "";
            OleDbDataReader v;
            if (Data.isAddAwardBtn == false)
            {
                v = Database.getReader("Select employees.id, lname, fname, patre From employees, awardemps where awardemps.emp_id = employees.id and awardemps.id = " + Data.awardEmpId + "");
            }
            else
            {
                v = Database.getReader("Select employees.id, lname, fname, patre From employees where id = " + Data.empId + "");

            }
            if (v.Read() == true)
            {
                Data.empId = Int32.Parse(v.GetValue(0).ToString());
                label3.Text += " " + v.GetValue(1) + " " + v.GetValue(2) + " " + v.GetValue(3);
            }
            label3.Text += " ";
            v.Close();
            v = Database.getReader("Select * From reward_types");
            while (v.Read() == true)
            {
                comboBox3.Items.Add(v.GetValue(0));
                comboBox1.Items.Add(v.GetValue(1));
            }
            v.Close();

            v = Database.getReader("Select * From LocalAct");
            while (v.Read() == true)
            {
                comboBox6.Items.Add(v.GetValue(0));
                comboBox5.Items.Add(v.GetValue(1));
            }
            v.Close();
            if (Data.isAddAwardBtn == true)
            {
                checkBox1.Checked = false;
                dateTimePicker2.Enabled = false;
                comboBox5.Enabled = false;
                label5.Enabled = false;
                label6.Enabled = false;
                label7.Enabled = false;
                label8.Enabled = false;
                dateTimePicker3.Enabled = false;
                textBox1.Enabled = false;
            }
            else
            {
                var reader = Database.getReader("select [reward_types].[type_name], [rewards].[reward_name]," +
                "[date_get], [date_award], [act_name], [act_num], [act_date], [comment], [doc] FROM Reward_types INNER JOIN (Rewards INNER JOIN (Employees INNER JOIN (awardemps LEFT JOIN localact ON [awardemps].[act_id] = [localact].[id]) ON [Employees].[id] = [awardemps].[emp_id])" +
                " ON [Rewards].[id] = [awardemps].[reward_id]) ON [Reward_types].[id] = [Rewards].[id_type] where [awardemps].[id] = " + Data.awardEmpId + "");
                while (reader.Read())
                {
                    comboBox1.SelectedIndex = comboBox1.Items.IndexOf(reader.GetValue(0).ToString());//тип награды
                    comboBox2.SelectedIndex = comboBox2.Items.IndexOf(reader.GetValue(1).ToString());//вид награды
                    dateTimePicker1.Value = DateTime.Parse(reader.GetValue(2).ToString());//дата представления
                    textBox3.Text = reader.GetValue(7).ToString();//комментарий
                    olddoc = reader.GetValue(8).ToString();//документ
                    if (!(reader.GetValue(3).ToString() == "" || reader.GetValue(4).ToString() == ""
                        || reader.GetValue(5).ToString() == "" || reader.GetValue(6).ToString() == ""))
                    {
                        checkBox1.Checked = true;
                        DateTime dt = new DateTime();
                        if (DateTime.TryParse(reader.GetValue(3).ToString(), out dt))//дата полученния
                            dateTimePicker2.Value = dt;
                        else
                            dateTimePicker2.Value = DateTime.Now;
                        comboBox5.SelectedIndex = comboBox5.Items.IndexOf(reader.GetValue(4).ToString());//название акта
                        textBox1.Text = reader.GetValue(5).ToString();//номер акта 
                        if (DateTime.TryParse(reader.GetValue(6).ToString(), out dt))//дата акта
                            dateTimePicker3.Value = dt;
                        else
                            dateTimePicker3.Value = DateTime.Now;
                    }
                    else
                    {
                        checkBox1.Checked = false;
                        dateTimePicker2.Enabled = false;
                        comboBox5.Enabled = false;
                        label5.Enabled = false;
                        label6.Enabled = false;
                        label7.Enabled = false;
                        label8.Enabled = false;
                        dateTimePicker3.Enabled = false;
                        textBox1.Enabled = false;
                    }
                }
                reader.Close();

            }
        }
        private void AddAwardEmp_Load(object sender, EventArgs e)
        {
            loadaddawardemp();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox4.Items.Clear();
            comboBox2.Items.Clear();
            var v=Database.getReader("Select id, reward_name From Rewards where id_type = " + comboBox3.Items[comboBox1.SelectedIndex].ToString() + "");
            while (v.Read() == true)
            {
                comboBox4.Items.Add(v.GetValue(0));
                comboBox2.Items.Add(v.GetValue(1));
            }
                
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                dateTimePicker2.Enabled = true;
                comboBox5.Enabled = true;
                label5.Enabled = true;
                label6.Enabled = true;
                label7.Enabled = true;
                label8.Enabled = true;
                dateTimePicker3.Enabled = true;
                textBox1.Enabled = true;
            }
            else
            {
                dateTimePicker2.Enabled = false;
                comboBox5.Enabled = false;
                label5.Enabled = false;
                label6.Enabled = false;
                label7.Enabled = false;
                label8.Enabled = false;
                dateTimePicker3.Enabled = false;
                textBox1.Enabled = false;

            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {            
            var ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //newdoc = ofd.FileName.Split(new[] { '\\' }).Last();                
                formatdoc = ofd.FileName.Split(new[] { '.' }).Last();
                newdoc = Data.empId + "." + formatdoc;
                sourcedoc = ofd.FileName;               
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (sourcedoc != "")
                    Process.Start(sourcedoc);
                else if (olddoc != "")
                    Process.Start(AppDomain.CurrentDomain.BaseDirectory + "docs\\" + olddoc);
                else
                    MessageBox.Show("Не удаётся найти указанный файл");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"Ошибка");
            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(olddoc!="")
            {
                File.Delete(AppDomain.CurrentDomain.BaseDirectory + "docs\\" + olddoc);
                Database.execute("update awardemps set doc = ''");
                MessageBox.Show("Файл удалён");
                newdoc = "";
                sourcedoc = "";
            }
            else if(newdoc!="")
            {
                newdoc = "";
                sourcedoc = "";
            }
            else
            {
                MessageBox.Show("Файл не был загружен", "Ошибка");
            }
        }
    }
}
