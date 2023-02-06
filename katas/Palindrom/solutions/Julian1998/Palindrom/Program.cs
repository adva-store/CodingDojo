namespace Palindrome;

internal class Program
{
    static void Main()
    {
        Start();

        Run();

        Stop();
    }


    public static void Run()
    {
        PalindromeService service = new PalindromeService();
        string input = "go";

        while (!string.IsNullOrEmpty(input))
        {
            Console.WriteLine("Enter word:");

            try
            {
                input = Console.ReadLine(); 
            } catch(Exception e) {
                Console.WriteLine(String.Format("Input could not be parsed! {0}{1}", e.ToString(), Environment.NewLine));
                break;
            }
           

            if (string.IsNullOrEmpty(input))
            {
                break;
            }

            if (service.IsPalindrome(input))
            {
                Console.WriteLine(String.Format("{0} is a palindrome{1}", input, Environment.NewLine));
            }
            else
            {
                Console.WriteLine(String.Format("{0} is NOT a palindrome{1}", input, Environment.NewLine));
            }
        }
    }

    public static void Start()
    {
        Console.WriteLine(String.Format("{0} started{1}", DateTime.Now, Environment.NewLine));
    }

    public static void Stop()
    {
        Console.WriteLine(String.Format("{0} stopped{1}", DateTime.Now, Environment.NewLine));
    }
}