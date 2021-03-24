using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibMandelbrot;

namespace Mandelbrot
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LibMandelbrot.Mandelbrot m = new LibMandelbrot.Mandelbrot();
            pictureBox1.BackgroundImage = m.Draw(int.Parse(textBox5.Text), 1920, 1080, double.Parse(textBox1.Text), double.Parse(textBox2.Text), double.Parse(textBox3.Text), double.Parse(textBox4.Text)); //-0.952465, -0.567696, -0.302106, -0.102307);
        }
    }
}
