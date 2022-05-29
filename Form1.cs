using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql;
using MySql.Data.MySqlClient;

namespace GroceryManagement
{
    public partial class Form1 : Form
    {
        public string _id, _user, _fname;
        public int _conInt;
        public Form1()
        {
            InitializeComponent();
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

        private void chkShow_CheckedChanged(object sender, EventArgs e)
        {
            if (chkShow.Checked)
            {
                txtPass.UseSystemPasswordChar = false;
                var chechBox = (CheckBox)sender;
                chechBox.Text = "Hide";
            }
            if (!chkShow.Checked)
            {
                txtPass.UseSystemPasswordChar = true;
                var chechBox = (CheckBox)sender;
                chechBox.Text = "Show";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public void btnLogin_Click(object sender, EventArgs e)
        {
            string userName = txtUser.Text;
            string passWord = txtPass.Text;

            string connString = "datasource=127.0.0.1;port=3306;Database=grocery_store;username=root;password=Sasuke0962Sasuke0962";
            string query = "SELECT * FROM grocery_store.grocery_manager WHERE user_manager = @USER COLLATE utf8mb4_bin AND pass_manager = @PASSW COLLATE utf8mb4_bin;";

            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand cmd = new MySqlCommand(query, conn);
            conn.Open();
            cmd.Parameters.AddWithValue("@USER", userName);
            cmd.Parameters.AddWithValue("@PASSW", passWord);
            int res = Convert.ToInt32(cmd.ExecuteScalar());
            if (res > 0)
            {
                MySqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                _id = reader["id_manager"].ToString();
                _user = reader["user_manager"].ToString();
                _fname = reader["fname_manager"].ToString();

                this.Hide();
                Form2 f2 = new Form2(this);
                f2.lblID.Text = _id;
                f2.lblName.Text = _fname;
                f2.userAdmin = _user;
                f2.Show();
                reader.Close();
            }
            else
            {
                MessageBox.Show("There's no such admin!");
            }
            
            conn.Close();

        }
    }
}