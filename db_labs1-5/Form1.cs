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
            query = "select * from menu";
            command = new MySqlCommand(query, SQLConn);
            adapter = new MySqlDataAdapter(command);
             dataTable1 = new DataTable();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            adapter.Fill(dataTable1);
            dataGridView1.DataSource = dataTable1;
            adapter.Dispose();

           

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
            pictureBox1.Image = Image.FromFile(dataTable1.Rows[e.RowIndex]["photo"].ToString());


            query = "select order_table.date_of_order дата, order_table.num_of_place столик, order_table.fio ФИО from order_table join order_list on order_table.id=order_list.id_order where id_dish=" + (e.RowIndex+1).ToString() ;
            command = new MySqlCommand(query, SQLConn);
            adapter = new MySqlDataAdapter(command);
            dataTable2 = new DataTable();
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            adapter.Fill(dataTable2);
            dataGridView2.DataSource = dataTable2;
            adapter.Dispose();

            query = "select order_list.id_order `номер заказа`, menu.title `название блюда`, order_list.amount количество  from order_table join order_list on order_table.id = order_list.id_order join menu on menu.id=order_list.id_dish where order_list.id_dish=" + (e.RowIndex+1).ToString();
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
    }
}
