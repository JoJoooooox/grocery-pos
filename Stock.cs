﻿using System;
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

namespace GroceryManagement
{
    public partial class Stock : Form
    {
        double _total;
        MySqlConnection conn;
        MySqlCommand cmd;
        MySqlDataReader reader;
        string connString = "datasource=127.0.0.1;port=3306;Database=grocery_store;username=root;password=Sasuke0962Sasuke0962";

        private PictureBox pb;
        private Label price,name;

        public string btnTag;
        private Button btn;
        public Stock()
        {
            InitializeComponent();
        }

        private void Stock_Load(object sender, EventArgs e)
        {
            LoadButton();
            GetStock();
        }

        public void LoadButton()
        {
            conn = new MySqlConnection(connString);
            conn.Open();
            string query = "SELECT DISTINCT kind_product FROM grocery_store.grocery_products;";
            cmd = new MySqlCommand(query, conn);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                btn = new Button();
                btn.Text = reader["kind_product"].ToString();
                btn.Width = 124;
                btn.Height = 40;
                btn.FlatStyle = FlatStyle.Flat;
                btn.BackColor = Color.FromArgb(242, 179, 255);
                btn.ForeColor = Color.White;
                btn.FlatAppearance.BorderSize = 0;

                flowLayoutPanel2.Controls.Add(btn);
                btn.Click += new EventHandler(isBtnClick);
            }
            reader.Close();
            conn.Close();
        }

        public void isBtnClick(object sender, EventArgs e)
        {
            flowLayoutPanel1.BringToFront();
            btnTag = ((Button)sender).Text.ToString();
            GetStock();
        }

        public void GetStock()
        {
            flowLayoutPanel1.Controls.Clear();
            conn = new MySqlConnection(connString);
            string query = "SELECT image_product, kind_product, price_product, name_product, id_product FROM grocery_store.grocery_products WHERE kind_product LIKE '%" + btnTag + "%' AND name_product LIKE '%" + txtName.Text + "%'";
            conn.Open();
            cmd = new MySqlCommand(query, conn);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                long len = reader.GetBytes(0, 0, null, 0, 0);
                byte[] arr = new byte[Convert.ToInt32(len) + 1];
                reader.GetBytes(0, 0, arr, 0, Convert.ToInt32(len));
                pb = new PictureBox();
                pb.Width = 150;
                pb.Height = 150;
                pb.BackgroundImageLayout = ImageLayout.Stretch;
                pb.BorderStyle = BorderStyle.FixedSingle;
                pb.Tag = reader["id_product"].ToString();

                price = new Label();
                price.Text = reader["price_product"].ToString();
                price.Width = 50;
                price.BackColor = Color.LightCyan;
                price.TextAlign = ContentAlignment.MiddleCenter;

                name = new Label();
                name.Text = reader["name_product"].ToString();
                name.BackColor = Color.LightGreen;
                name.TextAlign = ContentAlignment.MiddleCenter;
                name.Dock = DockStyle.Bottom;

                MemoryStream ms = new MemoryStream(arr);
                Bitmap bm = new Bitmap(ms);
                pb.BackgroundImage = bm;

                pb.Controls.Add(price);
                pb.Controls.Add(name);
                pb.Cursor = Cursors.Hand;
                flowLayoutPanel1.Controls.Add(pb);

                pb.Click += new EventHandler(isPBClick);
            }
            reader.Close();
            conn.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            GetStock();
        }

        public void isPBClick(object sender, EventArgs e)
        {
            String tag = ((PictureBox)sender).Tag.ToString();
            conn.Open();
            string query = "SELECT * FROM grocery_store.grocery_products WHERE id_product like '"+tag+"';";
            cmd = new MySqlCommand(query, conn);
            reader = cmd.ExecuteReader();
            reader.Read();
            if(reader.HasRows)
            {
                _total += double.Parse(reader["price_product"].ToString());
                dataGridView1.Rows.Add(dataGridView1.Rows.Count + 1, reader["description_product"].ToString(), double.Parse(reader["price_product"].ToString()).ToString("#,##0.00"));
            }
            reader.Close();
            conn.Close();
            lblTotal.Text = _total.ToString("#,##0.00");
        }
    }
}
