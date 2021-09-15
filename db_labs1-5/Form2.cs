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
    public partial class Form2 : Form
    { 
        MySqlConnection SQLConn = new MySqlConnection("server=localhost;user=root;database=obuv;password=1111");
        string query;
        MySqlCommand command;
        MySqlDataAdapter adapter;
        DataTable dataTable1;
        DataTable dataTable2;
        DataTable dataTable3;
        public Form2()
        {

            InitializeComponent();

           
        }

       
        //query = "select title название, weigth вес, coast цена,category категория,is_enable доступность from menu";

        private void Form2_Load(object sender, EventArgs e)
        {
         
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                query = $"update    ProductCategory set is_enable = 0 where title like '{textBox1.Text}%' ";
                command = new MySqlCommand(query, SQLConn);
                command.Connection.Open();
                try
                {
                    if (command.ExecuteNonQuery() > 0)
                    {
                        MessageBox.Show("Успешно удалено");

                    }
                    else
                    {
                        MessageBox.Show("Такого товара не найдено");

                    }
                }
                catch (Exception)
                {

                    MessageBox.Show("Что то пошло не так. В числовые поля введите числа.");
                }
               
                command.Connection.Close();
                Close();
            }

            else
            {
                MessageBox.Show("Введите название");
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
    }
    



