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
    public partial class TAStatus : Form
    {
        private string connectionString = "Data Source=DELL\\SQLEXPRESS;Initial Catalog=db_project;Integrated Security=True";

        private string loggedInUsername; // Variable to store the logged-in username

        public TAStatus(string username)
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
                    //string query = "SELECT * " +
                    //            "FROM AssignTaskTA " +
                    //            "WHERE facultyusername = @loggedInUsername " +
                    //            "ORDER BY CASE WHEN Status = 'Completed' THEN 1 ELSE 2 END, Deadline ASC";
                    string query = "SELECT * " +
                 "FROM AssignTaskTA " +
                 "WHERE facultyusername = @loggedInUsername AND Status IN ('Completed', 'Pending') " +
                 "ORDER BY CASE WHEN Status = 'Completed' THEN 1 ELSE 2 END, Deadline ASC";

                    //string query = "   SELECT* FROM AssignTaskTA WHERE UserID = @loggedInUsername";

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
            TaskStatus taskmen = new TaskStatus(loggedInUsername);
            taskmen.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
