using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ОДБ = System.Data.OleDb;

namespace Nagrady
{
    public partial class Rewards : Form
    {
        public Rewards()
        {
            InitializeComponent();
        }
       ОДБ.OleDbConnection con = new ОДБ.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source= rewards.mdb");
        ОДБ.OleDbCommand ucommand = new ОДБ.OleDbCommand();
     private void Form2_Load(object sender, EventArgs e)
        {
            con = new ОДБ.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0; Data Source = rewards.mdb");
            con.Open();
            var comanda = new ОДБ.OleDbCommand("Select * From Reward_types", con);
            ОДБ.OleDbDataReader выполнение = comanda.ExecuteReader();
            while (выполнение.Read() == true)
            {
                comboBox1.Items.Add(выполнение.GetValue(1));
                comboBox2.Items.Add(выполнение.GetValue(0));
            }
            
            выполнение.Close();
            con.Close();

        }

      

        private void button1_Click(object sender, EventArgs e)
        {
            //добавление
            try
            {
                con.Open();
            var comanda = new ОДБ.OleDbCommand("INSERT INTO Rewards (reward_name, id_type) VALUES (?, ?)");
            comanda.Parameters.Add("reward_name", ОДБ.OleDbType.VarWChar, 300).Value = textBox1.Text.ToString();
            comanda.Parameters.Add("id_type", ОДБ.OleDbType.Integer, 30).Value = comboBox2.Items[comboBox1.SelectedIndex].ToString();
            comanda.Connection = con;
            comanda.ExecuteNonQuery();
            MessageBox.Show("В таблицу добавлена запись");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }

            //обновление
            var comand = new ОДБ.OleDbCommand("Select * from Rewards where Rewards.id_type=?", con);
            comand.Parameters.Add("Reward_types", ОДБ.OleDbType.Integer, 30).Value = comboBox2.Items[comboBox1.SelectedIndex].ToString();

            ОДБ.OleDbDataReader выполнение = comand.ExecuteReader();
            DataTable mytable = new DataTable();
            mytable.Columns.Add(выполнение.GetName(1));

            while (выполнение.Read() == true)
                mytable.Rows.Add(new object[] { выполнение.GetValue(1) });
            выполнение.Close();
            con.Close();
            dataGridView1.DataSource = mytable;
            dataGridView1.Columns[0].HeaderCell.Value = "Вид награды";
            dataGridView1.Columns[0].Width = 700;

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            con = new ОДБ.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0; Data Source = rewards.mdb");
            con.Open();
           
            var comanda = new ОДБ.OleDbCommand("Select * from Rewards where Rewards.id_type=?", con);
            comanda.Parameters.Add("Reward_types", ОДБ.OleDbType.Integer, 30).Value = comboBox2.Items[comboBox1.SelectedIndex].ToString();

            ОДБ.OleDbDataReader выполнение = comanda.ExecuteReader();
            DataTable mytable = new DataTable();
         
            mytable.Columns.Add(выполнение.GetName(1));
           
            while (выполнение.Read() == true)
                mytable.Rows.Add(new object[] { выполнение.GetValue(1)});
            выполнение.Close();
            con.Close();
            dataGridView1.DataSource = mytable;
            dataGridView1.Columns[0].HeaderCell.Value = "Вид награды";
            dataGridView1.Columns[0].Width = 700;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
               
                dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
           //удаление
            String t = (String)dataGridView1.CurrentRow.Cells[0].Value;
            con.Open();

            var comanda = new ОДБ.OleDbCommand("Delete * From Rewards where Rewards.reward_name = ?", con);
            comanda.Parameters.Add("reward_name", ОДБ.OleDbType.VarChar, 50).Value = t;
            try
            {
                int kol = comanda.ExecuteNonQuery();
               
          
            
            //обновление
            var comand = new ОДБ.OleDbCommand("Select * from Rewards where Rewards.id_type=?", con);
            comand.Parameters.Add("Reward_types", ОДБ.OleDbType.Integer, 30).Value = comboBox2.Items[comboBox1.SelectedIndex].ToString();

            ОДБ.OleDbDataReader выполнение = comand.ExecuteReader();
            DataTable mytable = new DataTable();
            mytable.Columns.Add(выполнение.GetName(1));

            while (выполнение.Read() == true)
                mytable.Rows.Add(new object[] { выполнение.GetValue(1) });
            выполнение.Close();
            con.Close();
            dataGridView1.DataSource = mytable;
            dataGridView1.Columns[0].HeaderCell.Value = "Вид награды";
            dataGridView1.Columns[0].Width = 700;
            MessageBox.Show("Обновлено " + kol + " записей");
        }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            con = new ОДБ.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0; Data Source = rewards.mdb");
            con.Open();

            var comanda = new ОДБ.OleDbCommand("Select * from Rewards where Rewards.id_type=?", con);
            comanda.Parameters.Add("Reward_types", ОДБ.OleDbType.Integer, 30).Value = comboBox2.Items[comboBox1.SelectedIndex].ToString();

            ОДБ.OleDbDataReader выполнение = comanda.ExecuteReader();
            DataTable mytable = new DataTable();
            mytable.Columns.Add(выполнение.GetName(1));

            while (выполнение.Read() == true)
                mytable.Rows.Add(new object[] { выполнение.GetValue(1) });
            выполнение.Close();
            con.Close();
            dataGridView1.DataSource = mytable;
            dataGridView1.Columns[0].HeaderCell.Value = "Вид награды";
            dataGridView1.Columns[0].Width = 700;
        }

        

        private void checkBox1_CheckStateChanged(object sender, EventArgs e)
        {
           

            if (checkBox1.Checked == true && comboBox1.SelectedIndex != -1)
            {
                
                button1.Enabled = true;
                button2.Enabled = true;
                textBox1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
                button2.Enabled = false;
                textBox1.Enabled = false;
            }
        }

       
    }
}
