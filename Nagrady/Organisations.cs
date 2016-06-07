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
    public partial class Organisations : Form
    {
        public Organisations()
        {
            InitializeComponent();
        }
        public void update()
        {
            try
            {
                var v = Database.getReader("Select * from Organisations");
                DataTable mytable = new DataTable();
                mytable.Columns.Add(v.GetName(0));
                mytable.Columns.Add(v.GetName(1));

                while (v.Read() == true)
                    mytable.Rows.Add(new object[] { v.GetValue(0), v.GetValue(1) });
                v.Close();
                dataGridView1.DataSource = mytable;
                dataGridView1.Columns[0].HeaderCell.Value = "ID";
                dataGridView1.Columns[0].Width = 50;
                dataGridView1.Columns[1].HeaderCell.Value = "Организация";
                dataGridView1.Columns[1].Width = 500;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка ввода данных");
            }
        }
        private void Organisations_Load(object sender, EventArgs e)
        {
            update();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            var result = MessageBox.Show("Удалить организацию из базы?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    Database.execute("Delete * From organisations where organisations.id = " + id + "");
                    MessageBox.Show("Запись удалена");
                    update();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка выбора данных");
                }
            }       
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Data.isAddOrgBtn = true;
            EditOrg f = new EditOrg();
            f.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Data.isAddOrgBtn = false;
            Data.orgId = Int16.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            EditOrg f = new EditOrg();
            f.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            update();
        }
    }
}
