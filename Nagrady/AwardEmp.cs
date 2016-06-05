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
using Ворд = Microsoft.Office.Interop;

namespace Nagrady
{
    public partial class AwardEmp : Form
    {
        public AwardEmp()
        {
            InitializeComponent();
        }
        ОДБ.OleDbConnection con = new ОДБ.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source= rewards.mdb");
        
        void loadawardemp()
        {
            con.Open();
            var comanda = new ОДБ.OleDbCommand("select [awardemps].[id], [reward_types].[type_name], [rewards].[reward_name], [employees].[lname]&' '&[employees].[fname]&' '&[employees].[patre]," +
                "[date_get], [date_award], [act_name], [act_num], [act_date], [comment] from [employees], [rewards], [awardemps], [localact], [reward_types]"+
                "where [employees].[id] = [awardemps].[emp_id] and [rewards].[id]=[awardemps].[reward_id] and [reward_types].[id]=[rewards].[id_type] and [localact].[id] = [awardemps].[act_id]", con);
            ОДБ.OleDbDataReader v = comanda.ExecuteReader();
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
            dataGridView1.Columns[0].Width = 25;
            dataGridView1.Columns[1].HeaderCell.Value = "Тип награды";
            dataGridView1.Columns[1].Width = 100;
            dataGridView1.Columns[2].HeaderCell.Value = "Вид награды";
            dataGridView1.Columns[2].Width = 100;
            dataGridView1.Columns[3].HeaderCell.Value = "Сотрудник";
            dataGridView1.Columns[3].Width = 100;
            dataGridView1.Columns[4].HeaderCell.Value = "Дата представления";
            dataGridView1.Columns[4].Width = 200;
            dataGridView1.Columns[5].HeaderCell.Value = "Дата получения награды";
            dataGridView1.Columns[5].Width = 200;
            dataGridView1.Columns[6].HeaderCell.Value = "Вид локального акта";
            dataGridView1.Columns[6].Width = 100;
            dataGridView1.Columns[7].HeaderCell.Value = "Номер локального акта";
            dataGridView1.Columns[7].Width = 100;
            dataGridView1.Columns[8].HeaderCell.Value = "Дата локального акта";
            dataGridView1.Columns[8].Width = 100;
            dataGridView1.Columns[9].HeaderCell.Value = "Примечания";
            dataGridView1.Columns[9].Width = 100;
      
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

            //con.Open();
            const string message = "Удалить запись из базы?";
            const string caption = "Удаление";
            var result = MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    Database.execute("Delete * From AwardEmps where id = "+id+"");                
                    MessageBox.Show("Запись удалена");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка выбора данных");
                }
            }

           // con.Close();
            loadawardemp();
        }
    }
}
