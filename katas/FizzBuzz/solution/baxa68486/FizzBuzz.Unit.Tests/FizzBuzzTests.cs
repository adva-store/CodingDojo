using FluentAssertions;
using Xunit;

namespace FizzBuzz.Unit.Tests
{
    public class FizzBuzzTests
    {
        private readonly RulesEngine _rulesEngine = new RulesEngine();

        [Theory]
        [InlineData(3)]
        public void EveluateNumbers_CheckDivisionToThreeRule_ReturnsFizz(int n)
        {
            var expected = new List<string> { "1", "2", "Fizz" };
            var res = _rulesEngine.EveluateNumbers(n);
            expected.Should().BeEquivalentTo(res);
        }

        [Theory]
        [InlineData(5)]
        public void EveluateNumbers_CheckDivisionToFiveRule_ReturnsBuzz(int n)
        {
            var expected = new List<string> { "1", "2", "Fizz", "4", "Buzz" };
            var res = _rulesEngine.EveluateNumbers(n);
            expected.Should().BeEquivalentTo(res);
        }


        [Theory]
        [InlineData(15)]
        public void EveluateNumbers_CheckDivisionToThreeAndFiveRule_ResultContainsFizzBuzz(int n)
        {
            var res = _rulesEngine.EveluateNumbers(n);
            res.Should().Contain("FizzBuzz");
        }
    }
}