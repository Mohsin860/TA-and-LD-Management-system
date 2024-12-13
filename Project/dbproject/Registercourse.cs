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
    public partial class Registercourse : Form
    {
        private string connectionString = "Data Source=DELL\\SQLEXPRESS;Initial Catalog=db_project;Integrated Security=True";
        private string loggedInUsername;
        public Registercourse(string usernaem)
        {
            InitializeComponent();
            loggedInUsername = usernaem;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Insert the course information into the Courses table only if the combination doesn't exist
                    string insertQuery = "INSERT INTO Courses (CourseName, CourseCode, InstructorID) " +
                                         "SELECT @CourseName, @CourseCode, @InstructorID " +
                                         "WHERE NOT EXISTS (SELECT 1 FROM Courses " +
                                         "                 WHERE CourseName = @CourseName AND InstructorID = @InstructorID)";

                    using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@CourseName", textBox1.Text);
                        insertCommand.Parameters.AddWithValue("@CourseCode", textBox2.Text);
                        insertCommand.Parameters.AddWithValue("@InstructorID", textBox3.Text);

                        int rowsAffected = insertCommand.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Course registered successfully!");
                            // Optionally, you can close this form or perform other actions after a successful registration
                        }
                        else
                        {
                            MessageBox.Show("This course is already registered for the selected instructor.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }




        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            admin admin = new admin(loggedInUsername);
            admin.Show();
        }
    }
}
