using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Nagrady
{
    public partial class EditEmp : Form
    {
        static string s;
        public EditEmp()
        {
            InitializeComponent();            
        }
            public void addEmp(string lname, string fname, string patre, string org, string pos, string gender, string birth, 
            string dbegin_org, string dbegin_industry, string dbegin_general)
        {
            try
            {
                Database.execute("INSERT INTO Employees (lname, fname, patre, org, pos, gender, birth, dbegin_org, dbegin_industry) VALUES ('" + lname + "', '" + fname + "', '" + patre
                    + "', '" + org + "', '" + pos + "', '" + gender + "', '" + birth + "', '" + dbegin_org + "', '" + dbegin_industry + "')");
                
                MessageBox.Show("В таблицу добавлена запись");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка ввода данных");
            }
        }
        public void editEmp(string lname, string fname, string patre, string org, string pos, string gender, string birth,
            string dbegin_org, string dbegin_industry, string dbegin_general, int id)
        {
            try
            {
                Database.execute("Update Employees SET lname = '" + lname + "', fname = '" + fname + "', patre = '" + patre + "',  org = '" + org + "',gender = '" + gender +
                    "', birth = " + birth + ", dbegin_org = " + dbegin_org + ", dbegin_industry = " + dbegin_industry + ", dbegin_general = " + dbegin_general + ", pos = '" + pos + "' WHERE (id = " + id + ")");            
                MessageBox.Show("Запись обновлена");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка ввода данных");
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string birth="null";
            string dbegin_org="null";
            string dbegin_industry="null";
            string dbegin_general="null";
            DialogResult result = DialogResult.Yes;
            try
            {
                birth = "'"+DateTime.Parse(dateTimePicker1.Value.Date.ToString()).ToString("dd.MM.yyyy")+"'";
            }
            catch
            {
                birth = "null";
                result = MessageBox.Show("Не запонено поле Дата рождения! Продолжить?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            }
            if (result == DialogResult.Yes)
            try
            {
                dbegin_org = "'"+DateTime.Parse(DateTime.Now.AddYears((-1) * Int32.Parse(textBox6.Text)).Date.ToString()).ToString("dd.MM.yyyy")+"'";
            }
            catch
            {
                dbegin_org = "null";
                result = MessageBox.Show("Не запонено поле Стаж работы в организации! Продолжить?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            }
            if (result == DialogResult.Yes)
            try
            {
                dbegin_industry = "'"+DateTime.Parse(DateTime.Now.AddYears((-1) * Int32.Parse(textBox7.Text)).Date.ToString()).ToString("dd.MM.yyyy")+"'";
            }
            catch
            {
                dbegin_industry = "null";
                result = MessageBox.Show("Не запонено поле Стаж работы в отрасли! Продолжить?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            }
            if (result == DialogResult.Yes)
            try
            {
                dbegin_general = "'"+DateTime.Parse(DateTime.Now.AddYears((-1) * Int32.Parse(textBox8.Text)).Date.ToString()).ToString("dd.MM.yyyy")+"'";
            }
            catch
            {
                dbegin_general = "null";
                result = MessageBox.Show("Не запонено поле Общий стаж! Продолжить?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            }
            if (result == DialogResult.Yes)
            {
                try
                {
                    if (!Data.isAddBtn)
                    {
                        editEmp(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, comboBox1.Items[comboBox1.SelectedIndex].ToString(), birth,
                          dbegin_org, dbegin_industry, dbegin_general, Data.empId);
                    }
                    else
                    {
                        addEmp(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, comboBox1.Items[comboBox1.SelectedIndex].ToString(), birth,
                            dbegin_org,dbegin_industry, dbegin_general);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка ввода данных");
                }
            }
        }
        public void loadData()
        {
            comboBox1.Items.Add("Мужской");
            comboBox1.Items.Add("Женский");

            if (Data.isAddBtn == false)
            {
                var reader = Database.getReader("select [id], [lname], [fname], [patre], [org], [gender], [birth],  fix((date()-[dbegin_org])/365.25), fix((date()-[dbegin_industry])/365.25), fix((date()-[dbegin_general])/365.25),  [pos] from employees where id = "+Data.empId+"");
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
        }
        private void EditEmp_Load(object sender, EventArgs e)
        {
            loadData();
        }
    }
}
