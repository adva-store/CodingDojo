using System.Data;
using System.Text;

namespace FizzBuzz
{
    /*
        It could be implemented using OOP by encapsulating rules in separate classes
        and handling them in RulesEngine
    */
    public class RulesEngine
    {
        List<KeyValuePair<Func<int, bool>, string>> _rules =
                                new List<KeyValuePair<Func<int, bool>, string>>
        {
            new KeyValuePair<Func<int, bool>, string>(x => x%3 == 0, "Fizz"),
            new KeyValuePair<Func<int, bool>, string>(x => x%5 == 0, "Buzz"),
        };

        public List<string> EveluateNumbers(int n)
        {
            List<string> ls = new List<string>();
            var res = new StringBuilder();

            for (int i = 1; i <= n; i++)
            {
                foreach (var rule in _rules)
                {
                    if (rule.Key(i))
                    {
                        res.Append(rule.Value);
                    }
                }
                if (res.Length == 0)
                    ls.Add(i.ToString());
                else
                    ls.Add(res.ToString());

                res.Clear();
            }
            return ls;
        }
    }
}
