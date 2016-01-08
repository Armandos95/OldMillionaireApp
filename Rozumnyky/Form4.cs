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
    public partial class Form4 : Form
    {
        private string pictureName;

        public Form4(string picName)
        {
            InitializeComponent();
            pictureName = picName;
            this.pictureBox1.Image = (Bitmap)Image.FromFile(Application.StartupPath + "\\photos\\" + picName + ".png");
        }
    }
}
