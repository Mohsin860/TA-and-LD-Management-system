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
    public partial class Feedbackmenu : Form
    {
        private string connectionString = "Data Source=DELL\\SQLEXPRESS;Initial Catalog=db_project;Integrated Security=True";

        private string loggedusername;
        public Feedbackmenu(string username)
        {
            InitializeComponent();
            loggedusername = username;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();

            Form4 form4 = new Form4(loggedusername);
            form4.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            TAFeedback tafeedback = new TAFeedback(loggedusername);
            tafeedback.ShowDialog();
           

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            LDFeedback ldfeedback = new LDFeedback(loggedusername);
            ldfeedback.ShowDialog();
        }
    }
}
