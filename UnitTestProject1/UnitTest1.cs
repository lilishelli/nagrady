using System;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Forms;
using Nagrady;
namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        EditEmp editEmp;
        [TestInitialize]
        public void init()
        {
            
            MyCon.connect = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source = rewards1.mdb";
            editEmp = new EditEmp();
        }
        [TestMethod]
        public void TestloadEmps()
        {
        }
        [TestMethod]
        public void TestAddEmp()
        {            
            //editEmp.addEmp("q","q","q","q","q","Мужской",DateTime.Now, DateTime.Now, DateTime.Now,DateTime.Now);

        }
        [TestMethod]
        public void TestEditEmp()
        {

        }
    }
}
