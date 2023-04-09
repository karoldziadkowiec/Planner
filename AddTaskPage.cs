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
    public partial class AddTaskPage : Form
    {
        Employee em = null;
        public AddTaskPage(Employee employee)
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

        private void button8_Click(object sender, EventArgs e)
        {
            CreateProjectPage createprojectpage = new CreateProjectPage(em);
            createprojectpage.Show();
            this.Hide();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            string project = comboBox1.Text;
            string name = textBox1.Text;
            DateTime start_date = DateTime.Now;
            string end_date = dateTimePicker1.Text;
            string description = textBox3.Text;
            int activity = 0;

            if (project.Length == 0 || name.Length == 0 || end_date.Length == 0 || description.Length == 0)
            {
                MessageBox.Show("Complete the empty fields.", "Planner");
                return;
            }

            try
            {
                string connectionString = "server=localhost;database=planner;username=root;password=;";
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string sqlQuery = "INSERT INTO tasks (name, start_date, end_date, description, project, activity) VALUES (@name, @start_date, @end_date, @description, @project, @activity)";
                    MySqlCommand command = new MySqlCommand(sqlQuery, conn);
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@start_date", start_date);
                    command.Parameters.AddWithValue("@end_date", end_date);
                    command.Parameters.AddWithValue("@description", description);
                    command.Parameters.AddWithValue("@project", project);
                    command.Parameters.AddWithValue("@activity", activity);
                    command.ExecuteNonQuery();
                    MessageBox.Show("The task has been created.", "Planner");
                    conn.Close();
                    ActiveTasksPage activetaskspage = new ActiveTasksPage(em);
                    activetaskspage.Show();
                    this.Hide();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Planner");
            }
        }

        private void AddTaskPage_Load(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
