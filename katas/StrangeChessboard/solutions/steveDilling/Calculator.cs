using System;

namespace StrangeChessboard
{
    public class Calculator
    {
        private int[] cs = new int[0];
        private int[] rs = new int[0];

        public Calculator(int[] cs, int[] rs)
        {
            this.cs = cs;
            this.rs = rs;
        }


        public Tuple<int, int> Calculate(){

            return new Tuple<int, int>(calculateColor(true),calculateColor(false));
        }

        private int calculateColor(bool white = true)
        {
            int result = 0;
            for(int rowIndex = 0; rowIndex < rs.Length; rowIndex++){
                result += calculateRowColorSpace(rowIndex, white);
            }

            return result;
        }

        private int calculateRowColorSpace(int row, bool white = true)
        {
            int result = 0;
            int startColumn = 0;
            if(white && row % 2 == 1){
                startColumn = 1;
            }
            else if(!white && row % 2 == 0){
                startColumn = 1;
            }
            
            for(int column = startColumn; column < cs.Length; column += 2){
                result += (cs[column] * rs[row]);
            }

            return result;
        }

    }
}