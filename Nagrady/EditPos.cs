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
    public partial class EditPos : Form
    {
        public EditPos()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                if (Data.isAddPosBtn == false)
                {
                    Database.execute("Update Positions set pos_name = '" + textBox1.Text + "' where id = " + Data.posId + "");
                    MessageBox.Show("Запись обновлена");
                }
                else
                {
                    Database.execute("insert into Positions (pos_name) values ('" + textBox1.Text.ToString() + "')");
                    MessageBox.Show("В таблицу добавлена запись");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка ввода данных");
            }
        }

        private void EditPos_Load(object sender, EventArgs e)
        {
            if (Data.isAddPosBtn == false)
            {
                var v = Database.getReader("Select * From Positions where id = " + Data.posId + "");
                if (v.Read() == true)
                {
                    textBox1.Text = v.GetValue(1).ToString();
                }
                v.Close();
            }
        }
    }
}
