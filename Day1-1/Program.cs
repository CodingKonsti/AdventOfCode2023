using System.Diagnostics;

var sr = new StreamReader(@"..\..\..\Data\input.txt");

var stopWatch = new Stopwatch();
stopWatch.Start();

int value = 0;

while (sr.Peek() >= 0)
{
    var line = sr.ReadLine();
    int strLength = line.Length;
    int firstDigit = 0;
    int lastDigit = 0;

    for (int i = 0; i < strLength; i++) {
        char c = line[i];
        bool isInt = int.TryParse(c.ToString(), out int digit);
        if (isInt)
        {
            firstDigit = digit;
            break;
        }
    }
    for (int i = strLength-1; i >=0; i--)
    {
        char c = line[i];
        bool isInt = int.TryParse(c.ToString(), out int digit);
        if (isInt)
        {
            lastDigit = digit;
            break;
        }
    }
    value += firstDigit * 10 + lastDigit;
}

Console.WriteLine(value);
stopWatch.Stop();
TimeSpan ts = stopWatch.Elapsed;
string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
    ts.Hours, ts.Minutes, ts.Seconds,
    ts.Milliseconds / 10);
Console.WriteLine("RunTime " + elapsedTime);