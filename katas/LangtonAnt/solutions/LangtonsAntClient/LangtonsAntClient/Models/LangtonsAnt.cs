using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangtonsAntClient.Models
{
    class LangtonsAnt
    {

        public int EdgeLength { get; set; }

        public int NumberOfSteps { get; set; }

        public int StartX { get; set; }

        public int StartY { get; set; }

        public string StartDirection { get; set; }

        public string ResultText { get; set; }

        public string ErrMessage { get; set; }
    }
}
