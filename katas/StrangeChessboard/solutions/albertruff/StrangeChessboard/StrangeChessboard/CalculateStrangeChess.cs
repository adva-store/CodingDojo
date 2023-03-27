using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrangeChessboard
{
    public static class CalculateStrangeChess
    {
        public static Tuple<int, int> CalculateChessboardColorAreas(List<int> cs, List<int> rs)
        {
            int totalWhiteArea = 0;
            int totalBlackArea = 0;
            int fieldCounter = 0;
            Tuple<int,int> retTuple = new Tuple<int,int>(0, 0);

            if (cs.Count != rs.Count || cs.Count <= 0) {
                return retTuple;
            }

            foreach (var col in cs)
            {
                foreach (var row in rs)
                {
                    if (fieldCounter % 2 == 0 )
                    {
                        totalWhiteArea += col * row;
                    }
                    else
                    {
                        totalBlackArea += col * row;
                    }
                    fieldCounter++;
                }
            }
            
            retTuple = Tuple.Create(totalWhiteArea, totalBlackArea);

            return retTuple;
        }
    }
}
