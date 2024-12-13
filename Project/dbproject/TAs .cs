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
    public partial class TAs : Form
    {
        private string connectionString = "Data Source=DELL\\SQLEXPRESS;Initial Catalog=db_project;Integrated Security=True";

        private string loggedInUsername; // Variable to store the logged-in username

        public TAs(string username)
        {
            InitializeComponent();
            loggedInUsername = username; // Set the logged-in username
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            EditTAProfile editTAProfile = new EditTAProfile(loggedInUsername);
            editTAProfile.Show();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            // Check if a username is available
            if (!string.IsNullOrEmpty(loggedInUsername))
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();

                        // Retrieve the profile information for the logged-in faculty member
                        string query = "SELECT FName, LName,username,password, Contact FROM TAs WHERE username = @Username";
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@Username", loggedInUsername);

                            SqlDataReader reader = command.ExecuteReader();

                            if (reader.Read())
                            {
                                // Display the profile information
                                string fName = reader["FName"].ToString();
                                string lName = reader["LName"].ToString();
                                string Username = reader["username"].ToString();
                                string assword = reader["password"].ToString();
                                string contact = reader["Contact"].ToString();

                                MessageBox.Show($"TA Profile\n\nName: {fName} {lName}\nUsername: {Username}\nPassword: {assword}\nContact: {contact}");
                            }
                            else
                            {
                                MessageBox.Show("Profile not found for the logged-in TA member.");
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
            else
            {
                MessageBox.Show("Username not available.");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            ViewTATask viewtatask = new ViewTATask(loggedInUsername);
            viewtatask.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            TAReviews rew = new TAReviews(loggedInUsername);
            rew.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            TACompleteTask com = new TACompleteTask(loggedInUsername);
            com.ShowDialog();
        }
    }
}
