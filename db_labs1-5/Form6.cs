using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace db_labs1_5
{
    public partial class Form6 : Form
    {

        MySqlConnection SQLConn = new MySqlConnection("server=localhost;user=root;database=obuv;password=1111");
        string query;
        MySqlCommand command;
        MySqlDataAdapter adapter;
        DataTable dataTable1;
        DataTable dataTable2;
        DataTable dataTable3;
        List<int> menuArr = new List<int>();

        public Form6()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            int id = 1;
            query = $"select * from productcategory";
            command = new MySqlCommand(query, SQLConn);
            command.Connection.Open();

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    int temp_id;


                    while (reader.Read())
                    {
                        // Индекс (index) столбца Emp_ID в команде SQL.
                        temp_id = int.Parse(reader.GetValue(0).ToString());
                        if (temp_id > id)
                        {
                            id = temp_id;
                        }


                    }
                }
                id++;
            }


            command.Connection.Close();

            query = $"insert into product values(\"{id}\",\"{textBox1.Text}\",\"{textBox2.Text}\",\"{textBox3.Text}\",\"{textBox4.Text}\",\"{textBox5.Text}\",1,\"{menuArr[comboBox1.SelectedIndex]}\")";
            MessageBox.Show(query);
            command = new MySqlCommand(query, SQLConn);            
            command.Connection.Open();
            command.ExecuteNonQuery();
            command.Connection.Close();
            comboBox1.Text = "";
           
            command.Connection.Close();
            Close();
        }

        private void Form6_Load(object sender, EventArgs e)
        {

            query = "select id, title from ProductCategory where is_enable = 1";
            command = new MySqlCommand(query, SQLConn);
            //adapter = new MySqlDataAdapter(command);
            //dataTable1 = new DataTable();
            //adapter.Fill(dataTable1);
            //adapter.Dispose();
            command.Connection.Open();
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        comboBox1.Items.Add(reader.GetValue(1).ToString());
                        menuArr.Add(int.Parse(reader.GetValue(0).ToString()));
                    }
                }
            }
            command.Connection.Close();



            //query = "select title from ProductCategory where is_enable = 1 ";
            //command = new MySqlCommand(query, SQLConn);
            ////adapter = new MySqlDataAdapter(command);
            ////dataTable1 = new DataTable();
            ////adapter.Fill(dataTable1);
            ////adapter.Dispose();
            //command.Connection.Open();
            //using (MySqlDataReader reader = command.ExecuteReader())
            //{
            //    if (reader.HasRows)
            //    {
            //        while (reader.Read())
            //        {
            //            comboBox1.Items.Add(reader.GetValue(0).ToString());
            //        }
            //    }
            //}
            command.Connection.Close();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
