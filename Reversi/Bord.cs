﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi
{
    class Bord
    {
        public int[,] Stenen;
        public int w;                                   // Aantal vakjes breed
        public int h;                                   // Aantal vakjes hoog
        bool beurt = true;                              // Beurt aangeven
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
        bool isLeeg(int x, int y)
        {
            return Stenen[x, y] == LEEG;
        }

        public void Click(int x, int y)
        {
            if (isLeeg(x, y))
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
        public int GetScore(int Speler)
        {
            int result = 0;

            for (int i = 0; i < w; i++)
            {
                for (int j =0; j<h; j++)
                {
                    if(Stenen[i,j] == Speler)
                    {
                        result++;
                    }
                }
            }
            return result;
        }

    }
}
