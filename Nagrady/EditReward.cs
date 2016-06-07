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
    public partial class EditReward : Form
    {
        public EditReward()
        {
            InitializeComponent();
        }

        private void EditReward_Load(object sender, EventArgs e)
        {
            var v = Database.getReader("Select * From Reward_types");
            while (v.Read() == true)
            {
                comboBox1.Items.Add(v.GetValue(1));
                comboBox2.Items.Add(v.GetValue(0));
            }
            v.Close();
            if (Data.isAddRewardBtn == false)
            {
                v = Database.getReader("Select * From Rewards where id = "+Data.rewardId+"");
                if (v.Read() == true)
                {
                    comboBox1.SelectedIndex = comboBox2.Items.IndexOf(v.GetValue(2));
                    textBox1.Text = v.GetValue(1).ToString();
                }
                v.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (Data.isAddRewardBtn == false)
                {               
                    Database.execute("Update rewards set reward_name = '" + textBox1.Text + "', id_type = " + comboBox2.Items[comboBox1.SelectedIndex].ToString()+" where id = "+Data.rewardId+"");
                    MessageBox.Show("Запись обновлена");
                }
                else
                {
                    Database.execute("insert into rewards (reward_name, id_type) values ('" + textBox1.Text + "', " + comboBox2.Items[comboBox1.SelectedIndex].ToString() + ")");
                    MessageBox.Show("В таблицу добавлена запись");
                } 
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка ввода данных");
            }
        }
    }
}
