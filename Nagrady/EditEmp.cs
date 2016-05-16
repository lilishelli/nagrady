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
        ОДБ.OleDbCommand command = new ОДБ.OleDbCommand();
        ОДБ.OleDbDataAdapter Adapter;
        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();
            if (!Data.isAddBtn)
            {
                command.CommandText = "Update Employees SET lname = ?, fname = ?, patre = ?, org = ?, position = ?, gender = ?, birth = ?, dbegin_org = ?, dbegin_industry = ?, dbegin_grneral = ? WHERE (id = ?)";

            }
            else
            {
                command.CommandText = "INSERT INTO (lname,fname,patre,org,position,gender,birth,dbegin_org,dbegin_industry,dbegin_grneral) values(?,?,?,?,?,?,?,?,?,?)";
            }
            command.Parameters.Add("lname", ОДБ.OleDbType.VarWChar, 50).Value = textBox1.Text;
            command.Parameters.Add("fname", ОДБ.OleDbType.VarWChar, 50).Value = textBox2.Text;
            command.Parameters.Add("patre", ОДБ.OleDbType.VarWChar, 50).Value = textBox3.Text;
            command.Parameters.Add("gender", ОДБ.OleDbType.VarWChar, 50).Value = comboBox2.Items[comboBox1.SelectedIndex].ToString();
            command.Parameters.Add("birth", ОДБ.OleDbType.Date, 10).Value = dateTimePicker1.Value;
            command.Parameters.Add("org", ОДБ.OleDbType.VarWChar, 50).Value = textBox4.Text;
            command.Parameters.Add("position", ОДБ.OleDbType.VarWChar, 50).Value = textBox5.Text;
            command.Parameters.Add("dbegin_org", ОДБ.OleDbType.VarWChar, 50).Value = DateTime.Now.Subtract(new TimeSpan(360, 0, 0));
            command.Parameters.Add("dbegin_industry", ОДБ.OleDbType.VarWChar, 50);
            command.Parameters.Add("dbegin_general", ОДБ.OleDbType.VarWChar, 50);
            command.Parameters.Add(new ОДБ.OleDbParameter("Original_id", ОДБ.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, (byte)0, (byte)0, "id", System.Data.DataRowVersion.Original, null));
            Adapter.UpdateCommand = command;
            command.Connection = con;
            con.Close();
        }
        public void loadData()
        {
            con.Open();
            var cmd = new ОДБ.OleDbCommand("Select * From gender", con);
            ОДБ.OleDbDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read() == true)
            {
                comboBox2.Items.Add(rdr.GetValue(0));
                comboBox1.Items.Add(rdr.GetValue(1));
            }
            rdr.Close();
            con.Close();
            if (Data.isAddBtn == false)
            {
                MessageBox.Show(Data.empId+"");
                con.Open();
                var comanda = new ОДБ.OleDbCommand("Select * From Employees where id = ?", con);
                comanda.Parameters.Add("id", ОДБ.OleDbType.Integer, 30).Value = Data.empId;
                ОДБ.OleDbDataReader reader = comanda.ExecuteReader();
                while (reader.Read())
                {                    
                    textBox1.Text = reader.GetValue(1).ToString();
                    textBox2.Text = reader.GetValue(2).ToString();
                    textBox3.Text = reader.GetValue(3).ToString();
                    comboBox1.SelectedItem = Int16.Parse(reader.GetValue(6).ToString())-1;
                    dateTimePicker1.Value = DateTime.Parse(reader.GetValue(7).ToString());
                    textBox4.Text = reader.GetValue(4).ToString();
                    textBox5.Text = reader.GetValue(5).ToString();
                    textBox6.Text = reader.GetValue(8).ToString();
                    textBox7.Text = reader.GetValue(9).ToString();
                    textBox8.Text = reader.GetValue(10).ToString();                    
                }
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
