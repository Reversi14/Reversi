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
      bool beurt = false;                          // beurt aangeven
      Rectangle[,] steen;                          // stenen in de array
      Rectangle[,] rect;                           // rechthoekige array      
      int[,] Bord;                                 // waardes in de array
      int w = 8;                                   // aantal vakjes breed
      int h = 8;                                   // aantal vakjes hoog
      int X, Y;                                    // muis X en Y locatie
      
      public ReversiForm()
      {
         InitializeComponent();
         Bord = new int[w, h];
         rect = new Rectangle[w, h];
         steen = new Rectangle[w, h];

         this.Paint += ReversiForm_Paint;
         panel1.Paint += panel1_Paint;
         panel1.MouseClick += panel1_MouseClick;
         panel1.MouseMove += panel1_MouseMove;
      }
      void ReversiForm_Paint(object sender, PaintEventArgs pea)
      {
         // Tekent een rode en blauwe steen bij aantalen stenen voor de spelers.
         Graphics g = pea.Graphics;
         g.DrawImage(Reversi.Properties.Resources.img_blue, 65, 68, 15, 15);
         g.DrawImage(Reversi.Properties.Resources.img_red, 65, 95, 15, 15);
      }

      void panel1_MouseMove(object sender, MouseEventArgs e)
      {
         this.Invalidate();
         DoubleBuffered = true;
      }

      public void panel1_MouseClick(object sender, MouseEventArgs e)
      {
          X = e.X;
          Y = e.Y;

          int i, j;
          i = X / (panel1.Width / w);
          j = Y / (panel1.Height / h);

          this.Text = String.Format("{0},{1},{2}", Bord[i, j], i, j);

          if ( Bord[i, j] == -1)
          {
              if (beurt)
                  Bord[i, j] = 0;
              else
                  Bord[i, j] = 1;
              
              beurt = !beurt;
          }
          panel1.Invalidate();
      }

      public void panel1_Paint(object obj, PaintEventArgs pea)
      {
         int breedteVakje = panel1.Width / w;
         int hoogteVakje = panel1.Height / h;

         Graphics g = pea.Graphics;
         Pen blackPen = new Pen(Brushes.Black, 1);  

         for (int i = 0; i < w; i++)
         {
            for (int j = 0; j < h; j++)
            {
               // Vormgeving bord + alle arrays krijgen waarde -1
               rect[i, j] = new Rectangle(i * breedteVakje, j * hoogteVakje, breedteVakje, hoogteVakje);
               g.DrawRectangle(blackPen, rect[i, j]);               

               // Vormgeving van stenen.
               // Optel en aftrek handelingen zorgen ervoor dat de steen kleiner dan de rechthoek is
               // en in het midden van de rechthoek.
               steen[i, j] = new Rectangle(i * breedteVakje + 2, j * hoogteVakje + 2, breedteVakje - 3, hoogteVakje - 3);

               Bord[i, j] = -1;
            }
         }
         int a = w / 2;    // midden bord horizontaal.
         int b = h / 2;    // midden bord verticaal.

         // Beginstand bord d.m.v. waardebepaling.
         Bord[a, b] = 0;
         Bord[a - 1, b - 1] = 0;         

         Bord[a - 1, b] = 1;
         Bord[a, b - 1] = 1;

         // Deze waardebepaling hierboven kleur steen toewijzen.
         for (int i = 0; i < w; i++)
         {
            for (int j = 0; j < h; j++)
            {               
               if (Bord[i, j] == 0) // blauwe stenen
                  g.DrawImage(Reversi.Properties.Resources.img_blue, steen[i, j]);

               else if (Bord[i, j] == 1) // rode stenen
                  g.DrawImage(Reversi.Properties.Resources.img_red, steen[i, j]);     
            }
         }
      }
   }
}
