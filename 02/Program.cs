using System.Collections;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

// a report only counts as safe if both of the following are true:
// The levels are either all increasing or all decreasing.
// Any two adjacent levels differ by at least one and at most three.

// solution: How many reports are safe?

class Program
{
    public static void Main(string[] args)
    {
        // parse intput
        string inputRaw = File.ReadAllText("./input.txt");
        List<List<int>> input = new List<List<int>>();
        var listStr = inputRaw.Split('\n').ToList();

        // transform string list to 2d int list
        foreach (string s in listStr)
        {
            List<int> report = s.Split(" ").Select(Int32.Parse).ToList();
            input.Add(report);
        }
        System.Console.WriteLine($"solution for 1: {GetSafeReports(input)}");
        System.Console.WriteLine($"solution for 2: {GetSafeReportsDampened(input)}");
    }

    // solution for part 1
    static int GetSafeReports(List<List<int>> input)
    {
        // loop over all reports and check conditions 
        int safeReports = 0;

        foreach (List<int> report in input)
        {
            // check first condition: increasing or decreasing
            bool isIncreasing = true;
            bool isDecreasing = true;
            bool isValidDiff = true;

            for (int i = 0; i < report.Count - 1; i++)
            {
                if (report[i] < report[i + 1])
                {
                    // of one is found, report is unsafe
                    isDecreasing = false;
                }
                if (report[i] > report[i + 1])
                {
                    isIncreasing = false;
                }

                int diff = Math.Abs(report[i] - report[i + 1]);
                if (diff < 1 || diff > 3)
                {
                    isValidDiff = false;
                    break;
                }
            }

            if ((isIncreasing || isDecreasing) && isValidDiff)
            {
                safeReports++;
            }
        }
        return safeReports;
    }

    // solution for part 2, this is duplicate code but idc
    static bool isReportSafe(List<int> report)
    {
        bool isIncreasing = true;
        bool isDecreasing = true;
        bool isValidDiff = true;

        for (int i = 0; i < report.Count - 1; i++)
        {
            if (report[i] < report[i + 1])
            {
                isDecreasing = false;
            }
            if (report[i] > report[i + 1])
            {
                isIncreasing = false;
            }

            int diff = Math.Abs(report[i] - report[i + 1]);
            if (diff < 1 || diff > 3)
            {
                isValidDiff = false;
                break;
            }
        }

        return (isIncreasing || isDecreasing) && isValidDiff;
    }

    // fuck it we bruteforce
    static int GetSafeReportsDampened(List<List<int>> reports)
    {
        int safeReports = 0;

        foreach (var report in reports)
        {
            if (!isReportSafe(report))
            {
                // try to remove every element
                for (int i = 0; i < report.Count; i++)
                {
                    var modifiedReport = new List<int>(report);
                    modifiedReport.RemoveAt(i);
                    if (isReportSafe(modifiedReport))
                    {
                        safeReports++;
                        break;
                    }
                }
            }
            else
            {
                safeReports++;
            }
        }
        return safeReports;
    }
}