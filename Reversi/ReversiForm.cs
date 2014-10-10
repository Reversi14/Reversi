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
            bord = new Bord(6, 6);
            rect = new Rectangle[bord.w, bord.h];
            steen = new Rectangle[bord.w, bord.h];

            this.Paint += ReversiForm_Paint;
            panel1.Paint += panel1_Paint;
            panel1.MouseClick += panel1_MouseClick;            
        }

        void ReversiForm_Paint(object sender, PaintEventArgs pea)
        {
            // Tekent een rode en blauwe steen op het form.
            pea.Graphics.DrawImage(Reversi.Properties.Resources.img_blue, 105, 68, 15, 15);
            pea.Graphics.DrawImage(Reversi.Properties.Resources.img_red, 105, 95, 15, 15);
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

            label1.Text = string.Format("heeft {0} veld(en)", bord.Score(Bord.BLAUW));
            label2.Text = string.Format("heeft {0} veld(en)", bord.Score(Bord.ROOD));

            if ((bord.Score(Bord.BLAUW) + bord.Score(Bord.ROOD)) == (bord.w * bord.h))
            {
                if (bord.Score(Bord.BLAUW) > bord.Score(Bord.ROOD))
                {
                    label3.Text = String.Format("Blauw heeft gewonnen!");
                }
                else if (bord.Score(Bord.ROOD) > bord.Score(Bord.BLAUW))
                {
                    label3.Text = String.Format("Rood heeft gewonnen!");
                }
                else
                {
                    label3.Text = String.Format("Revanche!?");
                }
            }
            else
            {
                label3.Text = String.Format("... aan zet");
            }
            
           
            Graphics g = pea.Graphics;
            Pen blackPen = new Pen(Brushes.Black, 1);
            // afstand en bepaling  voor de steen en hulp rechthoek.
            int a = 2;
            int b = 2 * a;
            int c = 2 * b;

            for (int i = 0; i < bord.w; i++)
            {
                for (int j = 0; j < bord.h; j++)
                {
                    rect[i, j] = new Rectangle(i * breedteVakje, j * hoogteVakje, breedteVakje, hoogteVakje);
                    g.DrawRectangle(blackPen, rect[i, j]);

                    // Vormgeving van stenen.
                    // Optel en aftrek handelingen zorgen ervoor dat de steen kleiner dan de rechthoek is
                    // en in het midden van de rechthoek.
                    steen[i, j] = new Rectangle(i * breedteVakje + a, j * hoogteVakje + a, breedteVakje - b, hoogteVakje - b);
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

                    if (bord.geldigeZet(i, j) && hulp)
                    {
                        g.DrawRectangle(Pens.Black, i * breedteVakje + b, j * hoogteVakje + b, breedteVakje - c, hoogteVakje - c);
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bord.NieuwSpel();
            panel1.Invalidate();
        } 

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            hulp = !hulp;
            panel1.Invalidate();
        }
       
    }
}
