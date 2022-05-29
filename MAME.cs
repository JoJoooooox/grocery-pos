using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using MySql;
using MySql.Data.MySqlClient;

namespace GroceryManagement
{
    public partial class MAME : Form
    {
        MySqlConnection conn;
        MySqlCommand cmd;
        public static string connString = "datasource=127.0.0.1;port=3306;Database=grocery_store;username=root;password=Sasuke0962Sasuke0962";
        ManageAddManager mam;

        public MAME(ManageAddManager fmam)
        {
            InitializeComponent();
            mam = fmam;
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

        private void iconPictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "Choose Image(*.jpg; *.jpeg; *.png; *.gif)|*.jpg; *.jpeg; *.png; *.gif";
            if (op.ShowDialog() == DialogResult.OK)
            {
                pbAdminImage.BackgroundImage = Image.FromFile(op.FileName);
                pbAdminImage.BackgroundImageLayout = ImageLayout.Stretch;
            }
        }

        public void Clear()
        {
            txtUser.Clear();
            txtPass.Clear();
            txtFname.Clear();
            txtLname.Clear();
            txtEmail.Clear();
            txtContact.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if ((txtFname.Text == string.Empty) || (txtLname.Text == string.Empty) || (txtEmail.Text == string.Empty) || (txtContact.Text == string.Empty))
                {
                    MessageBox.Show("Fill up the text box!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                MemoryStream ms = new MemoryStream();
                pbAdminImage.BackgroundImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] arr = ms.GetBuffer();

                string query = "UPDATE grocery_store.grocery_manager SET image_Manager = @image, fname_manager = '" + txtFname.Text + "', lname_manager = '" + txtLname.Text + "', email_manager = '" + txtEmail.Text + "', contact_manager = '" + txtContact.Text + "', user_manager = '"+ txtUser.Text +"', pass_manager = '"+ txtPass.Text +"' WHERE id_manager like '" + lblID.Text + "'";
                conn = new MySqlConnection(connString);
                cmd = new MySqlCommand(query, conn);
                conn.Open();
                cmd.Parameters.AddWithValue("@image", arr);
                cmd.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("Successfully Updated!", "Employee Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Clear();
                mam.GetAdmin();
                this.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
