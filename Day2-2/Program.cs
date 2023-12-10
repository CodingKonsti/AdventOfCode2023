using System.Diagnostics;
using System.Text.RegularExpressions;

var sr = new StreamReader(@"..\..\..\Data\input.txt");

var stopWatch = new Stopwatch();
stopWatch.Start();

int value = 0;

string patternRed = "([0-9]+\\s+){1}red";
string patternGreen = "([0-9]+\\s+){1}green";
string patternBlue = "([0-9]+\\s+){1}blue";

while (sr.Peek() >= 0)
{
    var line = sr.ReadLine();
    string[] split = line.Split(':');
    var sets = split[1].Split(";");

    int minimumRed = 0;
    int minimumGreen = 0;
    int minimumBlue = 0;

    foreach (var set in sets)
    {
        MatchCollection matchesRed = Regex.Matches(set, patternRed);
        var countRed = int.Parse(matchesRed.FirstOrDefault()?.Groups[1].ToString() ?? "0");
        if (countRed > minimumRed)
        {
            minimumRed = countRed;
        }

        MatchCollection matchesGreen = Regex.Matches(set, patternGreen);
        var countGreen = int.Parse(matchesGreen.FirstOrDefault()?.Groups[1].ToString() ?? "0");
        if (countGreen > minimumGreen)
        {
            minimumGreen = countGreen;
        }

        MatchCollection matchesBlue = Regex.Matches(set, patternBlue);
        var countBlue = int.Parse(matchesBlue.FirstOrDefault()?.Groups[1].ToString() ?? "0");
        if (countBlue > minimumBlue)
        {
            minimumBlue = countBlue;
        }
    }

    value += minimumRed * minimumGreen * minimumBlue;
}

Console.WriteLine(value);
stopWatch.Stop();
TimeSpan ts = stopWatch.Elapsed;
Console.WriteLine("RunTime " + ts.ToString());