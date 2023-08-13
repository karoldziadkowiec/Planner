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
    public partial class EditTaskPage : Form
    {
        Employee em = null;
        public EditTaskPage(Employee employee)
        {
            InitializeComponent();

            em = employee;

            if (em.position != "Leader")
            {
                button3.Hide();
                button5.Hide();
            }

            string connectionString = "server=localhost;database=planner;username=root;password=;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlCommand cmdDataBase1 = new MySqlCommand("SELECT name FROM projects ORDER BY name", connection);

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

        private void button6_Click(object sender, EventArgs e)
        {
            MyProfilePage myprofilepage = new MyProfilePage(em);
            myprofilepage.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            TeamPage teampage = new TeamPage(em);
            teampage.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            MainPage mainpage = new MainPage(em);
            mainpage.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ActiveTasksPage activetaskspage = new ActiveTasksPage(em);
            activetaskspage.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TasksDonePage tasksdonepage = new TasksDonePage(em);
            tasksdonepage.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AddTaskPage addtaskpage = new AddTaskPage(em);
            addtaskpage.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            LoginPage loginpage = new LoginPage();
            loginpage.Show();
            this.Hide();
        }

        private void EditTaskPage_Load(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            string project = comboBox1.Text;
            string connectionString = "server=localhost;database=planner;username=root;password=;";

            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlCommand cmdDataBase2 = new MySqlCommand("SELECT name FROM tasks WHERE project = '" + project + "' AND activity != 1 ORDER BY name", connection);
            
            try
            {
                connection.Open();

                //COMBOBOX2
                MySqlDataAdapter adapter2 = new MySqlDataAdapter(cmdDataBase2);
                DataTable dt2 = new DataTable();
                adapter2.Fill(dt2);

                // dodanie danych do comboBox1
                comboBox2.DataSource = dt2;
                comboBox2.DisplayMember = "name";
                comboBox2.ValueMember = "name";
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

        MySqlConnection conn = new MySqlConnection("datasource=localhost;username=root;password=;database=planner");
        private void button12_Click(object sender, EventArgs e)
        {
            string task = comboBox2.Text;

            if (task.Length == 0)
            {
                MessageBox.Show("Please choose the task.", "Planner");
                return;
            }

            try
            {
                conn.Open();
                string query = "SELECT * FROM tasks WHERE name = '" + task + "'";
                MySqlCommand command = new MySqlCommand(query, conn);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string name = reader.GetString(1);
                        string end_date = reader.GetString(3);
                        string description = reader.GetString(4);

                        textBox1.Text = name;
                        dateTimePicker1.Text = end_date;
                        textBox3.Text = description;
                    }
                }
                else
                {
                    MessageBox.Show("Task not found.", "Planner");
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

        private void button10_Click(object sender, EventArgs e)
        {
            string task = comboBox2.Text;
            String name = textBox1.Text;
            String end_date = dateTimePicker1.Text;
            String description = textBox3.Text;

            if (name.Length == 0 || end_date.Length == 0 || description.Length == 0 )
            {
                MessageBox.Show("Complete the empty fields.", "Planner");
                return;
            }

            try
            {
                conn.Open();
                string sqlQuery = "UPDATE tasks SET name='" + name + "', end_date='" + end_date + "', description='" + description + "' WHERE name='" + task + "'";
                MySqlCommand command = new MySqlCommand(sqlQuery, conn);
                command.ExecuteNonQuery();

                MessageBox.Show("Task updated successfully.", "Planner");
                ActiveTasksPage activetaskspage = new ActiveTasksPage(em);
                activetaskspage.Show();
                this.Hide();
            }
            catch
            {
                MessageBox.Show("Editing error.", "Planner");
            }
            finally
            {
                conn.Close();
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string name = comboBox2.Text;

            try
            {
                string connectionString = "server=localhost;database=planner;username=root;password=;";

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string sqlQuery = "DELETE FROM tasks WHERE name = @name";
                    MySqlCommand command = new MySqlCommand(sqlQuery, conn);
                    command.Parameters.AddWithValue("@name", name);
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Task successfully removed.", "Planner");
                    }
                    else
                    {
                        MessageBox.Show("Task not found.", "Planner");
                        return;
                    }

                    conn.Close();
                    ActiveTasksPage activetaskspage = new ActiveTasksPage(em);
                    activetaskspage.Show();
                    this.Hide();
                }
            }
            catch
            {
                MessageBox.Show("Error removing profile.", "Planner");
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            EditProjectPage editprojectpage = new EditProjectPage(em);
            editprojectpage.Show();
            this.Hide();
        }
    }
}
