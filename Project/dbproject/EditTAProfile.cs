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
    public partial class EditTAProfile : Form
    {
        private string connectionString = "Data Source=DELL\\SQLEXPRESS;Initial Catalog=db_project;Integrated Security=True";

        private string loggedInUsername;
        public EditTAProfile(string username)
        {
            InitializeComponent();
            loggedInUsername = username;
        }

       

        private void EditTAProfile_Load(object sender, EventArgs e)
        {
            LoadCurrentProfile();
        }
        private void LoadCurrentProfile()
        {
            // Check if a username is available
            if (!string.IsNullOrEmpty(loggedInUsername))
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();

                        // Retrieve the current profile information for the logged-in faculty member
                        string query = "SELECT FName, LName, Password, Contact FROM TAs WHERE username = @Username";
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@Username", loggedInUsername);

                            SqlDataReader reader = command.ExecuteReader();

                            if (reader.Read())
                            {
                                // Display the current profile information in the textboxes
                                textBox1.Text = reader["FName"].ToString();
                                textBox2.Text = reader["LName"].ToString();
                                textBox3.Text = reader["Password"].ToString();
                                textBox4.Text = reader["Contact"].ToString();
                            }
                            else
                            {
                                MessageBox.Show("Profile not found for the logged-in faculty member.");
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
        private void button1_Click(object sender, EventArgs e)
        {
            // Perform the update when the "Update Profile" button is clicked
            UpdateProfile();
        }

        private void UpdateProfile()
        {
            // Check if a username is available
            if (!string.IsNullOrEmpty(loggedInUsername))
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();

                        // Update the profile information in the database
                        string updateQuery = "UPDATE TAs SET FName = @FName, LName = @LName, Password = @Password, Contact = @Contact WHERE username = @Username";
                        using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                        {
                            updateCommand.Parameters.AddWithValue("@FName", textBox1.Text);
                            updateCommand.Parameters.AddWithValue("@LName", textBox2.Text);
                            updateCommand.Parameters.AddWithValue("@Password", textBox3.Text);
                            updateCommand.Parameters.AddWithValue("@Contact", textBox4.Text);
                            updateCommand.Parameters.AddWithValue("@Username", loggedInUsername);

                            int rowsAffected = updateCommand.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Profile updated successfully!");
                                // Optionally, you can close this form or perform other actions after a successful update
                            }
                            else
                            {
                                MessageBox.Show("Profile update failed. Please try again.");
                            }
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

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();

            TAs tas = new TAs(loggedInUsername);
            tas.Show();
           
        }
    }
        }
