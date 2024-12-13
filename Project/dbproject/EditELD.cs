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
    public partial class EditELD : Form
    {
        private string connectionString = "Data Source=DELL\\SQLEXPRESS;Initial Catalog=db_project;Integrated Security=True";
        private string loggedInUsername; // Variable to store the logged-in username

        public EditELD(string username)
        {
            InitializeComponent();
            loggedInUsername = username; // Set the logged-in username
        }

      

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Editaccount EC = new Editaccount(loggedInUsername);
            EC.ShowDialog();
        }

        private void EditELD_Load(object sender, EventArgs e)
        {
            // Load the current profile information when the form loads
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
                        string query = "SELECT FName, LName, Password, Contact FROM LDs WHERE username = @loggedInUsername";
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@loggedInUsername", loggedInUsername);

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

        private void button1_Click(object sender, EventArgs e)
        {
            // Validate the password format
            if (!IsPasswordValid(textBox3.Text))
            {
                MessageBox.Show("Password must contain at least one special character and two numeric digits.");
                return;
            }

            // Validate the contact number format
            if (!IsContactValid(textBox4.Text))
            {
                MessageBox.Show("Contact number must have a minimum of 10 digits and a maximum of 11 digits.");
                return;
            }

            // Perform the update when the "Update Profile" button is clicked
            UpdateProfile();
        }

        // Validate password format
        private bool IsPasswordValid(string password)
        {
            // Check if the password contains at least one special character and two numeric digits
            int specialCharacterCount = 0;
            int numericDigitCount = 0;

            foreach (char c in password)
            {
                if (char.IsLetterOrDigit(c))
                {
                    if (char.IsDigit(c))
                    {
                        numericDigitCount++;
                    }
                }
                else
                {
                    specialCharacterCount++;
                }
            }

            return specialCharacterCount >= 1 && numericDigitCount >= 2;
        }

        // Validate contact number format
        private bool IsContactValid(string contact)
        {
            // Check if the contact number has a minimum of 10 digits and a maximum of 11 digits
            return contact.Length >= 10 && contact.Length <= 11 && contact.All(char.IsDigit);
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
                        string updateQuery = "UPDATE LDs SET FName = @FName, LName = @LName, Password = @Password, Contact = @Contact WHERE username = @Username";
                        using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                        {
                            updateCommand.Parameters.AddWithValue("@FName", textBox1.Text);
                            updateCommand.Parameters.AddWithValue("@LName", textBox2.Text);
                            updateCommand.Parameters.AddWithValue("@Password", textBox3.Text);
                            updateCommand.Parameters.AddWithValue("@Contact", textBox4.Text);
                            updateCommand.Parameters.AddWithValue("@Username", loggedInUsername);

                            // Display the update query
                            Console.WriteLine("Update Query: " + updateCommand.CommandText);
                            foreach (SqlParameter parameter in updateCommand.Parameters)
                            {
                                Console.WriteLine($"{parameter.ParameterName}: {parameter.Value}");
                            }

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

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

       
    }
}
