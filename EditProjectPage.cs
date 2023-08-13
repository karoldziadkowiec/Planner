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
    public partial class EditProjectPage : Form
    {
        Employee em = null;
        public EditProjectPage(Employee employee)
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
            EditTaskPage edittaskpage = new EditTaskPage(em);
            edittaskpage.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            LoginPage loginpage = new LoginPage();
            loginpage.Show();
            this.Hide();
        }

        MySqlConnection conn = new MySqlConnection("datasource=localhost;username=root;password=;database=planner");
        private void button11_Click(object sender, EventArgs e)
        {
            Project.Text = comboBox1.Text;
            string project = comboBox1.Text;

            if (project.Length == 0)
            {
                MessageBox.Show("Please choose the project.", "Planner");
                return;
            }

            try
            {
                conn.Open();
                string query = "SELECT * FROM projects WHERE name = '" + project + "'";
                MySqlCommand command = new MySqlCommand(query, conn);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string end_date = reader.GetString(3);
                        string description = reader.GetString(4);

                        dateTimePicker1.Text = end_date;
                        textBox3.Text = description;
                    }
                }
                else
                {
                    MessageBox.Show("Project not found.", "Planner");
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
            String end_date = dateTimePicker1.Text;
            String description = textBox3.Text;

            if (end_date.Length == 0 || description.Length == 0)
            {
                MessageBox.Show("Complete the empty fields.", "Planner");
                return;
            }
            try
            {
                conn.Open();
                string sqlQuery = "UPDATE projects SET end_date='" + end_date + "', description='" + description + "' WHERE name='" + Project.Text + "'";
                MySqlCommand command = new MySqlCommand(sqlQuery, conn);
                command.ExecuteNonQuery();

                MessageBox.Show("Project updated successfully.", "Planner");
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
            string name = comboBox1.Text;

            try
            {
                string connectionString = "server=localhost;database=planner;username=root;password=;";

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string sqlQuery = "DELETE FROM projects WHERE name = @name";
                    MySqlCommand command = new MySqlCommand(sqlQuery, conn);
                    command.Parameters.AddWithValue("@name", name);
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Project successfully removed.", "Planner");
                    }
                    else
                    {
                        MessageBox.Show("Project not found.", "Planner");
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
    }
}
