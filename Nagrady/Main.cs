﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Linq;
using ОДБ = System.Data.OleDb;
using Ворд = Microsoft.Office.Interop;

namespace Nagrady
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }
        int insert = 0;
        // DataSet rewards;
       // ОДБ.OleDbDataAdapter Adapter;
        void loademp()
        {
            ОДБ.OleDbDataReader выполнение = Database.getReader("select [id], [lname], [fname], [patre], [org], [pos], [gender], [birth], fix((date()-[dbegin_org])/365.25), fix((date()-[dbegin_industry])/365.25), fix((date()-[dbegin_general])/365.25) from employees");
            DataTable mytable = new DataTable();
            mytable.Columns.Add(выполнение.GetName(0));
            mytable.Columns.Add(выполнение.GetName(1));
            mytable.Columns.Add(выполнение.GetName(2));
            mytable.Columns.Add(выполнение.GetName(3));
            mytable.Columns.Add(выполнение.GetName(4));
            mytable.Columns.Add(выполнение.GetName(5));
            mytable.Columns.Add(выполнение.GetName(6));
            mytable.Columns.Add(выполнение.GetName(7));
            mytable.Columns.Add(выполнение.GetName(8));
            mytable.Columns.Add(выполнение.GetName(9));
            mytable.Columns.Add(выполнение.GetName(10));
            while (выполнение.Read() == true)
                mytable.Rows.Add(new object[] { выполнение.GetValue(0), выполнение.GetValue(1), выполнение.GetValue(2), выполнение.GetValue(3), выполнение.GetValue(4), выполнение.GetValue(5),
                    выполнение.GetValue(6), DateTime.Parse(выполнение.GetValue(7).ToString()).Date.ToString("dd.MM.yyyy"), выполнение.GetValue(8), выполнение.GetValue(9), выполнение.GetValue(10) });
            выполнение.Close();
            dataGridView1.DataSource = mytable;
            dataGridView1.Columns[0].HeaderCell.Value = "ID";
            dataGridView1.Columns[0].Width = 25;
            dataGridView1.Columns[1].HeaderCell.Value = "Фамилия";
            dataGridView1.Columns[1].Width = 100;
            dataGridView1.Columns[2].HeaderCell.Value = "Имя";
            dataGridView1.Columns[2].Width = 100;
            dataGridView1.Columns[3].HeaderCell.Value = "Отчество";
            dataGridView1.Columns[3].Width = 100;
            dataGridView1.Columns[4].HeaderCell.Value = "Место работы";
            dataGridView1.Columns[4].Width = 200;
            dataGridView1.Columns[5].HeaderCell.Value = "Должность";
            dataGridView1.Columns[5].Width = 200;
            dataGridView1.Columns[6].HeaderCell.Value = "Пол";
            dataGridView1.Columns[6].Width = 100;
            dataGridView1.Columns[7].HeaderCell.Value = "Дата рождения";
            dataGridView1.Columns[7].Width = 100;
            dataGridView1.Columns[8].HeaderCell.Value = "Стаж работы в организации";
            dataGridView1.Columns[8].Width = 100;
            dataGridView1.Columns[9].HeaderCell.Value = "Стаж работы в отрасли";
            dataGridView1.Columns[9].Width = 100;
            dataGridView1.Columns[10].HeaderCell.Value = "Общий стаж";
            dataGridView1.Columns[10].Width = 100;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            loademp();
            DateTime d = DateTime.Now.Date;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Data.isAddBtn = true;
            MyCon.connect = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source = rewards.mdb";
            EditEmp f = new EditEmp();
            f.Show();
            insert = 2;

        }

        private void проверитьПодключениеСбазойДанныхToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if(Database.connect())
                MessageBox.Show("Подключение выполнено");
                else MessageBox.Show("Ошибка подключения");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void списокНаградToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Rewards f = new Rewards();
            f.Show();
        }

        private void editbtn_Click(object sender, EventArgs e)
        {
            Data.isAddBtn = false;
            Data.empId = Int16.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            EditEmp f = new EditEmp();
            f.Show();
            insert = 1;
        }

        private void addRewardToEmpbtn_Click(object sender, EventArgs e)
        {
            Data.isAddAwardBtn = true;
            Data.empId = Int16.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            AddAwardEmp f = new AddAwardEmp();
            f.Show();
        }

        private void составитьОтчётToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Otchet f = new Otchet();
            f.Show();
        }

        private void авторыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Author f = new Author();
            f.Show();
        }

        private void списокЛюдейПредставленныхКНаградамToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AwardEmp f = new AwardEmp();
            f.Show();
            /*label1.Visible = true;
            textBox2.Visible = true;
            search.Visible = true;
            dataGridView1.Visible = true;
            editbtn.Visible = true;
            addRewardToEmpbtn.Visible = true;
            button1.Visible = true;
            checkBox1.Visible = true;
            button2.Visible = true;*/
        }

        private void checkBox1_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {

                button1.Enabled = true;
                editbtn.Enabled = true;
                addRewardToEmpbtn.Enabled = true;
                button2.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
                editbtn.Enabled = false;
                addRewardToEmpbtn.Enabled = false;
                button2.Enabled = false;
            }
        }

        private void search_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "") { loademp(); }
            else
            {
                try
                {
                    ОДБ.OleDbDataReader выполнение = Database.getReader("Select * From Employees where Employees.lname = '"+textBox2.Text.ToString()+"'");
                    DataTable mytable = new DataTable();
                    mytable.Columns.Add(выполнение.GetName(0));
                    mytable.Columns.Add(выполнение.GetName(1));
                    mytable.Columns.Add(выполнение.GetName(2));
                    mytable.Columns.Add(выполнение.GetName(3));
                    mytable.Columns.Add(выполнение.GetName(4));
                    mytable.Columns.Add(выполнение.GetName(5));
                    mytable.Columns.Add(выполнение.GetName(6));
                    mytable.Columns.Add(выполнение.GetName(7));
                    mytable.Columns.Add(выполнение.GetName(8));
                    mytable.Columns.Add(выполнение.GetName(9));
                    mytable.Columns.Add(выполнение.GetName(10));

                    while (выполнение.Read() == true)
                        mytable.Rows.Add(new object[] { выполнение.GetValue(0), выполнение.GetValue(1), выполнение.GetValue(2), выполнение.GetValue(3), выполнение.GetValue(4), выполнение.GetValue(10), выполнение.GetValue(5), выполнение.GetValue(6), выполнение.GetValue(7), выполнение.GetValue(8), выполнение.GetValue(9) });

                    выполнение.Close();
                    dataGridView1.DataSource = mytable;
                    dataGridView1.Columns[0].HeaderCell.Value = "ID";
                    dataGridView1.Columns[0].Width = 50;
                    dataGridView1.Columns[1].HeaderCell.Value = "Фамилия";
                    dataGridView1.Columns[1].Width = 200;
                    dataGridView1.Columns[2].HeaderCell.Value = "Имя";
                    dataGridView1.Columns[2].Width = 200;
                    dataGridView1.Columns[3].HeaderCell.Value = "Отчество";
                    dataGridView1.Columns[3].Width = 200;
                    dataGridView1.Columns[4].HeaderCell.Value = "Место работы";
                    dataGridView1.Columns[4].Width = 300;
                    dataGridView1.Columns[5].HeaderCell.Value = "Должность";
                    dataGridView1.Columns[5].Width = 300;
                    dataGridView1.Columns[6].HeaderCell.Value = "Пол";
                    dataGridView1.Columns[6].Width = 100;
                    dataGridView1.Columns[7].HeaderCell.Value = "Дата рождения";
                    dataGridView1.Columns[7].Width = 100;
                    dataGridView1.Columns[8].HeaderCell.Value = "Стаж работы в организации";
                    dataGridView1.Columns[8].Width = 100;
                    dataGridView1.Columns[9].HeaderCell.Value = "Стаж работы в отрасли";
                    dataGridView1.Columns[9].Width = 100;
                    dataGridView1.Columns[10].HeaderCell.Value = "Общий стаж";
                    dataGridView1.Columns[10].Width = 100;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка ввода данных");
                }
            }
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        { // удаление человека из списка по фамилии            
            string id = dataGridView1.CurrentRow.Cells[0].Value.ToString();

            const string message = "Удалить сотрудника из базы?";
            const string caption = "Удаление";
            var result = MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            // If the no button was pressed ...
            if (result == DialogResult.Yes)
            {
                Database.execute("Delete * From Employees where Employees.id = "+id+"");
                try
                {
                    MessageBox.Show("Запись удалена");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка выбора данных");
                }
            }
            
            loademp();
        }
    }

}
