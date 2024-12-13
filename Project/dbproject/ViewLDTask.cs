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
    public partial class ViewLDTask : Form
    {
        private string connectionString = "Data Source=DELL\\SQLEXPRESS;Initial Catalog=db_project;Integrated Security=True";
        private string ldUsername;

        public ViewLDTask(string taUsername)
        {
            InitializeComponent();
            this.ldUsername = taUsername;
            // LoadAssignedTasks();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            LDsHome ldhome = new LDsHome(ldUsername);
            ldhome.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT A.TaskID, A.TaskName, A.Description, A.Deadline, A.Status, C.CourseName, F.Username AS Faculty_ID, CONCAT(F.FName, ' ', F.LName) AS Faculty_Name " +
                                   "FROM AssignTaskLD A " +
                                   "JOIN LDs T ON A.UserID = T.Username " +
                                   "JOIN Courses C ON A.courseID = C.CourseID " +
                                   "JOIN faculty F ON A.facultyusername = F.Username " +
                                   "WHERE A.UserID = @UserID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", ldUsername);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Display tasks in the DataGridView
                    dataGridView1.DataSource = dataTable;
                }
            }
        }
    }
}
