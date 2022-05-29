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
    public partial class MU : Form
    {
        MySqlConnection conn;
        MySqlCommand cmd;
        public static string connString = "datasource=127.0.0.1;port=3306;Database=grocery_store;username=root;password=Sasuke0962Sasuke0962";
        ManageEmployee muf;

        public MU(ManageEmployee frmmu)
        {
            InitializeComponent();
            muf = frmmu;
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
                iconPictureBox1.BackgroundImage = Image.FromFile(op.FileName);
                iconPictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            }
        }

        public void Clear()
        {
            txtfName.Clear();
            txtlName.Clear();
            txtEmail.Clear();
            txtContact.Clear();
            txtAddress.Clear();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if ((txtfName.Text == string.Empty) || (txtlName.Text == string.Empty) || (txtEmail.Text == string.Empty) || (txtContact.Text == string.Empty))
                {
                    MessageBox.Show("Fill up the text box!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                MemoryStream ms = new MemoryStream();
                iconPictureBox1.BackgroundImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] arr = ms.GetBuffer();

                string query = "UPDATE grocery_store.grocery_employee SET image_employee = @image, fname_employee = '"+ txtfName.Text +"', lname_employee = '"+ txtlName.Text +"',gender_employee = '"+cbGender.Text+"', birthdate_employee = '"+txtBirth.Text+ "', doa_employee = '" + txtApply.Text + "', address_employee = '" + txtAddress.Text + "', email_employee = '" + txtEmail.Text +"', contact_employee = '"+ txtContact.Text + "', role_employee = '" + cbRole.Text + "' WHERE id_employee like '" + lblID.Text +"'";
                conn = new MySqlConnection(connString);
                cmd = new MySqlCommand(query, conn);
                conn.Open();
                cmd.Parameters.AddWithValue("@image", arr);
                cmd.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("Successfully Updated!", "Employee Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Clear();
                muf.GetEmployee();
                this.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
