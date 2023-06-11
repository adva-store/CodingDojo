using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LangtonAntProject
{
    public class LangtonAnt
    {
        private int gridSize;
        private int[,] grid;
        private Direction startDirection;
        private int startPosition_X;
        private int startPosition_Y;
        private int iterationCount;
        public LangtonAnt(int gridSize, Direction startDirection, int startPosition_X, int startPosition_Y, int iterationCount)
        {
            this.gridSize = gridSize;
            this.startDirection = startDirection;

            grid = new int[this.gridSize, this.gridSize];

            this.startPosition_X = startPosition_X;
            this.startPosition_Y = startPosition_Y;
            this.iterationCount = iterationCount;

            //grid initialize
            for (int i=0; i < this.gridSize; i++)
            {
                for(int  j=0; j < this.gridSize; j++)
                {
                    grid[i, j] = Convert.ToInt32(Color.w);
                }
            }
        }
        //do all iterations and save result match fields to a file
        public string Start()
        {
            if (!Directory.Exists(@"..\..\Output"))
            {
                Directory.CreateDirectory(@"..\..\Output");
            }
            var path = @"..\..\Output\Result.txt";
            
            int currentPosition_x = this.startPosition_X;
            int currentPosition_y = this.startPosition_Y;
            Direction currentDirection = this.startDirection;

            StringBuilder matchField = new StringBuilder();

            for (var i = 0; i < iterationCount; i++)
            {

                if (grid[currentPosition_x, currentPosition_y] == (int)Color.s)
                {
                    grid[currentPosition_x, currentPosition_y] = (int)Color.w;

                    TurnToLeft(ref currentDirection, ref currentPosition_x, ref currentPosition_y);

                }
                else
                {
                    grid[currentPosition_x, currentPosition_y] = (int)Color.s;

                    TurnToRight(ref currentDirection, ref currentPosition_x, ref currentPosition_y);

                }


                //save grid values
                for (int x = 0; x < gridSize; x++)
                {
                    for (int y = 0; y < gridSize; y++)
                    {
                        if (x == currentPosition_x && y == currentPosition_y) matchField.Append(currentDirection.ToString());
                        var color = Enum.Parse(typeof(Color), grid[x, y].ToString());

                        matchField.Append(color.ToString()).Append(",");


                    }
                }
                matchField.AppendLine();
            }

            //write the current match field to file
            using (StreamWriter stream = new StreamWriter(path,false))
            {
                stream.Write(matchField.ToString());
            }
            return path;
        }

        private void TurnToRight(ref Direction direction, ref int currentPosition_x, ref int currentPosition_y)
        {
            switch (direction)
            {
                case Direction.n:
                    direction = Direction.o;
                    currentPosition_x++;
                    break;
                case Direction.o:
                    direction = Direction.s;
                    currentPosition_y++;
                    break;
                case Direction.s:
                    direction = Direction.w;
                    currentPosition_x = currentPosition_x + gridSize - 1;
                    break;
                case Direction.w:
                    direction = Direction.n;
                    currentPosition_y = currentPosition_y + gridSize - 1;
                    break;
            }

            currentPosition_x = currentPosition_x % gridSize;
            currentPosition_y = currentPosition_y % gridSize;
        }
        private void TurnToLeft(ref Direction direction, ref int currentPosition_x, ref int currentPosition_y)
        {
            switch (direction)
            {
                case Direction.n:
                    direction = Direction.w;
                    currentPosition_x = currentPosition_x + gridSize - 1;
                    break;
                case Direction.w:
                    direction = Direction.s;
                    currentPosition_y++;
                    break;
                case Direction.s:
                    direction = Direction.o;
                    currentPosition_x++;
                    break;
                case Direction.o:
                    direction = Direction.n;
                    currentPosition_y= currentPosition_y+gridSize-1;
                    break;
            }
            currentPosition_x = currentPosition_x % gridSize;
            currentPosition_y = currentPosition_y % gridSize;
        }
    }

    public enum Direction
    {
        n, o, s, w

    }
    public enum Color
    {
        s, w
    }
}
