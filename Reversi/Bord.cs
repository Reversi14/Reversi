﻿using System;
using System.Collections.Generic;
using System.Drawing;
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
                CheckMove(x, y);
                
            }
        }
        private void wisselBeurt()
        {
            beurt = !beurt;
        }
        private void switchColor(int x1, int x2, int y1, int y2, int dx, int dy)
        {
            int p = x1;
            int q = y1;

            int b;

            if (beurt)
                b = BLAUW;
            else
                b = ROOD;

            while ( (p > x2 && dx == -1) || (p < x2 && dx == 1) || (q > y2 && dy == -1) || (q < y2 && dy == 1))
            {
                Stenen[p, q] = b;

                p += dx;
                q += dy;
            }


        }
        private void CheckMove(int x, int y)
        {
            int b;
            int dx;
            int dy;

            if (beurt)
                b = BLAUW;
            else
                b = ROOD;

            for (dx = -1; dx <= 1; dx++)
            {
                for (dy = -1; dy <= 1; dy++)
                {
                    if (!(dx == 0 && dy == 0))
                    {
                        int i = x + dx;
                        int j = y + dy;
                        while (i >= 0 && j >= 0 && i < w && j < h)
                        {
                            if (isLeeg(i, j))
                                break;
                            if (Stenen[i, j] == b)
                            {
                                if ((i > x + 1 && dx == 1) || (i < x - 1 && dx == -1) || (j > y + 1 && dy == 1) || (j < y - 1 && dy == -1))
                                {
                                    switchColor(x, y, i, j, dx, dy);
                                    break;
                                }
                                else
                                    break;
                            }
                            if (Stenen[i, j] != b)
                            {
                                i += dx;
                                j += dy;
                            }
                        }
                    }
                }
            }
        }


        private bool validMove(int x, int y)
        {
            int b;
            int dx;
            int dy;

            if (beurt)
                b = BLAUW;
            else
                b = ROOD;

            for ( dx = -1; dx <= 1; dx++)
            {
                for ( dy = -1; dy <= 1; dy++)
                {
                    if( !(dx == 0 && dy == 0))
                    {
                        int i = x + dx;
                        int j = y + dy;
                        while( i >= 0 && j >= 0 && i < w && j < h)
                        {
                            if (isLeeg(i, j))
                                break;
                            if (Stenen[i,j] == b)
                            {
                                if ((i > x + 1 && dx == 1) || (i < x - 1 && dx == -1) || (j > y + 1 && dy == 1) || (j < y - 1 && dy == -1))
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
