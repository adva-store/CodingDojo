using LangtonAnt.Backend;
using LangtonAnt.Frontend;

int spielbrettGröße = 11;
int visualStepDelayInMS = 500;
string filename = string.Format("LangtonAnt_Jagiolka_Result_{0}.txt", DateTime.Now.ToString("yyyyMMdd_HH-mm"));
int movingSteps = 100;


Console.WriteLine("Langton Ant");
Console.WriteLine("===========");
Console.WriteLine();
Console.WriteLine("Wieviele Felder soll das Brett haben: (default=11)");

string? inputSpielbrett = Console.ReadLine();
int input = 0;

if(int.TryParse(inputSpielbrett, out input) && spielbrettGröße > 0) 
{
  spielbrettGröße = input;
  Console.WriteLine("Brettgröße übernommen.");
}
else 
{
  Console.WriteLine("Ungültige Eingabe. (int >0)");
}
Console.WriteLine($"Brettgröße: {spielbrettGröße}");
Console.WriteLine($"Anzahl der Bewegungen: {movingSteps}");

var spiel = new Spielregeln();
spiel.NeuesSpielBrett(spielbrettGröße);
for (int i = 0; i < movingSteps; i++)
{
  spiel.AmeiseBewegen();
}

var visualisierung = new Visualisierung();
visualisierung.Show(spiel.Spielablauf, spielbrettGröße, visualStepDelayInMS);

Console.WriteLine("Sollen die Daten gespeichert werden? (j/n)");

if (Console.ReadLine().ToLower() == "j") 
{
  string path = string.Empty;
  do
  {
    Console.WriteLine("Bitte geben sie den Pfad, ohne Dateinamen, ein: (muss bereits existieren)");
    path = Console.ReadLine();
  }
  while (!Directory.Exists(path));

  path = Path.Combine(path, filename);
  try
  {
    File.WriteAllLines(path, spiel.Spielablauf);
  }
  catch (Exception ex) 
  {
    Console.WriteLine("Ein Fehler ist aufgetreten:");
    Console.WriteLine(ex.Message);
    Console.WriteLine();
    Console.WriteLine(ex.StackTrace);
  }
  Console.WriteLine($"Erfolgreich gespeichert unter: {path}");
}

Console.ReadLine();
Console.WriteLine("[Enter] drücken zum beenden.");
Console.ReadLine();

Console.WriteLine("\n\n\n\n\n\n\n\n\n\n"); // soll Abstand zum Standard Debuggingtext geben