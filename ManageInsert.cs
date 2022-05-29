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
    public partial class ManageInsert : Form
    {
        MySqlConnection conn;
        MySqlCommand cmd;
        public static string connString = "datasource=127.0.0.1;port=3306;Database=grocery_store;username=root;password=Sasuke0962Sasuke0962";

        public ManageInsert()
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "Choose Image(*.jpg; *.jpeg; *.png; *.gif)|*.jpg; *.jpeg; *.png; *.gif";
            if (op.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.BackgroundImage = Image.FromFile(op.FileName);
                pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            }
        }

        private void iconButton1_Click_1(object sender, EventArgs e)
        {
            try
            {
                if ((cbRole.Text == string.Empty) || (cbGender.Text == string.Empty) || (txtAddress.Text == string.Empty) || (txtApply.Text == string.Empty) || (txtBirth.Text == string.Empty) || (txtFname.Text == string.Empty) || (txtLname.Text == string.Empty) || (txtBirth.Text == string.Empty) || (txtApply.Text == string.Empty) || (pictureBox1.BackgroundImage == null))
                {
                    MessageBox.Show("Fill up the Textbox and Image!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                MemoryStream ms = new MemoryStream();
                pictureBox1.BackgroundImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] arr = ms.GetBuffer();

                string query = "INSERT INTO grocery_store.grocery_employee(fname_employee, lname_employee, gender_employee, birthdate_employee, doa_employee, address_employee, email_employee, contact_employee, role_employee, image_employee)" +
                    "VALUE('" + txtFname.Text + "', '" + txtLname.Text + "', '" + cbGender.Text + "', '" + txtBirth.Text + "', '" + txtApply.Text + "', '" + txtAddress.Text + "', '" + txtEmail.Text + "', '" + txtContact.Text + "', '" + cbRole.Text + "', @image)";
                conn = new MySqlConnection(connString);
                cmd = new MySqlCommand(query, conn);
                conn.Open();
                cmd.Parameters.AddWithValue("@image", arr);


                if (cmd.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Successfully Inserted!");
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
