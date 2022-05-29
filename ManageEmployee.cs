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
    public partial class ManageEmployee : Form
    {
        MySqlConnection conn;
        MySqlCommand cmd;
        MySqlDataReader reader;
        public static string connString = "datasource=127.0.0.1;port=3306;Database=grocery_store;username=root;password=Sasuke0962Sasuke0962";
        string _id, _fname, _lname, _gender, _bdate, _doa, _address, _email, _contact, _role;
        byte[] _imgdata;
        MemoryStream mer;
        Image _image;

        public ManageEmployee()
        {
            InitializeComponent();
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            MU mu = new MU(this);
            mu.lblID.Text = _id;
            mu.txtfName.Text = _fname;
            mu.txtlName.Text = _lname;
            mu.cbGender.Text = _gender;
            mu.txtBirth.Text = _bdate;
            mu.txtApply.Text = _doa;
            mu.txtAddress.Text = _address;
            mu.txtEmail.Text = _email;
            mu.txtContact.Text = _contact;
            mu.cbRole.Text = _role;
            mu.iconPictureBox1.BackgroundImage = _image;
            mu.Show();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            conn = new MySqlConnection(connString);
            int i = dataGridView1.CurrentRow.Index;
            _id = dataGridView1[0, i].Value.ToString();
            _fname = dataGridView1[1, i].Value.ToString();
            _lname = dataGridView1[2, i].Value.ToString();
            _gender = dataGridView1[3, i].Value.ToString();
            _bdate = dataGridView1[4, i].Value.ToString();
            _doa = dataGridView1[5, i].Value.ToString();
            _address = dataGridView1[6, i].Value.ToString();
            _email = dataGridView1[7, i].Value.ToString();
            _contact = dataGridView1[8, i].Value.ToString();
            _role = dataGridView1[9, i].Value.ToString();
            _imgdata = (byte[])dataGridView1[10, i].Value;
            mer = new MemoryStream(_imgdata);
            _image = Image.FromStream(mer);
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete record?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                string query = "DELETE FROM grocery_store.grocery_employee WHERE id_employee like '" + _id + "'";
                conn.Open();
                cmd = new MySqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Record has been successfully deleted!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GetEmployee();
            }
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            ManageInsert mi = new ManageInsert();
            mi.Enabled = true;
            mi.Show();
        }

        private void ManageEmployee_Load(object sender, EventArgs e)
        {
            GetEmployee();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            GetEmployee();
        }

        public void GetEmployee()
        {
            dataGridView1.Rows.Clear();
            string query = "SELECT * FROM grocery_store.grocery_employee where id_employee like '%" + txtSearch.Text + "%';";
            conn = new MySqlConnection(connString);
            cmd = new MySqlCommand(query, conn);
            conn.Open();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                dataGridView1.Rows.Add(reader["id_employee"].ToString(), reader["fname_employee"].ToString(), reader["lname_employee"].ToString(), reader["gender_employee"].ToString(), Convert.ToDateTime(reader["birthdate_employee"]).ToString("yyyy/MM/dd"), Convert.ToDateTime(reader["doa_employee"]).ToString("yyyy/MM/dd"), reader["address_employee"].ToString(), reader["email_employee"].ToString(), reader["contact_employee"].ToString(), reader["role_employee"].ToString(), reader["image_employee"]);
            }
            reader.Close();
            conn.Close();

            for (var i = 0; i <= dataGridView1.Rows.Count - 1; i++)
            {
                DataGridViewRow r = dataGridView1.Rows[i];
                r.Height = 100;
            }

            var imageColumn = (DataGridViewImageColumn)dataGridView1.Columns["Column11"];
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Stretch;
        }
    }
}
