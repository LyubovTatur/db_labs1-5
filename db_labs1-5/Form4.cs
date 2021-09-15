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
    public partial class Form4 : Form
    {
        MySqlConnection SQLConn = new MySqlConnection("server=localhost;user=root;database=obuv;password=1111");
        string query;
        MySqlCommand command;
        MySqlDataAdapter adapter;
        DataTable dataTable1;
        List<int> list_intov = new List<int>();

        public Form4()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int id = 1;
            query = $"select * from sales";
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


            try
            {
                query = $"insert into sales values(\"{id.ToString()}\",\"{textBox1.Text}\",\"{textBox4.Text}\",\"{textBox5.Text}\",\"{ dateTimePicker1.Value.Year }-{dateTimePicker1.Value.Month}-{dateTimePicker1.Value.Day}\",{list_intov[comboBox1.SelectedIndex]}) ";
                MessageBox.Show(query);
                command = new MySqlCommand(query, SQLConn);
                command.ExecuteNonQuery();
                Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Что то пошло не так. В числовые поля введите числа.");
            }
            command.Connection.Close();
            Close();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void Form4_Load(object sender, EventArgs e)
        {
            query = "select id, title from product where is_enable = 1";
            command = new MySqlCommand(query, SQLConn);
            //adapter = new MySqlDataAdapter(command);
            //dataTable1 = new DataTable();
            //adapter.Fill(dataTable1);eofaoiiofai
            //adapter.Dispose();
            command.Connection.Open();
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        comboBox1.Items.Add(reader.GetValue(1).ToString());
                        list_intov.Add(int.Parse(reader.GetValue(0).ToString()));
                    }
                }
            }
            command.Connection.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
