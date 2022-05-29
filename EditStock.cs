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
    public partial class EditStock : Form
    {
        MySqlConnection conn;
        MySqlCommand cmd;
        public static string connString = "datasource=127.0.0.1;port=3306;Database=grocery_store;username=root;password=Sasuke0962Sasuke0962";
        OrganizeStock org;
        public EditStock(OrganizeStock forg)
        {
            InitializeComponent();
            org = forg;
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

        public void Clear()
        {
            txtDesc.Clear();
            txtPrice.Clear();
            txtName.Clear();
            txtKind.Clear();
            txtSize.Clear();
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if ((txtName.Text == string.Empty) || (txtSize.Text == string.Empty) || (txtDesc.Text == string.Empty) || (txtPrice.Text == string.Empty) || (txtKind.Text == string.Empty))
                {
                    MessageBox.Show("Fill up the text box!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                MemoryStream ms = new MemoryStream();
                pictureBox1.BackgroundImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] arr = ms.GetBuffer();

                string query = "UPDATE grocery_store.grocery_manager SET image_Manager = @image, name_product = '" + txtName.Text + "', price_product = '" + txtPrice.Text + "', kind_product = '" + txtKind.Text + "', size_product = '" + txtSize.Text + "', description_product = '" + txtDesc.Text + "' WHERE id_manager like '" + lblID.Text + "'";
                conn = new MySqlConnection(connString);
                cmd = new MySqlCommand(query, conn);
                conn.Open();
                cmd.Parameters.AddWithValue("@image", arr);
                cmd.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("Successfully Updated!", "Employee Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Clear();
                org.GetStock();
                this.Dispose();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
