using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rozumnyky
{
    public partial class Form1 : Form
    {
        private int amountOfFFF;
        private int amountOfQuestions;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] fileRows = File.ReadAllLines(Application.StartupPath + "//settings.ini");
            amountOfFFF = Convert.ToInt32(fileRows[0]);
            amountOfQuestions = Convert.ToInt32(fileRows[1]);
        }

        private void buttonFFF_Click(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2(amountOfFFF);
            frm2.ShowDialog();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonMainGame_Click(object sender, EventArgs e)
        {
            Form3 frm3 = new Form3(amountOfQuestions);
            frm3.ShowDialog();
        }
    }
}
