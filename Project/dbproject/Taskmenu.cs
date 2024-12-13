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
    public partial class Taskmenu : Form
    {
        private string loggedInUsername;
        public Taskmenu(string username)
        {
            InitializeComponent();
            loggedInUsername = username;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            TAtask tas = new TAtask(loggedInUsername);
            tas.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            LDtask tas = new LDtask(loggedInUsername);
            tas.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form4 form4 = new Form4(loggedInUsername);
            form4.ShowDialog();

        }
    }
}
