using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mastermind
{
    public class Pion : Kleurvakje
    {
        static readonly Random rng = new();
        public Pion()
        {
            switch (rng.Next(1, 7))
            {
                case 1:
                    Kleur = 'R'; // Rood (red)
                    break;
                case 2:
                    Kleur = 'G'; // Groen (green)
                    break;
                case 3:
                    Kleur = 'B'; // Blauw (blue)
                    break;
                case 4:
                    Kleur = 'Y'; // Geel (yellow)
                    break;
                case 5:
                    Kleur = 'P'; // Paars (purple)
                    break;
                case 6:
                    Kleur = 'O'; // Oranje (orange)
                    break;
            }
        }
        public override bool Equals(object? obj)
        {
            if (obj is Pion temp)
            {
                return (temp.Kleur == Kleur);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override void ToonKleur()
        {
            base.ToonKleur();
            Console.Write($" {Kleur} ");
            Console.ResetColor();
            Console.Write($" ");
        }
    }
}
