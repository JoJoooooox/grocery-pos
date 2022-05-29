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

namespace GroceryManagement
{
    public partial class ManageAddManager : Form
    {
        MySqlConnection conn;
        MySqlCommand cmd;
        MySqlDataReader reader;
        public static string connString = "datasource=127.0.0.1;port=3306;Database=grocery_store;username=root;password=Sasuke0962Sasuke0962";
        string _id, _fname, _lname, _email, _contact, _user, _pass;
        byte[] _imgdata;
        MemoryStream mer;
        Image _image;

        //add
        private void iconButton1_Click(object sender, EventArgs e)
        {
            MAMA mama = new MAMA();
            mama.Enabled = true;
            mama.Show();
        }

        //delete
        private void iconButton3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete record?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                string query = "DELETE FROM grocery_store.grocery_manager WHERE id_manager like '" + _id + "'";
                conn.Open();
                cmd = new MySqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Record has been successfully deleted!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GetAdmin();
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        //edit
        private void iconButton2_Click(object sender, EventArgs e)
        {
            MAME mame = new MAME(this);
            mame.lblID.Text = _id;
            mame.txtUser.Text = _user;
            mame.txtPass.Text = _pass;
            mame.txtEmail.Text = _email;
            mame.txtContact.Text = _contact;
            mame.txtFname.Text = _fname;
            mame.txtLname.Text = _lname;
            mame.pbAdminImage.BackgroundImage = _image;
            mame.Show();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            int i = dataGridView1.CurrentRow.Index;
            _id = dataGridView1[0, i].Value.ToString();
            _user = dataGridView1[1, i].Value.ToString();
            _pass = dataGridView1[2, i].Value.ToString();
            _email = dataGridView1[3, i].Value.ToString();
            _contact = dataGridView1[4, i].Value.ToString();
            _fname = dataGridView1[5, i].Value.ToString();
            _lname = dataGridView1[6, i].Value.ToString();
            _imgdata = (byte[])dataGridView1[7, i].Value;
            mer = new MemoryStream(_imgdata);
            _image = Image.FromStream(mer);
        }

        public ManageAddManager()
        {
            InitializeComponent();
        }

        private void ManageAddManager_Load(object sender, EventArgs e)
        {
            GetAdmin();
        }

        public void GetAdmin()
        {
            dataGridView1.Rows.Clear();
            string query = "SELECT * FROM grocery_store.grocery_manager where id_manager like '%" + txtSearch.Text + "%';";
            conn = new MySqlConnection(connString);
            cmd = new MySqlCommand(query, conn);
            conn.Open();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                dataGridView1.Rows.Add(reader["id_manager"].ToString(), reader["user_manager"].ToString(), reader["pass_manager"].ToString(), reader["email_manager"].ToString(), reader["contact_manager"].ToString(), reader["fname_manager"].ToString(), reader["lname_manager"].ToString(), reader["image_manager"]);
            }
            reader.Close();
            conn.Close();

            for (var i = 0; i <= dataGridView1.Rows.Count - 1; i++)
            {
                DataGridViewRow r = dataGridView1.Rows[i];
                r.Height = 100;
            }

            var imageColumn = (DataGridViewImageColumn)dataGridView1.Columns["Column8"];
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Stretch;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            GetAdmin();
        }

        
    }
}
