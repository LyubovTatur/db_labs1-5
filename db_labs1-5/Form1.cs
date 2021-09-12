using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace db_labs1_5
{
    public partial class Form1 : Form
    {
        MySqlConnection SQLConn = new MySqlConnection("server=localhost;user=root;database=pizzeria;password=1111");
        string query;
        MySqlCommand command;
        MySqlDataAdapter adapter;
        DataTable dataTable1 ;
        DataTable dataTable2 ;
        DataTable dataTable3 ;


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        { 

        }

        private void button1_Click(object sender, EventArgs e)
        {
            query = "select title название, weigth вес, coast цена,category категория from menu where is_enable = 1";
            command = new MySqlCommand(query, SQLConn);
            comboBox3.Items.Clear();
            command.Connection.Open();
            command.ExecuteNonQuery();
            adapter = new MySqlDataAdapter(command);
            dataTable1 = new DataTable();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            adapter.Fill(dataTable1);
            dataGridView1.DataSource = dataTable1;
            adapter.Dispose();
            for (int i = 0; i < dataGridView1.RowCount-1; i++)
            {
                if (!comboBox3.Items.Contains(dataTable1.Rows[i]["категория"].ToString()))
                {
                    comboBox3.Items.Add(dataTable1.Rows[i]["категория"].ToString());
                }
            }
             
            
            
            command.Connection.Close();
           

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //string name = table.rows[i]["columnname"].ToString();
            //MessageBox.Show(dataTable1.Rows[e.RowIndex]["photo"].ToString());

            query = "select * from menu";
            command = new MySqlCommand(query, SQLConn);
            adapter = new MySqlDataAdapter(command);
            dataTable1 = new DataTable();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            adapter.Fill(dataTable1);
            pictureBox1.Image = Image.FromFile(dataTable1.Rows[e.RowIndex]["photo"].ToString());
            

            query = "select order_list.id_order `номер заказа`, order_table.date_of_order дата, order_table.num_of_place столик, order_table.fio ФИО ,sum(menu.coast*order_list.amount) stoimost from order_table join order_list on order_table.id = order_list.id_order join menu on menu.id=order_list.id_dish where id_dish=" + (e.RowIndex+1).ToString() ;
            command = new MySqlCommand(query, SQLConn);
            adapter = new MySqlDataAdapter(command);
            dataTable2 = new DataTable();
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            adapter.Fill(dataTable2);
            dataGridView2.DataSource = dataTable2;
            adapter.Dispose();

            //query = "select order_list.id_order `номер заказа`, menu.title `название блюда`, order_list.amount количество  from order_table join order_list on order_table.id = order_list.id_order join menu on menu.id=order_list.id_dish ";
            query = "select order_list.id_order `номер заказа`, menu.title `название блюда`, order_list.amount количество, menu.coast*order_list.amount `общая цена`  from order_table join order_list on order_table.id = order_list.id_order join menu on menu.id=order_list.id_dish where order_list.id_dish=" + (e.RowIndex+1).ToString();
            command = new MySqlCommand(query, SQLConn);
            adapter = new MySqlDataAdapter(command);
            dataTable3 = new DataTable();
            dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            adapter.Fill(dataTable3);
            dataGridView3.DataSource = dataTable3;
            adapter.Dispose();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 form3= new Form3();
            form3.Show();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
        }

        private void order_table_Click(object sender, EventArgs e)
        {
            Form6 form6 = new Form6();
            form6.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();
            form5.Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            if (comboBox1.Text != "")
            {
                query = $"select title название, weigth вес, coast цена,category категория from menu where is_enable = 1 and title like \"%{comboBox1.Text}%\"";


            }
            else
            {
                query = $"select title название, weigth вес, coast цена,category категория from menu where is_enable = 1 ";

            }
            command = new MySqlCommand(query, SQLConn);
            command.Connection.Open();
            command.ExecuteNonQuery();
           
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {


                        while (reader.Read())
                        {
                        if (!comboBox1.Items.Contains(reader.GetValue(0).ToString()))

                            comboBox1.Items.Add(reader.GetValue(0).ToString());

                        }
                        
                    }
                    else
                    {
                        MessageBox.Show("Результатов не найденно, создайте новую запись.");

                    }
                }
            
            adapter = new MySqlDataAdapter(command);
            dataTable1 = new DataTable();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            adapter.Fill(dataTable1);
            dataGridView1.DataSource = dataTable1;
            adapter.Dispose();
            command.Connection.Close();
        }

        private void comboBox2_TextChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            if (comboBox2.Text != "")
            {
                query = $"select order_list.id_order `номер заказа`, order_table.date_of_order дата, order_table.num_of_place столик, order_table.fio ФИО from order_table join order_list on order_table.id=order_list.id_order where order_table.fio like \"{comboBox2.Text}%\"";


            }
            else
            {
                query = "select order_list.id_order `номер заказа`, order_table.date_of_order дата, order_table.num_of_place столик, order_table.fio ФИО from order_table join order_list on order_table.id=order_list.id_order";

            }
            command = new MySqlCommand(query, SQLConn);
            command.Connection.Open();
            command.ExecuteNonQuery();

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {


                    while (reader.Read())
                    {
                        if (!comboBox2.Items.Contains(reader.GetValue(3).ToString()))
                        {
                            comboBox2.Items.Add(reader.GetValue(3).ToString());

                        }
                        // Индекс (index) столбца Emp_ID в команде SQL.


                    }
                }
                else
                {
                    MessageBox.Show("Результатов не найденно, создайте новую запись.");

                }
            }

            adapter = new MySqlDataAdapter(command);
            dataTable2 = new DataTable();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            adapter.Fill(dataTable2);
            dataGridView2.DataSource = dataTable2;
            adapter.Dispose();
            command.Connection.Close();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            query = $"select title название, weigth вес, coast цена,category категория from menu where is_enable = 1 and category like \"{comboBox3.Text}%\" and weigth <= 0{textBox1.Text}";
            command = new MySqlCommand(query, SQLConn);
            comboBox3.Items.Clear();
            command.Connection.Open();
            command.ExecuteNonQuery();
            adapter = new MySqlDataAdapter(command);
            dataTable1 = new DataTable();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            adapter.Fill(dataTable1);
            dataGridView1.DataSource = dataTable1;
            adapter.Dispose();
            command.Connection.Close();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            query = $"select title название, weigth вес, coast цена,category категория from menu where is_enable = 1 and category like \"{comboBox3.Text}%\" and weigth <= 0{textBox1.Text}";
            command = new MySqlCommand(query, SQLConn);
            comboBox3.Items.Clear();
            command.Connection.Open();
            command.ExecuteNonQuery();
            adapter = new MySqlDataAdapter(command);
            dataTable1 = new DataTable();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            adapter.Fill(dataTable1);
            dataGridView1.DataSource = dataTable1;
            adapter.Dispose();
            command.Connection.Close();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            
                query = $"select title, weigth from menu where coast = (select max(coast) from menu)";

            command = new MySqlCommand(query, SQLConn);
            command.Connection.Open();
            command.ExecuteNonQuery();
            string messageText = "макс цена у таких блюд как  \n";
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {


                    while (reader.Read())
                    {
                        messageText += $"{reader.GetValue(0)} ({reader.GetValue(1)}грамм) \n";


                    }
                }
                else
                {
                    MessageBox.Show("Результатов не найденно");

                }
            }

            adapter = new MySqlDataAdapter(command);
            dataTable2 = new DataTable();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            adapter.Fill(dataTable2);
            dataGridView2.DataSource = dataTable2;
            adapter.Dispose();
            command.Connection.Close();
            MessageBox.Show(messageText);
        }

        private void button7_Click(object sender, EventArgs e)
        {

            query = $"select menu.title from order_table join order_list on order_table.id = order_list.id_order join menu on menu.id=order_list.id_dish where order_table.date_of_order = now() and order_table.num_of_place={textBox2.Text}";

            command = new MySqlCommand(query, SQLConn);
            command.Connection.Open();
            command.ExecuteNonQuery();
            string messageText = "результат:  \n";
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {


                    while (reader.Read())
                    {
                        messageText += $"{reader.GetValue(0)} \n";


                    }
                }
                else
                {
                    MessageBox.Show("Результатов не найденно");

                }
            }

            adapter = new MySqlDataAdapter(command);
            dataTable2 = new DataTable();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            adapter.Fill(dataTable2);
            dataGridView2.DataSource = dataTable2;
            adapter.Dispose();
            command.Connection.Close();
            MessageBox.Show(messageText);
        }

        private void button8_Click(object sender, EventArgs e)
        {

            query = $"select menu.title from order_table join order_list on order_table.id = order_list.id_order join menu on menu.id=order_list.id_dish group by order_list.id_dish having sum(order_list.amount)>{textBox3.Text}";

            command = new MySqlCommand(query, SQLConn);
            command.Connection.Open();
            command.ExecuteNonQuery();
            string messageText = "результат:  \n";
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {


                    while (reader.Read())
                    {
                        messageText += $"{reader.GetValue(0)} \n";


                    }
                }
                else
                {
                    MessageBox.Show("Результатов не найденно");

                }
            }

            adapter = new MySqlDataAdapter(command);
            dataTable2 = new DataTable();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            adapter.Fill(dataTable2);
            dataGridView2.DataSource = dataTable2;
            adapter.Dispose();
            command.Connection.Close();
            MessageBox.Show(messageText);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            query = $"select avg(menu.coast) from order_table join order_list on order_table.id = order_list.id_order join menu on menu.id=order_list.id_dish where order_table.date_of_order = now() ";

            command = new MySqlCommand(query, SQLConn);
            command.Connection.Open();
            command.ExecuteNonQuery();
            string messageText = "результат:  \n";
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {


                    while (reader.Read())
                    {
                        messageText += $"{reader.GetValue(0)} \n";


                    }
                }
                else
                {
                    MessageBox.Show("Результатов не найденно");

                }
            }

            adapter = new MySqlDataAdapter(command);
            dataTable2 = new DataTable();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            adapter.Fill(dataTable2);
            dataGridView2.DataSource = dataTable2;
            adapter.Dispose();
            command.Connection.Close();
            MessageBox.Show(messageText);
        }
    }
}
