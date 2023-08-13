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
    public partial class TasksDonePage : Form
    {
        Employee em = null;
        public TasksDonePage(Employee employee)
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
            ActiveTasksPage activetaskspage = new ActiveTasksPage(em);
            activetaskspage.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {

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

        private void TasksDonePage_Load(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            string project = comboBox1.Text;
            string connectionString = "server=localhost;database=planner;username=root;password=;";

            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            MySqlCommand cmdDataBase = new MySqlCommand("SELECT name AS 'NAME', start_date AS 'START DATE', end_date AS 'END DATE' FROM tasks WHERE project = @project AND activity = 1 ORDER BY end_date ASC", connection);
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
    }
}
