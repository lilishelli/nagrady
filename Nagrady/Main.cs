using System;
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

        // DataSet rewards;
        ОДБ.OleDbConnection con = new ОДБ.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source= rewards.mdb");
        ОДБ.OleDbCommand ucommand = new ОДБ.OleDbCommand();
        // ОДБ.OleDbDataAdapter Adapter;
        void loademp()
        {
            con.Open();
            var comanda = new ОДБ.OleDbCommand("Select * From Employees", con);
            ОДБ.OleDbDataReader выполнение = comanda.ExecuteReader();
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
                mytable.Rows.Add(new object[] { выполнение.GetValue(0), выполнение.GetValue(1), выполнение.GetValue(2), выполнение.GetValue(3), выполнение.GetValue(4), выполнение.GetValue(5), выполнение.GetValue(6), выполнение.GetValue(7), выполнение.GetValue(8), выполнение.GetValue(9), выполнение.GetValue(10) });
            выполнение.Close();
            con.Close();
            dataGridView1.DataSource = mytable;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            loademp();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Data.isAddBtn = true;
            EditEmp f = new EditEmp();
            f.Show();
        }

        private void проверитьПодключениеСбазойДанныхToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                MessageBox.Show("Подключение выполнено");
                con.Close();
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
        }

        private void addRewardToEmpbtn_Click(object sender, EventArgs e)
        {
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
            label1.Visible = true;
            textBox2.Visible = true;
            search.Visible = true;
            dataGridView1.Visible = true;
            editbtn.Visible = true;
            addRewardToEmpbtn.Visible = true;
            button1.Visible = true;
            checkBox1.Visible = true;
        }

        private void checkBox1_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {

                button1.Enabled = true;
                editbtn.Enabled = true;
                addRewardToEmpbtn.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
                editbtn.Enabled = false;
                addRewardToEmpbtn.Enabled = false;
            }
        }
    }

}
