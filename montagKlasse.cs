using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AntMe.Deutsch;
using AntMe.English;

namespace AntMe.Player.sfd224
{
    /// <summary>
    /// Diese Datei enthält die Beschreibung für deine Ameise. Die einzelnen Code-Blöcke 
    /// (Beginnend mit "public override void") fassen zusammen, wie deine Ameise in den 
    /// entsprechenden Situationen reagieren soll. Welche Befehle du hier verwenden kannst, 
    /// findest du auf der Befehlsübersicht im Wiki.
    /// 
    /// Wenn du etwas Unterstützung bei der Erstellung einer Ameise brauchst, findest du
    /// in den AntMe!-Lektionen ein paar Schritt-für-Schritt Anleitungen.
    ///
    /// Link zum Wiki: https://wiki.antme.net
    /// </summary>
    [Spieler(
        Volkname = "sfd224",   // Hier kannst du den Namen des Volkes festlegen
        Vorname = "",       // An dieser Stelle kannst du dich als Schöpfer der Ameise eintragen
        Nachname = ""       // An dieser Stelle kannst du dich als Schöpfer der Ameise eintragen
    )]

    /// Kasten stellen "Berufsgruppen" innerhalb deines Ameisenvolkes dar. Du kannst hier mit
    /// den Fähigkeiten einzelner Ameisen arbeiten. Wie genau das funktioniert kannst du der 
    /// Lektion zur Spezialisierung von Ameisen entnehmen.
    [Kaste(
        Name = "Kämpfer",                  // Name der Berufsgruppe
        AngriffModifikator = 1,             // Angriffsstärke einer Ameise
        DrehgeschwindigkeitModifikator = -1, // Drehgeschwindigkeit einer Ameise
        EnergieModifikator = 1,             // Lebensenergie einer Ameise
        GeschwindigkeitModifikator = 0,     // Laufgeschwindigkeit einer Ameise
        LastModifikator = -1,                // Tragkraft einer Ameise
        ReichweiteModifikator = 1,          // Ausdauer einer Ameise
        SichtweiteModifikator = -1           // Sichtweite einer Ameise


    )]
    [Kaste(
        Name = "Träger",                  // Name der Berufsgruppe
        AngriffModifikator = -1,             // Angriffsstärke einer Ameise
        DrehgeschwindigkeitModifikator = -1, // Drehgeschwindigkeit einer Ameise
        EnergieModifikator = -1,             // Lebensenergie einer Ameise
        GeschwindigkeitModifikator = 0,     // Laufgeschwindigkeit einer Ameise
        LastModifikator = 2,                // Tragkraft einer Ameise
        ReichweiteModifikator = 1,          // Ausdauer einer Ameise
        SichtweiteModifikator = 0           // Sichtweite einer Ameise


    )]
    [Kaste(
        Name = "Späher",                  // Name der Berufsgruppe
        AngriffModifikator = -1,             // Angriffsstärke einer Ameise
        DrehgeschwindigkeitModifikator = -1, // Drehgeschwindigkeit einer Ameise
        EnergieModifikator = -1,             // Lebensenergie einer Ameise
        GeschwindigkeitModifikator = 1,     // Laufgeschwindigkeit einer Ameise
        LastModifikator = -1,                // Tragkraft einer Ameise
        ReichweiteModifikator = 1,          // Ausdauer einer Ameise
        SichtweiteModifikator = 2           // Sichtweite einer Ameise


    )]


    [Kaste(
        Name = "Verteidiger",                  // Name der Berufsgruppe
        AngriffModifikator = 2,             // Angriffsstärke einer Ameise
        DrehgeschwindigkeitModifikator = -1, // Drehgeschwindigkeit einer Ameise
        EnergieModifikator = 1,             // Lebensenergie einer Ameise
        GeschwindigkeitModifikator = 0,     // Laufgeschwindigkeit einer Ameise
        LastModifikator = -1,                // Tragkraft einer Ameise
        ReichweiteModifikator = -1,          // Ausdauer einer Ameise
        SichtweiteModifikator = 0           // Sichtweite einer Ameise


    )]

