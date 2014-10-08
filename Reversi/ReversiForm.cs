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
   public partial class ReversiForm : Form  //ebola
   {
      Rectangle[,] rect;                           // rechthoekige array
      Rectangle[,] steen;                          // stenen in de array
      int[,] Bord;                                 // waardes in de array
      int w = 8;                                   // aantal vakjes breed
      int h = 8;                                   // aantal vakjes hoog
      int X, Y;
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

         panel1.Refresh();
      }

      public void panel1_Paint(object obj, PaintEventArgs pea)
      {
         int breedteVakje = panel1.Width / w;
         int hoogteVakje = panel1.Height / h;

         Graphics g = pea.Graphics;
         Pen blackPen = new Pen(Brushes.Black, 1);  

         for (int x = 0; x < w; x++)
         {
            for (int y = 0; y < h; y++)
            {
               // Vormgeving bord + alle arrays krijgen waarde -1
               rect[x, y] = new Rectangle(x * breedteVakje, y * hoogteVakje, breedteVakje, hoogteVakje);
               g.DrawRectangle(blackPen, rect[x, y]);

               Bord[x, y] = -1;

               // Vormgeving van stenen.
               // Optel en aftrek handelingen zorgen ervoor dat de steen kleiner dan de rechthoek is
               // en in het midden van de rechthoek.
               steen[x, y] = new Rectangle(x * breedteVakje + 2, y * hoogteVakje + 2, breedteVakje - 3, hoogteVakje - 3);
            }
         }
         int a = w / 2;    // midden bord horizontaal.
         int b = h / 2;    // midden bord verticaal.

         // Beginstand bord d.m.v. waardebepaling.
         Bord[a - 1, b - 1] = 0;
         Bord[a, b] = 0;

         Bord[a - 1, b] = 1;
         Bord[a, b - 1] = 1;

         // Deze waardebepaling hierboven kleur steen toewijzen.
         for (int x = 0; x < w; x++)
         {
            for (int y = 0; y < h; y++)
            {
               if (Bord[x, y] == 0) // blauwe stenen
                  g.DrawImage(Reversi.Properties.Resources.img_blue, steen[x, y]);

               else if (Bord[x, y] == 1) // rode stenen
                  g.DrawImage(Reversi.Properties.Resources.img_red, steen[x, y]);               
            }
         }
      }
   }
}
