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
    public partial class MyProfilePage : Form
    {
        Employee em = null;
        public MyProfilePage(Employee employee)
        {
            InitializeComponent();
            name.Text = employee.name;
            surname.Text = employee.surname;
            login.Text = employee.login;
            numer.Text = employee.phone;
            email.Text = employee.email;
            data.Text = employee.date;
            position.Text = employee.position;
            em = employee;
            if (em.position != "Leader")
            {
                button3.Hide();
                button5.Hide();
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {

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

        private void MyProfilePage_Load(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            EditProfilePage editprofilepage = new EditProfilePage(em);
            editprofilepage.Show();
            this.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            int id = em.id;
            try
            {
                string connectionString = "server=localhost;database=planner;username=root;password=;";

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string sqlQuery = "DELETE FROM employees WHERE id = @id";
                    MySqlCommand command = new MySqlCommand(sqlQuery, conn);
                    command.Parameters.AddWithValue("@id", id);
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Profile successfully removed.", "Planner");
                    }
                    else
                    {
                        MessageBox.Show("Profile not found.", "Planner");
                    }

                    conn.Close();
                    LoginPage loginpage = new LoginPage();
                    loginpage.Show();
                    this.Hide();
                }
            }
            catch
            {
                MessageBox.Show("Error removing profile.", "Planner");
            }
        }

        private void position_Click(object sender, EventArgs e)
        {

        }
    }
}
