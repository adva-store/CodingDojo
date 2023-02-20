namespace LangtonFrontend
{
    internal class Program
    {
        /// <summary>
        /// Annahme: Erster Parameter ist Zuggeschwindigkeit in ms, zweiter Parameter ist Dateipfad
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            if (args.Length < 2) 
            {
                Console.WriteLine("Missing parameters. No data to process. Example LangtonFrontend.exe 1000 C:\\source\\LangtonAnt\\LangtonAnt\\LangtonBackend\\bin\\Debug\\net6.0\\Langtonb7e1a0ee-a4e6-4c0c-85e7-c7c3c8465c03.txt");
            }

            bool valid = int.TryParse( args[0], out int waittime);
            if (!valid)
            {
                waittime = 1000;
            }
            
            string filename = args[1];

            if (!File.Exists(filename))
            {
                Console.WriteLine("File not found. No data to process: " + filename);
            }

            DoFileProcess(filename, waittime);
            
        }

        /// <summary>
        /// Datei einlesen, im Intervall der übergebenen waittime ausgeben:
        /// Kantenlänge, Startposition der Ameise, Blickrichtung der Ameise(n, o, s, w), Anzahl der Züge
        /// </summary>
        /// <param name="filename">Dateiname</param>
        /// <param name="waittime">Ausgabeintervall</param>
        private static void DoFileProcess(string filename, int waittime)
        {
            string[] lines = File.ReadAllLines(filename);

            int fieldlength = 0;
            int stepCounter = 1;
            int linecounter = 0;
            bool found = false;

            foreach (string line in lines) 
            {
                string[] fieldValues = line.Split(','); 

                if (stepCounter == 1)
                {
                    fieldlength = fieldValues.Length - 1; //-1 wegen Komma am Zeilenende
                }

                //Ameise in der Zeile suchen, falls noch nicht in diesem Block gefunden
                if (!found)
                {
                    int xcounter = 0;
                    foreach (string fieldval in fieldValues)
                    {
                        //Treffer wenn Positionsinhalt zweistellig und keine Klammer enthalten ist
                        if (fieldval.Length.Equals(2) && !fieldval[0].Equals('(') && !fieldval[1].Equals(')') )
                        {
                            //Ameise gefunden
                            Console.WriteLine(String.Format("Step {0}: Kantenlänge: {1}, Startposition: X={2} Y={3}, Blickrichtung: {4}",
                                                            stepCounter, fieldlength, xcounter, linecounter, fieldval[0]));
                            found = true;

                            break;
                        }

                        xcounter++;
                    }
                }

                if (!found) 
                { 
                    linecounter++; 
                }
                else
                {
                    //wenn Ameise bereits gefunden, den Block Terminator suchen und ggf. resetten
                    if (fieldValues.Contains(")"))
                    {
                        found = false;

                        //nach gefundener Ameise im nächsten Block wieder die Zeilen von 0 beginnend zählen.
                        linecounter = 0;

                        //einen erfolgreichen Step hochzählen
                        stepCounter++;

                        //apply waittime
                        Thread.Sleep(waittime);
                    }
                }
            }

        } 
        
    }
}