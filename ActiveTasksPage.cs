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
    public partial class ActiveTasksPage : Form
    {
        Employee em = null;
        public ActiveTasksPage(Employee employee)
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
                    //DATAGRIDVIEW
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                string selectedValue = row.Cells[0].Value.ToString();
                textBox1.Text = selectedValue;

                DataGridViewRow row2 = dataGridView1.Rows[e.RowIndex];
                string selectedValue2 = row2.Cells[3].Value.ToString();
                label5.Text = selectedValue2;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            string project = comboBox1.Text;
            string connectionString = "server=localhost;database=planner;username=root;password=;";

            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            MySqlCommand cmdDataBase = new MySqlCommand("SELECT name AS 'NAME', start_date AS 'START DATE', end_date AS 'END DATE', description AS 'DESCRIPTION' FROM tasks WHERE project = @project AND activity != 1 ORDER BY end_date ASC", connection); 
            cmdDataBase.Parameters.AddWithValue("@project", project);
            try
            {
                //DATAGRIDVIEW
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmdDataBase);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;

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
        private void button9_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            DateTime end_date = DateTime.Now;
            if (name.Length == 0)
            {
                MessageBox.Show("Please enter the name of the task to complete.", "Planner");
                return;
            }

            try
            {
                using (MySqlConnection connection = new MySqlConnection("server=localhost;database=planner;username=root;password=;"))
                {
                    connection.Open();
                    string sqlQuery = "UPDATE tasks SET activity='1', end_date = @end_date WHERE name=@name";
                    MySqlCommand command = new MySqlCommand(sqlQuery, connection);
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@end_date", end_date);
                    int rowsUpdated = command.ExecuteNonQuery();

                    if (rowsUpdated > 0)
                    {
                        MessageBox.Show("The task has been completed.", "Planner");
                    }
                    else
                    {
                        MessageBox.Show("Task not found or already completed.", "Planner");
                    }

                }
                ActiveTasksPage activetaskspage = new ActiveTasksPage(em);
                activetaskspage.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while updating the database: " + ex.Message, "Planner");
            }
        }

        private void ActiveTasksPage_Load(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
