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
    public partial class Manager : Form
    {
        MySqlConnection conn;
        MySqlCommand cmd;
        MySqlDataReader reader;

        public Manager()
        {
            InitializeComponent();
        }

        private void Manager_Load(object sender, EventArgs e)
        {
            GetManager();
        }

        public void GetManager()
        {
            dataGridView1.Rows.Clear();
            string connString = "datasource=127.0.0.1;port=3306;Database=grocery_store;username=root;password=Sasuke0962Sasuke0962";
            string query = "SELECT * FROM grocery_store.grocery_manager WHERE id_manager LIKE '%" + txtSearch.Text + "%';";
            conn = new MySqlConnection(connString);
            cmd = new MySqlCommand(query, conn);
            conn.Open();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                dataGridView1.Rows.Add(reader["id_manager"].ToString(), reader["fname_manager"].ToString(), reader["lname_manager"].ToString(), reader["email_manager"].ToString(), reader["contact_manager"].ToString(), reader["image_manager"]);
            }
            reader.Close();
            conn.Close();

            for (var i = 0; i <= dataGridView1.Rows.Count - 1; i++)
            {
                DataGridViewRow r = dataGridView1.Rows[i];
                r.Height = 100;
            }

            var imageColumn = (DataGridViewImageColumn)dataGridView1.Columns["Column6"];
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Stretch;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            GetManager();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}