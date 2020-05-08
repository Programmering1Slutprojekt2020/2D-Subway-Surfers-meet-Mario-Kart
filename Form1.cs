using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2D_Subway_Surfers_meet_Mario_Kart
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A || e.KeyCode == Keys.Left)
            {
                if(Car.Left>50)
                Car.Left += -55;
            }
            if (e.KeyCode == Keys.D || e.KeyCode == Keys.Right)
            {
                if (Car.Left < 200)
                Car.Left += 55;
            }
        }
    }
}
