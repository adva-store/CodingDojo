using Palindrome;

namespace UnitTest
{
    [TestClass]
    public class PalindromeServiceTest
    {
        [TestMethod]
        [DynamicData(nameof(PalindromeData))]
        public void TestIsPalindrome(string word, bool expectedResult)
        {
            PalindromeService service = new PalindromeService();
            bool result = service.IsPalindrome(word);
            Assert.AreEqual(result, expectedResult);
        }

        private static IEnumerable<object[]> PalindromeData
        {
            get
            {
                return new[]
                {
                    new object[] { "Abba", true },
                    new object[] { "Lagerregal", true },
                    new object[] { "Reliefpfeiler", true },
                    new object[] { "Rentner", true },
                    new object[] { "Dienstmannamtsneid", true },
                    new object[] { "Tarne nie deinen Rat!", true },
                    new object[] { "Eine güldne, gute Tugend: Lüge nie!", true },
                    new object[] { "Ein agiler Hit reizt sie. Geist?! Biertrunk nur treibt sie. Geist ziert ihre Liga nie!", true },
                    new object[] { "a", true },
                    new object[] { "abc", false },
                    new object[] { "!AbcDcbba!", false },
                };
            }
        }
    }
}