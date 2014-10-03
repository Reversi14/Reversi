using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi
{
   public class Bord : Form
   {
      int muisX, muisY;
      int w = 6, h = 6;
      int gBlokje = 40;

      public Bord()
      {
         this.Width = 400;
         this.Height = 400;
         this.Paint += Bord_Paint;
         this.MouseClick += Bord_MouseClick;
      }

      void Bord_MouseClick(object sender, MouseEventArgs e)
      {
         muisX = e.X;
         muisY = e.Y;

         Invalidate();         
      }

      void Bord_Paint(object sender, PaintEventArgs e)
      {
         Graphics g = e.Graphics;
         Pen blackPen = new Pen(Brushes.Black, 1);

         int x;
         int y;
         int[,] bord;
         bord = new int[w, h];

         for (x = 0; x < h; x++)
         {

            for (y = 0; y < w; y++)
            {
               g.DrawRectangle(blackPen, gBlokje * x, gBlokje * y, gBlokje, gBlokje);
               g.DrawRectangle(blackPen, gBlokje * x, gBlokje * y, gBlokje, gBlokje);
            }
         }

         for (x = (h / 2) -1; x <= (h / 2); x++ )
         {
            for (y = (h / 2) -1; y <= (h / 2); y++ )
            {
               g.FillEllipse(Brushes.Red, 40* x + 3, 40* y + 3, gBlokje - 6, gBlokje - 6);
            }
         }

            g.FillEllipse(Brushes.Red, muisX - (gBlokje / 2), muisY - (gBlokje / 2), gBlokje - 6, gBlokje - 6);
      }
      static void Main()
      {
         Application.Run(new Bord());
      }
   }
}
