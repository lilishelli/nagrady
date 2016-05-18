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
            //loademp();
            con.Open();
            DataTable mytable = new DataTable();
            var comand1 = new ОДБ.OleDbCommand(" (SELECT s.n, s.c, s1.c1 from (select reward_types.type_name as n, Count(*) as c "+
                  "FROM awardemps, rewards, reward_types  "+
                  "WHERE rewards.id=awardemps.reward_id and reward_types.id = rewards.id_type  "+
                 " AND awardemps.date_get>#01/01/2016# And awardemps.date_get<#06/06/2016# and awardemps.date_award>#06/06/2016#  "+
				  "GROUP BY  reward_types.type_name) as s  left join "+
"(select  reward_types.type_name as n, Count(*) as  c1 "+
               "   FROM awardemps, rewards, reward_types  "+
                "  WHERE rewards.id=awardemps.reward_id and reward_types.id = rewards.id_type  "+
               "   AND awardemps.date_award>#01/01/2016# And awardemps.date_award<#06/06/2016#  "+
"GROUP BY reward_types.type_name)  as s1 on s1.n = s.n)  union "+
"(SELECT s1.n, s.c, s1.c1 from (select  reward_types.type_name as n, Count(*) as c "+
             "     FROM awardemps, rewards, reward_types  "+
              "    WHERE rewards.id=awardemps.reward_id and reward_types.id = rewards.id_type  "+
               "   AND awardemps.date_get>#01/01/2016# And awardemps.date_get<#06/06/2016# and awardemps.date_award>#06/06/2016#  "+
				"  GROUP BY reward_types.type_name) as s right join "+
"(select  reward_types.type_name as n, Count(*) as  c1 "+
           "       FROM awardemps, rewards, reward_types  "+
             "     WHERE rewards.id=awardemps.reward_id and reward_types.id = rewards.id_type  "+
              "    AND awardemps.date_award>#01/01/2016# And awardemps.date_award<#06/06/2016#  "+
"GROUP BY reward_types.type_name) as s1 on s1.n = s.n) ", con);
            ОДБ.OleDbDataReader reader = comand1.ExecuteReader();
            mytable.Columns.Add("тип");
            mytable.Columns.Add("количество представленных");
            mytable.Columns.Add("количество награжленных");
            while (reader.Read() == true)
            {
                mytable.Rows.Add(new object[] { reader.GetValue(0), reader.GetValue(1), reader.GetValue(2) });
            
                var comanda = new ОДБ.OleDbCommand("(SELECT s.n, s.c, s1.c1 from (select rewards.reward_name as n, Count(*) as c "+
                "  FROM awardemps, rewards, reward_types WHERE rewards.id=awardemps.reward_id and reward_types.id = rewards.id_type   "+
                  " AND awardemps.date_get>#01/01/2016# And awardemps.date_get<#06/06/2016# and awardemps.date_award>#06/06/2016#  and  reward_types.type_name = '" + reader.GetValue(0) +
				  "' GROUP BY rewards.reward_name) as s  left join  (select rewards.reward_name as n, Count(*) as  c1  FROM awardemps, rewards, reward_types  WHERE rewards.id=awardemps.reward_id and reward_types.id = rewards.id_type   "+
                 "  AND awardemps.date_award>#01/01/2016# And awardemps.date_award<#06/06/2016#  and  reward_types.type_name = '" + reader.GetValue(0) +
"' GROUP BY rewards.reward_name)  as s1 on s1.n = s.n)  union (SELECT s1.n, s.c, s1.c1 from (select rewards.reward_name as n, Count(*) as c  "+
                "   FROM awardemps, rewards, reward_types WHERE rewards.id=awardemps.reward_id and reward_types.id = rewards.id_type   "+
                "    AND awardemps.date_get>#01/01/2016# And awardemps.date_get<#06/06/2016# and awardemps.date_award>#06/06/2016#  and  reward_types.type_name = '" + reader.GetValue(0) +
				"'  GROUP BY rewards.reward_name) as s right join (select rewards.reward_name as n, Count(*) as  c1  FROM awardemps, rewards, reward_types   "+
                "   WHERE rewards.id=awardemps.reward_id and reward_types.id = rewards.id_type   "+
               "    AND awardemps.date_award>#01/01/2016# And awardemps.date_award<#06/06/2016#  and  reward_types.type_name = '"+ reader.GetValue(0)+
              "'   GROUP BY rewards.reward_name)  as s1 on s1.n = s.n)", con);
                ОДБ.OleDbDataReader выполнение = comanda.ExecuteReader();               
               
                while (выполнение.Read() == true)
                    mytable.Rows.Add(new object[] { выполнение.GetValue(0), выполнение.GetValue(1), выполнение.GetValue(2) });
                выполнение.Close();
           }
            reader.Close();
            con.Close();
            dataGridView1.DataSource = mytable; 
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
            Data.empId =  Int16.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
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

    }

}
