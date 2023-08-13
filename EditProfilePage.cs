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
    public partial class EditProfilePage : Form
    {
        Employee em = null;
        public EditProfilePage(Employee employee)
        {
            InitializeComponent();

            name2.Text = employee.name;
            surname2.Text = employee.surname;
            login2.Text = employee.login;
            number2.Text = employee.phone;
            password2.Text = employee.password;
            cpassword2.Text = employee.password;
            email2.Text = employee.email;
            birthday2.Text = employee.date;
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

        private void EditProfilePage_Load(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            String name = name2.Text;
            String surname = surname2.Text;
            String login = login2.Text;
            String phone = number2.Text;
            String password = password2.Text;
            String confirmPassword = cpassword2.Text;
            String birthday = birthday2.Text;
            String email = email2.Text;

            if (name.Length == 0 || surname.Length == 0 || phone.Length == 0 || email.Length == 0 || birthday.Length == 0)
            {
                MessageBox.Show("Complete the empty fields.", "Planner");
                return;
            }
            if ((password.Length < 5 && password.Length != 0) || (password.Length > 15 && password.Length != 0))
            {
                MessageBox.Show("The password must contain between 5 and 15 characters.", "Planner");
                return;
            }
            if (phone.Length != 9)
            {
                MessageBox.Show("The phone number must contain 9 characters.", "Planner");
                return;
            }
            if (password != confirmPassword)
            {
                MessageBox.Show("The passwords provided are different. Correct the data.", "Planner");
                return;
            }
            if (!email.Contains('@') || !email.Contains('.'))
            {
                MessageBox.Show("Please enter a valid email.", "Planner");
                return;
            }
            try
            {
                string connectionString = "server=localhost;database=planner;username=root;password=;";
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string sqlQuery = "UPDATE employees SET name='" + name + "', surname='" + surname + "', password='" + password + "', phone='" + phone + "', email='" + email + "', birthday='" + birthday + "' WHERE login='" + login + "';";
                    MySqlCommand command = new MySqlCommand(sqlQuery, conn);
                    command.ExecuteNonQuery();
                    em.name = this.name2.Text;
                    em.surname = this.surname2.Text;
                    em.phone = this.number2.Text;
                    em.password = this.password2.Text;
                    em.date = this.birthday2.Text;
                    em.email = this.email2.Text;

                    MessageBox.Show("Successfully corrected the data.", "Planner");
                    conn.Close();

                    MyProfilePage myprofilepage = new MyProfilePage(em);
                    myprofilepage.Show();
                    this.Hide();
                }
            }
            catch
            {
                MessageBox.Show("Account edit error. Correct the data.", "Planner");
            }
        }
    }
}
