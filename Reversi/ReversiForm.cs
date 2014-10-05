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
      Rectangle[,] rect;
      int[,] Bord;
      int w = 6;                  //breedte bord
      int h = 6;                  //hoogte bord
      int gBlokje = 40;           //breedte en hoogte (grootte) van elk blokje
      int muisX, muisY;

      public ReversiForm()
      {
         InitializeComponent();
         Bord = new int[w, h];
         rect = new Rectangle[w, h];
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
          Graphics g = pea.Graphics;
          Pen blackPen = new Pen(Brushes.Black, 1);
          Brush Blue = new SolidBrush(Color.Blue);
          Brush Red = new SolidBrush(Color.Red);



          for (int x = 0; x < w; x++)
          {
              for (int y = 0; y < h; y++)
              {
                  Bord[x, y] = -1;
              }
          }
          /*
          Installeert de beginstenen
           */
          Bord[w / 2 - 1, h / 2 - 1] = 0;
          Bord[w / 2, h / 2] = 0;
          Bord[w / 2 - 1, h / 2] = 1;
          Bord[w / 2, h / 2 - 1] = 1;

          /* 
          Installeert de vakjes
           */
          for (int x = 0; x < w; x++)
          {
              for (int y = 0; y < h; y++)
              {
                  rect[x, y] = new Rectangle(x * (panel1.Width / w), y * (panel1.Height / h), panel1.Width / w, panel1.Height / h);
              }
          }

          for (int x = 0; x < w; x++)
          {
              for (int y = 0; y < h; y++)
              {
                  g.DrawRectangle(blackPen, rect[x, y]);
              }
          }

          for (int x = 0; x < w; x++)
          {
              for (int y = 0; y < h; y++)
              {
                  if (Bord[x, y] == 0)
                  {
                      g.FillEllipse(Blue, rect[x, y]);
                  }
                  else if (Bord[x, y] == 1)
                  {
                      g.FillEllipse(Red, rect[x, y]);
                  }
              }
          }
      }
   }
}
