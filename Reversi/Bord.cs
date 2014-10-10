using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi
{
    class Bord
    {
        public int[,] Stenen;
        public int w;
        public int h;
        public bool beurt = true;
        public const int LEEG = -1;
        public const int BLAUW = 0;
        public const int ROOD = 1;

        public Bord(int w, int h)
        {
            this.w = w;
            this.h = h;
            Stenen = new int[w, h];

            // Declaraties van bord waarden.
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    Stenen[i, j] = LEEG;
                }
            }
            // Midden bord horizontaal en verticaal.
            int a = w / 2;
            int b = h / 2;

            // Beginstand bord d.m.v. waardebepaling.
            Stenen[a, b] = BLAUW;
            Stenen[a - 1, b - 1] = BLAUW;

            Stenen[a - 1, b] = ROOD;
            Stenen[a, b - 1] = ROOD;
        }

        #region Definities vakjes waarde (isLeeg, -Blauw en -Rood)
        public bool isLeeg(int x, int y)
        {
            return Stenen[x, y] == LEEG;
        }
        public bool isBlauw(int x, int y)
        {
            return Stenen[x, y] == BLAUW;
        }
        public bool isRood(int x, int y)
        {
            return Stenen[x, y] == ROOD;
        }
        #endregion

        public void Click(int x, int y)
        {
            if (isLeeg(x, y) && validMove(x,y))
            {
                if (beurt)
                    Stenen[x, y] = BLAUW;
                else
                    Stenen[x, y] = ROOD;

                wisselBeurt();
                
            }
        }
        private void wisselBeurt()
        {
            beurt = !beurt;
        }

        #region Horizontaal Check
        private bool validMove(int x, int y)
        {
            int b;

            if (beurt)
                b = BLAUW;
            else
                b = ROOD;

            for ( int dx = -1; dx < 2; dx++)
            {
                for ( int dy = -1; dy < 2; dy++)
                {
                    if( !(dx == 0 && dy == 0))
                    {
                        int i = x + dx;
                        int j = y + dy;
                        while( i >= 0 && j >= 0 && i < x && j < y)
                        {
                            if (isLeeg(i, j))
                                break;
                            if (Stenen[i,j] == b)
                            {
                                if ((i > x + 1 && dx == 1) || (i < x - 1 && dx == 1) || (j > y + 1 && dy == 1) || (j < y - 1 && dy == 1) && isLeeg(i, j))
                                    return true;
                                else
                                    break;
                            }
                            if (Stenen[i, j] !=b)
                            {
                                i += dx;
                                j += dy;
                            }                            
                        }
                    }
                }
            }
            return false;
        }
        #endregion

        public int GetScore(int Speler)
        {
            int result = 0;
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    if (Stenen[i, j] == Speler)
                    {
                        result++;
                    }
                }
            }
            return result;
        }
    }
}
