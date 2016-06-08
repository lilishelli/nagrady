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

        void regular()
        {
            //String[] test;
            //test = textBox1.Lines;
            //String pattern = @"\b[А-Я]{1}[а-я]+\b";
            //Regex regex = new Regex(pattern);
            //String pattern1 = @"\W";
            //Regex regex1 = new Regex(pattern1);

            //foreach (String str in test)
            //{
            //    if (regex.IsMatch(str) && regex1.IsMatch(str) == false)
            //        richTextBox5.Text += String.Format("\r\nФамилия: \"{0}\" введена правильно", str);
            //    else
            //        richTextBox5.Text += String.Format("\r\nФамилия: \"{0}\" введена не правильно", str);

            //}
        }




            public void addEmp(string lname, string fname, string patre, string org, string pos, string gender, string birth, 
            string dbegin_org, string dbegin_industry, string dbegin_general)
        {
            try
            {
                Database.execute("INSERT INTO Employees (lname, fname, patre, org, pos, gender, birth, dbegin_org, dbegin_industry, dbegin_general) VALUES ('" + lname + "', '" + fname + "', '" + patre
                    + "', " + org + ", " + pos + ", '" + gender + "', " + birth + ", " + dbegin_org + ", " + dbegin_industry + ", " + dbegin_general + ")");
                
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
                Database.execute("Update Employees SET lname = '" + lname + "', fname = '" + fname + "', patre = '" + patre + "',  org = " + org + ",gender = '" + gender +
                    "', birth = " + birth + ", dbegin_org = " + dbegin_org + ", dbegin_industry = " + dbegin_industry + ", dbegin_general = " + dbegin_general + ", pos = " + pos + " WHERE (id = " + id + ")");            
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
                        editEmp(textBox1.Text, textBox2.Text, textBox3.Text, comboBox3.Items[comboBox2.SelectedIndex].ToString(), comboBox5.Items[comboBox4.SelectedIndex].ToString(), comboBox1.Items[comboBox1.SelectedIndex].ToString(), birth,
                          dbegin_org, dbegin_industry, dbegin_general, Data.empId);
                    }
                    else
                    {
                        addEmp(textBox1.Text, textBox2.Text, textBox3.Text, comboBox3.Items[comboBox2.SelectedIndex].ToString(), comboBox5.Items[comboBox4.SelectedIndex].ToString(), comboBox1.Items[comboBox1.SelectedIndex].ToString(), birth,
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
            comboBox2.Items.Clear();
            comboBox4.Items.Clear();
            comboBox3.Items.Clear();
            comboBox5.Items.Clear();
            comboBox1.Items.Clear();
            comboBox1.Items.Add("Мужской");
            comboBox1.Items.Add("Женский");
            var v = Database.getReader("Select * From organisations");
            //загружаем организации
            while (v.Read() == true)
            {
                comboBox3.Items.Add(v.GetValue(0));
                comboBox2.Items.Add(v.GetValue(1));
            }
            v.Close();
            //загружаем должности
            v = Database.getReader("Select * From positions");
            while (v.Read() == true)
            {
                comboBox5.Items.Add(v.GetValue(0));
                comboBox4.Items.Add(v.GetValue(1));
            }
            v.Close();
            if (Data.isAddBtn == false)
            {
                var reader = Database.getReader("select [id], [lname], [fname], [patre], [org], [gender], [birth],  fix((date()-[dbegin_org])/365.25), fix((date()-[dbegin_industry])/365.25), fix((date()-[dbegin_general])/365.25),  [pos] from employees where id = "+Data.empId+"");
                while (reader.Read())
                {
                    textBox1.Text = reader.GetValue(1).ToString(); //фамилия
                    textBox2.Text = reader.GetValue(2).ToString();// имя
                    textBox3.Text = reader.GetValue(3).ToString();//отчество
                    comboBox2.SelectedIndex = comboBox3.Items.IndexOf(reader.GetValue(4));
                    comboBox1.SelectedIndex = comboBox1.Items.IndexOf(reader.GetValue(5).ToString());
                    dateTimePicker1.Value = DateTime.Parse(reader.GetValue(6).ToString()); 
                    comboBox4.SelectedIndex = comboBox5.Items.IndexOf(reader.GetValue(10));
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

        private void NotifyAboutClosedChildForm(object sender, FormClosedEventArgs args)
        {            
            loadData();
            comboBox1.SelectedIndex = comboBox1.Items.IndexOf(comboBox1.Text);
            comboBox4.SelectedIndex = comboBox4.Items.IndexOf(comboBox4.Text);
            comboBox2.SelectedIndex = comboBox2.Items.IndexOf(comboBox2.Text);
        }

        private void addorg_Click(object sender, EventArgs e)
        {
            Data.isAddOrgBtn = true;
            EditOrg f = new EditOrg();            
            f.Owner = this;
            f.FormClosed += new FormClosedEventHandler(NotifyAboutClosedChildForm);
            f.Show();
        }

        private void addpos_Click(object sender, EventArgs e)
        {
            Data.isAddPosBtn = true;
            EditPos f = new EditPos();
            f.Owner = this;
            f.FormClosed += new FormClosedEventHandler(NotifyAboutClosedChildForm);
            f.Show();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
