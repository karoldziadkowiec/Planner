using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Planner
{
    public partial class LoginPage : Form
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        MySqlConnection conn = new MySqlConnection("datasource=localhost;username=root;password=;database=planner");
        private void LoginPage_Load(object sender, EventArgs e)
        {

        }

        Employee employee = null;
        private void button1_Click(object sender, EventArgs e)
        {
            string login, password;
            login = textBox1.Text;
            password = textBox2.Text;

            MySqlCommand command = new MySqlCommand();

            try
            {
                conn.Open();
                String querry = "SELECT * FROM employees WHERE login = '" + login + "' AND password = '" + password + "'";

                command.CommandText = querry;
                command.Connection = conn;
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string name = reader.GetString(1);
                        string surname = reader.GetString(2);
                        login = reader.GetString(3);
                        string email = reader.GetString(4);
                        password = reader.GetString(5);
                        string date = reader.GetString(6);
                        string phone = reader.GetString(7);
                        string position = reader.GetString(8);
                        employee = new Employee(id, name, surname, login, email, password, date, phone, position);

                        MainPage main = new MainPage(employee);
                        main.Show();
                        this.Hide();
                    }
                }
                else if (textBox1.TextLength == 0 || textBox2.TextLength == 0)
                {
                    MessageBox.Show("Empty login fields. Please enter the correct details.", "Planner");
                }
                else
                {
                    MessageBox.Show("Please enter the correct login details.", "Planner");
                }
            }
            catch
            {
                MessageBox.Show("Error.", "Planner");
            }
            finally
            {
                conn.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            RegisterPage registerpage = new RegisterPage();
            registerpage.Show();
            this.Hide();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
