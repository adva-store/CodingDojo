// See https://aka.ms/new-console-template for more information


Console.WriteLine("Test case 1:");
var rs = new int[] { 3, 1, 2, 7, 1 };
var cs = new int[] { 1, 8, 4, 5, 2 };

var result=Calculate(rs, cs);

Console.WriteLine("total_white_area: " + result.Item1);
Console.WriteLine("total_black_area: " + result.Item2);

var testCase1 = ((result.Item1 + result.Item2) == (rs.Sum() * cs.Sum())) ? true : false;

Console.WriteLine("Test case 1 passed: " + testCase1.ToString());

//Determine the total white area and the total black area of his board.
(int, int) Calculate(int[]rs, int[]cs)
{
    (int total_white_area, int total_black_area) result=(0,0);

    bool w = true;
    foreach(int r in rs)
    {
        foreach(int c in cs)
        {
            if (w)
            {
                result.total_white_area += r * c;
            }
            else
            {
                result.total_black_area += r * c;
            }
            w = !w;
        }
    }
    return result;
}