using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mastermind
{
    public class Pin : Kleurvakje
    {
        public override void ToonKleur()
        {
            base.ToonKleur();
            Console.Write($"{Kleur}");
            Console.ResetColor();
            Console.Write($" ");
        }
    }
}
