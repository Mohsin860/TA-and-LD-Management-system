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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace dbproject
{
    public partial class LDFeedback : Form
    {
        private string connectionString = "Data Source=DELL\\SQLEXPRESS;Initial Catalog=db_project;Integrated Security=True";
        private string loggedInUsername; // Variable to store the logged-in username
        public LDFeedback(string username)
        {
            InitializeComponent();
            loggedInUsername=username;
        }



        private void button2_Click(object sender, EventArgs e)
        {

            this.Hide();
            Feedbackmenu feedbackmenu = new Feedbackmenu(loggedInUsername);
            feedbackmenu.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Use a JOIN to check if the TaskID and facultyusername match
                    // and ensure that there is no existing feedback for the same task and faculty
                    string insertQuery = "INSERT INTO FeedbackLD (TaskID,courseID, FeedbackText, Rating, facultyusername) " +
                                         "SELECT @TaskID,@courseID, @FeedbackText, @Rating, @facultyusername " +
                                         "FROM AssignTaskLD " +
                                         "WHERE TaskID = @TaskID AND facultyusername = @facultyusername AND courseID=@courseID " +
                                         "AND NOT EXISTS (SELECT 1 FROM FeedbackLD WHERE TaskID = @TaskID AND facultyusername = @facultyusername AND courseID=@courseID )";

                    using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@TaskID", textBox1.Text);
                        insertCommand.Parameters.AddWithValue("@CourseID", textBox3.Text);

                        insertCommand.Parameters.AddWithValue("@FeedbackText", textBox2.Text);
                        insertCommand.Parameters.AddWithValue("@Rating", Convert.ToInt32(numericUpDown1.Value));
                        insertCommand.Parameters.AddWithValue("@facultyusername", loggedInUsername);

                        int rowsAffected = insertCommand.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Feedback submitted successfully!");
                        }
                        else
                        {
                            MessageBox.Show("Invalid TaskID for the given facultyusername or feedback already provided for this task.");
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
