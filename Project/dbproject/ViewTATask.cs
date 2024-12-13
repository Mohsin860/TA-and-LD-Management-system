using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace dbproject
{
    public partial class ViewTATask : Form
    {
        private string connectionString = "Data Source=DELL\\SQLEXPRESS;Initial Catalog=db_project;Integrated Security=True";
        private string taUsername;

        public ViewTATask(string taUsername)
        {
            InitializeComponent();
            this.taUsername = taUsername;
           // LoadAssignedTasks();
        }

       private void ViewTATask_Load(object sender, EventArgs e)
        {
                   }

      


        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT A.TaskID, A.TaskName, A.Description, A.Deadline, A.Status, C.CourseName, F.Username AS Faculty_ID, CONCAT(F.FName, ' ', F.LName) AS Faculty_Name " +
                                   "FROM AssignTaskTA A " +
                                   "JOIN TAs T ON A.UserID = T.Username " +
                                   "JOIN Courses C ON A.courseID = C.CourseID " +
                                   "JOIN faculty F ON A.facultyusername = F.Username " +
                                   "WHERE A.UserID = @UserID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserID", taUsername);

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Display tasks in the DataGridView
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
            TAs tas = new TAs(taUsername);
            tas.ShowDialog();

        }
    }

}
