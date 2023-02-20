using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangtonBackend
{
    /// <summary>
    /// Eine Position innerhalb des Feldes.
    /// </summary>
    internal class Position
    {
        public int X { get; set; }
        public int Y { get; set; }

        /// <summary>
        /// Default: false
        /// </summary>
        public bool IsBlack { get; set; }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
            IsBlack= false; 
        }

        /// <summary>
        /// Weiße Feldposition erstellen.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="field">Das "Parent" Feld der Position</param>
        public Position(int x, int y, Field field)
        {
            X = x;
            Y = y;
            IsBlack = false;
        }
    }
}
