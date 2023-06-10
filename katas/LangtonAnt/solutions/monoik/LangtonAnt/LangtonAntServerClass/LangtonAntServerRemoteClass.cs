using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace LangtonAntServerClass
{
    public class LangtonAntServerRemoteClass : MarshalByRefObject
    {
		private static readonly Dictionary<string, GameState> runningGameStates = new Dictionary<string, GameState>();

        private class GameState
        {
            // Unique Id for identifing the game by clients.
            public string gameId;
            // The size of the play field. It's always square.
            public readonly int playFieldSize;

            // The current postition of the ant.
            public (int X, int Y) antPos;

            // The current orientation of the ant.
            public AntOrientation antOrientation;

            // A helper field showing the white/black status of the playing field. False is white, true is black.
            public bool[,] playField;

            // The current move number, counted from 1.
            public int currentMoveNumber;

            // Total moves the client should show.
            public int totalMoves;

            public GameState(string gameId, int playFieldSize, int movesCount)
            {
                this.gameId = gameId;
                this.playFieldSize = playFieldSize;
                totalMoves = movesCount;
                playField = new bool[playFieldSize, playFieldSize];
            }

			internal string GetStateString()
			{
                string result = "";
                for (int y = 0; y < playFieldSize; y++)
				{
                    for (int x = 0; x < playFieldSize; x++)
                    {
                        if (antPos.X == x && antPos.Y == y)
                        {
                            result += GetOrientationString(antOrientation);

                        }
                        result += GetColorString(x, y) + ",";
					}
				}
                return result;
			}

			private string GetOrientationString(AntOrientation antOrientation)
            {
                if (antOrientation == AntOrientation.West) return "w";
                if (antOrientation == AntOrientation.North) return "n";
                if (antOrientation == AntOrientation.East) return "o";
                if (antOrientation == AntOrientation.South) return "s";
                return "";
            }
            private Color GetColor(int x, int y)
            {
                var currField = playField[x,y];
                if (currField == false) return Colors.White;
                if (currField == true) return Colors.Black;
                return Colors.Red; // Error
            }

            private string GetColorString(int x, int y)
            {
                var color = GetColor(x, y);
                if (color == Colors.White) return "w";
                if (color == Colors.Black) return "s";

                return "";

            }

            private string GetColorString((int X, int Y) antPosition)
            {
                return GetColorString(antPosition.X, antPosition.Y);
            }

            internal void MoveAnt()
            {
				string currentColor = GetColorString(antPos);

                FlipCurrentField();

                antPos.X += GetDeltaX(antOrientation, currentColor);
                antPos.Y += GetDeltaY(antOrientation, currentColor);
                antOrientation = GetNextOrientation(antOrientation, currentColor);
            }

			private AntOrientation GetNextOrientation(AntOrientation antOrientation, string currentColor)
			{
                if (currentColor == "w")
				{
                    // rotating clockwise
                    switch (antOrientation)
					{
                        case AntOrientation.North:
                            return AntOrientation.East;
                        case AntOrientation.East:
                            return AntOrientation.South;
                        case AntOrientation.South:
                            return AntOrientation.West;
                        case AntOrientation.West:
                            return AntOrientation.North;
                    }
                } else
				{
                    // rotating anticlockwise
                    switch (antOrientation)
                    {
                        case AntOrientation.North:
                            return AntOrientation.West;
                        case AntOrientation.West:
                            return AntOrientation.South;
                        case AntOrientation.South:
                            return AntOrientation.East;
                        case AntOrientation.East:
                            return AntOrientation.North;
                    }
                }

                return AntOrientation.North;
			}

			private int GetDeltaX(AntOrientation antOrientation, string currentColor)
			{
                switch(antOrientation)
				{
                    case AntOrientation.East:
                    case AntOrientation.West:
                    default:
                        return 0;
                    case AntOrientation.North:
                        return currentColor == "w" ? 1 : -1;
                    case AntOrientation.South:
                        return currentColor == "w" ? -1 : 1;
                }
            }

			private int GetDeltaY(AntOrientation antOrientation, string currentColor)
			{
                switch (antOrientation)
                {
                    case AntOrientation.North:
                    case AntOrientation.South:
                    default:
                        return 0;
                    case AntOrientation.East:
                        return currentColor == "w" ? 1 : -1;
                    case AntOrientation.West:
                        return currentColor == "w" ? -1 : 1;
                }
            }

			internal void FlipCurrentField()
			{
                playField[antPos.X,antPos.Y] = !playField[antPos.X,antPos.Y];
            }
		}
        public enum AntOrientation { North, East, South, West, Unknown };

        public string StartNewGame(int fieldSize, int startPosX, int startPosY, AntOrientation orientation, int movesCount)
        {
            try
            {
				string gameId = Guid.NewGuid().ToString();
				GameState state = new GameState(gameId, fieldSize, movesCount)
                {
                    antPos = (startPosX, startPosY),
                    antOrientation = orientation,
                };
                runningGameStates.Add(gameId, state);

                Console.WriteLine("New game created: " + gameId);

                return gameId.ToString();
            }
            catch
            {
                return "";
            }
        }

        public string GetGameState(string gameId)
        {
            try
            {
                var game = runningGameStates.FirstOrDefault(g => g.Key == gameId);
                return game.Value.GetStateString();
            } catch
			{
                return "";
			}
        }

        public string GetNextGameState(string gameId)
		{
            try
            {
                var game = runningGameStates.FirstOrDefault(g => g.Key == gameId).Value;

				game.MoveAnt();

                game.currentMoveNumber++;
                return game.GetStateString();
            }
            catch
            {
                return "";
            }
        }
    } 
}
