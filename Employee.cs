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
    public partial class Employee : Form
    {
        MySqlConnection conn;
        MySqlCommand cmd;
        MySqlDataReader reader;

        public Employee()
        {
            InitializeComponent();
        }

        private void Employee_Load(object sender, EventArgs e)
        {
            LoadEmployee();
        }

        public void LoadEmployee()
        {
            dataGridView1.Rows.Clear();
            string connString = "datasource=127.0.0.1;port=3306;Database=grocery_store;username=root;password=Sasuke0962Sasuke0962";
            string query = "SELECT * FROM grocery_store.grocery_employee WHERE id_employee LIKE '%"+ txtSearch.Text +"%';";
            conn = new MySqlConnection(connString);
            cmd = new MySqlCommand(query, conn);
            conn.Open();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                dataGridView1.Rows.Add(reader["id_employee"].ToString(), reader["fname_employee"].ToString(), reader["lname_employee"].ToString(), reader["gender_employee"].ToString(), Convert.ToDateTime(reader["birthdate_employee"]).ToString("yyyy/mm/dd"), Convert.ToDateTime(reader["doa_employee"]).ToString("yyyy/mm/dd"), reader["address_employee"].ToString(), reader["email_employee"].ToString(), reader["contact_employee"].ToString(), reader["role_employee"].ToString(), reader["image_employee"]);
            }
            reader.Close();
            conn.Close();

            for(var i =0; i <= dataGridView1.Rows.Count -1; i++)
            {
                DataGridViewRow r = dataGridView1.Rows[i];
                r.Height = 100;
            }

            var imageColumn = (DataGridViewImageColumn)dataGridView1.Columns["Column11"];
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Stretch;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadEmployee();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}