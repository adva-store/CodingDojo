using StrangeChessboard;

namespace StrangeChesboardUnitTests
{
    [TestClass]
    public class CalculateStrangeChessTest
    {
        [TestMethod]
        public void Chesboard_Empthy()
        {
            List<int> cs = new List<int>();
            List<int> rs = new List<int>();
            Tuple<int, int> ret = CalculateStrangeChess.CalculateChessboardColorAreas(cs, rs);
            Assert.IsTrue((ret.Item1 + ret.Item2) == 0);
        }

        [TestMethod]
        public void Chesboard_1()
        {
            List<int> cs = new List<int>() { 3, 1, 2, 7, 1 };
            List<int> rs = new List<int>() { 1, 8, 4, 5, 2 };
            Tuple<int, int> ret = CalculateStrangeChess.CalculateChessboardColorAreas(cs, rs);
            int fieldTotal = cs.Sum(x => x) * rs.Sum(x => x) ;
            Assert.IsTrue((ret.Item1 + ret.Item2) == fieldTotal);
        }
    }
}