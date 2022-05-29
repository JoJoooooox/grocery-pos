using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GroceryManagement
{
    public partial class EMMA : Form
    {
        public EMMA()
        {
            InitializeComponent();
        }

        public void btnManager_Click(object sender, EventArgs e)
        {
            pnlMain.Controls.Clear();
            Manager mng = new Manager();
            mng.TopLevel = false;
            pnlMain.Controls.Add(mng);
            mng.Show();
        }


        public void btnEmployee_Click(object sender, EventArgs e)
        {
            pnlMain.Controls.Clear();
            Employee emp = new Employee();
            emp.TopLevel = false;
            pnlMain.Controls.Add(emp);
            emp.Show();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            panel2.BringToFront();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            panel1.BringToFront();
        }
    }
}
