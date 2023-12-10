using System.Diagnostics;
using System.Text.RegularExpressions;

var sr = new StreamReader(@"..\..\..\Data\input.txt");

var stopWatch = new Stopwatch();
stopWatch.Start();

int redCubes = 12;
int greenCubes = 13;
int blueCubes = 14;

bool validGame;

int value = 0;

string patternRed= "([0-9]+\\s+){1}red";
string patternGreen = "([0-9]+\\s+){1}green";
string patternBlue = "([0-9]+\\s+){1}blue";

while (sr.Peek() >= 0)
{
    var line = sr.ReadLine();
    string[] split = line.Split(':');
    var sets = split[1].Split(";");

    validGame = true;
    foreach (var set in sets)
    {
        MatchCollection matchesRed = Regex.Matches(set, patternRed);
        var countRed = int.Parse(matchesRed.FirstOrDefault()?.Groups[1].ToString() ?? "0");
        if (countRed > redCubes)
        {
            validGame = false;
            break;
        }

        MatchCollection matchesGreen = Regex.Matches(set, patternGreen);
        var countGreen = int.Parse(matchesGreen.FirstOrDefault()?.Groups[1].ToString() ?? "0");
        if (countGreen > greenCubes)
        {
            validGame = false;
            break;
        }

        MatchCollection matchesBlue = Regex.Matches(set, patternBlue);
        var countBlue = int.Parse(matchesBlue.FirstOrDefault()?.Groups[1].ToString() ?? "0");
        if (countBlue > blueCubes)
        {
            validGame = false;
            break;
        }
    }
    if(validGame)
    {
        value += int.Parse(split[0].Split(' ')[1]);
    }
}

Console.WriteLine(value);
stopWatch.Stop();
TimeSpan ts = stopWatch.Elapsed;
Console.WriteLine("RunTime " + ts.ToString());