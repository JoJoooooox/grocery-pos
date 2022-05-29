using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql;
using MySql.Data.MySqlClient;
using System.IO;
using System.Runtime.InteropServices;

namespace GroceryManagement
{
    public partial class Form2 : Form
    {
        public string userAdmin;
        public int idAd;
        Form1 frm1;

        public Form2(Form1 frm)
        {
            InitializeComponent();
            frm1 = frm;
            lblID.Text = frm._id;
            lblName.Text = frm._fname;
            frm._conInt = Convert.ToInt32(frm._id);
            idAd = frm._conInt;
            frm1 = frm;
            
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            GetImage();
        }


        private void GetImage()
        {
            string connString = "datasource=127.0.0.1;port=3306;Database=grocery_store;username=root;password=Sasuke0962Sasuke0962";
            string query = "SELECT image_manager FROM grocery_store.grocery_manager WHERE user_manager = '" + userAdmin + "' AND id_manager = '" + idAd + "';";
            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand cmd = new MySqlCommand(query, conn);
            conn.Open();
            MySqlDataReader reader = cmd.ExecuteReader();
            if(reader.Read())
            {
                byte[] image = (byte[])reader["image_manager"];
                MemoryStream ms = new MemoryStream(image);

                pbUserPic.BackgroundImage = Image.FromStream(ms);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnEmployee_Click(object sender, EventArgs e)
        {
            panelPerForm.Controls.Clear();
            EMMA emma = new EMMA();
            emma.TopLevel = false;
            panelPerForm.Controls.Add(emma);
            emma.Show();
        }

        private void btnEmployeeManage_Click(object sender, EventArgs e)
        {
            panelPerForm.Controls.Clear();
            Manage mng = new Manage();
            mng.TopLevel = false;
            panelPerForm.Controls.Add(mng);
            mng.Show();
        }

        private void btnStocks_Click(object sender, EventArgs e)
        {
            panelPerForm.Controls.Clear();
            Stock sck = new Stock();
            sck.TopLevel = false;
            panelPerForm.Controls.Add(sck);
            sck.Show();
        }

        private void btnStockOrganize_Click(object sender, EventArgs e)
        {
            panelPerForm.Controls.Clear();
            OrganizeStock os = new OrganizeStock();
            os.TopLevel = false;
            panelPerForm.Controls.Add(os);
            os.Show();
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            Application.Restart();
            EMMA emma = new EMMA();
        }

        private void lblID_Click(object sender, EventArgs e)
        {
            panelPerForm.Controls.Clear();
            Reset re = new Reset();
            re.TopLevel = false;
            panelPerForm.Controls.Add(re);
            re.Show();
        }

        private void pbUserPic_Click(object sender, EventArgs e)
        {
            panelPerForm.Controls.Clear();
            Reset re = new Reset();
            re.TopLevel = false;
            panelPerForm.Controls.Add(re);
            re.Show();
        }

        private void lblName_Click(object sender, EventArgs e)
        {
            panelPerForm.Controls.Clear();
            Reset re = new Reset();
            re.TopLevel = false;
            panelPerForm.Controls.Add(re);
            re.Show();
        }

        private void panelAdminMenu_Click(object sender, EventArgs e)
        {
            panelPerForm.Controls.Clear();
            Reset re = new Reset();
            re.TopLevel = false;
            panelPerForm.Controls.Add(re);
            re.Show();
        }
    }
}
