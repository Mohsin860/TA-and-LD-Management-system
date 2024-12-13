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
    public partial class LDCompleteTask : Form
    {
        private string connectionString = "Data Source=DELL\\SQLEXPRESS;Initial Catalog=db_project;Integrated Security=True";
        private string loggedInUsername; // Variable to store the logged-in username
        public LDCompleteTask(string username)
        {
            InitializeComponent();
            loggedInUsername = username;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            LDsHome hom = new LDsHome(loggedInUsername);
            hom.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Check if the task belongs to the logged-in TA
                    string checkOwnershipQuery = "SELECT COUNT(*) FROM AssignTaskLD WHERE TaskID = @TaskID AND UserID = @loggedInUsername";

                    using (SqlCommand checkOwnershipCommand = new SqlCommand(checkOwnershipQuery, connection))
                    {
                        // Assuming you have the TaskID and logged-in username values, replace them accordingly
                        checkOwnershipCommand.Parameters.AddWithValue("@TaskID", textBox1.Text); // Replace with the actual TaskID
                        checkOwnershipCommand.Parameters.AddWithValue("@loggedInUsername", loggedInUsername); // Fix parameter name

                        int ownershipCount = (int)checkOwnershipCommand.ExecuteScalar();

                        if (ownershipCount > 0)
                        {
                            // Update the status in the AssignTaskTA table
                            string updateQuery = "UPDATE AssignTaskLD SET Status = 'Submitted' WHERE TaskID = @TaskID";

                            using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                            {
                                updateCommand.Parameters.AddWithValue("@TaskID", textBox1.Text); // Replace with the actual TaskID

                                int rowsAffected = updateCommand.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("Task submitted successfully!");
                                    // Close the form or perform other actions if needed
                                    // this.Close();
                                }
                                else
                                {
                                    MessageBox.Show("Task submission failed. Please try again.");
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("You do not have ownership of this task.");
                        }
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
