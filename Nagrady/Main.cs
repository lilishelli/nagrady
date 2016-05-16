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

        DataSet rewards;
        ОДБ.OleDbConnection con = new ОДБ.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source= rewards.mdb");
        ОДБ.OleDbCommand ucommand = new ОДБ.OleDbCommand();
        ОДБ.OleDbDataAdapter Adapter;

        private void Form1_Load(object sender, EventArgs e)
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
                mytable.Rows.Add(new object[] { выполнение.GetValue(0), выполнение.GetValue(1), выполнение.GetValue(2), выполнение.GetValue(3), выполнение.GetValue(4), выполнение.GetValue(5), выполнение.GetValue(6), выполнение.GetValue(7), выполнение.GetValue(8), выполнение.GetValue(9), выполнение.GetValue(10)});
            выполнение.Close();
            con.Close();
            dataGridView1.DataSource = mytable;

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void проверитьПодключениеСбазойДанныхToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var con = new ОДБ.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0; Data Source = rewards.mdb");
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

        private void отчётОНаграднойДеятельностиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            con = new ОДБ.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0; Data Source = rewards.mdb");
            con.Open();
              //var comanda = new ОДБ.OleDbCommand("Select * From Employees", con);
           var comanda = new ОДБ.OleDbCommand("SELECT Reward_types.type_name FROM Reward_types", con);
            ОДБ.OleDbDataReader выполнение = comanda.ExecuteReader();
            var Word1 = new Ворд.Word.Application();
            Word1.Visible = true;
            Word1.Documents.Add();
            Word1.Selection.TypeText("СВЕДЕНИЯ О НАГРАДНОЙ ДЕЯТЕЛЬНОСТИ\r\n");
            Word1.Selection.TypeText("Министерство сельского хозяйства и рыбной промышленности Астраханской области\r\n");
            int i = 1;
            Word1.ActiveDocument.Tables.Add(Word1.Selection.Range, 20, 3, Ворд.Word.WdDefaultTableBehavior.wdWord9TableBehavior, Ворд.Word.WdAutoFitBehavior.wdAutoFitContent);
            while (выполнение.Read() == true)
            {
                Word1.ActiveDocument.Tables[1].Cell(i, 1).Range.InsertAfter(выполнение.GetValue(0).ToString());
                //Word1.ActiveDocument.Tables[1].Cell(i, 2).Range.InsertAfter(выполнение.GetValue(1).ToString());
               // Word1.ActiveDocument.Tables[1].Cell(i, 3).Range.InsertAfter(выполнение.GetValue(2).ToString());
                i++;
            }
            Word1.Selection.MoveDown(Ворд.Word.WdUnits.wdLine, 15);
            выполнение.Close();
            con.Close();
        }

        private void списокНаградToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Rewards f = new Rewards();
            f.Show();
        }
    }
}
