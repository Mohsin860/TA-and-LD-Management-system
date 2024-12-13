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
    public partial class LDstatus : Form
    {
        private string connectionString = "Data Source=DELL\\SQLEXPRESS;Initial Catalog=db_project;Integrated Security=True";

        private string loggedInUsername; // Variable to store the logged-in username

        public LDstatus(string username)
        {
            InitializeComponent();
            loggedInUsername = username; // Set the logged-in username
        }
        public LDstatus()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Taskmenu taskmen = new Taskmenu(loggedInUsername);
            taskmen.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();


                    string query = "SELECT Status, COUNT(*) as TaskCount " +
                "FROM AssignTasLD " +
                "WHERE facultyusername = @loggedInUsername " +
                "GROUP BY Status " +
                "HAVING Status IN ('Completed', 'Pending') " +
                "ORDER BY CASE WHEN Status = 'Completed' THEN 1 ELSE 2 END, Deadline ASC";



                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@loggedInUsername", loggedInUsername);

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
    }
}
