using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        void save()
        {
            con.Open();
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
            con.Close();            
        }
        private void button1_Click(object sender, EventArgs e)
        {
            save();
        }

        private void AddAwardEmp_Load(object sender, EventArgs e)
        {
            dateTimePicker2.Enabled = false;
            comboBox5.Enabled = false;
            dateTimePicker3.Enabled = false;
            textBox1.Enabled = false;
            con.Open();
            var comanda = new odb.OleDbCommand("Select lname, fname, patre From employees where id = "+Data.empId+"", con);
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
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            con.Open();
            comboBox4.Items.Clear();
            comboBox2.Items.Clear();
            var comanda = new odb.OleDbCommand("Select id, reward_name From Rewards where id_type = " + comboBox3.Items[comboBox1.SelectedIndex].ToString() + "", con);
            odb.OleDbDataReader выполнение = comanda.ExecuteReader();
            while (выполнение.Read() == true)
            {
                comboBox4.Items.Add(выполнение.GetValue(0));
                comboBox2.Items.Add(выполнение.GetValue(1));
            }
            con.Close();
                
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
