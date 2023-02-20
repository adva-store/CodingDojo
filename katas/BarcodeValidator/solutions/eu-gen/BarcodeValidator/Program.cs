using System;
using System.Numerics;

namespace BarcodeValidator 
{   class Program
    {
        //Liste der gültigen Barcode Längen
        private static readonly List<int> validBarcodeLengths = new() {8, 13};

        static void Main()
        {
            string[] arguments = Environment.GetCommandLineArgs();
            
            List<string> validBarcodes = new();

            if (arguments.Length > 0)
            { 
                ValidateBarcodes(arguments, validBarcodes);

                OutputResult(validBarcodes);
            };

            Console.ReadKey();
        }

        /// <summary>
        /// Übergebene Liste in Commandline ausgeben.
        /// </summary>
        /// <param name="validBarcodes">Lister der gültigen Barcodes</param>
        private static void OutputResult(List<string> validBarcodes)
        {
            foreach (string bc in validBarcodes)
            {
                Console.WriteLine(bc);
            }
        }

        /// <summary>
        /// Übergebene Barcodes prüfen auf Länge, numerischen Inhalt und Prüfsumme
        /// </summary>
        /// <param name="arguments">Liste von Barcodes aus der Commandline</param>
        /// <param name="validBarcodes">Liste der Barcodes, die als gültig befunden wurden</param>
        private static void ValidateBarcodes(string[] arguments, List<string> validBarcodes)
        {
            
            foreach (string barcode in arguments)
            {
                string bc = barcode.Trim();
                
                //Länge prüfen
                if (!validBarcodeLengths.Contains(bc.Length))
                {
                    continue;    
                }

                //Numerischen Inhalt prüfen
                if (!Int64.TryParse(bc, out _))
                {
                    continue;
                }

                //Prüfsummen-Validierung aufrufen
                if (!ValidateChecksum(bc))
                {
                    continue;
                }

                //Alle Prüfungen bestanden, Barcode für Ausgabe in Liste ablegen
                validBarcodes.Add(bc);

            }
        }

        /// <summary>
        /// Prüfsummenberechnung nach 313mod10 und Vergleich der Prüfziffer
        /// Vergleichsrechner z.B. https://www.gs1-germany.de/serviceverzeichnis/pruefziffernrechner/
        /// </summary>
        /// <param name="barcode">zu prüfender Barcode als String</param>
        /// <returns>true, wenn Prüfziffer korrekt</returns>
        private static bool ValidateChecksum(string barcode)
        {
            int product = 0;
            char[] reversed = (char[])barcode.ToCharArray();
            Array.Reverse(reversed);

            //i = 0 überspringen, da das die vorhandene angenommene Prüfziffer ist
            for (int i = 1; i < reversed.Length; i++)
            {
                int factor = ( i % 2 == 0) ? 1 : 3;

                product += int.Parse(reversed[i].ToString()) * factor;
            }

            //Prüfziffer ist 0, wenn durch 10 teilbar, ansonsten Differenz zur nächsten 10
            int checksum = (product % 10 == 0) ? 0 : 10 - (product % 10);

            //Prüfen, ob bestehende Prüfziffer mit berechneter übereinstimmt
            return (checksum == int.Parse(reversed[0].ToString()));

        }
    }

}
