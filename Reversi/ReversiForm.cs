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
        Rectangle[,] steen;                         // Array voor de stenen.
        Rectangle[,] rect;                          // Array voor de rechthoeken.
        Bord bord;                                  // Verwijzing naar Bord klasse
        int X, Y;                                   // Muis X en Y declaratie
        bool hulp;                                  // Bool voor hulpknop.

        public ReversiForm()
        {
            InitializeComponent();
            hulp = false;                           // Hulpknop staat door 'false'-waarde uit in het begin.
            bord = new Bord(6, 6);                  // Hoogte en breedte instellen van bord.
            rect = new Rectangle[bord.w, bord.h];   // aantal rechthoeken in breedte en hoogte, resp. 'w' en 'h'.
            steen = new Rectangle[bord.w, bord.h];  // aantal stenen                  "

            // Methoden voor het Form
            this.Paint += ReversiForm_Paint;
            panel1.Paint += panel1_Paint;
            panel1.MouseClick += panel1_MouseClick;
        }

        void ReversiForm_Paint(object sender, PaintEventArgs pea)
        {
            // Tekent een rode en blauwe steen op het Form.
            pea.Graphics.FillEllipse(Brushes.DarkBlue, 105, 70, 15, 15);
            pea.Graphics.FillEllipse(Brushes.Red, 105, 95, 15, 15);
        }

        public void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            // Muislocatie
            X = e.X;
            Y = e.Y;

            // Bepaling in welke rechthoek je zit met je muis.
            int i, j;
            i = X / (panel1.Width / bord.w);
            j = Y / (panel1.Height / bord.h);

            // Initialiseren van methode Klik in klasse Bord.
            bord.Klik(i, j);

            // Toepassen op panel.
            panel1.Invalidate();
            DoubleBuffered = true;
        }

        public void panel1_Paint(object obj, PaintEventArgs pea)
        {
            // Declaratie grootte vakjes, de '-1' zorgt ervoor dat er beneden en links 
            // niet buiten het panel wordt getekend.
            int breedteVakje = (panel1.Width - 1) / bord.w;
            int hoogteVakje = (panel1.Height - 1) / bord.h;

            // Updaten van scores van beide spelers.
            label1.Text = string.Format("heeft {0} veld(en)", bord.Score(Bord.Player1));
            label2.Text = string.Format("heeft {0} veld(en)", bord.Score(Bord.Player2));

            // Tijdens het spel zeggen wie er aan de beurt is en
            // aan het einde van het spel (vol bord of geen mogelijke zetten) uitslag geven.
            if (bord.eindSpel())
            {
                if (bord.Score(Bord.Player1) > bord.Score(Bord.Player2))
                {
                    label3.Text = String.Format("{0} heeft gewonnen!", bord.hoogsteScore());
                }
                else if (bord.Score(Bord.Player2) > bord.Score(Bord.Player1))
                {
                    label3.Text = String.Format("{0} heeft gewonnen!", bord.hoogsteScore());
                }
                else
                {
                    label3.Text = String.Format("Revanche!?");
                }
            }
            else
            {
                label3.Text = String.Format("{0} aan zet...", bord.Spelers());
                if (bord.beurt)
                    label3.ForeColor = Color.DarkBlue;
                else
                    label3.ForeColor = Color.Red;

            }


            Graphics g = pea.Graphics;
            Pen blackPen = new Pen(Brushes.Black, 1);

            // Plaats bepalen voor de steen en hulp rechthoek d.m.v. constanten.
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
                    if (bord.isPlayer1(i, j)) // blauwe stenen
                        g.FillEllipse(Brushes.DarkBlue, steen[i, j]);

                    else if (bord.isPlayer2(i, j)) // rode stenen
                        g.FillEllipse(Brushes.Red, steen[i, j]);

                    // Hulp rechthoeken tekenen als 'help' is aangevinkt.
                    if (bord.geldigeZet(i, j) && hulp)
                    {
                        if (bord.beurt)
                            g.DrawRectangle(Pens.DarkBlue, i * breedteVakje + b, j * hoogteVakje + b, breedteVakje - c, hoogteVakje - c);
                        else
                            g.DrawRectangle(Pens.Red, i * breedteVakje + b, j * hoogteVakje + b, breedteVakje - c, hoogteVakje - c);
                    }
                }
            }
        }

        // Nieuw spel button.
        private void button1_Click(object sender, EventArgs e)
        {
            bord.nieuwSpel();
            panel1.Invalidate();
        }

        // Help checkbox.
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            hulp = !hulp;
            panel1.Invalidate();
        }

    }
}
