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
    public partial class SignUp : Form
    {
        public SignUp()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();

            FacultyReg facultyreg = new FacultyReg();
            facultyreg.ShowDialog();
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();

            TAsReg tasreg = new TAsReg();
            tasreg.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            LDsReg ldsreg = new LDsReg();
            ldsreg.ShowDialog();
        }
    }
}
