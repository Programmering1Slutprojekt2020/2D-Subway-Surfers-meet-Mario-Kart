using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace _2D_Subway_Surfers_meet_Mario_Kart
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ConeList.AddRange(new List<PictureBox>() { Cone0, Cone1, Cone2, Cone3, Cone4, Cone5, Cone6, Cone7 }); //lägger till cone i en lista
        }


        Random r = new Random(); //random
        int _ticks; // variabel för att hålla koll hur långt man kommit 
        int Lose_cashe = 0; //cashe för att hålla 3 liv
        List<PictureBox> ConeList = new List<PictureBox>();//skapar lista med alla hinder


        private void Form1_KeyDown(object sender, KeyEventArgs e)//alla knappar
        {
            if (e.KeyCode == Keys.A || e.KeyCode == Keys.Left) //för att styra
            {
                if (Car.Left > 55)
                    Car.Left += -55;
            }
            if (e.KeyCode == Keys.D || e.KeyCode == Keys.Right) //för att styra
            {
                if (Car.Left < 255)
                    Car.Left += 55;
            }
            if (e.KeyCode == Keys.W || e.KeyCode == Keys.Up)//för att köra upp bilen 
            {
                if (Car.Top > 100)
                    Car.Top += -30;
            }
            if (e.KeyCode == Keys.S || e.KeyCode == Keys.Down)//för att få ner bilen 
            {
                if (Car.Top < 488)
                    Car.Top += 30;
            }
            if (e.KeyCode == Keys.Enter) //enter för att starta om spel
            {
                Application.Restart();
            }
        }


        void Random() //för att spåna hinder när koner är utanför banan
        {
            foreach (PictureBox Cone in ConeList)
            {
                if (((PictureBox)Cone).Top > 580) // när de är utanför
                {
                    int r1 = r.Next(1, 6);
                    r1 = r1 * 55;
                    ((PictureBox)Cone).Left = r1;
                    ((PictureBox)Cone).Top = -80;
                }
            }
        }


        void MoveCone()//för att flytta fram alla 
        {
            foreach (PictureBox Cone in ConeList)
            {
                Cone.Top += 3;
            }
        }


        void Gameover()//kör och kollar när bilen träffar konen
        {
            foreach (PictureBox Cone in ConeList)
            {
                if (Car.Bounds.IntersectsWith(((PictureBox)Cone).Bounds)) //när bilen kroickar med conens .bound
                {
                    Lose_cashe++; //lägger till när man förlorar ett liv
                    panel1.Invalidate();   // updaterar livkara
                    ((PictureBox)Cone).Top = -80; //flyttar den högst upp
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)//Huvudlopen
        {
            _ticks++; // varjabel för att hålla koll hur långt man kommit 
            Distance.Text = _ticks.ToString();
            MoveCone();
            Gameover();

            if (Lose_cashe == 3)//när alla liv är slut
            {
                timer1.Enabled = false; //stänger av klockan
                File.AppendAllText("Score.txt", _ticks.ToString() + Environment.NewLine); //sparar alla spelgångar i en txt fil
                panel2.Invalidate(); //för att uppdatera
                panel2.Visible = true; //tar fram end screan
            }
        }

        private void timer2_Tick(object sender, EventArgs e) //segare klocka för att flytta upp och spåna
        {
            Random();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)//målar ut liven
        {
            Graphics g = e.Graphics;
            SolidBrush b1 = new SolidBrush(Color.Orange);
            SolidBrush b2 = new SolidBrush(Color.Black);

            g.FillEllipse(b1, 7, 7, 26, 26);
            g.FillEllipse(b1, 40, 7, 26, 26);
            g.FillEllipse(b1, 73, 7, 26, 26);

            if (Lose_cashe == 3)
            {
                g.FillEllipse(b2, 10, 10, 20, 20);
            }
            if (Lose_cashe == 3 || Lose_cashe == 2)
            {
                g.FillEllipse(b2, 43, 10, 20, 20);
            }
            if (Lose_cashe > 1)
            {
                g.FillEllipse(b2, 76, 10, 20, 20);
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)//endscrean
        {
            Graphics g = e.Graphics;
            Font areal16 = new Font("Arial", 16);
            Font areal14 = new Font("Arial", 14);
            SolidBrush Fcolor = new SolidBrush(Color.Black);

            g.DrawString("GAME OVER", areal16, Fcolor, 75, 15);
            g.DrawString("Your score is " + _ticks + " m", areal16, Fcolor, 40, 40);

            string[] lines = File.ReadAllLines("Score.txt");//öppnar txt filen 
            int[] record = Array.ConvertAll(lines, s => int.Parse(s));//för till arrey
            Array.Sort(record);//soterar och vänder den
            Array.Reverse(record);

            g.DrawString("Leaderboard:", areal16, Fcolor, 80, 70);
            g.DrawString("1: " + (record[0]) + " m", areal16, Fcolor, 80, 90);
            g.DrawString("2: " + (record[1]) + " m", areal16, Fcolor, 80, 110);
            g.DrawString("3: " + (record[2]) + " m", areal16, Fcolor, 80, 130);
            g.DrawString("4: " + (record[3]) + " m", areal16, Fcolor, 80, 150);
            g.DrawString("5: " + (record[4]) + " m", areal16, Fcolor, 80, 170);

            if (record[0] > _ticks)
            {
                int Need = record[0] - _ticks;
                g.DrawString(Need + " m to brake record!", areal14, Fcolor, 25, 200);
            }
            else
            {
                g.DrawString("New Record!", areal16, Fcolor, 70, 200);
            }
            g.DrawString("Press Enter to Restart", areal16, Fcolor, 25, 250);

        }
    }
}
