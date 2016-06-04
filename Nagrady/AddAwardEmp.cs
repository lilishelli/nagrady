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
using odb = System.Data.OleDb;
namespace Nagrady
{
    public partial class AddAwardEmp : Form
    {
        public AddAwardEmp()
        {
            InitializeComponent();
        }
        odb.OleDbConnection con = new odb.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source= rewards.mdb");
        odb.OleDbDataAdapter Adapter;
        void update()
        {
            try
            {
                var command = new odb.OleDbCommand();
                if (comboBox5.SelectedIndex > 0)
                {
                    command = new odb.OleDbCommand("Update Awardemps SET reward_id = ?, emp_id = ?, date_get = ?,  date_award = ?,act_id = ?, act_num = ?, act_date = ?, comment = ? WHERE (id = ?)");
                    command.Parameters.Add("reward_id", odb.OleDbType.Integer, 30).Value = comboBox4.Items[comboBox2.SelectedIndex].ToString();
                    command.Parameters.Add("emp_id", odb.OleDbType.Integer, 30).Value = Data.empId;
                    command.Parameters.Add("date_get", odb.OleDbType.Date).Value = dateTimePicker1.Value.Date;
                    command.Parameters.Add("date_award", odb.OleDbType.Date).Value = dateTimePicker2.Value.Date;
                    command.Parameters.Add("act_id", odb.OleDbType.Integer, 30).Value = Int32.Parse(comboBox6.Items[comboBox5.SelectedIndex].ToString());
                    command.Parameters.Add("act_num", odb.OleDbType.VarWChar, 80).Value = textBox1.Text;
                    command.Parameters.Add("act_date", odb.OleDbType.Date).Value = dateTimePicker3.Value.Date;
                }
                else
                {
                    command = new odb.OleDbCommand("Update Awardemps SET reward_id = ?, emp_id = ?, date_get = ?,  comment = ? WHERE (id = ?)");
                    command.Parameters.Add("reward_id", odb.OleDbType.Integer, 30).Value = comboBox4.Items[comboBox2.SelectedIndex].ToString();
                    command.Parameters.Add("emp_id", odb.OleDbType.Integer, 30).Value = Data.empId;
                    command.Parameters.Add("date_get", odb.OleDbType.Date).Value = dateTimePicker1.Value.Date;
                }
                command.Parameters.Add("comment", odb.OleDbType.VarWChar, 255).Value = textBox3.Text;
                command.Parameters.Add("id", odb.OleDbType.Integer, 30).Value = Data.awardEmpId;
                Adapter = new odb.OleDbDataAdapter(command);
                Adapter.UpdateCommand = command;
                command.Connection = con;
                command.ExecuteNonQuery();
                MessageBox.Show("Запись обновлена");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка ввода данных");
            }
        }
        void add()
        {
           // con.Open();
           var comanda = new odb.OleDbCommand();
           if (comboBox5.SelectedIndex > 0)
            {
                comanda.CommandText = "INSERT INTO Awardemps (reward_id, emp_id, date_get, date_award,act_id,act_num, act_date, comment) VALUES (?, ?, ?, ?, ?, ?, ?, ?)";
                comanda.Parameters.Add("reward_id", odb.OleDbType.Integer, 30).Value = comboBox4.Items[comboBox2.SelectedIndex].ToString();
                comanda.Parameters.Add("emp_id", odb.OleDbType.Integer, 30).Value = Data.empId;
                comanda.Parameters.Add("date_get", odb.OleDbType.Date).Value = dateTimePicker1.Value.Date;
                comanda.Parameters.Add("date_award", odb.OleDbType.Date).Value = dateTimePicker2.Value.Date;
                comanda.Parameters.Add("act_id", odb.OleDbType.Integer, 30).Value = Int32.Parse(comboBox6.Items[comboBox5.SelectedIndex].ToString());
                comanda.Parameters.Add("act_num", odb.OleDbType.VarWChar, 80).Value = textBox1.Text;
                comanda.Parameters.Add("act_date", odb.OleDbType.Date).Value = dateTimePicker3.Value.Date;
            }
            else
            {
                comanda.CommandText = "INSERT INTO Awardemps (reward_id, emp_id, date_get, comment) VALUES (?, ?, ?, ?)";
                //comanda = new odb.OleDbCommand("INSERT INTO Awardemps (reward_id, emp_id, date_get, comment) VALUES (?, ?, ?, ?)");
                comanda.Parameters.Add("reward_id", odb.OleDbType.Integer, 30).Value = comboBox4.Items[comboBox2.SelectedIndex].ToString();
                comanda.Parameters.Add("emp_id", odb.OleDbType.Integer, 30).Value = Data.empId;
                comanda.Parameters.Add("date_get", odb.OleDbType.Date).Value = dateTimePicker1.Value.Date;              
            }
            comanda.Parameters.Add("comment", odb.OleDbType.VarWChar, 255).Value = textBox3.Text;
            comanda.Connection = con;
            comanda.ExecuteNonQuery();
            MessageBox.Show("В таблицу добавлена запись");
           // con.Close();            
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                if (!Data.isAddBtn)
                {
                    update();
                }
                else
                {
                    add();
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка ввода данных");
                con.Close();
            }
        }