    [Kaste(
        Name = "Anführer",                  // Name der Berufsgruppe
        AngriffModifikator = -1,             // Angriffsstärke einer Ameise
        DrehgeschwindigkeitModifikator = 0, // Drehgeschwindigkeit einer Ameise
        EnergieModifikator = 0,             // Lebensenergie einer Ameise
        GeschwindigkeitModifikator = -1,     // Laufgeschwindigkeit einer Ameise
        LastModifikator = 0,                // Tragkraft einer Ameise
        ReichweiteModifikator = 0,          // Ausdauer einer Ameise
        SichtweiteModifikator = 2           // Sichtweite einer Ameise
        )]
    public class sfd224Klasse : Basisameise
    {
        #region Rollen

        private Zucker letzterZucker = null;
        private Obst letzterObst = null;
        private Markierung letzterHinweis = null;

        private Spielobjekt letzterFundort = null;
        private bool gehtHeimMitFund = false;
        private bool kehrtZurückZumFundort = false;
        private bool warImBau = false;


        public string[] KasteNamen => new[] { "Kämpfer", "Träger", "Späher", "Verteidiger", "Anführer" };

        public override string BestimmeKaste(Dictionary<string, int> anzahl)
        {
            int gesamt = 0;
            foreach (var paar in anzahl)
                gesamt += paar.Value;

            if (gesamt % 6 == 0)
            {
                Denke("Ich bin Anführer!");
                return "Anführer";
            }
            if (gesamt % 5 == 0)
            {
                Denke("Ich bin Verteidiger!");
                return "Verteidiger";
            }


            int nummer = gesamt % 12;
            if (nummer < 4)
            {
                Denke("Ich bin Kämpfer!");
                return "Kämpfer";
            }
            if (nummer < 8)
            {
                Denke("Ich bin Träger!");
                return "Träger";
            }
            
            Denke("Ich bin Späher!");
            return "Späher"; 
        }
        #endregion Rollen

        #region Kasten

        /// <summary>
        /// Jedes mal, wenn eine neue Ameise geboren wird, muss ihre Berufsgruppe
        /// bestimmt werden. Das kannst du mit Hilfe dieses Rückgabewertes dieser 
        /// Methode steuern.
        /// </summary>
        /// <param name="anzahl">Anzahl Ameisen pro Kaste</param>
        /// <returns>Name der Kaste zu der die geborene Ameise gehören soll</returns>

        #endregion

        #region Fortbewegung

        /// <summary>
        /// Wenn die Ameise keinerlei Aufträge hat, wartet sie auf neue Aufgaben. Um dir das 
        /// mitzuteilen, wird diese Methode hier aufgerufen.
        /// </summary>
        public override void Wartet()
        {
        }

        /// <summary>
        /// Erreicht eine Ameise ein drittel ihrer Laufreichweite, wird diese Methode aufgerufen.
        /// </summary>
        public override void WirdMüde()
        {
        }

        /// <summary>
        /// Wenn eine Ameise stirbt, wird diese Methode aufgerufen. Man erfährt dadurch, wie 
        /// die Ameise gestorben ist. Die Ameise kann zu diesem Zeitpunkt aber keinerlei Aktion 
        /// mehr ausführen.
        /// </summary>
        /// <param name="todesart">Art des Todes</param>
        public override void IstGestorben(Todesart todesart)
        {
        }

        /// <summary>
        /// Diese Methode wird in jeder Simulationsrunde aufgerufen - ungeachtet von zusätzlichen 
        /// Bedingungen. Dies eignet sich für Aktionen, die unter Bedingungen ausgeführt werden 
        /// sollen, die von den anderen Methoden nicht behandelt werden.
        /// </summary>


        private const int KreisRadius = 100;
        private bool kreisStart = false;
        private bool hatGedreht = false;

