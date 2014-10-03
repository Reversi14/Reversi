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

      int w = 6;                  //breedte bord
      int h = 6;                  //hoogte bord
      int gBlokje = 40;           //breedte en hoogte (grootte) van elk blokje
      int muisX, muisY;

      public ReversiForm()
      {
         InitializeComponent();
         panel1.Paint += this.TekenBord;
         panel1.MouseClick += Bord_MouseClick;
         panel1.MouseMove += Bord_MouseMove;
      }

      void Bord_MouseMove(object sender, MouseEventArgs e)
      {

      }

      public void Bord_MouseClick(object sender, MouseEventArgs e)
      {
         muisX = e.X;
         muisY = e.Y;
         panel1.Invalidate();
         DoubleBuffered = true;
      }
      public void TekenBord(object obj, PaintEventArgs pea)
      {
         int[,] Bord;
         Bord = new int[w, h];


         for (int x = 0; x < w; x++)
         {
            for (int y = 0; y < h; y++)
               pea.Graphics.DrawRectangle(Pens.Black, x * gBlokje, y * gBlokje, gBlokje, gBlokje);
         }

         pea.Graphics.FillEllipse(Brushes.Red, muisX - (gBlokje / 2), muisY - (gBlokje / 2), gBlokje - 6, gBlokje - 6);


      }
   }
}
