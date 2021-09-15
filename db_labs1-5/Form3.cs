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
    public partial class Form3 : Form
    {
        private string photoLink="";
        MySqlConnection SQLConn = new MySqlConnection("server=localhost;user=root;database=obuv;password=1111");
        string query;
        MySqlCommand command;
        MySqlDataAdapter adapter;
        DataTable dataTable1;
        DataTable dataTable2;
        DataTable dataTable3;

        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            //query = "select title название, weigth вес, coast цена,category категория,is_enable доступность from menu";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog open_dialog = new OpenFileDialog(); //создание диалогового окна для выбора файла
            open_dialog.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*"; //формат загружаемого файла
            if (open_dialog.ShowDialog() == DialogResult.OK) //если в окне была нажата кнопка "ОК"
            {
                try
                {
                    pictureBox1.Image = new Bitmap(open_dialog.FileName);
                    photoLink = open_dialog.SafeFileName;
                    //photoLink.Replace(@"\", @"\\");
                   // MessageBox.Show(photoLink);
                    //вместо pictureBox1 укажите pictureBox, в который нужно загрузить изображение 
                    // this.pictureBox1.Size = pictureBox1.Image.Size;
                   // pictureBox1.Image = pictureBox1.Image;
                    pictureBox1.Invalidate();
                    object object_ = new object();
                    EventArgs a;
                    textBox5_TextChanged(object_,e);
                }
                catch
                {
                    DialogResult rezult = MessageBox.Show("Невозможно открыть выбранный файл",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int id = 1;
            bool goInsert = true;
            query = $"select * from ProductCategory";
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

                        if (textBox2.Text == reader.GetValue(1).ToString())
                        {
                            MessageBox.Show("Такое название уже существует.");
                            goInsert = false;
                            break;

                        }
                    }
                }
                id++;
            }
            if (goInsert)
            {
                try
                {
                    query = $"insert into ProductCategory values(\"{id.ToString()}\",\"{ textBox1.Text } \",\"{textBox3.Text}\",\"{ textBox2.Text}\",\"{ photoLink}\",1) ";
                    MessageBox.Show(query);
                    command = new MySqlCommand(query, SQLConn);
                    command.ExecuteNonQuery();
                    Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Что то пошло не так. В числовые поля введите числа.");
                }
                
            }

            command.Connection.Close();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (
                textBox1.Text != "" &&
                textBox2.Text != "" &&
                
                photoLink!=""
                )
            {
                button2.Enabled = true;
                label4.Visible = false;
            }
            else
            {
                button2.Enabled = false;
                label4.Visible = true;
            }
        }
    }
}
