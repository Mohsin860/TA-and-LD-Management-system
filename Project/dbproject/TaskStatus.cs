using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dbproject
{
    public partial class TaskStatus : Form
    {
        private string connectionString = "Data Source=DELL\\SQLEXPRESS;Initial Catalog=db_project;Integrated Security=True";

        private string loggedInUsername; // Variable to store the logged-in username

        public TaskStatus(string username)
        {
            InitializeComponent();
            loggedInUsername = username; // Set the logged-in username
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            TAStatus ldstat = new TAStatus(loggedInUsername);
            ldstat.ShowDialog();


        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            LDstatus ldstat = new LDstatus(loggedInUsername);
            ldstat.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form4 form4 = new Form4(loggedInUsername);
            form4.ShowDialog();

        }
    }
}
