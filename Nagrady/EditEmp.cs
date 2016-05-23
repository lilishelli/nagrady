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
        static string s;
        public EditEmp()
        {
            InitializeComponent();            
        }
        public ОДБ.OleDbConnection con = new ОДБ.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source= rewards.mdb");
        ОДБ.OleDbCommand command = new ОДБ.OleDbCommand();
        ОДБ.OleDbDataAdapter Adapter;
        public void addEmp(string lname, string fname, string patre, string org, string pos, string gender, DateTime birth, 
            DateTime dbegin_org, DateTime dbegin_industry, DateTime dbegin_general)
        {
            try
            {
                var command = new ОДБ.OleDbCommand("INSERT INTO Employees (lname, fname, patre, org, pos, gender, birth, dbegin_org, dbegin_industry, dbegin_general) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?)");
                command.Parameters.Add("lname", ОДБ.OleDbType.VarWChar, 50).Value = lname;
                command.Parameters.Add("fname", ОДБ.OleDbType.VarWChar, 50).Value = fname;
                command.Parameters.Add("patre", ОДБ.OleDbType.VarWChar, 50).Value = patre;
                command.Parameters.Add("org", ОДБ.OleDbType.VarWChar, 50).Value = org;
                command.Parameters.Add("pos", ОДБ.OleDbType.VarWChar, 50).Value = pos;
                command.Parameters.Add("gender", ОДБ.OleDbType.VarWChar, 50).Value = gender;
                command.Parameters.Add("birth", ОДБ.OleDbType.Date, 10).Value = birth;
                command.Parameters.Add("dbegin_org", ОДБ.OleDbType.Date, 50).Value = dbegin_org;
                command.Parameters.Add("dbegin_industry", ОДБ.OleDbType.Date, 50).Value = dbegin_industry;
                command.Parameters.Add("dbegin_general", ОДБ.OleDbType.Date, 50).Value = dbegin_general;
                command.Connection = con;
                command.ExecuteNonQuery();
                MessageBox.Show("В таблицу добавлена запись");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка ввода данных");
            }
        }
        public void editEmp(string lname, string fname, string patre, string org, string pos, string gender, DateTime birth,
            DateTime dbegin_org, DateTime dbegin_industry, DateTime dbegin_general, int id)
        {
            try
            {
                var command = new ОДБ.OleDbCommand("Update Employees SET lname = ?, fname = ?, patre = ?,  org = ?,gender = ?, birth = ?, dbegin_org = ?, dbegin_industry = ?, dbegin_general = ?, pos = ? WHERE (id = ?)");
                command.Parameters.Add("lname", ОДБ.OleDbType.VarWChar, 50).Value = lname;
                command.Parameters.Add("fname", ОДБ.OleDbType.VarWChar, 50).Value = fname;
                command.Parameters.Add("patre", ОДБ.OleDbType.VarWChar, 50).Value = patre;
                command.Parameters.Add("org", ОДБ.OleDbType.VarWChar, 50).Value = org;
                command.Parameters.Add("gender", ОДБ.OleDbType.VarWChar, 50).Value = gender;
                command.Parameters.Add("birth", ОДБ.OleDbType.Date, 10).Value = birth;
                command.Parameters.Add("dbegin_org", ОДБ.OleDbType.Date, 50).Value = dbegin_org;
                command.Parameters.Add("dbegin_industry", ОДБ.OleDbType.Date, 50).Value = dbegin_industry;
                command.Parameters.Add("dbegin_general", ОДБ.OleDbType.Date, 50).Value = dbegin_general;
                command.Parameters.Add("pos", ОДБ.OleDbType.VarWChar, 50).Value = pos;
                command.Parameters.Add("id", ОДБ.OleDbType.Integer, 30).Value = id;
                Adapter = new ОДБ.OleDbDataAdapter(command);
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
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                if (!Data.isAddBtn)
                {
                    editEmp(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, comboBox1.Items[comboBox1.SelectedIndex].ToString(), dateTimePicker1.Value,
                        DateTime.Now.AddYears((-1) * Int32.Parse(textBox6.Text)).Date, DateTime.Now.AddYears((-1) * Int32.Parse(textBox7.Text)).Date,
                        DateTime.Now.AddYears((-1) * Int32.Parse(textBox8.Text)).Date, Data.empId);
                }
                else
                {
                    addEmp(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, comboBox1.Items[comboBox1.SelectedIndex].ToString(), dateTimePicker1.Value,
                        DateTime.Now.AddYears((-1) * Int32.Parse(textBox6.Text)).Date, DateTime.Now.AddYears((-1) * Int32.Parse(textBox7.Text)).Date,
                        DateTime.Now.AddYears((-1) * Int32.Parse(textBox8.Text)).Date);
                }
                con.Close();                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка ввода данных");
                con.Close();  
            }
        }
        public void loadData()
        {
            con.Open();
            comboBox1.Items.Add("Мужской");
            comboBox1.Items.Add("Женский");

            if (Data.isAddBtn == false)
            {
                var comanda = new ОДБ.OleDbCommand("select [id], [lname], [fname], [patre], [org], [gender], [birth],  fix((date()-[dbegin_org])/365.25), fix((date()-[dbegin_industry])/365.25), fix((date()-[dbegin_general])/365.25),  [pos] from employees where id = ?", con);
                comanda.Parameters.Add("id", ОДБ.OleDbType.Integer, 1000).Value = Data.empId;
                ОДБ.OleDbDataReader reader = comanda.ExecuteReader();
                while (reader.Read())
                {
                    textBox1.Text = reader.GetValue(1).ToString(); //фамилия
                    textBox2.Text = reader.GetValue(2).ToString();// имя
                    textBox3.Text = reader.GetValue(3).ToString();//отчество
                    textBox4.Text = reader.GetValue(4).ToString();  //место работы
                    comboBox1.SelectedIndex = comboBox1.Items.IndexOf(reader.GetValue(5).ToString());
                    dateTimePicker1.Value = DateTime.Parse(reader.GetValue(6).ToString());
                    textBox5.Text = reader.GetValue(10).ToString();    // должность
                    textBox6.Text = reader.GetValue(7).ToString();
                    
                    textBox7.Text = reader.GetValue(8).ToString();

                    textBox8.Text = reader.GetValue(9).ToString();

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
