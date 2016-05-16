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
        DataSet rewards;
        ОДБ.OleDbConnection con = new ОДБ.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source= rewards.mdb");
        ОДБ.OleDbCommand ucommand = new ОДБ.OleDbCommand();
        ОДБ.OleDbDataAdapter Adapter;
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
            ucommand.CommandText = "Update Rewards SET reward_name = ?, rewards.id_type = ?";
            ucommand.Parameters.Add("reward_name", ОДБ.OleDbType.VarWChar, 50, "reward_name");
            //  ucommand.Parameters.Add(new ОДБ.OleDbParameter("id_type", ОДБ.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, (byte)0, (byte)0, "id", System.Data.DataRowVersion.Original, null));
            ucommand.Parameters.Add(new ОДБ.OleDbParameter("id_type", ОДБ.OleDbType.Integer, 10)).Value = comboBox1.SelectedIndex;
            Adapter.UpdateCommand = ucommand;
            ucommand.Connection = con;
            try
            {
                int kol = Adapter.Update(rewards, "Rewards");
                MessageBox.Show("Обновлено " + kol + " записей");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            con = new ОДБ.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0; Data Source = rewards.mdb");
            con.Open();
            BindingSource bs1 = new BindingSource();
            var comanda = new ОДБ.OleDbCommand("Select * from Rewards where Rewards.id_type=?", con);
            comanda.Parameters.Add("Reward_types", ОДБ.OleDbType.Integer, 30).Value = comboBox2.Items[comboBox1.SelectedIndex].ToString();

            ОДБ.OleDbDataReader выполнение = comanda.ExecuteReader();
            DataTable mytable = new DataTable();
            bs1.DataSource = mytable;
            mytable.Columns.Add(выполнение.GetName(1));

            while (выполнение.Read() == true)
                mytable.Rows.Add(new object[] { выполнение.GetValue(1) });
            выполнение.Close();
            con.Close();
            dataGridView1.DataSource = mytable;
            dataGridView1.Columns[0].HeaderCell.Value = "Вид награды";
            dataGridView1.Columns[0].Width = 620;
            bindingNavigator1.BindingSource = bs1;
            dataGridView1.DataSource = bs1;

        }

        private void button2_Click(object sender, EventArgs e)
        {
           
            String t = (String)dataGridView1.CurrentRow.Cells[0].Value;
            con.Open();

            var comanda = new ОДБ.OleDbCommand("Delete * From Rewards where Rewards.reward_name = ?", con);
            comanda.Parameters.Add("reward_name", ОДБ.OleDbType.VarChar, 50).Value = t;
            try
            {
                int kol = comanda.ExecuteNonQuery();
                MessageBox.Show("Обновлено " + kol + " записей");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
            con.Close();
            
           
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
            dataGridView1.Columns[0].Width = 620;
        }
    }
}
