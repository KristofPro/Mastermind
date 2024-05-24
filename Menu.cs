using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mastermind
{
    public static class Menu
    {
        public static void DraaiProgramma()
        {
            Speelbord speelbord = new(false);
            int keuze;
            Hoofdmenu keuzemenu;

            while (true)
            {
                Console.WriteLine("Welkom bij Mastermind!\nKies een optie uit het keuzemenu:");
                Console.WriteLine("1. Speel Mastermind");
                Console.WriteLine("2. Lees de spelregels (van wikipedia)");
                Console.WriteLine("3. Verlaat het programma");
                keuze = int.Parse(Console.ReadLine());
                keuzemenu = (Hoofdmenu)keuze;
                Console.Clear();
                switch (keuzemenu)
                {
                    case Hoofdmenu.Spelen:
                        speelbord.SpeelMasterMind(); 
                        TerugNaarHoofdmenu();
                        break;
                    case Hoofdmenu.Spelregels:
                        WebsiteTonen.ToonSite();
                        TerugNaarHoofdmenu();
                        break;
                    case Hoofdmenu.Exit: Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Foutieve invoer. Probeer het nog eens.");
                        break;
                }
            }
        }
        
        private static void TerugNaarHoofdmenu()
        {
            Console.Write("\nDruk op enter om terug te keren naar het hoofdmenu.");
            Console.ReadLine();
            Console.Clear();
        }
    }

    public enum Hoofdmenu { Spelen = 1, Spelregels, Exit}
}
