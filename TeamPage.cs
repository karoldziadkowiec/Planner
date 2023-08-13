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
    public partial class TeamPage : Form
    {
        Employee em = null;
        public TeamPage(Employee employee)
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
            connection.Open();
            MySqlCommand cmdDataBase = new MySqlCommand("SELECT name AS 'NAME', surname AS 'SURNAME', login AS 'LOGIN', position AS 'POSITION' FROM employees WHERE login != '" + em.login + "' ORDER BY id ASC", connection);
            
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

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            MyProfilePage myprofilepage = new MyProfilePage(em);
            myprofilepage.Show();
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void TeamPage_Load(object sender, EventArgs e)
        {

        }
    }
}
