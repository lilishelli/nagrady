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

        public void update()
        {
            try
            {
                var v = Database.getReader("Select * from Rewards where Rewards.id_type = " + comboBox2.Items[comboBox1.SelectedIndex].ToString() + "");
                DataTable mytable = new DataTable();
                mytable.Columns.Add(v.GetName(0));
                mytable.Columns.Add(v.GetName(1));

                while (v.Read() == true)
                    mytable.Rows.Add(new object[] { v.GetValue(0), v.GetValue(1) });
                v.Close();
                dataGridView1.DataSource = mytable;
                dataGridView1.Columns[0].HeaderCell.Value = "Шифр";
                dataGridView1.Columns[0].Width = 100;
                dataGridView1.Columns[1].HeaderCell.Value = "Вид награды";
                dataGridView1.Columns[1].Width = 700;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка ввода данных");
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            var v = Database.getReader("Select * From Reward_types");
            while (v.Read() == true)
            {
                comboBox1.Items.Add(v.GetValue(1));
                comboBox2.Items.Add(v.GetValue(0));
            }
            v.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Data.isAddRewardBtn = true;
            EditReward f = new EditReward();
            f.Show();
            //добавление
            /*try
            {
                Database.execute("INSERT INTO Rewards (reward_name, id_type) VALUES ('" + textBox1.Text.ToString() + "', " + comboBox2.Items[comboBox1.SelectedIndex].ToString() + ")");
            MessageBox.Show("В таблицу добавлена запись");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }

            //обновление
            update();*/
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            update();
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            var result = MessageBox.Show("Удалить награду из базы?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    Database.execute("Delete * From Rewards where Rewards.id = " + id + "");
                    MessageBox.Show("Запись удалена");
                    update();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка выбора данных");
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            update();
        }

        private void checkBox1_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true && comboBox1.SelectedIndex != -1)
            {
                button1.Enabled = true;
                button2.Enabled = true;
                button3.Enabled = true;
                button4.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Data.isAddRewardBtn = false;
            Data.rewardId = Int16.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            EditReward f = new EditReward();
            f.Show();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            update();
        }
    }
}