        private const int LinieRadius = 260;
        private bool LinieStart = false;

        public override void Tick()
        {
            if (Kaste == "Verteidiger")
            {
                KreisLauf();

            }

            if (Kaste == "Späher")
            {
                Linie();

            }

        }
        public enum Markierungstyp
        {
            Linie, // typ 0 = Linie
            Obst, // typ 1 = obst
            Feind // typ 2 feind
        }

        public void RiechtMarkierung(int typ)
        {
            if (typ == 0 && AktuelleLast == 0 && Ziel == null)
            {
                GeheGeradeaus();
                Denke("Ich folge einer Markierung!");
            }
        }



        private void KreisLauf()
        {
            // Schritt 1: Rauslaufen bis zur gewünschten Radius-Entfernung
            if (!kreisStart)
            {
                if (EntfernungZuBau < KreisRadius)
                {
                    GeheGeradeaus();
                    Denke("Ich gehe raus bis zur Reichweite!");
                }
                else
                {
                    kreisStart = true;
                    hatGedreht = false;
                    Denke("Reichweite erreicht, ich starte den endlosen Kreis!");
                }
                return;
            }

            // Schritt 2: Die Ameise dreht sich kontinuierlich weiter
            DreheUmWinkel(2);  // Kleine Rotation → ergibt Kreis
            Denke("Ich drehe mich weiter für den Kreis!");

            // Schritt 3: Immer wieder ein kleines Stück laufen
            GeheGeradeaus();
            Denke("Ich laufe ein kleines Stück vorwärts im Kreis!");

            // Schritt 4: Wenn Ameise wieder nah am Bau ist → Kreis neu starten
            if (EntfernungZuBau < 100)
            {
                kreisStart = true;
                hatGedreht = false;
                Denke("Zurück am Bau, Starte Kreis erneut!");
            }
        }


        private void Linie()
        {
            // Wenn der Startpunkt noch nicht erreicht wurde
            if (!LinieStart)
            {
                if (EntfernungZuBau < LinieRadius)
                {
                    GeheGeradeaus();
                    Denke("Ich gehe bis zur Reichweite!");
                }
                else
                {
                    LinieStart = true;
                    hatGedreht = false; // Reset für erneutes Drehen
                    Denke("Reichweite erreicht, Drehe um!");
                }
                return;
            }

            // Sobald die Ameise an der Reichweite ist, soll sie sich drehen
            if (!hatGedreht)
            {
               
                DreheUmWinkel(180);
                SprüheMarkierung(0, 30);
                Denke("Ich drehe mich und sprühe Markierung!");
                hatGedreht = true;
                return;
            }

            // Nachdem sie sich gedreht hat, geht sie zurück
            GeheGeradeaus();
            SprüheMarkierung(0, 30);
            Denke("Ich laufe geradeaus zurück zum Bau mit Markierung!");

            // Wenn sie wieder in der Nähe des Baus ist → Zyklus erneut starten
            if (EntfernungZuBau < 260)
            {
                LinieStart = false;
                hatGedreht = false;
                SprüheMarkierung(0, 30);
                Denke("Zurück am Bau , Starte Linie erneut!");
            }
        }
    }
}



#endregion

#region Nahrung




#endregion

#region Kommunikation





/// <summary>
/// So wie Ameisen unterschiedliche Nahrungsmittel erspähen können, entdecken Sie auch 
/// andere Spielelemente. Entdeckt die Ameise eine Ameise aus dem eigenen Volk, so 
/// wird diese Methode aufgerufen.
/// </summary>
/// <param name="ameise">Erspähte befreundete Ameise</param>

#endregion

#region Kampf

/// <summary>
/// So wie Ameisen unterschiedliche Nahrungsmittel erspähen können, entdecken Sie auch 
/// andere Spielelemente. Entdeckt die Ameise eine Ameise aus einem feindlichen Volk, 
/// so wird diese Methode aufgerufen.
/// </summary>


#endregion

