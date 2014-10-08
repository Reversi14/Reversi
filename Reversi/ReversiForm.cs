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
        Rectangle[,] steen;                          // Stenen in de array
        Rectangle[,] rect;                           // Rechthoekige array 
        Bord bord;                                   // Waardes in de array
        int X, Y;                                    // Muis X en Y locatie

        public ReversiForm()
        {
            InitializeComponent();
            bord = new Bord(6,6);
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
                    // Vormgeving bord + alle arrays krijgen waarde -1
                    rect[i, j] = new Rectangle(i * breedteVakje, j * hoogteVakje, breedteVakje, hoogteVakje);
                    g.DrawRectangle(blackPen, rect[i, j]);

                    // Vormgeving van stenen.
                    // Optel en aftrek handelingen zorgen ervoor dat de steen kleiner dan de rechthoek is
                    // en in het midden van de rechthoek.
                    steen[i, j] = new Rectangle(i * breedteVakje + 2, j * hoogteVakje + 2, breedteVakje - 3, hoogteVakje - 3);
                }
            }

            // Deze waardebepaling hierboven kleur steen toewijzen.
            for (int i = 0; i < bord.w; i++)
            {
                for (int j = 0; j < bord.h; j++)
                {
                    if (bord.Stenen[i, j] == Bord.BLAUW) // blauwe stenen
                        g.DrawImage(Reversi.Properties.Resources.img_blue, steen[i, j]);

                    else if (bord.Stenen[i, j] == Bord.ROOD) // rode stenen
                        g.DrawImage(Reversi.Properties.Resources.img_red, steen[i, j]);
                }
            }
        }
    }
}
