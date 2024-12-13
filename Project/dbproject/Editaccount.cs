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
    public partial class Editaccount : Form
    {
        private string connectionString = "Data Source=DELL\\SQLEXPRESS;Initial Catalog=db_project;Integrated Security=True";
        private string user;
        public Editaccount(string userr)
        {
            InitializeComponent();
            this.user = userr;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
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
                    string query = "SELECT COUNT(*) FROM faculty WHERE username = @enteredUsername ";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@enteredUsername", enteredUsername);

                        int count = (int)command.ExecuteScalar();

                        if (count > 0)
                        {
                            MessageBox.Show(" successfully entered username!");
                            // Perform actions after successful login
                            this.Hide();

                            // Create and show Form4, passing the logged-in username
                            adminEF EF = new adminEF(enteredUsername);
                            EF.ShowDialog();

                            // Hide Form3 (if needed)
                           

                            // Optionally close Form3
                            // this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Invalid username or username not found in faculty database. Please try again.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string enteredUsername = textBox2.Text;

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
                    string query = "SELECT COUNT(*) FROM LDs WHERE username = @enteredUsername ";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@enteredUsername", enteredUsername);

                        int count = (int)command.ExecuteScalar();

                        if (count > 0)
                        {
                            MessageBox.Show(" successfully entered username!");
                            // Perform actions after successful login
                            this.Hide();

                            // Create and show Form4, passing the logged-in username
                            EditELD EF = new EditELD(enteredUsername);
                            EF.ShowDialog();

                            // Hide Form3 (if needed)


                            // Optionally close Form3
                            // this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Invalid username or username not found in LDs database.  try again.");
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
            string enteredUsername = textBox3.Text;

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
                    string query = "SELECT COUNT(*) FROM TAs WHERE username = @enteredUsername ";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@enteredUsername", enteredUsername);

                        int count = (int)command.ExecuteScalar();

                        if (count > 0)
                        {
                            MessageBox.Show(" successfully entered username!");
                            // Perform actions after successful login
                            this.Hide();

                            // Create and show Form4, passing the logged-in username
                            EditETA EF = new EditETA(enteredUsername);
                            EF.ShowDialog();

                            // Hide Form3 (if needed)


                            // Optionally close Form3
                            // this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Invalid username or username not found in TAs database. Please try again.");
                        }
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
            admin ad = new admin(user);
            ad.ShowDialog();
        }
    }
}
