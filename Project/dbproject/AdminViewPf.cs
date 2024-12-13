using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dbproject
{
    public partial class AdminViewPf : Form
    {
        private string connectionString = "Data Source=DELL\\SQLEXPRESS;Initial Catalog=db_project;Integrated Security=True";
        private string loggedInUsername; 

        public AdminViewPf(string username)
        {
            InitializeComponent();
            loggedInUsername = username; // Set the logged-in username
        }

        private void AdminViewPf_Load(object sender, EventArgs e)
        {





        }

        private void button1_Click(object sender, EventArgs e)
        {

            string enteredUsername = textBox1.Text;

            if (string.IsNullOrEmpty(enteredUsername))
            {
                MessageBox.Show("Please enter faculty username .");
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Check if the entered username and password exist in the database
                    string query = "SELECT FName, LName, Contact " +
                            "FROM faculty " +
                            "WHERE username = (SELECT TOP 1 username FROM faculty WHERE username = @enteredUsername)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@enteredUsername", enteredUsername);

                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            // Display the TA profile information
                            string fName = reader["FName"].ToString();
                            string lName = reader["LName"].ToString();
                            string contact = reader["Contact"].ToString();

                            MessageBox.Show($"faculty Profile\n\nName: {fName} {lName}\nContact: {contact}");
                        }
                        else
                        {
                            MessageBox.Show("faculty profile not found for the entered username.");
                        }

                        reader.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }



        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            admin ad = new admin(loggedInUsername);
            ad.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string enteredUsername = textBox2.Text;

            if (string.IsNullOrEmpty(enteredUsername))
            {
                MessageBox.Show("Please enter TA username .");
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Check if the entered username and password exist in the database
                    string query = "SELECT FName, LName, Contact " +
                            "FROM TAs " +
                            "WHERE username = (SELECT TOP 1 username FROM TAs WHERE username = @enteredUsername)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@enteredUsername", enteredUsername);

                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            // Display the TA profile information
                            string fName = reader["FName"].ToString();
                            string lName = reader["LName"].ToString();
                            string contact = reader["Contact"].ToString();

                            MessageBox.Show($"TA Profile\n\nName: {fName} {lName}\nContact: {contact}");
                        }
                        else
                        {
                            MessageBox.Show("TA profile not found for the entered username.");
                        }

                        reader.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string enteredUsername = textBox3.Text;

            if (string.IsNullOrEmpty(enteredUsername))
            {
                MessageBox.Show("Please enter LD username .");
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Check if the entered username and password exist in the database
                    string query = "SELECT FName, LName, Contact " +
                            "FROM LDs " +
                            "WHERE username = (SELECT TOP 1 username FROM LDs WHERE username = @enteredUsername)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@enteredUsername", enteredUsername);

                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            // Display the TA profile information
                            string fName = reader["FName"].ToString();
                            string lName = reader["LName"].ToString();
                            string contact = reader["Contact"].ToString();

                            MessageBox.Show($"LD Profile\n\nName: {fName} {lName}\nContact: {contact}");
                        }
                        else
                        {
                            MessageBox.Show("LD profile not found for the entered username.");
                        }

                        reader.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Retrieve the total number of faculty members
                    string query = "SELECT COUNT(*) FROM faculty";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        int totalFacultyMembers = (int)command.ExecuteScalar();

                        MessageBox.Show($"Total Faculty Members: {totalFacultyMembers}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT COUNT(*) FROM TAs";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        int totalFacultyMembers = (int)command.ExecuteScalar();

                        MessageBox.Show($"Total TAs Members: {totalFacultyMembers}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Retrieve the total number of faculty members
                    string query = "SELECT COUNT(*) FROM LDs";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        int totalFacultyMembers = (int)command.ExecuteScalar();

                        MessageBox.Show($"Total LDs Members: {totalFacultyMembers}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
