using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reversi
{
    class Bord : Form //super class
    {
        int w = 6;                  //breedte bord
        int h = 6;                  //hoogte bord
        int gBlokje = 40;           //breedte en hoogte (grootte) van elk blokje
        int muisX, muisY;

        public Bord()
        {
            this.Width = 400;
            this.Height = 400;
            this.Paint += this.TekenBord;
            this.MouseClick += Bord_MouseClick;
            this.MouseMove += Bord_MouseMove;
        }

        void Bord_MouseMove(object sender, MouseEventArgs e)
        {
            
        }

        public void Bord_MouseClick(object sender, MouseEventArgs e)
        {
            muisX = e.X;
            muisY = e.Y;
            Invalidate();
            DoubleBuffered = true;
        }
        public void TekenBord(object obj, PaintEventArgs pea)
        {
            int [,] Bord;
            Bord = new int[w, h];
            Button[,] Vakjes;
            Vakjes = new Button[w, h];

            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    Bord[i, j] = 0;
                    Vakjes[i, j] = new Button();
                    Vakjes[i, j].Size = new Size(gBlokje, gBlokje);
                    Vakjes[i, j].Location = new Point(i * gBlokje, j * gBlokje);
                    Vakjes[i, j].BackColor = Color.Black;
                }
            }
                for (int x = 0; x < w; x++)
                {
                    for (int y = 0; y < h; y++)
                        pea.Graphics.DrawRectangle(Pens.Black, x * gBlokje, y * gBlokje, gBlokje, gBlokje);
                }
            pea.Graphics.FillEllipse(Brushes.Red, muisX - (gBlokje / 2), muisY - (gBlokje / 2), gBlokje, gBlokje);
                
        }
        static void Main()
        {
            Application.Run(new Bord());
        }
    }

}
