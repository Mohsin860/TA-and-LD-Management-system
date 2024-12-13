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
    public partial class TAReviews : Form
    {
        private string connectionString = "Data Source=DELL\\SQLEXPRESS;Initial Catalog=db_project;Integrated Security=True";
        private string loggedInUsername; // Variable to store the logged-in username
        public TAReviews(string username)
        {
            InitializeComponent();
            loggedInUsername = username; // Set the logged-in username
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            TAs tas = new TAs(loggedInUsername);
            tas.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Retrieve feedback for the logged-in TA
                    string query = "SELECT  FT.TaskID, FT.FeedbackText, FT.Rating, FT.DateTimeProvided, A.TaskName, C.CourseName " +
                             "FROM FeedbackTA FT " +
                             "JOIN AssignTaskTA A ON FT.TaskID = A.TaskID " +
                             "JOIN Courses C ON A.courseID = C.CourseID " +
                             "WHERE A.UserID = @loggedInUsername";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@loggedInUsername", loggedInUsername);

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Assuming you have a DataGridView named dataGridViewFeedback
                        dataGridView1.DataSource = dataTable;
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
