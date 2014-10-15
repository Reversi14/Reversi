using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi
{
    class Bord
    {
        public int[,] Stenen;                           // Int-array voor de stenen.
        public int w;                                   // Aantal vakjes breed.
        public int h;                                   // Aantal vakjes hoog.
        public bool beurt;                              // Bool om te kijken wie er aan de beurt is.
        public const int Leeg = -1;                     // Als int-array is '-1' lege rechthoek
        public const int Player1 = 0;                   // Als '0' is de rechthoek van blauw en wordt er een blauwe steen ingezet.
        public const int Player2 = 1;                   // Ditzelfde geldt voor rood bij de waarde '1'.

        public Bord(int w, int h)
        {
            this.w = w;
            this.h = h;

            // Grootte int-array stenen in 'w' en 'h'.
            Stenen = new int[w, h];

            // Initialiseren van methode nieuwSpel
            nieuwSpel();
        }

        public void nieuwSpel()
        {
            // Declaratie hier zorgt ervoor dat blauw altijd begint.
            beurt = true;

            // Declaraties van bord waarden.
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    Stenen[i, j] = Leeg;
                }
            }
            // Midden bord horizontaal en verticaal.
            int a = w / 2;
            int b = h / 2;

            // Beginstand bord d.m.v. waardebepaling.
            Stenen[a, b] = Player1;
            Stenen[a - 1, b - 1] = Player1;

            Stenen[a - 1, b] = Player2;
            Stenen[a, b - 1] = Player2;
        }

        #region Definities vakjes waarde (isLeeg, -Player1 en -Player2)

        // Makkelijke afkortingen voor veelgebruikte bools.
        public bool isLeeg(int x, int y)
        {
            return Stenen[x, y] == Leeg;
        }
        public bool isPlayer1(int x, int y)
        {
            return Stenen[x, y] == Player1;
        }
        public bool isPlayer2(int x, int y)
        {
            return Stenen[x, y] == Player2;
        }
        #endregion

        // Geeft einde van het spel aan als het bord vol is of
        // er geen mogelijke zet is voor de huidige speler.
        public bool eindSpel()
        {
            for (int p = 0; p < w; p++)
                for (int q = 0; q < h; q++)
                {
                    if (geldigeZet(p, q))
                        return false;
                }
            wisselBeurt();
            for (int p = 0; p < w; p++)
                for (int q = 0; q < h; q++)
                {
                    if (geldigeZet(p, q))
                        return false;
                }
            return true;
        }

        // Deze methode geeft aan of je daar een steen mag neerleggen,
        // wisselt kleur van de stenen die ertussen liggen en
        // wisselt de beurt.
        public void Klik(int x, int y)
        {
            if (isLeeg(x, y) && geldigeZet(x, y))
            {
                if (beurt)
                    Stenen[x, y] = Player1;
                else
                    Stenen[x, y] = Player2;

                omslagKleur(x, y);
                wisselBeurt();

            }
        }

        // Beurtwissel.
        private void wisselBeurt()
        {
            beurt = !beurt;
        }

        // Zorgt voor de omslag van kleur van tussenliggende stenen.
        private void omslagKleur(int x, int y)
        {
            int spelerBeurt;
            int dx;
            int dy;

            if (beurt)
                spelerBeurt = Player1;
            else
                spelerBeurt = Player2;

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
                            if (Stenen[i, j] == spelerBeurt)
                            {
                                if ((i > x + 1 && dx == 1) || (i < x - 1 && dx == -1) || (j > y + 1 && dy == 1) || (j < y - 1 && dy == -1))
                                {
                                    wisselKleur(x, y, i, j, dx, dy);
                                    break;
                                }
                                else
                                    break;
                            }
                            if (Stenen[i, j] != spelerBeurt)
                            {
                                i += dx;
                                j += dy;
                            }
                        }
                    }
                }
            }
        }

        // Wisselt de kleur van tussenliggende stenen.
        private void wisselKleur(int x1, int y1, int x2, int y2, int dx, int dy)
        {
            int spelerBeurt;
            int p = x1;
            int q = y1;

            if (beurt)
                spelerBeurt = Player1;
            else
                spelerBeurt = Player2;

            while ((p > x2 && dx == -1) || (p < x2 && dx == 1) || (q > y2 && dy == -1) || (q < y2 && dy == 1))
            {
                Stenen[p, q] = spelerBeurt;

                p += dx;
                q += dy;
            }
        }

        // Checkt of er een zet mogelijk is.
        public bool geldigeZet(int x, int y)
        {
            int spelerBeurt;
            int dx;
            int dy;

            if (beurt)
                spelerBeurt = Player1;
            else
                spelerBeurt = Player2;

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
                            if (Stenen[i, j] == spelerBeurt)
                            {
                                if (((i > x + 1 && dx == 1) || (i < x - 1 && dx == -1) || (j > y + 1 && dy == 1) || (j < y - 1 && dy == -1)) && isLeeg(x, y))
                                    return true;
                                else
                                    break;
                            }
                            if (Stenen[i, j] != spelerBeurt)
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

        // Geeft aantal stenen van de spelers.
        public int Score(int Speler)
        {
            int resultaat = 0;
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    if (Stenen[i, j] == Speler)
                    {
                        resultaat++;
                    }
                }
            }
            return resultaat;
        }

        // Geeft aan wie er aan de beurt is.
        public string Spelers()
        {
            string resultaat = "";

            if (beurt)
                return resultaat += "Blauw";
            else
                return resultaat += "Rood";
        }

        // Laat zien wie er een hogere score heeft.
        public string hoogsteScore()
        {
            string resultaat = "";

            if (Score(Player1) > Score(Player2))
                return resultaat += "Blauw";
            else
                return resultaat += "Rood";
        }
    }
}
