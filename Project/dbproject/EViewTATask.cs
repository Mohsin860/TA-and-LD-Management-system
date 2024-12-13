using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace dbproject
{
    public partial class EViewTATask : Form
    {
        private string connectionString = "Data Source=DELL\\SQLEXPRESS;Initial Catalog=db_project;Integrated Security=True";

        private string loggedem;
        public EViewTATask(string username)
        {
            InitializeComponent();
            loggedem = username;
        }

        private void button1_Click(object sender, EventArgs e)
        {



            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Retrieve all LD tasks
                    string query = "SELECT A.TaskID, A.TaskName, A.Description, A.Deadline, A.Status, C.CourseName, T.username AS TAUsername " +
                            "FROM AssignTaskTA A " +
                            "JOIN Courses C ON A.courseID = C.CourseID " +
                            "JOIN TAs T ON A.UserID = T.username";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void AViewLDtask_Load(object sender, EventArgs e)
        {

        }

      

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            ATaskmenu tm = new ATaskmenu(loggedem);
            tm.ShowDialog();
        }
    }
}
