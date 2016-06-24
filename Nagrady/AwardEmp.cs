using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ворд = Microsoft.Office.Interop;

namespace Nagrady
{
    public partial class AwardEmp : Form
    {
        public AwardEmp()
        {
            InitializeComponent();
        }

        void loadawardemp()
        {
            var v = Database.getReader("SELECT [awardemps].[id], [reward_types].[type_name], [Rewards].[reward_name], [employees].[lname] & ' ' & [employees].[fname] & ' ' & [employees].[patre]," +
            " [awardemps].[date_get] , [awardemps].[date_award], [localact].[act_name], [awardemps].[act_num], " +
            " [awardemps].[act_date], [awardemps].[comment]" +
            " FROM Reward_types INNER JOIN (Rewards INNER JOIN (Employees INNER JOIN (awardemps LEFT JOIN localact ON [awardemps].[act_id] = [localact].[id]) ON [Employees].[id] = [awardemps].[emp_id])" +
            " ON [Rewards].[id] = [awardemps].[reward_id]) ON [Reward_types].[id] = [Rewards].[id_type]");
            DataTable mytable = new DataTable();
            mytable.Columns.Add(v.GetName(0));
            mytable.Columns.Add(v.GetName(1));
            mytable.Columns.Add(v.GetName(2));
            mytable.Columns.Add(v.GetName(3));
            mytable.Columns.Add(v.GetName(4));
            mytable.Columns.Add(v.GetName(5));
            mytable.Columns.Add(v.GetName(6));
            mytable.Columns.Add(v.GetName(7));
            mytable.Columns.Add(v.GetName(8));
            mytable.Columns.Add(v.GetName(9));
            while (v.Read() == true)
            {
                string date_get;
                string date_award;
                string date_act;
                try
                {
                    date_get = DateTime.Parse(v.GetValue(4).ToString()).Date.ToString("dd.MM.yyyy");
                }
                catch
                {
                    date_get = "";
                }
                try
                {
                    date_award = DateTime.Parse(v.GetValue(5).ToString()).Date.ToString("dd.MM.yyyy");
                }
                catch
                {
                    date_award = "";
                }
                try
                {
                    date_act = DateTime.Parse(v.GetValue(8).ToString()).Date.ToString("dd.MM.yyyy");
                }
                catch
                {
                    date_act = "";
                }
                mytable.Rows.Add(new object[] { v.GetValue(0), v.GetValue(1), v.GetValue(2), v.GetValue(3), date_get,
                   date_award, v.GetValue(6), v.GetValue(7).ToString(), date_act, v.GetValue(9) });
            }
            v.Close();
            load(mytable);

            var rewards_types = Database.getReader("Select * From Reward_types");
            while (rewards_types.Read() == true)
            {
                comboBox1.Items.Add(rewards_types.GetValue(1));
                comboBox3.Items.Add(rewards_types.GetValue(0));
            }
            rewards_types.Close();

            var local = Database.getReader("Select * From LocalAct");
            while (local.Read() == true)
            {
                comboBox4.Items.Add(local.GetValue(1));
            }
            local.Close();
        }
        void openButtonStateChange()
        {
            try
            {
                string doc = (string)Database.getScalar("select doc from awardemps where id = " + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "");
                if (doc != "" && checkBox1.Checked == true)
                    button5.Enabled = true;
                else button5.Enabled = false;
            }
            catch { }
        }
        private void AwardEmp_Load(object sender, EventArgs e)
        {
            loadawardemp();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Data.isAddAwardBtn = false;
            Data.awardEmpId = Int16.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());

            AddAwardEmp f = new AddAwardEmp();
            f.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string id = dataGridView1.CurrentRow.Cells[0].Value.ToString();

