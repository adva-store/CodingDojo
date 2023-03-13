using StrangeChessboard;

namespace TestProject
{
    public class UnitTestMainPageViewModel
    {
        [SetUp]
        public void Setup()
        { }

        [Test]
        public void Test_MainPageViewMoel_GetSum()
        {
            var viewModel = new MainPageViewModel();

            Assert.IsTrue(viewModel.GetSum(new int[] { 3, 1, 2, 7, 1 }) == 14);
            Assert.IsTrue(viewModel.GetSum(new int[] { 1, 8, 4, 5, 2 }) == 20);
            Assert.IsTrue(viewModel.GetSum(new int[] { 1 }) == 1);
        }

        [Test]
        public void Test_MainPageViewMoel_CalculateArea()
        {
            var cs = new int[] { 3, 1, 2, 7, 1 };
            var rs = new int[] { 1, 8, 4, 5, 2 };

            var viewModel = new MainPageViewModel();
            var result = viewModel.CalculateArea(cs, rs);

            var totalArea = result.Item1 + result.Item2;
            var totalSum = viewModel.GetSum(cs) * viewModel.GetSum(rs);

            Assert.IsTrue(totalArea == totalSum);
        }


        [Test]
        public void Test_MainPageViewMoel_CalculateAreaRandom()
        {
            var random = new Random();
            var randomSize = random.Next(1, 5000);

            var cs = generateRandom(randomSize);
            var rs = generateRandom(randomSize);

            int[] generateRandom(int size)
            {
                var listRandom = new List<int>();
                for (var index = 0; index < size; index++)
                {
                    listRandom.Add(random.Next(1, 5000));
                }
                return listRandom.ToArray();
            }

            var viewModel = new MainPageViewModel();
            var result = viewModel.CalculateArea(cs, rs);

            var totalArea = result.Item1 + result.Item2;
            var totalSum = viewModel.GetSum(cs) * viewModel.GetSum(rs);

            Assert.IsTrue(totalArea == totalSum);
        }
    }
}