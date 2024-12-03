using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Security.Cryptography;

class Program
{
    public static void Main(string[] args)
    {
        List<List<int>> input = ParseInput("input.txt");
        int totalDistance = GetTotalDistance(input);
        System.Console.WriteLine($"The total distance is: {totalDistance}");
        int simScore = GetSimilarityScore(input);
        System.Console.WriteLine($"The similarity score is: {simScore}");
    }

    // returns input as a 2D list with column1 and column2 as the dimensions
    static List<List<int>> ParseInput(string path)
    {
        var lines = File.ReadAllLines(path);
        var result = new List<List<int>> { new List<int>(), new List<int>() };

        foreach (var line in lines)
        {
            var numbers = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
            result[0].Add(numbers[0]);
            result[1].Add(numbers[1]);
        }
        return result;
    }

    static int GetTotalDistance(List<List<int>> input)
    {
        // pair up smallest numbers -> input[0].Min and input[1].Min
        // measure distance between them
        // add distance to total

        int totalDistance = 0;

        List<int> sortedList1 = input[0].OrderBy(x => x).ToList();
        List<int> sortedList2 = input[1].OrderBy(x => x).ToList();

        for (int i = 0; i < sortedList1.Count(); i++)
        {
            totalDistance += Math.Abs(sortedList1[i] - sortedList2[i]);
        }

        return totalDistance;
    }

    //Calculate a total similarity score by adding up each number in the left list after multiplying it by the number of times that number appears in the right list.
    static int GetSimilarityScore(List<List<int>> input)
    {
        int simScore = 0;

        List<int> sortedList1 = input[0].OrderBy(x => x).ToList();
        List<int> sortedList2 = input[1].OrderBy(x => x).ToList();

        Dictionary<int, int> mapping = sortedList1
        .ToDictionary(key => key, key => sortedList2.Count(x => x.Equals(key)));

        foreach (var pair in mapping)
        {
            simScore += pair.Key * pair.Value;
        }

        return simScore;
    }
}