            const string message = "Удалить запись из базы?";
            const string caption = "Удаление";
            var result = MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    Database.execute("Delete * From AwardEmps where id = " + id + "");
                    MessageBox.Show("Запись удалена");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка выбора данных");
                }
            }

            loadawardemp();
        }

        private void checkBox1_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                button1.Enabled = true;
                button2.Enabled = true;
                //button5.Enabled = true;
                button3.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
                button2.Enabled = false;
                button5.Enabled = false;
                button3.Enabled = false;
            }
            openButtonStateChange();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            loadawardemp();
        }

       private void button4_Click_1(object sender, EventArgs e)
        {
        if (textBox1.Text == "") { loadawardemp(); }
        else
        {
            try
            {
                var v = Database.getReader("SELECT [awardemps].[id], [reward_types].[type_name], [Rewards].[reward_name], [employees].[lname] & ' ' & [employees].[fname] & ' ' & [employees].[patre]," +
                   " [awardemps].[date_get] , [awardemps].[date_award], [localact].[act_name], [awardemps].[act_num], " +
                   " [awardemps].[act_date], [awardemps].[comment]" +
                   " FROM Reward_types INNER JOIN (Rewards INNER JOIN (Employees INNER JOIN (awardemps LEFT JOIN localact ON [awardemps].[act_id] = [localact].[id]) ON [Employees].[id] = [awardemps].[emp_id])" +
                   " ON [Rewards].[id] = [awardemps].[reward_id]) ON [Reward_types].[id] = [Rewards].[id_type] where [Employees].[lname] = '" + textBox1.Text + "'");
                DataTable mytable = new DataTable();
                mytable.Columns.Add(v.GetName(0));
                mytable.Columns.Add(v.GetName(1));
                mytable.Columns.Add(v.GetName(2));
                mytable.Columns.Add(v.GetName(3));
                mytable.Columns.Add(v.GetName(4));
                mytable.Columns.Add(v.GetName(5));
                mytable.Columns.Add(v.GetName(6));
                mytable.Columns.Add(v.GetName(7));
                mytable.Columns.Add(v.GetName(8));
                mytable.Columns.Add(v.GetName(9));
                while (v.Read() == true)
                {
                    string date_get;
                    string date_award;
                    string date_act;
                    try
                    {
                        date_get = DateTime.Parse(v.GetValue(4).ToString()).Date.ToString("dd.MM.yyyy");
                    }
                    catch
                    {
                        date_get = "";
                    }
                    try
                    {
                        date_award = DateTime.Parse(v.GetValue(5).ToString()).Date.ToString("dd.MM.yyyy");
                    }
                    catch
                    {
                        date_award = "";
                    }
                    try
                    {
                        date_act = DateTime.Parse(v.GetValue(8).ToString()).Date.ToString("dd.MM.yyyy");
                    }
                    catch
                    {
                        date_act = "";
                    }
                    mytable.Rows.Add(new object[] { v.GetValue(0), v.GetValue(1), v.GetValue(2), v.GetValue(3), date_get,
                   date_award, v.GetValue(6), v.GetValue(7).ToString(), date_act, v.GetValue(9) });
                }
                v.Close();
                // con.Close();
                dataGridView1.DataSource = mytable;
                dataGridView1.Columns[0].HeaderCell.Value = "ID";
                dataGridView1.Columns[0].Width = 50;
                dataGridView1.Columns[1].HeaderCell.Value = "Тип награды";
                dataGridView1.Columns[1].Width = 300;
                dataGridView1.Columns[2].HeaderCell.Value = "Вид награды";
                dataGridView1.Columns[2].Width = 300;
                dataGridView1.Columns[3].HeaderCell.Value = "Сотрудник";
                dataGridView1.Columns[3].Width = 250;
                dataGridView1.Columns[4].HeaderCell.Value = "Дата представления";
                dataGridView1.Columns[4].Width = 150;
                dataGridView1.Columns[5].HeaderCell.Value = "Дата получения награды";
                dataGridView1.Columns[5].Width = 120;
                dataGridView1.Columns[6].HeaderCell.Value = "Вид локального акта";
                dataGridView1.Columns[6].Width = 150;
                dataGridView1.Columns[7].HeaderCell.Value = "Номер локального акта";
                dataGridView1.Columns[7].Width = 120;
                dataGridView1.Columns[8].HeaderCell.Value = "Дата локального акта";
                dataGridView1.Columns[8].Width = 150;
                dataGridView1.Columns[9].HeaderCell.Value = "Примечания";
                dataGridView1.Columns[9].Width = 200;
                //----------->Внешний вид DataGridView
                DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();
                DataGridViewCellStyle columnHeaderStyle1 = new DataGridViewCellStyle();
                columnHeaderStyle.Font = new Font("Verdana", 12);
                columnHeaderStyle1.Font = new Font("Verdana", 12, FontStyle.Bold);
                dataGridView1.ColumnHeadersDefaultCellStyle = columnHeaderStyle1;//изменения для головы
                dataGridView1.RowsDefaultCellStyle = columnHeaderStyle;//изменения для остальных строк
                for (int i = 0; i < dataGridView1.Rows.Count; i++)//увеличить высоту ячеек
                    dataGridView1.Rows[i].Height += 60;
                this.dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;//перенос слов
                dataGridView1.Refresh();//обновить
                                        //----------------<
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка ввода данных");
            }
        }
        }

       private void button5_Click(object sender, EventArgs e)
       {
           try
           {
               string doc = (string)Database.getScalar("select doc from awardemps where id = " + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "");
               if (doc!="")
                       Process.Start(AppDomain.CurrentDomain.BaseDirectory + "docs\\" + doc);
                   else
                       MessageBox.Show("Не удаётся найти указанный файл");   
           }
           catch
           {
               MessageBox.Show("Не удаётся найти указанный файл");  
           }
       }

       private void dataGridView1_SelectionChanged(object sender, EventArgs e)
       {
           openButtonStateChange();
       }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                comboBox2.Items.Clear();
                comboBox2.Text = "";
                var rewards = Database.getReader("Select * from Rewards where Rewards.id_type = " + comboBox3.Items[comboBox1.SelectedIndex].ToString() + "");
                while (rewards.Read() == true)
                    comboBox2.Items.Add(rewards.GetValue(1));
                rewards.Close();
                var v = Database.getReader("SELECT [awardemps].[id], [reward_types].[type_name], [Rewards].[reward_name], [employees].[lname] & ' ' & [employees].[fname] & ' ' & [employees].[patre]," +
                    " [awardemps].[date_get] , [awardemps].[date_award], [localact].[act_name], [awardemps].[act_num], " +
                    " [awardemps].[act_date], [awardemps].[comment]" +
                    " FROM Reward_types INNER JOIN (Rewards INNER JOIN (Employees INNER JOIN (awardemps LEFT JOIN localact ON [awardemps].[act_id] = [localact].[id]) ON [Employees].[id] = [awardemps].[emp_id])" +
                    " ON [Rewards].[id] = [awardemps].[reward_id]) ON [Reward_types].[id] = [Rewards].[id_type] where [reward_types].[type_name] = '" + comboBox1.Text + "'");
                DataTable mytable = new DataTable();
                mytable.Columns.Add(v.GetName(0));
                mytable.Columns.Add(v.GetName(1));
                mytable.Columns.Add(v.GetName(2));
                mytable.Columns.Add(v.GetName(3));
                mytable.Columns.Add(v.GetName(4));
                mytable.Columns.Add(v.GetName(5));
                mytable.Columns.Add(v.GetName(6));
                mytable.Columns.Add(v.GetName(7));
                mytable.Columns.Add(v.GetName(8));
                mytable.Columns.Add(v.GetName(9));
                while (v.Read() == true)
                {
                    string date_get;
                    string date_award;
                    string date_act;
                    try
                    {
                        date_get = DateTime.Parse(v.GetValue(4).ToString()).Date.ToString("dd.MM.yyyy");
                    }
                    catch
                    {
                        date_get = "";
                    }
                    try
                    {
                        date_award = DateTime.Parse(v.GetValue(5).ToString()).Date.ToString("dd.MM.yyyy");
                    }
                    catch
                    {
                        date_award = "";
                    }
                    try
                    {
                        date_act = DateTime.Parse(v.GetValue(8).ToString()).Date.ToString("dd.MM.yyyy");
                    }
                    catch
                    {
                        date_act = "";
                    }
                    mytable.Rows.Add(new object[] { v.GetValue(0), v.GetValue(1), v.GetValue(2), v.GetValue(3), date_get,
                   date_award, v.GetValue(6), v.GetValue(7).ToString(), date_act, v.GetValue(9) });
                }
                v.Close();
                load(mytable);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка ввода данных");
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var v = Database.getReader("SELECT [awardemps].[id], [reward_types].[type_name], [Rewards].[reward_name], [employees].[lname] & ' ' & [employees].[fname] & ' ' & [employees].[patre]," +
                   " [awardemps].[date_get] , [awardemps].[date_award], [localact].[act_name], [awardemps].[act_num], " +
                   " [awardemps].[act_date], [awardemps].[comment]" +
                   " FROM Reward_types INNER JOIN (Rewards INNER JOIN (Employees INNER JOIN (awardemps LEFT JOIN localact ON [awardemps].[act_id] = [localact].[id]) ON [Employees].[id] = [awardemps].[emp_id])" +
                   " ON [Rewards].[id] = [awardemps].[reward_id]) ON [Reward_types].[id] = [Rewards].[id_type] where [Rewards].[reward_name] = '" + comboBox2.Text + "'");
                DataTable mytable = new DataTable();
                mytable.Columns.Add(v.GetName(0));
                mytable.Columns.Add(v.GetName(1));
                mytable.Columns.Add(v.GetName(2));
                mytable.Columns.Add(v.GetName(3));
                mytable.Columns.Add(v.GetName(4));
                mytable.Columns.Add(v.GetName(5));
                mytable.Columns.Add(v.GetName(6));
                mytable.Columns.Add(v.GetName(7));
                mytable.Columns.Add(v.GetName(8));
                mytable.Columns.Add(v.GetName(9));
                while (v.Read() == true)
                {
                    string date_get;
                    string date_award;
                    string date_act;
                    try
                    {
                        date_get = DateTime.Parse(v.GetValue(4).ToString()).Date.ToString("dd.MM.yyyy");
                    }
                    catch
                    {
                        date_get = "";
                    }
                    try
                    {
                        date_award = DateTime.Parse(v.GetValue(5).ToString()).Date.ToString("dd.MM.yyyy");
                    }
                    catch
                    {
                        date_award = "";
                    }
                    try
                    {
                        date_act = DateTime.Parse(v.GetValue(8).ToString()).Date.ToString("dd.MM.yyyy");
                    }
                    catch
                    {
                        date_act = "";
                    }
                    mytable.Rows.Add(new object[] { v.GetValue(0), v.GetValue(1), v.GetValue(2), v.GetValue(3), date_get,
                   date_award, v.GetValue(6), v.GetValue(7).ToString(), date_act, v.GetValue(9) });
                }
                v.Close();
                load(mytable);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка ввода данных");
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var v = Database.getReader("SELECT [awardemps].[id], [reward_types].[type_name], [Rewards].[reward_name], [employees].[lname] & ' ' & [employees].[fname] & ' ' & [employees].[patre]," +
                   " [awardemps].[date_get] , [awardemps].[date_award], [localact].[act_name], [awardemps].[act_num], " +
                   " [awardemps].[act_date], [awardemps].[comment]" +
                   " FROM Reward_types INNER JOIN (Rewards INNER JOIN (Employees INNER JOIN (awardemps LEFT JOIN localact ON [awardemps].[act_id] = [localact].[id]) ON [Employees].[id] = [awardemps].[emp_id])" +
                   " ON [Rewards].[id] = [awardemps].[reward_id]) ON [Reward_types].[id] = [Rewards].[id_type] where [localact].[act_name] = '" + comboBox4.Text + "'");
                DataTable mytable = new DataTable();
                mytable.Columns.Add(v.GetName(0));
                mytable.Columns.Add(v.GetName(1));
                mytable.Columns.Add(v.GetName(2));
                mytable.Columns.Add(v.GetName(3));
                mytable.Columns.Add(v.GetName(4));
                mytable.Columns.Add(v.GetName(5));
                mytable.Columns.Add(v.GetName(6));
                mytable.Columns.Add(v.GetName(7));
                mytable.Columns.Add(v.GetName(8));
                mytable.Columns.Add(v.GetName(9));
                while (v.Read() == true)
                {
                    string date_get;
                    string date_award;
                    string date_act;
                    try
                    {
                        date_get = DateTime.Parse(v.GetValue(4).ToString()).Date.ToString("dd.MM.yyyy");
                    }
                    catch
                    {
                        date_get = "";
                    }
                    try
                    {
                        date_award = DateTime.Parse(v.GetValue(5).ToString()).Date.ToString("dd.MM.yyyy");
                    }
                    catch
                    {
                        date_award = "";
                    }
                    try
                    {
                        date_act = DateTime.Parse(v.GetValue(8).ToString()).Date.ToString("dd.MM.yyyy");
                    }
                    catch
                    {
                        date_act = "";
                    }
                    mytable.Rows.Add(new object[] { v.GetValue(0), v.GetValue(1), v.GetValue(2), v.GetValue(3), date_get,
                   date_award, v.GetValue(6), v.GetValue(7).ToString(), date_act, v.GetValue(9) });
                }
                v.Close();
                load(mytable);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка ввода данных");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                var v = Database.getReader("SELECT [awardemps].[id], [reward_types].[type_name], [Rewards].[reward_name], [employees].[lname] & ' ' & [employees].[fname] & ' ' & [employees].[patre]," +
                   " [awardemps].[date_get] , [awardemps].[date_award], [localact].[act_name], [awardemps].[act_num], " +
                   " [awardemps].[act_date], [awardemps].[comment]" +
                   " FROM Reward_types INNER JOIN (Rewards INNER JOIN (Employees INNER JOIN (awardemps LEFT JOIN localact ON [awardemps].[act_id] = [localact].[id]) ON [Employees].[id] = [awardemps].[emp_id])" +
                   " ON [Rewards].[id] = [awardemps].[reward_id]) ON [Reward_types].[id] = [Rewards].[id_type] where [awardemps].[act_num] = '" + textBox2.Text + "'");
                DataTable mytable = new DataTable();
                mytable.Columns.Add(v.GetName(0));
                mytable.Columns.Add(v.GetName(1));
                mytable.Columns.Add(v.GetName(2));
                mytable.Columns.Add(v.GetName(3));
                mytable.Columns.Add(v.GetName(4));
                mytable.Columns.Add(v.GetName(5));
                mytable.Columns.Add(v.GetName(6));
                mytable.Columns.Add(v.GetName(7));
                mytable.Columns.Add(v.GetName(8));
                mytable.Columns.Add(v.GetName(9));
                while (v.Read() == true)
                {
                    string date_get;
                    string date_award;
                    string date_act;
                    try
                    {
                        date_get = DateTime.Parse(v.GetValue(4).ToString()).Date.ToString("dd.MM.yyyy");
                    }
                    catch
                    {
                        date_get = "";
                    }
                    try
                    {
                        date_award = DateTime.Parse(v.GetValue(5).ToString()).Date.ToString("dd.MM.yyyy");
                    }
                    catch
                    {
                        date_award = "";
                    }
                    try
                    {
                        date_act = DateTime.Parse(v.GetValue(8).ToString()).Date.ToString("dd.MM.yyyy");
                    }
                    catch
                    {
                        date_act = "";
                    }
                    mytable.Rows.Add(new object[] { v.GetValue(0), v.GetValue(1), v.GetValue(2), v.GetValue(3), date_get,
                   date_award, v.GetValue(6), v.GetValue(7).ToString(), date_act, v.GetValue(9) });
                }
                v.Close();
                load(mytable);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка ввода данных");
            }
        }

        void load(DataTable q)
        {
            try
            {
                dataGridView1.DataSource = q;
                dataGridView1.Columns[0].HeaderCell.Value = "ID";
                dataGridView1.Columns[0].Width = 50;
                dataGridView1.Columns[1].HeaderCell.Value = "Тип награды";
                dataGridView1.Columns[1].Width = 300;
                dataGridView1.Columns[2].HeaderCell.Value = "Вид награды";
                dataGridView1.Columns[2].Width = 300;
                dataGridView1.Columns[3].HeaderCell.Value = "Сотрудник";
                dataGridView1.Columns[3].Width = 250;
                dataGridView1.Columns[4].HeaderCell.Value = "Дата представления";
                dataGridView1.Columns[4].Width = 150;
                dataGridView1.Columns[5].HeaderCell.Value = "Дата получения награды";
                dataGridView1.Columns[5].Width = 120;
                dataGridView1.Columns[6].HeaderCell.Value = "Вид локального акта";
                dataGridView1.Columns[6].Width = 150;
                dataGridView1.Columns[7].HeaderCell.Value = "Номер локального акта";
                dataGridView1.Columns[7].Width = 120;
                dataGridView1.Columns[8].HeaderCell.Value = "Дата локального акта";
                dataGridView1.Columns[8].Width = 150;
                dataGridView1.Columns[9].HeaderCell.Value = "Примечания";
                dataGridView1.Columns[9].Width = 200;
                //----------->Внешний вид DataGridView
                DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();
                DataGridViewCellStyle columnHeaderStyle1 = new DataGridViewCellStyle();
                columnHeaderStyle.Font = new Font("Verdana", 12);
                columnHeaderStyle1.Font = new Font("Verdana", 12, FontStyle.Bold);
                dataGridView1.ColumnHeadersDefaultCellStyle = columnHeaderStyle1;//изменения для головы
                dataGridView1.RowsDefaultCellStyle = columnHeaderStyle;//изменения для остальных строк
                for (int i = 0; i < dataGridView1.Rows.Count; i++)//увеличить высоту ячеек
                    dataGridView1.Rows[i].Height += 60;
                this.dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;//перенос слов
                dataGridView1.Refresh();//обновить
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка ввода данных");
            }                      //----------------<
        }
    }
}
