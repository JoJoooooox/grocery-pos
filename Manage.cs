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
    public partial class Manage : Form
    {
        public Manage()
        {
            InitializeComponent();
        }

        private void btnEmployeeManage_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            ManageEmployee me = new ManageEmployee();
            me.TopLevel = false;
            panel2.Controls.Add(me);
            me.Show();
        }

        private void btnAddManager_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            ManageAddManager mam = new ManageAddManager();
            mam.TopLevel = false;
            panel2.Controls.Add(mam);
            mam.Show();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            panel3.BringToFront();
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            panel1.BringToFront();
        }
    }
}
