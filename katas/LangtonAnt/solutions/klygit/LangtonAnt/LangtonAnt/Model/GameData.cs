using System;
using System.Collections.Generic;
using System.Text;

namespace LangtonAnt
{
    public class GameData
    {
        private const string CODE__ANT_DIRECTION_NORTH = "n";
        private const string CODE__ANT_DIRECTION_EAST = "o";
        private const string CODE__ANT_DIRECTION_SOUTH = "s";
        private const string CODE__ANT_DIRECTION_WEST = "w";

        private const string CODE__CELL_COLOR_WHITE = "w";
        private const string CODE__CELL_COLOR_BLACK = "s";

        private const char CODE__SEPERATOR_MOVE = ',';


        internal int GridSize { get; private set; }
        internal int CountSteps { get; private set; }

        public int AntStartPosX { get; private set; }
        public int AntStartPosY { get; private set; }
        public AntDirectionValue AntStartDirection { get; private set; }

        private int AntPosX { get; set; }
        private int AntPosY { get; set; }

        private AntDirectionValue AntDirection { get; set; }


        private readonly CellData[,] gridCellData;
        private readonly List<CellData> listCellData = new();

        private readonly List<string> listGameSteps = new List<string>();

        public GameData(int gridSize, int countSteps, int antPosX, int antPosY, AntDirectionValue antDirection)
        {
            GridSize = gridSize;
            CountSteps = countSteps;

            AntPosX = antPosX;
            AntPosY = antPosY;
            AntDirection = antDirection;

            AntStartPosX = antPosX;
            AntStartPosY = antPosY;
            AntStartDirection = antDirection;

            gridCellData = new CellData[gridSize, gridSize];

            for (var indexX = 0; indexX < gridSize; indexX++)
            {
                for (var indexY = 0; indexY < gridSize; indexY++)
                {
                    var cellData = new CellData();
                    gridCellData[indexX, indexY] = cellData;
                    listCellData.Add(cellData);
                }
            }
        }

        internal void LoadCode(string code)
        {
            var listDrawData = GetListDrawDataByIndex(code);

            if (listCellData.Count != listDrawData.Count)
                return;

            var index = 0;

            for (var indexX = 0; indexX < GridSize; indexX++)
            {
                for (var indexY = 0; indexY < GridSize; indexY++)
                {
                    var drawData = listDrawData[index];
                    var cellData = listCellData[index];

                    cellData.CellValue = drawData.CellValue;
                    if (drawData.AntDirectionValue != null)
                    {
                        AntDirection = drawData.AntDirectionValue;
                        AntPosX = indexX;
                        AntPosY = indexY;
                    }

                    index++;
                }
            }

            CalculateAllSteps();
        }

        internal void CalculateAllSteps()
        {
            listGameSteps.Clear();
            addGameStep();

            for (var indexSteps = 0; indexSteps < CountSteps; indexSteps++)
            {
                var currentCell = gridCellData[AntPosX, AntPosY];

                rotateAnt(currentCell);

                toggleCellData(currentCell);
                moveForwardAnt();
                addGameStep();
            }

            void rotateAnt(CellData cellData)
            {
                if (cellData.CellValue == CellValue.White) // turn 90° clickwise
                {
                    if (AntDirection == AntDirectionValue.North)
                    {
                        AntDirection = AntDirectionValue.East;
                        return;
                    }
                    if (AntDirection == AntDirectionValue.East)
                    {
                        AntDirection = AntDirectionValue.South;
                        return;
                    }
                    if (AntDirection == AntDirectionValue.South)
                    {
                        AntDirection = AntDirectionValue.West;
                        return;
                    }
                    if (AntDirection == AntDirectionValue.West)
                    {
                        AntDirection = AntDirectionValue.North;
                        return;
                    }
                    throw new NotImplementedException(AntDirection.Code);
                }

                if (cellData.CellValue == CellValue.Black) // turn 90° counter-clickwise
                {
                    if (AntDirection == AntDirectionValue.North)
                    {
                        AntDirection = AntDirectionValue.West;
                        return;
                    }
                    if (AntDirection == AntDirectionValue.West)
                    {
                        AntDirection = AntDirectionValue.South;
                        return;
                    }
                    if (AntDirection == AntDirectionValue.South)
                    {
                        AntDirection = AntDirectionValue.East;
                        return;
                    }
                    if (AntDirection == AntDirectionValue.East)
                    {
                        AntDirection = AntDirectionValue.North;
                        return;
                    }
                    throw new NotImplementedException(AntDirection.Code);
                }


                throw new NotImplementedException(cellData.CellValue.ToString());
            }

            void toggleCellData(CellData cellData)
            {
                if (cellData.CellValue == CellValue.White)
                {
                    cellData.CellValue = CellValue.Black;
                    return;
                }

                if (cellData.CellValue == CellValue.Black)
                {
                    cellData.CellValue = CellValue.White;
                    return;
                }

                throw new NotImplementedException(cellData.CellValue.ToString());
            }

            void moveForwardAnt()
            {
                if (AntDirection == AntDirectionValue.North)
                {
                    AntPosY--;
                    if (AntPosY < 0)
                        AntPosY = GridSize - 1;
                    return;
                }
                if (AntDirection == AntDirectionValue.East)
                {
                    AntPosX++;
                    if (AntPosX >= GridSize)
                        AntPosX = 0;
                    return;
                }
                if (AntDirection == AntDirectionValue.South)
                {
                    AntPosY++;
                    if (AntPosY >= GridSize)
                        AntPosY = 0;
                    return;
                }
                if (AntDirection == AntDirectionValue.West)
                {
                    AntPosX--;
                    if (AntPosX < 0)
                        AntPosX = GridSize - 1;
                    return;
                }
                throw new NotImplementedException(AntDirection.Code);

            }

            void addGameStep()
            {
                var sbGameStep = new StringBuilder();

                for (var indexX = 0; indexX < GridSize; indexX++)
                {
                    for (var indexY = 0; indexY < GridSize; indexY++)
                    {
                        if (indexX == AntPosX && indexY == AntPosY)
                            sbGameStep.Append(AntDirection.Code);


                        sbGameStep.Append(gridCellData[indexX, indexY].CellValue.Code).Append(CODE__SEPERATOR_MOVE);
                    }

                    sbGameStep.Append(Environment.NewLine);
                }

                listGameSteps.Add(sbGameStep.ToString());
            }
        }

