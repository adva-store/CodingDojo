using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangtonAntKataApp
{
    public enum AntDirections
    {
        North,
        East,
        South,
        West,
    }

    public class AntCoordinates
    {
        public int X { get; set; }
        public int Y { get; set; }

        public AntCoordinates(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    public static class LangtonAntAlgorythmus
    {
        

        //Was tun wenn Methode mehrmals aufgerufen wird

        //Begrenzungen nicht vergessen, wenn Ameise hinter die grenzen des Feldes geht

        //Entscheiden wie mit dem init Feld umgegangen

        public static List<string> GetAuntsTravelling(int fieldLengt, AntCoordinates antStartPosition, AntDirections antDirection, int antNumberOfMoves)
        {
            try
            {
                var list = new List<string>();
                bool[][] field = _GetInitField(fieldLengt);
                AntCoordinates antPos = antStartPosition;
                bool isActualFieldBlack = false;
                for (int i = 0; i < antNumberOfMoves; i++)
                {
                    if (antPos.Y >= field.Length || antPos.X >= field[antPos.Y].Length)
                    {
                        //Die Position der Ameise liegt auserhalb des Feldes
                        break;
                    }

                    list.Add(_PrintFieldAsString(field, antPos, antDirection));

                    isActualFieldBlack = field[antPos.Y][antPos.X];
                    field[antPos.Y][antPos.X] = !field[antPos.Y][antPos.X];
                    antPos = _GetNextAntPos(antPos, antDirection, isActualFieldBlack);
                    antDirection = _GetNextAntDirection(antDirection, isActualFieldBlack);
                }

                return list;
            }
            catch (Exception ex)
            {
                return new List<string>();
            }
            
            //return Task<List<string>>.Factory.StartNew(() =>
            //{
            //    var list = new List<string>();
            //    bool[][] field = _GetInitField(fieldLengt);
            //    (int x, int y) antPos = antStartPosition;
            //    bool isActualFieldBlack = false;
            //    for (int i = 0; i < antNumberOfMoves; i++)
            //    {
            //        if (antPos.y >= field.Length || antPos.x >= field[antPos.y].Length)
            //        {
            //            //Die Position der Ameise liegt auserhalb des Feldes
            //            break;
            //        }
            //        list.Add(_PrintFieldAsString(field, antPos, antDirection));

            //        isActualFieldBlack = field[antPos.y][antPos.x];
            //        field[antPos.y][antPos.x] = !field[antPos.y][antPos.x];
            //        antPos = _GetNextAntPos(antPos, antDirection, isActualFieldBlack);
            //    }

            //    return list;
            //});
            
        }

        private static bool[][] _GetInitField(int fieldLengt)
        {
            bool[][] field = new bool[fieldLengt][];
            for (int i = 0; i < field.Length; i++)
            {
                field[i] = new bool[fieldLengt];
            }
            return field;
        }

        private static AntCoordinates _GetNextAntPos(AntCoordinates actualPos,  AntDirections actualAntDirection, bool fieldIsBlack)
        {
            AntCoordinates nextPos = new AntCoordinates(actualPos.X, actualPos.Y);
            switch (actualAntDirection)
            {
                case AntDirections.North:
                    nextPos.X = fieldIsBlack ? actualPos.X - 1 : actualPos.X + 1;
                    break;
                case AntDirections.East:
                    nextPos.Y = fieldIsBlack ? actualPos.Y - 1 : actualPos.Y + 1;
                    break;
                case AntDirections.South:
                    nextPos.X = !fieldIsBlack ? actualPos.X - 1 : actualPos.X + 1;
                    break;
                case AntDirections.West:
                    nextPos.Y = !fieldIsBlack ? actualPos.Y - 1 : actualPos.Y + 1;
                    break;
            }
            return nextPos;
        }

        private static AntDirections _GetNextAntDirection(AntDirections actualAntDirection, bool fieldIsBlack)
        {
            switch (actualAntDirection)
            {
                case AntDirections.North:
                    return fieldIsBlack ? AntDirections.West : AntDirections.East;
                case AntDirections.East:
                    return fieldIsBlack ? AntDirections.North : AntDirections.South;
                case AntDirections.South:
                    return !fieldIsBlack ? AntDirections.West : AntDirections.East;
                case AntDirections.West:
                    return !fieldIsBlack ? AntDirections.North : AntDirections.South;
            }
            return actualAntDirection;
        }

        private static string _PrintFieldAsString(bool[][] field, AntCoordinates antPos, AntDirections actualAntDirection)
        {
            try
            {
                string fieldAsSting = string.Empty;
                int y = 0;
                int x = 0;
                for (y = 0; y < field.Length; y++)
                {
                    Console.WriteLine($"y-Pos: {y}");
                    for (x = 0; x < field[y].Length; x++)
                    {
                        Console.WriteLine($"x-Pos: {x}");
                        if (antPos.Y == y && antPos.X == x)
                        {
                            fieldAsSting += _GetDirectionString(actualAntDirection);
                        }

                        fieldAsSting += field[y][x] ? "s," : "w,";
                        //Console.WriteLine($"str-Pos: {fieldAsSting}");
                    }
                }
                return fieldAsSting;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        private static string _GetDirectionString(AntDirections direction)
        {
            switch (direction)
            {
                case AntDirections.North:
                    return "n";
                case AntDirections.East:
                    return "o";
                case AntDirections.South:
                    return "s";
                case AntDirections.West:
                    return "w";
            }
            return string.Empty;
        }

        public static AntDirections GetDirectionFromString(string direction)
        {
            switch (direction.ToLower())
            {
                case "n":
                    return AntDirections.North;
                case "o": 
                    return AntDirections.East;
                case "s": 
                    return AntDirections.South;
                case "w": 
                    return AntDirections.West;
            }
            return AntDirections.North;
        }
    }
}
