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
    public partial class LDsHome : Form
    {
        private string connectionString = "Data Source=DELL\\SQLEXPRESS;Initial Catalog=db_project;Integrated Security=True";

        private string loggedInUsername; // Variable to store the logged-in username




        public LDsHome(string username)
        {
            InitializeComponent();
            loggedInUsername = username;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                // Debugging: Check if the event is triggered
                MessageBox.Show("Button 2 Clicked!");

                // Open the profile update form (assuming you have created a form for updating profiles)
                EditLDsProfile editLDsProfile = new EditLDsProfile(loggedInUsername);
                editLDsProfile.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                // Debugging: Check for any exceptions
                MessageBox.Show("Error: " + ex.Message);
            }
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
                        string query = "SELECT FName, LName,username,password, Contact FROM LDs WHERE username = @Username";
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

                                MessageBox.Show($"LD Profile\n\nName: {fName} {lName}\nUsername: {Username}\nPassword: {assword}\nContact: {contact}");
                            }
                            else
                            {
                                MessageBox.Show("Profile not found for the logged-in LD member.");
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
            ViewLDTask viewlstask = new ViewLDTask(loggedInUsername);
            viewlstask.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            LDReviews viewlstask = new LDReviews(loggedInUsername);
            viewlstask.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            LDCompleteTask ldc = new LDCompleteTask(loggedInUsername);
            ldc.ShowDialog();
        }
    }
}
