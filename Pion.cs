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
            int kleurwaarde = rng.Next(1, 7);
            switch (kleurwaarde)
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
            switch (Kleur)
            {
                case 'R':
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    break;
                case 'G':
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    break;
                case 'B':
                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    break;
                case 'Y':
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    break;
                case 'P':
                    Console.BackgroundColor = ConsoleColor.Magenta;
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;
                case 'O':
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
            }
            Console.Write($" {Kleur} ");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($" ");
        }
    }
}
