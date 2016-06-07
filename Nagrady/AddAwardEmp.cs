using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
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
        void update()
        {
            try
            {
                if (comboBox5.SelectedIndex > 0)
                {
                    Database.execute("Update Awardemps SET reward_id = " + comboBox4.Items[comboBox2.SelectedIndex].ToString() + ", emp_id = " + Data.empId + ", date_get = '" + dateTimePicker1.Value.Date
                        + "',  date_award = '" + dateTimePicker2.Value.Date + "',act_id = " + comboBox6.Items[comboBox5.SelectedIndex].ToString()
                        + ", act_num = '" + textBox1.Text + "', act_date = '" + dateTimePicker3.Value.Date + "', comment = '" + textBox3.Text + "' WHERE (id = " + Data.awardEmpId + ")");
                    
                }
                else
                {
                    Database.execute("Update Awardemps SET reward_id = " + comboBox4.Items[comboBox2.SelectedIndex].ToString() + ", emp_id = " + Data.empId + ", date_get = '" + dateTimePicker1.Value.Date
                        + "',  comment = '" + textBox3.Text + "' WHERE (id = " + Data.awardEmpId + ")");
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
                Database.execute("INSERT INTO Awardemps (reward_id, emp_id, date_get, date_award,act_id,act_num, act_date, comment) VALUES (" + comboBox4.Items[comboBox2.SelectedIndex].ToString() + ", " + Data.empId
                    + ", '" + dateTimePicker1.Value.Date.ToString("dd.MM.yyyy") + "', '" + dateTimePicker2.Value.Date.ToString("dd.MM.yyyy") + "', " + Int32.Parse(comboBox6.Items[comboBox5.SelectedIndex].ToString()) + ", '" + textBox1.Text + "', '" + dateTimePicker3.Value.Date.ToString("dd.MM.yyyy") + "', '" + textBox3.Text + "')");
               
            }
            else
            {
                Database.execute("INSERT INTO Awardemps (reward_id, emp_id, date_get, comment) VALUES (" + comboBox4.Items[comboBox2.SelectedIndex].ToString() + ", " + Data.empId + ", '" + dateTimePicker1.Value.Date.ToString("dd.MM.yyyy") + "', '" + textBox3.Text + "')");
                    
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка ввода данных");
               
            }
        }

        private void AddAwardEmp_Load(object sender, EventArgs e)
        {            
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
                label3.Text += " "+v.GetValue(1)+" "+v.GetValue(2)+ " "+v.GetValue(3);
            }
            label3.Text += " ";
            v.Close();
            v=Database.getReader("Select * From reward_types");
            while (v.Read() == true)
            {
                comboBox3.Items.Add(v.GetValue(0));
                comboBox1.Items.Add(v.GetValue(1));
            }
            v.Close();

            v=Database.getReader("Select * From LocalAct");
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
                var reader= Database.getReader("select [reward_types].[type_name], [rewards].[reward_name]," +
                "[date_get], [date_award], [act_name], [act_num], [act_date], [comment] from [employees], [rewards], [awardemps], [localact], [reward_types]" +
                "where [employees].[id] = [awardemps].[emp_id] and [rewards].[id]=[awardemps].[reward_id] and [reward_types].[id]=[rewards].[id_type] and [localact].[id] = [awardemps].[act_id] and [awardemps].[id] = " + Data.awardEmpId+"");
                 while (reader.Read())
                { 
                    comboBox1.SelectedIndex = comboBox1.Items.IndexOf(reader.GetValue(0).ToString());
                    comboBox2.SelectedIndex = comboBox2.Items.IndexOf(reader.GetValue(1).ToString());
                    dateTimePicker1.Value = DateTime.Parse(reader.GetValue(2).ToString());
                    textBox3.Text = reader.GetValue(7).ToString();
                    if (!(reader.GetValue(3).ToString() == "" || reader.GetValue(4).ToString() == ""   
                        ||reader.GetValue(5).ToString() == "" || reader.GetValue(6).ToString() == ""))
                    {
                        checkBox1.Checked = true;
                        DateTime dt = new DateTime();
                        if (DateTime.TryParse(reader.GetValue(3).ToString(), out dt))
                            dateTimePicker2.Value = dt;
                        else
                            dateTimePicker2.Value = DateTime.Now;
                        comboBox5.SelectedIndex = comboBox5.Items.IndexOf(reader.GetValue(4).ToString());
                        textBox1.Text = reader.GetValue(5).ToString();
                        if (DateTime.TryParse(reader.GetValue(6).ToString(), out dt))
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
    }
}
