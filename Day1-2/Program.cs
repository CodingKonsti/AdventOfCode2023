using System.Diagnostics;
using System.Text.RegularExpressions;

var numberDict = new Dictionary<string, int>();
numberDict.Add("0", 0);
numberDict.Add("1", 1);
numberDict.Add("2", 2);
numberDict.Add("3", 3);
numberDict.Add("4", 4);
numberDict.Add("5", 5);
numberDict.Add("6", 6);
numberDict.Add("7", 7);
numberDict.Add("8", 8);
numberDict.Add("9", 9);
numberDict.Add("one", 1);
numberDict.Add("two", 2);
numberDict.Add("three", 3);
numberDict.Add("four", 4);
numberDict.Add("five", 5);
numberDict.Add("six", 6);
numberDict.Add("seven", 7);
numberDict.Add("eight", 8);
numberDict.Add("nine", 9);

var sr = new StreamReader(@"..\..\..\Data\input.txt");
string pattern = "(?=([0-9]|one|two|three|four|five|six|seven|eight|nine))";

int firstDigit;
int lastDigit;
int value = 0;

var stopWatch = new Stopwatch();
stopWatch.Start();

while (sr.Peek() >= 0)
{
    var line = sr.ReadLine();

    MatchCollection matches = Regex.Matches(line, pattern);

    firstDigit = numberDict[matches.First().Groups[1].ToString()];
    lastDigit = numberDict[matches.Last().Groups[1].ToString()];

    value += firstDigit * 10 + lastDigit;
}

Console.WriteLine(value);
stopWatch.Stop();
TimeSpan ts = stopWatch.Elapsed;
Console.WriteLine("RunTime " + ts.ToString());