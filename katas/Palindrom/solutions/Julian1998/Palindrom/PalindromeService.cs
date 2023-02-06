using System.Text.RegularExpressions;

namespace Palindrome
{
    public class PalindromeService
    {
        public PalindromeService() { }

        public bool IsPalindrome(string word)
        {
            if (string.IsNullOrEmpty(word))
            {
                return false;
            }

            // trim punctuation and capitalize letters
            word = removePunctuation(word).ToUpper();

            // if tested string returns empty => we have a palindrome
            if (!string.IsNullOrEmpty(testPalindromeRecursive(word)))
            {
                return false;
            }

            return true;
        }

        private string testPalindromeRecursive(string word)
        {
            // one (or no) letter is always a palindrome
            if (word.Length <= 1)
            {
                return "";
            }

            if (word[0] == word[word.Length - 1])
            {
                word = word.Substring(1, word.Length - 2);
                return testPalindromeRecursive(word);
            } else
            {
                return word;
            }
        }

        private string removePunctuation(string str)
        {
            return Regex.Replace(str, @"[^\w\d]", "");
        }
    }
}
