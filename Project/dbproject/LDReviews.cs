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
    public partial class LDReviews : Form
    {
        private string connectionString = "Data Source=DELL\\SQLEXPRESS;Initial Catalog=db_project;Integrated Security=True";
        private string loggedInUsername; // Variable to store the logged-in username
        public LDReviews(string username)
        {
            InitializeComponent();
            loggedInUsername = username; // Set the logged-in username
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Retrieve feedback for the logged-in TA
                    string query = "SELECT A.TaskName, FT.FeedbackText, FT.Rating, FT.DateTimeProvided, C.CourseName, F.Username AS Faculty_Username, CONCAT(F.FName, ' ', F.LName) AS Faculty_Name " +
                              "FROM FeedbackLD FT " +
                              "JOIN AssignTaskLD A ON FT.TaskID = A.TaskID " +
                              "JOIN Courses C ON A.courseID = C.CourseID " +
                              "JOIN faculty F ON A.facultyusername = F.Username " +
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

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            LDsHome viewlstask = new LDsHome(loggedInUsername);
            viewlstask.ShowDialog();
        }
    }
}
