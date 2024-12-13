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
    public partial class ATaskmenu : Form
    {

        private string connectionString = "Data Source=DELL\\SQLEXPRESS;Initial Catalog=db_project;Integrated Security=True";
        private string loggedem;
        public ATaskmenu(string username)
        {
            InitializeComponent();
            loggedem = username;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            admin ad = new admin(loggedem);
            ad.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            AViewLDtask fg = new AViewLDtask(loggedem);
            fg.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
           EViewTATask fg = new EViewTATask(loggedem);
            fg.ShowDialog();
        }
    }
}
