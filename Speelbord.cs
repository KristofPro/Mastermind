using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mastermind
{
    public class Speelbord
    {
        //Fields en properties
        private readonly bool toonOplossing;
        public int HuidigeRonde { get; private set; }               //Huidige ronde (-1) + fungeert als index voor lijsten
        public int AantalGevondenKleuren { get; private set; } = 0; //Aantal pionnen die op de juiste plaats staan met de juiste kleur
        public bool IsGewonnen { get; private set; } = false;       //Stopt het spel als het true wordt
        private Pion[]? kleurenArray;                                //array van kleuren die je per ronde ingeeft
        private readonly Pion[] correcteKleuren;                             //array van te raden pionnen
        private readonly Pin[] pinArray;                                     //array van pinnen die hints geven over de te kraken kleurcode
        private readonly List<Pion[]> ingegevenKleurenReeksen;      //Lijst van ingegeven antwoorden zodat je alle voorgaande antwoorden elke ronde kan herbekijken
        private readonly List<Pin[]> pinReeksen;                    //Lijst van alle pinnen zodat je de hints van elke ronde kan herbekijken
        private readonly List<int> rondenReeksen;                    //Lijst van ronden

        //Constructor om variabelen, arrays en lijsten te initialiseren
        public Speelbord(bool toonOplossing)
        {
            HuidigeRonde = 0;
            correcteKleuren = new Pion[4];
            pinArray = new Pin[4];
            for (int i = 0; i < correcteKleuren.Length; i++)
            {
                correcteKleuren[i] = new Pion();
                pinArray[i] = new Pin();
            }

            ingegevenKleurenReeksen = new List<Pion[]>();
            pinReeksen = new List<Pin[]>();
            rondenReeksen = new List<int>();
            this.toonOplossing = toonOplossing;
        }

        //Methoden

        //Toon/verberg de oplossing bovenaan in de console
        private void ToonOplossing()
        {
            Console.Write("Oplossing: ");
            foreach (var item in correcteKleuren)
            {
                item.ToonKleur();
            }
            Console.WriteLine();
        }

        // Geef de kleurenwaarde weer met alle reeds ingegeven kleurenreeksen
        private void TekenSpeelbord()
        {
            if (toonOplossing)
            {
                ToonOplossing();
            }
            // Bovenkant speelbord
            StringBuilder overzicht = new();
            overzicht.AppendLine("════════════ MASTERMIND ════════════");
            overzicht.AppendLine("Overzicht:");
            overzicht.AppendLine("╔═══════════════════════════════════╗");
            overzicht.AppendLine("║ R = rood | G = groen | B = blauw  ║");
            overzicht.AppendLine("║ Y = geel | P = paars | O = oranje ║");
            overzicht.AppendLine("╚═══════════════════════════════════╝");
            overzicht.AppendLine("Speelbord:");
            overzicht.AppendLine("╔═════════════════╦═════════╦══════╗");
            overzicht.AppendLine("║Pionnen          ║Pinnen   ║Ronde ║");
            overzicht.AppendLine("╟─────────────────╫─────────╫──────╢");
            Console.Write(overzicht.ToString());

            // Zorg ervoor dat elke ingegeven kleurenreeks en pinnen op het bord blijven staan
            TekenReeks();
        }

        // Geef een nieuwe kleurenreeks in
        private void RaadKleurencombinatie()
        {
            string rij = "";

            // Controleer eerst de lengte van de ingevoerde reeks om een error te voorkomen omdat de array te kort is
            while (rij.Length < 4)
            {
                Console.Write($"\nGeef de kleurenreeks van ronde {HuidigeRonde + 1} in: ");
                rij = Console.ReadLine().ToUpper();

                if (rij.Length < 4)
                {
                    Console.WriteLine("ERROR: de ingevoerde kleurenreeks moet minstens 4 letters bevatten.");
                }
            }

            //Zet de de invoer om naar een char array
            char[] tekeninvoer = rij.ToCharArray(); // char array aanmaken

            //Vervang elk afwijkend teken met de standaardwaarde 'R'
            for (int i = 0; i < 4; i++)
            {
                if (!"RGBYPO".Contains(tekeninvoer[i]))
                {
                    tekeninvoer[i] = 'R';
                }
            }

            // array van pionnen maken om de invoer af te drukken
            Pion[] pionneninvoer = new Pion[4];
            for (int i = 0; i < 4; i++)
            {
                pionneninvoer[i] = new Pion
                {
                    Kleur = tekeninvoer[i]
                };
            }
            ingegevenKleurenReeksen.Add(pionneninvoer);

            //Voeg ook meteen de huidige ronde toe
            rondenReeksen.Add(HuidigeRonde + 1);
        }

        // Controleer of de ingegeven kleurenreeks correct is
        private void ControleerIngevoerdeKleurenreeks()
        {
            AantalGevondenKleuren = 0; // Reset aantal gevonden kleuren per ronde!
            int aantalWittePinnen = 0; // Teller voor witte pinnen
            kleurenArray = ingegevenKleurenReeksen[HuidigeRonde];
            bool[] correctePositie = new bool[4]; // Bijhouden welke posities correct zijn
            bool[] correcteKleurenGevonden = new bool[4]; // Bijhouden welke correcte kleuren al gevonden zijn voor witte pinnen
            List<Pion> kleurenOpFoutePlaats = new();

            // Controleer eerst op rode pinnen
            for (int i = 0; i < 4; i++)
            {
                if (kleurenArray[i].Equals(correcteKleuren[i]))
                {
                    AantalGevondenKleuren++;
                    correctePositie[i] = true;         // Markeer de positie ...
                    correcteKleurenGevonden[i] = true; // én de correcte kleur als gevonden (anders genereer je in sommige gevallen meerdere pinnen)
                }
            }

            // Voeg niet-correct geplaatste kleuren toe aan een aparte lijst
            for (int i = 0; i < 4; i++)
            {
                if (!correctePositie[i])
                {
                    kleurenOpFoutePlaats.Add(kleurenArray[i]);
                }
            }

            // Controleer op witte pinnen
            for (int i = 0; i < kleurenOpFoutePlaats.Count; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (!correcteKleurenGevonden[j] && kleurenOpFoutePlaats[i].Equals(correcteKleuren[j]))
                    {
                        aantalWittePinnen++;
                        correcteKleurenGevonden[j] = true; // Markeer de correcte kleur als gevonden zodat het niet meermaals wordt opgenomen in aantalWittePinnen
                        break;
                    }
                }
            }

            // Genereer pinnen voor de huidige ronde
            GenereerPinnen(AantalGevondenKleuren, aantalWittePinnen);
        }


        private void GenereerPinnen(int rodePinnen, int wittePinnen)
        {
            Pin[] nieuwePinArray = new Pin[4]; // Nieuwe array voor deze ronde
            int index = 0;

            // Voeg rode pinnen toe
            for (int i = 0; i < rodePinnen; i++, index++)
            {
                nieuwePinArray[index] = new Pin { Kleur = 'R' }; // Rode pin voor elke correcte kleur
            }

            // Voeg witte pinnen toe
            for (int i = 0; i < wittePinnen; i++, index++)
            {
                nieuwePinArray[index] = new Pin { Kleur = 'W' }; // Witte pin voor elke correcte kleur op verkeerde positie
            }

            // Vul de rest met zwarte pinnen
            for (int i = index; i < 4; i++)
            {
                nieuwePinArray[i] = new Pin { Kleur = 'Z' }; // Lege (zwarte) pin voor de rest
            }

            pinReeksen.Add(nieuwePinArray); // Voeg de nieuwe pinarray toe aan de lijst
        }


        // Teken de ingegeven kleurenreeks
        private void TekenReeks()
        {
            // Toevoeging van elke reeks per ronde
            for (int i = 0; i < ingegevenKleurenReeksen.Count; i++)
            {
                //Pionnen toevoegen
                kleurenArray = ingegevenKleurenReeksen[i];
                Pin[] huidigePinArray = pinReeksen[i];
                Console.Write("║ ");
                for (int j = 0; j < 4; j++)
                {
                    kleurenArray[j].ToonKleur();
                }
                Console.Write($"║ ");

                //Pinnen toevoegen
                for (int j = 0; j < 4; j++)
                {
                    huidigePinArray[j].ToonKleur();
                }
                Console.WriteLine($"║  {rondenReeksen[i]:00}  ║");

                // Print de scheidingslijn alleen als het niet de laatste ingegeven reeks is (als je wint/verlies mag dit niet worden afgedrukt)
                if (i < ingegevenKleurenReeksen.Count - 1)
                {
                    Console.WriteLine("╟─────────────────╫─────────╫──────╢");
                }
            }
        }

        private bool UpdateSpeelbord()
        {
            ControleerIngevoerdeKleurenreeks();
            Console.Clear(); // Maak de console leeg en toon het geüpdatete bord
            TekenSpeelbord();

            if (AantalGevondenKleuren == 4) //Als je de kleurencode op tijd raadt: je wint het spel
            {
                IsGewonnen = true;
                // Onderkant speelbord
                Console.WriteLine("╚═════════════════╩═════════╩══════╝");
                Console.WriteLine($"\nYOU WIN!\nJe hebt de juiste kleurencode in ronde {HuidigeRonde + 1} geraden!");
                return true;
            }
            else if (HuidigeRonde == 9) //Als de 10 beurten op zijn voordat je de kleurencode kan raden: je verliest het spel
            {
                // Onderkant speelbord
                Console.WriteLine("╚═════════════════╩═════════╩══════╝");
                Console.WriteLine($"\nGAME OVER\nJe bent er helaas niet in geslaagd om de juiste kleurencode te kraken");
                return true;
            }
            else
            {
                HuidigeRonde++;
                return false;
            }
        }

        public void SpeelMasterMind()
        {
            // Teken het speelbord
            TekenSpeelbord();

            while (!IsGewonnen)
            {
                // Voeg een nieuwe kleurenreeks toe
                RaadKleurencombinatie();

                // Update het spel
                IsGewonnen = UpdateSpeelbord();
            }
        }

    }
}
