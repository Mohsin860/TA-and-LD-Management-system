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
    public partial class LDtask : Form
    {
        private string connectionString = "Data Source=DELL\\SQLEXPRESS;Initial Catalog=db_project;Integrated Security=True";
        private string loggedInUsername;
        public LDtask(string usernaem)
        {
            InitializeComponent();
            loggedInUsername = usernaem;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Validate input fields
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox4.Text))
            {
                MessageBox.Show("Please fill in all the fields.");
                return;
            }

            // Insert data into the AssignTaskTA table
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string insertQuery = "INSERT INTO AssignTaskLD (facultyusername,TaskName, Description, Deadline, UserID,courseID) " +
                                         "VALUES (@facultyusername,@TaskName, @Description, @Deadline, @UserID,@courseID)";

                    using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@facultyusername", loggedInUsername);
                        insertCommand.Parameters.AddWithValue("@TaskName", textBox1.Text);
                        insertCommand.Parameters.AddWithValue("@Description", textBox2.Text);
                        insertCommand.Parameters.AddWithValue("@Deadline", dateTimePicker1.Value.Date);
                        insertCommand.Parameters.AddWithValue("@UserID", textBox4.Text);
                        insertCommand.Parameters.AddWithValue("@courseID", textBox3.Text);
                        int rowsAffected = insertCommand.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Task assigned successfully!");
                            // Close the form or perform other actions if needed

                        }
                        else
                        {
                            MessageBox.Show("Task assignment failed. Please try again.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            this.Hide();
            Taskmenu taskmenu = new Taskmenu(loggedInUsername);
            taskmenu.ShowDialog();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