        private void AddAwardEmp_Load(object sender, EventArgs e)
        {            
            con.Open();
            OleDbCommand comanda;
            if (Data.isAddAwardBtn == false)
            {
                comanda = new odb.OleDbCommand("Select lname, fname, patre From employees, awardemps where awardemps.emp_id = employees.id and awardemps.id = " + Data.awardEmpId + "", con);
            }
            else
            {
                comanda = new odb.OleDbCommand("Select lname, fname, patre From employees where id = " + Data.empId + "", con);
            }
            odb.OleDbDataReader выполнение = comanda.ExecuteReader();
            if (выполнение.Read() == true)
            {
                label3.Text += " "+выполнение.GetValue(0)+" "+выполнение.GetValue(1)+ " "+выполнение.GetValue(2);
            }
            label3.Text += " ";
            comanda = new odb.OleDbCommand("Select * From reward_types", con);
            выполнение = comanda.ExecuteReader();
            while (выполнение.Read() == true)
            {
                comboBox3.Items.Add(выполнение.GetValue(0));
                comboBox1.Items.Add(выполнение.GetValue(1));
            }
            выполнение.Close();

            comanda = new odb.OleDbCommand("Select * From LocalAct", con);
            выполнение = comanda.ExecuteReader();
            while (выполнение.Read() == true)
            {
                comboBox5.Items.Add(выполнение.GetValue(1));
            }
            выполнение.Close();
            if (Data.isAddAwardBtn == true)
            {
                checkBox1.Checked = false;
                dateTimePicker2.Enabled = false;
                comboBox5.Enabled = false;
                button2.Enabled = false;
                label5.Enabled = false;
                label6.Enabled = false;
                label7.Enabled = false;
                label8.Enabled = false;
                dateTimePicker3.Enabled = false;
                textBox1.Enabled = false;
            }
            else 
            {
                var comanda2 = new odb.OleDbCommand("select [reward_types].[type_name], [rewards].[reward_name]," +
                "[date_get], [date_award], [act_name], [act_num], [act_date], [comment] from [employees], [rewards], [awardemps], [localact], [reward_types]" +
                "where [employees].[id] = [awardemps].[emp_id] and [rewards].[id]=[awardemps].[reward_id] and [reward_types].[id]=[rewards].[id_type] and [localact].[id] = [awardemps].[act_id] and [awardemps].[id] = " + Data.awardEmpId+"", con);
                odb.OleDbDataReader reader = comanda2.ExecuteReader();
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
                        button2.Enabled = false;
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
            //con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //con.Open();
            comboBox4.Items.Clear();
            comboBox2.Items.Clear();
            var comanda = new odb.OleDbCommand("Select id, reward_name From Rewards where id_type = " + comboBox3.Items[comboBox1.SelectedIndex].ToString() + "", con);
            odb.OleDbDataReader выполнение = comanda.ExecuteReader();
            while (выполнение.Read() == true)
            {
                comboBox4.Items.Add(выполнение.GetValue(0));
                comboBox2.Items.Add(выполнение.GetValue(1));
            }
            //con.Close();
                
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                dateTimePicker2.Enabled = true;
                comboBox5.Enabled = true;
                button2.Enabled = true;
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
                button2.Enabled = false;
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
