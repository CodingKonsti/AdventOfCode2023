using System.Diagnostics;
using System.Text.RegularExpressions;

string starPattern = "[*]";

var stopWatch = new Stopwatch();
stopWatch.Start();

long value = 0;

var lines = File.ReadLines(@"..\..\..\Data\input.txt");
int rows = lines.Count();
int columns = lines.First().Count();
string[,] array = new string[rows, columns];

int row = 0;
var sr = new StreamReader(@"..\..\..\Data\input.txt");
while (sr.Peek() >= 0)
{
    var line = sr.ReadLine();
    var charLine = line.ToCharArray();
    int col = 0;
    foreach (char c in charLine)
    {
        array[row, col] = c.ToString();
        col++;
    }
    row++;
}

for (int i = 0; i < rows; i++)
{
    for (int j = 0; j < columns; j++)
    {
        MatchCollection matches = Regex.Matches(array[i, j], starPattern);
        if (matches.Count > 0)
        {
            if (IsGear(array,i,j))
            {
                int ratio = GetGearRatio(array, i, j);
                value += ratio;
            }
        }
    }
}

Console.WriteLine(value);
stopWatch.Stop();
TimeSpan ts = stopWatch.Elapsed;
Console.WriteLine("RunTime " + ts.ToString());

static bool IsGear(string[,] array, int row, int col)
{
    int numberCount = 0;
    for (int i = row - 1; i <= row + 1; i++)
    {
        for (int j = col - 1; j <= col + 1; j++)
        {
            if (i >= 0 && j >= 0 && i < array.GetLength(0) && j < array.GetLength(1))
            {
                bool isInt = int.TryParse(array[i, j], out int digit);
                if (isInt)
                {
                    numberCount++;
                    if (numberCount > 1)
                    {
                        return true;
                    }
                    int numberLen = GetNumberLength(array, i, j);
                    j = j + numberLen;
                }
            }
        }
    }
    return false;
}

static int GetGearRatio(string[,] array, int row, int col)
{
    var gearNumbers = new List<int>(2);
    for (int i = row - 1; i <= row + 1; i++)
    {
        for (int j = col - 1; j <= col + 1; j++)
        {
            if (i >= 0 && j >= 0 && i < array.GetLength(0) && j < array.GetLength(1))
            {
                bool isInt = int.TryParse(array[i, j], out int digit);
                if (isInt)
                {
                    int number = GetNumberAtLocation(array, i, j);
                    gearNumbers.Add(number);
                    int numberLen = GetNumberLength(array, i, j);
                    j = j + numberLen;
                }
            }
        }
    }
    int gearNumber = 1;
    foreach (var number in gearNumbers)
    {
        gearNumber *= number;
    }
    return gearNumber;
}

static int GetNumberAtLocation(string[,] array, int row, int col)
{
    int numberLen = 1;
    bool prevSymbolIsNumber = true;
    while (prevSymbolIsNumber)
    {
        if (col - numberLen >= 0)
        {
            bool isIntNext = int.TryParse(array[row, col - numberLen], out int digitNext);
            if (isIntNext)
            {
                numberLen++;
            }
            else
            {
                prevSymbolIsNumber = false;
            }
        }
        else
        {
            prevSymbolIsNumber = false;
        }
    }

    int startColumn = col - numberLen + 1;
    var digits = GetDigits(array, row, startColumn);

    int number = 0;
    int baseNumber = 1;
    digits.Reverse();
    foreach (int d in digits)
    {
        number += d * baseNumber;
        baseNumber *= 10;
    }
    return number;
}
static List<int> GetDigits(string[,] array, int row, int col)
{
    int numberLen = 0;
    var digits = new List<int>();
    bool nextSymbolIsNumber = true;
    while (nextSymbolIsNumber)
    {
        if (col + numberLen < array.GetLength(1))
        {
            bool isIntNext = int.TryParse(array[row, col + numberLen], out int digitNext);
            if (isIntNext)
            {
                digits.Add(digitNext);
                numberLen++;
            }
            else
            {
                nextSymbolIsNumber = false;
            }
        }
        else
        {
            nextSymbolIsNumber = false;
        }
    }
    return digits;
}

static int GetNumberLength(string[,] array, int row, int col)
{
    int numberLen = 1;
    bool nextSymbolIsNumber = true;
    while (nextSymbolIsNumber)
    {
        if (col + numberLen < array.GetLength(1))
        {
            bool isIntNext = int.TryParse(array[row, col + numberLen], out int digitNext);
            if (isIntNext)
            {
                numberLen++;
            }
            else
            {
                nextSymbolIsNumber = false;
            }
        }
        else
        {
            nextSymbolIsNumber = false;
        }
    }
    return numberLen;
}