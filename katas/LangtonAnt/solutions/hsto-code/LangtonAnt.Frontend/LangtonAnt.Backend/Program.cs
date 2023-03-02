// See https://aka.ms/new-console-template for more information
using LangtonAnt;

Playfield playfield = new Playfield(11, new Point(5, 5), Direction.West, "/Users/stephanetolale/Projects/LangtonAnt.Frontend/LangtonAnt.Backend/bin/Debug/net7.0", false);
playfield.Start(30);

Console.ReadKey();