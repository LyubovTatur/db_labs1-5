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
        MySqlConnection SQLConn = new MySqlConnection("server=localhost;user=root;database=pizzeria;password=1111");
        string query;
        MySqlCommand command;
        MySqlDataAdapter adapter;
        DataTable dataTable1;
        public Form4()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int id = 1;
            query = $"select * from order_table";
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
                query = $"insert into order_table values(\"{id.ToString()}\",\"{ dateTimePicker1.Value.Year }-{dateTimePicker1.Value.Month}-{dateTimePicker1.Value.Day}\",\"{ textBox2.Text}\",\"{ textBox3.Text }\") ";
                command = new MySqlCommand(query, SQLConn);
                command.ExecuteNonQuery();
                Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Что то пошло не так. В числовые поля введите числа.");
            }
            command.Connection.Close();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text != "" && textBox3.Text!="")
            {
                label4.Visible = false;
                button2.Enabled = true;

            }
            else
            {
                label4.Visible = true;
                button2.Enabled = false;

            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
