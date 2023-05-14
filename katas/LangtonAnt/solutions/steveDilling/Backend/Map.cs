using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace Backend;

public class Map
{
    private readonly Field[,] _map;
    private readonly int _moves;
    private readonly Ant _ant;

    private readonly List<string> _moveHistory;

    private Map()
    {
        _ant = new(AntDirection.None, 0, 0);
        _map = new Field[1, 1];
        _moves = 100;
        _moveHistory = new();
    }

    public Map(int dimension, int startX, int startY, AntDirection antDirection, int moves) : this()
    {
        _ant = new(antDirection, startX, startY);
        _moves = moves;
        _map = new Field[dimension, dimension];

        //Init array
        for (int y = 0; y < dimension; y++)
        {
            for (int x = 0; x < dimension; x++)
            {
                _map.SetValue(new Field(FieldColor.White), x, y);
            }
        }
    }

    public void CalculateMoves()
    {
        for (int step = 1; step <= _moves; step++)
        {
            Field field = _map[_ant.XPos, _ant.YPos];
            
            //1. Change direction
            bool canMove = false;
            while (!canMove)
            {
                _ant.ChangeDirection(field.Color);
                canMove = _ant.CanMove(_map.GetLength(0));
            }

            //2. Change color
            field.ChangeColor();
            
            //3. Perform move
            _ant.Move();
            
            //4. Create map string
            var mapString = CreateMapString();
            Console.WriteLine("  Move {0}: {1}", step, mapString);
            //4. Save map
            _moveHistory.Add(mapString);
        }
    }

    private string CreateMapString()
    {
        string result = "";
        for (int y = 0; y < _map.GetLength(1); y++)
        {
            for (int  x= 0; x < _map.GetLength(0); x++)
            {
                if (result != string.Empty)
                {
                    result += ",";
                }

                if (_ant.XPos == x && _ant.YPos == y)
                {
                    if (_ant.Direction != AntDirection.None)
                    {
                        switch (_ant.Direction)
                        {
                            case AntDirection.East:
                                result += "o";
                                break;
                            case AntDirection.North:
                                result += "n";
                                break;
                            case AntDirection.South:
                                result += "s";
                                break;
                            case AntDirection.West:
                                result += "w";
                                break;
                        }
                    }
                }

                result += _map[x, y].Color == FieldColor.Black ? "s" : "w";
            }
        }

        return result;
    }

    public bool SaveHistory(string filePath)
    {
        try
        {
            using FileStream fileStream = new FileStream(filePath, FileMode.CreateNew, FileAccess.Write);
            using StreamWriter streamWriter = new StreamWriter(fileStream);
            foreach (string historyLine in _moveHistory)
            {
                streamWriter.WriteLine(historyLine);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }

        return true;
    }
}