class Program
{

    static void Main(string[] args)
    {
        //ToDo: Errorhandling Input Validation, individual methods for Inputs and Calculation
        Console.WriteLine("Geben Sie die Anzahl der Zeilen/Spalten ein:");
        int count = Convert.ToInt32(Console.ReadLine());
        int[] cs = new int[count];
        int[] rs = new int[count];

        Console.WriteLine($"Geben Sie {count} Breiten für die Spalten ein:");
        for (int i = 0; i < count; i++)
        {
            cs[i] = Convert.ToInt32(Console.ReadLine());
        }
        Console.WriteLine($"Geben Sie {count} Höhen für die Zeilen ein:");
        for (int i = 0; i < count; i++)
        {
            rs[i] = Convert.ToInt32(Console.ReadLine());
        }

        bool isWhite = true;
        (int white, int black) result = (0, 0);

        foreach (int r in rs)
        {
            foreach (int c in cs)
            {
                if (isWhite)
                {
                    result.white += r * c;
                }
                else
                {
                    result.black += r * c;
                }
                isWhite = !isWhite;
            }
        }

        Console.WriteLine("Testergebnis: " + (((result.white + result.black) == (rs.Sum() * cs.Sum())) ? "Erfolgreich" : "Fehlgeschlagen"));

        Console.WriteLine("Ergebnis: " + result);
    }
}

