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
    public partial class LDsReg : Form
    {
        private string connectionString = "Data Source=DELL\\SQLEXPRESS;Initial Catalog=db_project;Integrated Security=True";

        public LDsReg()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 form2 = new Form2();
            form2.ShowDialog();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!IsPasswordValid(textBox4.Text))
            {
                MessageBox.Show("Password must contain at least one special character and two numeric digits.");
                return;
            }

            if (!IsContactValid(textBox5.Text))
            {
                MessageBox.Show("Contact number must have a minimum of 10 digits and a maximum of 11 digits.");
                return;
            }

            if (!IsUsernameValid(textBox3.Text))
            {
                MessageBox.Show("Username must have at least 2 numeric values, a minimum length of 5 characters, and not use special characters except '_'.");
                return;
            }

            Signup();
        }

        // Validate password format
        private bool IsPasswordValid(string password)
        {
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
            return contact.Length >= 10 && contact.Length <= 11 && contact.All(char.IsDigit);
        }

        private bool IsUsernameValid(string username)
        {
         
            int numericCount = 0;

            foreach (char c in username)
            {
                if (char.IsDigit(c))
                {
                    numericCount++;
                }
                else if (!char.IsLetterOrDigit(c) && c != '_')
                {
                    return false; // Special character other than '_' found
                }
            }

            return numericCount >= 2 && username.Length >= 5;
        }

        private void Signup()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Check if the username already exists
                    string checkUsernameQuery = "SELECT COUNT(*) FROM LDs WHERE username = @Username";
                    using (SqlCommand checkUsernameCommand = new SqlCommand(checkUsernameQuery, connection))
                    {
                        checkUsernameCommand.Parameters.AddWithValue("@Username", textBox3.Text);

                        int existingUserCount = (int)checkUsernameCommand.ExecuteScalar();

                        if (existingUserCount > 0)
                        {
                            MessageBox.Show("Username already exists. Please choose a different username.");
                            return;
                        }
                    }

                    // Insert data into the faculty table
                    string insertQuery = "INSERT INTO LDs (FName, LName, username, password, Contact) " +
                                         "VALUES (@FName, @LName, @Username, @Password, @Contact)";
                    using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@FName", textBox1.Text);
                        insertCommand.Parameters.AddWithValue("@LName", textBox2.Text);
                        insertCommand.Parameters.AddWithValue("@Username", textBox3.Text);
                        insertCommand.Parameters.AddWithValue("@Password", textBox4.Text);
                        insertCommand.Parameters.AddWithValue("@Contact", textBox5.Text);

                        int rowsAffected = insertCommand.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Signup successful!");
                           
                        }
                        else
                        {
                            MessageBox.Show("Signup failed. Please try again.");
                        }
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
            this.Hide();
            SignUp signup = new SignUp();
            signup.ShowDialog();
        }
    }
}
