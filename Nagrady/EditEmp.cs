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
          //try {
                con.Open();
                if (!Data.isAddBtn)
                {
                    command.CommandText = "Update Employees SET lname = ?, fname = ?, patre = ?, gender = ?, birth = ?,org = ?, position = ?,  dbegin_org = ?, dbegin_industry = ?, dbegin_grneral = ? WHERE (id = ?)";
                }
                else
                {
                    command.CommandText = "INSERT INTO (lname,fname,patre,org,position,gender,birth,dbegin_org,dbegin_industry,dbegin_grneral) values(?,?,?,?,?,?,?,?,?,?)";
                }
                command.Parameters.Add("lname", ОДБ.OleDbType.VarWChar, 50).Value = textBox1.Text;
                command.Parameters.Add("fname", ОДБ.OleDbType.VarWChar, 50).Value = textBox2.Text;
                command.Parameters.Add("patre", ОДБ.OleDbType.VarWChar, 50).Value = textBox3.Text;
                command.Parameters.Add("gender", ОДБ.OleDbType.VarWChar, 50).Value = comboBox1.Items[comboBox1.SelectedIndex].ToString();
                command.Parameters.Add("birth", ОДБ.OleDbType.Date, 10).Value = dateTimePicker1.Value;
                command.Parameters.Add("org", ОДБ.OleDbType.VarWChar, 50).Value = textBox4.Text;
                command.Parameters.Add("position", ОДБ.OleDbType.VarWChar, 50).Value = textBox5.Text;
                command.Parameters.Add("dbegin_org", ОДБ.OleDbType.Date, 50).Value = DateTime.Now.AddYears((-1) * Int32.Parse(textBox6.Text)).Date;
                command.Parameters.Add("dbegin_industry", ОДБ.OleDbType.Date , 50).Value = DateTime.Now.AddYears((-1) * Int32.Parse(textBox7.Text)).Date;
                command.Parameters.Add("dbegin_general", ОДБ.OleDbType.Date, 50).Value = DateTime.Now.AddYears((-1) * Int32.Parse(textBox8.Text)).Date;
                command.Parameters.Add("id", ОДБ.OleDbType.Integer, 30).Value = Data.empId;
                Adapter = new ОДБ.OleDbDataAdapter(command);
                Adapter.UpdateCommand = command;
                command.Connection = con;
                if (Data.isAddBtn)
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("В таблицу добавлена запись");
                }
                con.Close();
            /*}
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "недоразумение");
            }*/

        }
        public void loadData()
        {
            con.Open();
            comboBox1.Items.Add("Мужской");
            comboBox1.Items.Add("Женский");
           
            if (Data.isAddBtn == false)
            {
                var comanda = new ОДБ.OleDbCommand("Select * From Employees where id = ?", con);
                comanda.Parameters.Add("id", ОДБ.OleDbType.Integer, 30).Value = Data.empId;
                ОДБ.OleDbDataReader reader = comanda.ExecuteReader();
                while (reader.Read())
                {                    
                    textBox1.Text = reader.GetValue(1).ToString();
                    textBox2.Text = reader.GetValue(2).ToString();
                    textBox3.Text = reader.GetValue(3).ToString();
                    comboBox1.SelectedItem = reader.GetValue(6);
                    comboBox1.SelectedText = reader.GetValue(6).ToString(); 
                    dateTimePicker1.Value = DateTime.Parse(reader.GetValue(7).ToString());
                    textBox4.Text = reader.GetValue(4).ToString();
                    textBox5.Text = reader.GetValue(5).ToString();
                    textBox6.Text = reader.GetValue(8).ToString();
                    textBox7.Text = reader.GetValue(9).ToString();
                    textBox8.Text = reader.GetValue(10).ToString();                    
                }
                reader.Close();
                
            }
            con.Close();
        }
        private void EditEmp_Load(object sender, EventArgs e)
        {
            loadData();
        }
    }
}