        internal string GetCodeByIndex(int index)
        {
            if (index < 0 || index >= listGameSteps.Count)
                return null;
            return listGameSteps[index];
        }

        internal List<DrawData> GetListDrawDataByIndex(int index)
        {
            return GetListDrawDataByIndex(GetCodeByIndex(index));
        }

        private static List<DrawData> GetListDrawDataByIndex(string code)
        {
            var listDrawData = new List<DrawData>();
            if (string.IsNullOrEmpty(code))
                return listDrawData;

            // parse code
            var arrCode = code.Split(CODE__SEPERATOR_MOVE);
            for (var indexCode = 0; indexCode < arrCode.Length - 1; indexCode++)
            {
                var codeLoop = arrCode[indexCode];

                AntDirectionValue antDirection = null;
                if (codeLoop.Length == 2)
                {
                    if (codeLoop.StartsWith(CODE__ANT_DIRECTION_NORTH))
                        antDirection = AntDirectionValue.North;
                    else if (codeLoop.StartsWith(CODE__ANT_DIRECTION_EAST))
                        antDirection = AntDirectionValue.East;
                    else if (codeLoop.StartsWith(CODE__ANT_DIRECTION_SOUTH))
                        antDirection = AntDirectionValue.South;
                    else if (codeLoop.StartsWith(CODE__ANT_DIRECTION_WEST))
                        antDirection = AntDirectionValue.West;

                    codeLoop = codeLoop.Substring(1);
                }

                var cellValue = codeLoop == CODE__CELL_COLOR_BLACK ? CellValue.Black : CellValue.White;

                listDrawData.Add(new DrawData(cellValue, antDirection));
            }

            return listDrawData;
        }


        internal class CellData
        {
            internal CellValue CellValue { get; set; } = CellValue.White;
        }

        internal class CellValue
        {
            internal static readonly CellValue White = new(CODE__CELL_COLOR_WHITE);
            internal static readonly CellValue Black = new(CODE__CELL_COLOR_BLACK);

            internal string Code { get; private set; }

            private CellValue(string code)
            {
                Code = code;
            }
        }

        public class AntDirectionValue
        {
            internal static readonly AntDirectionValue North = new(CODE__ANT_DIRECTION_NORTH);
            internal static readonly AntDirectionValue East = new(CODE__ANT_DIRECTION_EAST);
            internal static readonly AntDirectionValue South = new(CODE__ANT_DIRECTION_SOUTH);
            internal static readonly AntDirectionValue West = new(CODE__ANT_DIRECTION_WEST);

            internal string Code { get; private set; }

            private AntDirectionValue(string code)
            {
                Code = code;
            }
        }


        internal class DrawData
        {
            internal CellValue CellValue { get; private set; }
            internal AntDirectionValue AntDirectionValue { get; private set; }

            internal DrawData(CellValue cellValue, AntDirectionValue antDirectionValue)
            {
                CellValue = cellValue;
                AntDirectionValue = antDirectionValue;
            }
        }
    }
}
