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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Planner
{
    public partial class RegisterPage : Form
    {
        public RegisterPage()
        {
            InitializeComponent();
            string connectionString = "server=localhost;database=planner;username=root;password=;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlCommand cmdDataBase1 = new MySqlCommand("SELECT name FROM positions ORDER BY name", connection);
            try
            {
                connection.Open();

                //COMBOBOX1
                MySqlDataAdapter adapter1 = new MySqlDataAdapter(cmdDataBase1);
                DataTable dt1 = new DataTable();
                adapter1.Fill(dt1);

                // dodanie danych do comboBox1
                comboBox1.DataSource = dt1;
                comboBox1.DisplayMember = "name";
                comboBox1.ValueMember = "name";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoginPage loginpage = new LoginPage();
            loginpage.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String name = textBox1.Text;
            String surname = textBox2.Text;
            String login = textBox4.Text;
            String email = textBox3.Text;
            String password = textBox6.Text;
            String confirmPassword = textBox5.Text;
            String birthday = dateTimePicker1.Text;
            String phone = textBox7.Text;
            String position = comboBox1.Text;
            if (name.Length == 0 || surname.Length == 0 || email.Length == 0 || login.Length == 0 || password.Length == 0 || confirmPassword.Length == 0 || phone.Length == 0 || birthday.Length == 0)
            {
                MessageBox.Show("Complete the empty fields.", "Planner");
                return;
            }
            if (!email.Contains('@') || !email.Contains('.'))
            {
                MessageBox.Show("Please enter a valid email.", "Planner");
                return;
            }
            if ((login.Length < 5 && login.Length != 0) || (login.Length > 12 && login.Length != 0))
            {
                MessageBox.Show("The login must contain from 5 to 12 characters.", "Planner");
                return;
            }
            if ((password.Length < 5 && password.Length != 0) || (password.Length > 15 && password.Length != 0))
            {
                MessageBox.Show("The password must contain between 5 and 15 characters.", "Planner");
                return;
            }
            if (password != confirmPassword)
            {
                MessageBox.Show("The passwords provided are different. Correct the data.", "Planner");
                return;
            }
            if (phone.Length != 9)
            {
                MessageBox.Show("The phone number must contain 9 characters.", "Planner");
                return;
            }
            if (!checkBox1.Checked)
            {
                MessageBox.Show("Accept the terms of the regulations.", "Planner");
                return;
            }
            try
            {
                string connectionString = "server=localhost;database=planner;username=root;password=;";

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string sqlQuery = "INSERT INTO employees (name, surname, login, email, password, birthday, phone, position) VALUES (@name, @surname, @login, @email, @password, @birthday, @phone, @position)";
                    MySqlCommand command = new MySqlCommand(sqlQuery, conn);
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@surname", surname);
                    command.Parameters.AddWithValue("@login", login);
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@password", password);
                    command.Parameters.AddWithValue("@birthday", birthday);
                    command.Parameters.AddWithValue("@phone", phone);
                    command.Parameters.AddWithValue("@position", position);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Account successfully registered.", "Planner");
                    conn.Close();
                    LoginPage loginpage = new LoginPage();
                    loginpage.Show();
                    this.Hide();
                }
            }
            catch
            {
                MessageBox.Show("Registration error. The given login already has an account.", "Planner");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
