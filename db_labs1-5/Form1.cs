using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace db_labs1_5
{
    public partial class Form1 : Form
    {
        MySqlConnection SQLConn = new MySqlConnection("server=localhost;user=root;database=obuv;password=1111");
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
            query = "select title название,guarantee_period гарантия, rules правила from ProductCategory where is_enable=1";
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
            //for (int i = 0; i < dataGridView1.RowCount-1; i++)
            //{
            //    if (!comboBox3.Items.Contains(dataTable1.Rows[i]["категория"].ToString()))
            //    {
            //        comboBox3.Items.Add(dataTable1.Rows[i]["категория"].ToString());
            //    }
            //}
             
            
            
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

            query = "select * from ProductCategory";
            command = new MySqlCommand(query, SQLConn);
            adapter = new MySqlDataAdapter(command);
            dataTable1 = new DataTable();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            adapter.Fill(dataTable1);
            pictureBox1.Image = Image.FromFile(dataTable1.Rows[e.RowIndex]["photo"].ToString());


            query = "select product.title название, product.material материал, product.color цвет, product.price цена, product.stored_amount количество, ProductCategory.title категория from ProductCategory join product on ProductCategory.id = Product.category_id where product.category_id=" + (e.RowIndex + 1).ToString();
            command = new MySqlCommand(query, SQLConn);
            adapter = new MySqlDataAdapter(command);
            dataTable2 = new DataTable();
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            adapter.Fill(dataTable2);
            dataGridView2.DataSource = dataTable2;
            adapter.Dispose();

            query = "select sales.id `номер продажи`, sales.size размер, sales.amount количество,sales.discount скидка, sales.date_of_bying дата, product.title товар, sales.amount*product.price*((100-sales.discount)/100) `итоговая цена` from ProductCategory  join product on ProductCategory.id = Product.category_id join sales on sales.product_id=product.id where productcategory.id =" + (e.RowIndex + 1).ToString();
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
            Form4 form4 = new Form4();
            form4.Show();
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
                query = $"select title название,guarantee_period гарантия, rules правила from ProductCategory where is_enable=1 and title like \"{comboBox1.Text}%\"";


            }
            else
            {
                query = "select title название,guarantee_period гарантия, rules правила from ProductCategory where is_enable=1";

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
            
                query = $"select product.title,productcategory.title  from ProductCategory join product on ProductCategory.id = Product.category_id where product.stored_amount = (select max(stored_amount) from product)";

            command = new MySqlCommand(query, SQLConn);
            command.Connection.Open();
            command.ExecuteNonQuery();
            string messageText = "макс цена у таких товаров как  \n";
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {


                    while (reader.Read())
                    {
                        messageText += $"{reader.GetValue(0)} ({reader.GetValue(1)}) \n";


                    }
                }
                else
                {
                    MessageBox.Show("Результатов не найденно");

                }
            }

            adapter = new MySqlDataAdapter(command);
            adapter.Dispose();
            command.Connection.Close();
            MessageBox.Show(messageText);
        }

        private void button7_Click(object sender, EventArgs e)
        {

            query = $"select  product.title, product.price  from ProductCategory  join product on ProductCategory.id = Product.category_id join sales on sales.product_id=product.id where sales.date_of_bying = now() and sales.amount <= 0{textBox2.Text}";

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
            
            adapter.Dispose();
            command.Connection.Close();
            MessageBox.Show(messageText);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            // join product on ProductCategory.id = Product.category_id join sales on sales.product_id = product.id
            // Категории обуви, для которых средняя цена за шт.больше, чем вводит пользователь.
            query = $"select PRODUCTcategory.title, avg(product.price) from productcategory join product on ProductCategory.id = Product.category_id join sales on sales.product_id = product.id  group by productcategory.id  having avg(product.price) >  {textBox3.Text}";

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
            
            adapter.Dispose();
            command.Connection.Close();
            MessageBox.Show(messageText);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            query = $"select  sum(product.price) from productcategory join product on ProductCategory.id = Product.category_id join sales on sales.product_id = product.id  where month(sales.date_of_bying) = month(now())";
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
            adapter.Dispose();
            command.Connection.Close();
            MessageBox.Show(messageText);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form7 form7 = new Form7();
            form7.Show();

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            //  

            query = $"select sales.id `номер продажи`, sales.size размер, sales.amount количество,sales.discount скидка, sales.date_of_bying дата, product.title товар, sales.amount*product.price*((100-sales.discount)/100) `итоговая цена` from ProductCategory  join product on ProductCategory.id = Product.category_id join sales on sales.product_id=product.id where   sales.amount*product.price*((100-sales.discount)/100) <= 0{ textBox5.Text} and sales.size like \"{textBox4.Text}%\"";
            command = new MySqlCommand(query, SQLConn);
            adapter = new MySqlDataAdapter(command);
            dataTable3 = new DataTable();
            dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            adapter.Fill(dataTable3);
            dataGridView3.DataSource = dataTable3;
            adapter.Dispose();

        }

        private void button10_Click(object sender, EventArgs e)
        {
            query = $"select  sales.date_of_bying дата,  sum(sales.amount*product.price*((100-sales.discount)/100)) `sum` from ProductCategory  join product on ProductCategory.id = Product.category_id join sales on sales.product_id=product.id group by sales.date_of_bying";
            command = new MySqlCommand(query, SQLConn);
            adapter = new MySqlDataAdapter(command);
            dataTable3 = new DataTable();
            dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            adapter.Fill(dataTable3);
            dataGridView3.DataSource = dataTable3;
            adapter.Dispose();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            query = $"select  productcategory.title категория,  sum(sales.amount) `количество` from ProductCategory  join product on ProductCategory.id = Product.category_id join sales on sales.product_id=product.id group by productcategory.id having sum(sales.amount)>0{textBox6.Text}";
            command = new MySqlCommand(query, SQLConn);
            adapter = new MySqlDataAdapter(command);
            dataTable3 = new DataTable();
            dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            adapter.Fill(dataTable3);
            dataGridView3.DataSource = dataTable3;
            adapter.Dispose();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            query = $"select  product.title продукт from product  left join sales on sales.product_id=product.id where sales.id is null";
            command = new MySqlCommand(query, SQLConn);
            adapter = new MySqlDataAdapter(command);
            dataTable3 = new DataTable();
            dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            adapter.Fill(dataTable3);
            dataGridView3.DataSource = dataTable3;
            adapter.Dispose();
        }

        private void order_table_Click_1(object sender, EventArgs e)
        {
            Form6 form6 = new Form6();
            form6.Show();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            query = $"select  product.title `товар`, avg(sales.discount)  from ProductCategory  join product on ProductCategory.id = Product.category_id join sales on sales.product_id=product.id  where productcategory.title =\"{textBox7.Text}\" group by product.id";
            command = new MySqlCommand(query, SQLConn);
            adapter = new MySqlDataAdapter(command);
            dataTable3 = new DataTable();
            dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            adapter.Fill(dataTable3);
            dataGridView3.DataSource = dataTable3;
            adapter.Dispose();
        }
    }
}
