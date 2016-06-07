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
    public partial class EditOrg : Form
    {
        public EditOrg()
        {
            InitializeComponent();
        }

        private void EditOrg_Load(object sender, EventArgs e)
        {
            if (Data.isAddRewardBtn == false)
            {
                var v = Database.getReader("Select * From Organisations where id = " + Data.orgId + "");
                if (v.Read() == true)
                {
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
                    Database.execute("Update organisations set org_name = '" + textBox1.Text + "' where id = " + Data.orgId + "");
                    MessageBox.Show("Запись обновлена");
                }
                else
                {
                    Database.execute("insert into organisations (org_name) values ('" + textBox1.Text + "')");
                    MessageBox.Show("В таблицу добавлена запись");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка ввода данных");
            }
        }
    }
}
