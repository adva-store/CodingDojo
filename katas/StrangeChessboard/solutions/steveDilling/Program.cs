using System;
using System.Collections.Generic;
using System.IO;

namespace StrangeChessboard;

static class Program{
    public static void Main(params string[] args)
    {
        int[] rs = new int[0];
        int[] cs = new int[0];

        if(args.Length == 2){
            cs = readFile(args[0]);
            rs = readFile(args[1]);
            
        }
        else{
            //Let user input values
            cs = userInput("cs");
            rs = userInput("rs");
        }

        Calculator calc = new Calculator(cs, rs);
        var res = calc.Calculate();
        Console.WriteLine("({0}, {1})", res.Item1, res.Item2);
    }

    private static int[] userInput(string forWhat){
        
        Console.WriteLine("Please input comma separated {0}. Enter RETURN when ready", forWhat);
        string? input = Console.ReadLine();
        return separateInput(input);
    }

    private static int[] separateInput(string? input){
        if(string.IsNullOrWhiteSpace(input)){
            return new int[0];
        }

        List<int> result = new();
        string[] values = input.Split(",", StringSplitOptions.RemoveEmptyEntries);
        foreach(var val in values){
            int t = 0;
            if(int.TryParse(val, out t)){
                result.Add(t);
            }
        }

        return result.ToArray();
    }

    private static int[] readFile(string path){
        if(!File.Exists(path)){
            Console.WriteLine("File {0} not found.", path);
            return new int[0];
        }
        using FileStream fileStream = new(path, FileMode.Open, FileAccess.Read, FileShare.Read);
        using StreamReader streamReader = new(fileStream);
        var input = streamReader.ReadToEnd();
        return separateInput(input.ReplaceLineEndings(","));
    }
}
