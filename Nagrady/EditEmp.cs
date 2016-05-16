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
    public partial class EditEmp : Form
    {
        public EditEmp()
        {
            InitializeComponent();
        }
        ОДБ.OleDbConnection con = new ОДБ.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source= rewards.mdb");
        ОДБ.OleDbCommand ucommand = new ОДБ.OleDbCommand();
        private void button1_Click(object sender, EventArgs e)
        {
            
        }
        public void loadData()
        {
            if (Data.isAddBtn == false)
            {
                con.Open();
                var comanda = new ОДБ.OleDbCommand("Select * From Employees where id = ?", con);
                comanda.Parameters.Add("id", ОДБ.OleDbType.Integer, 30).Value = Data.empId;
                ОДБ.OleDbDataReader reader = comanda.ExecuteReader();
                textBox1.Text = reader.GetValue(1).ToString();
                textBox2.Text = reader.GetValue(2).ToString();
                textBox3.Text = reader.GetValue(3).ToString();
                dateTimePicker1.Value = DateTime.Parse(reader.GetValue(7).ToString());
                comboBox1.SelectedIndex = Int16.Parse(reader.GetValue(6).ToString()) - 1;
                textBox4.Text = reader.GetValue(4).ToString();
                textBox5.Text = reader.GetValue(5).ToString();
                textBox6.Text = reader.GetValue(8).ToString();
                textBox7.Text = reader.GetValue(9).ToString();
                textBox8.Text = reader.GetValue(10).ToString();
                reader.Close();
                con.Close();
            }
        }
        private void EditEmp_Load(object sender, EventArgs e)
        {
            loadData();
        }
    }
}
