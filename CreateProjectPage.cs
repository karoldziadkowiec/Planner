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
    public partial class CreateProjectPage : Form
    {
        Employee em = null;
        public CreateProjectPage(Employee employee)
        {
            InitializeComponent();
            em = employee;
            if (em.position != "Leader")
            {
                button3.Hide();
                button5.Hide();
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

        private void button10_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            DateTime start_date = DateTime.Now;
            string end_date = dateTimePicker1.Text;
            string description = textBox3.Text;

            if (name.Length == 0 || end_date.Length == 0 || description.Length == 0)
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
                    string sqlQuery = "INSERT INTO projects (name, start_date, end_date, description) VALUES (@name, @start_date, @end_date, @description)";
                    MySqlCommand command = new MySqlCommand(sqlQuery, conn);
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@start_date", start_date);
                    command.Parameters.AddWithValue("@end_date", end_date);
                    command.Parameters.AddWithValue("@description", description);
                    command.ExecuteNonQuery();

                    MessageBox.Show("The project has been created.", "Planner");
                    conn.Close();

                    AddTaskPage addtaskpage = new AddTaskPage(em);
                    addtaskpage.Show();
                    this.Hide();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Planner");
            }
        }
    }
}
