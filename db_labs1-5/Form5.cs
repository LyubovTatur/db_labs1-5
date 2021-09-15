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
    public partial class Form5 : Form
    {

        private string photoLink = "";
        MySqlConnection SQLConn = new MySqlConnection("server=localhost;user=root;database=obuv;password=1111");
        string query;
        MySqlCommand command;
        MySqlDataAdapter adapter;
        DataTable dataTable1;
        public Form5()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                query = $"delete from product where title = {int.Parse(textBox1.Text)} ";
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
    }
}
