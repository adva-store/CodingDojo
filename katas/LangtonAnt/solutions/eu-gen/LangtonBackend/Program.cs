namespace LangtonBackend
{
    internal class Program
    {

        //notwendige Werte, müssen in Commandline übergeben werden
        /// <summary>
        /// Kantenlänge des Feldes
        /// </summary>
        private static int width;

        /// <summary>
        /// Startkoordinate der Ameise waagrecht
        /// </summary>
        private static int startX;

        /// <summary>
        /// Startkoordinate der Ameise senkrecht
        /// </summary>
        private static int startY;

        /// <summary>
        /// Startausrichtung der Ameise
        /// </summary>
        private static char direction;

        /// <summary>
        /// Anzahl der Schritte, die durchgeführt werden
        /// </summary>
        private static int steps;

        /// <summary>
        /// optional: Ausgabe auch in Console, erhöht Laufzeit erheblich bei größeren Zahlen
        /// </summary>
        private static int printtoconsole = 0; 

        static void Main(string[] args)
        {
            Console.WriteLine("LangtonAnt Backend started.");

            if(!InitCommandLineParameters(args))
            {
                Console.WriteLine("InitCommandLineParameters failed. Parameter format example: LangtonBackend.exe width=10 x=5 y=5 direction=n steps=10");
            }

            if (!ParametersCheckValid())
            {
                Console.WriteLine("ParametersCheckValid failed. Parameters contain invalid values. Parameter format example: LangtonBackend.exe width=10 x=5 y=5 direction=n steps=10");
            }

            DoLangtonAnt();

            Console.WriteLine("LangtonAnt Backend finished. Press any key to exit.");
            Console.ReadKey();
        }

        private static void DoLangtonAnt()
        {
            int stepsPerformed = 0;
            Guid filenameGuid = Guid.NewGuid();
            Langton langton = new(width, startX, startY, direction);

            while (langton.CanStep()
                && stepsPerformed < steps)
            {
                //Bewegung, Färbung und Rotation ausführen
                langton.AntCurrentPosition = langton.Step();

                //gesamtes Feld in Console ausgeben
                if (printtoconsole == 1)
                {
                    langton.PrintField();
                }
                
                //gesamtes Feld in Datei ausgeben
                langton.SaveFieldAsync(filenameGuid);

                stepsPerformed++;
            }

            Console.WriteLine(String.Format("Output saved to File Langton{0}.txt", filenameGuid.ToString()));

        }

        

        /// <summary>
        /// Logische Prüfung der Parameter
        /// </summary>
        /// <returns>true, wenn alle Parameter ok sind</returns>
        private static bool ParametersCheckValid()
        {
            if(width < 1 || startX < 1 || startY < 1 || steps < 1 || startX > width || startY > width)
            {
                Console.WriteLine("Parameters out of bounds.");
                return false;
            }

            if (!(direction != 'n' || direction != 'o' || direction != 's' || direction != 'w'))
            {
                Console.WriteLine("Direction parameters must be any value of (n,o,s,w).");
                return false;
            }

            return true;
        }
        /// <summary>
        /// Ccommandline Parameter auslesen und zuordnen
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private static bool InitCommandLineParameters(string[] args)
        {
            if (args == null)
                return false;

            Console.WriteLine("InitCommandLineParameters: {0}", string.Join(", ", args));

            Dictionary<string, string> dict = new();

            if (args.Length > 0)
            {
                foreach (string arg in args)
                {
                    int ix = arg.IndexOf('=');
                    if (ix > 0)
                        dict[arg.Substring(0, ix)] = arg.Substring(ix + 1);
                }
            }

            if (dict.Count < 5)
            {
                Console.WriteLine("Invalid parameters. Example: LangtonBackend.exe width=10 x=2 y=3 direction=n steps=100");
                return false;
            }

            if (dict.ContainsKey("width"))
            {
                int.TryParse(dict["width"], out width);
            }
            else
            {
                Console.WriteLine("Missing parameter width, example: width=10");
                return false;
            }

            if (dict.ContainsKey("x"))
            {
                bool b = int.TryParse(dict["x"], out startX);
            }
            else
            {
                Console.WriteLine("Missing parameter x, example: x=2");
                return false;
            }

            if (dict.ContainsKey("y"))
            {
                bool b = int.TryParse(dict["y"], out startY);
            }
            else
            {
                Console.WriteLine("Missing parameter y, example: y=3");
                return false;
            }

            if (dict.ContainsKey("direction"))
            {
                direction = dict["direction"].ToCharArray()[0];
            }
            else
            {
                Console.WriteLine("Missing parameter direction (n,o,s,w), example: direction=n");
                return false;
            }

            if (dict.ContainsKey("steps"))
            {
                bool b = int.TryParse(dict["steps"], out steps);
            }
            else
            {
                Console.WriteLine("Missing parameter steps, example: steps=100");
                return false;
            }

            if (dict.ContainsKey("printtoconsole"))
            {
                bool b = int.TryParse(dict["printtoconsole"], out printtoconsole);
            }
            
            return true;
        }
    }
}