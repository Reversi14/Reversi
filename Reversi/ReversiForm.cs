using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reversi
{
    public partial class ReversiForm : Form
    {
        Rectangle[,] steen;
        Rectangle[,] rect;
        Bord bord;
        int X, Y;
        bool hulp;
        
        public ReversiForm()
        {
            InitializeComponent();
            hulp = false;
            bord = new Bord(8,8);
            rect = new Rectangle[bord.w, bord.h];
            steen = new Rectangle[bord.w, bord.h];

            this.Paint += ReversiForm_Paint;
            panel1.Paint += panel1_Paint;
            panel1.MouseClick += panel1_MouseClick;            
        }

        void ReversiForm_Paint(object sender, PaintEventArgs pea)
        {
            // Tekent een rode en blauwe steen bij aantalen stenen voor de spelers.
            Graphics g = pea.Graphics;
            g.DrawImage(Reversi.Properties.Resources.img_blue, 65, 68, 15, 15);
            g.DrawImage(Reversi.Properties.Resources.img_red, 65, 95, 15, 15);
            

        }

        public void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            X = e.X;
            Y = e.Y;

            int i, j;
            i = X / (panel1.Width / bord.w);
            j = Y / (panel1.Height / bord.h);

            bord.Click(i, j);            
           
            panel1.Invalidate();
            DoubleBuffered = true;
        }

        public void panel1_Paint(object obj, PaintEventArgs pea)
        {
            int breedteVakje = panel1.Width / bord.w;
            int hoogteVakje = panel1.Height / bord.h;

            label1.Text = string.Format("heeft {0} veld(en)", bord.GetScore(Bord.BLAUW));
            label2.Text = string.Format("heeft {0} veld(en)", bord.GetScore(Bord.ROOD));
           
            Graphics g = pea.Graphics;
            Pen blackPen = new Pen(Brushes.Black, 1);

            for (int i = 0; i < bord.w; i++)
            {
                for (int j = 0; j < bord.h; j++)
                {
                    rect[i, j] = new Rectangle(i * breedteVakje, j * hoogteVakje, breedteVakje, hoogteVakje);
                    g.DrawRectangle(blackPen, rect[i, j]);

                    // Vormgeving van stenen.
                    // Optel en aftrek handelingen zorgen ervoor dat de steen kleiner dan de rechthoek is
                    // en in het midden van de rechthoek.
                    steen[i, j] = new Rectangle(i * breedteVakje + 2, j * hoogteVakje + 2, breedteVakje - 3, hoogteVakje - 3);
                }
            }

            // Kleur steen toewijzen.
            for (int i = 0; i < bord.w; i++)
            {
                for (int j = 0; j < bord.h; j++)
                {
                    if (bord.isBlauw(i, j)) // blauwe stenen
                        g.DrawImage(Reversi.Properties.Resources.img_blue, steen[i, j]);

                    else if (bord.isRood(i, j)) // rode stenen
                        g.DrawImage(Reversi.Properties.Resources.img_red, steen[i, j]);

                    if (bord.validMove(i, j) && hulp)
                    {
                        g.DrawRectangle(Pens.Black, i * breedteVakje + 4, j * hoogteVakje + 4, breedteVakje - 8, hoogteVakje - 8);
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            hulp = !hulp;
            Invalidate();
        }
        /* 
        public void UpdateStatus()
        {
            if ()
            {
                int blauw = bord.GetScore(Bord.BLAUW);
                int rood = bord.GetScore(Bord.ROOD);
                if ( blauw > rood )
                {
                    label3.Text = String.Format("{0} heeft gewonnen!", );
                }
                else if ( rood > blauw )
                {
                    label3.Text = String.Format("{0} heeft gewonnen!", );
                }
                else 
                {
                    label3.Text = String.Format("Het is een gelijkspel", );
                }
            }
            else 
            {
                label3.Text = String.Format("De beurt is aan {0}", );
            }
        }
        */
    }
}
