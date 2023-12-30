using System.Diagnostics;
using System.Text.RegularExpressions;

var sr = new StreamReader(@"..\..\..\Data\input.txt");

string pattern = "[^a-zA-Z0-9.]";

var stopWatch = new Stopwatch();
stopWatch.Start();

int value = 0;

var lines = File.ReadLines(@"..\..\..\Data\input.txt");
int rows = lines.Count();
int columns = lines.First().Count();
string[,] array = new string[rows, columns];

int row = 0;
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
        bool isInt = int.TryParse(array[i,j], out int digit);
        if (isInt)
        {
            var digits = new List<int>
            {
                digit
            };
            int numberLen = 1;
            bool nextSymbolIsNumber = true;
            while (nextSymbolIsNumber)
            {
                if (j + numberLen < columns)
                {
                    bool isIntNext = int.TryParse(array[i, j + numberLen], out int digitNext);
                    if (isIntNext)
                    {
                        digits.Add(digitNext);
                        numberLen++;
                    } else
                    {
                        nextSymbolIsNumber = false;
                    }
                } else
                {
                    nextSymbolIsNumber = false;
                }
            }

            int number = 0;
            int baseNumber = 1;
            digits.Reverse();
            foreach (int d in digits)
            {
                number = number + d * baseNumber;
                baseNumber = baseNumber * 10;
            }
            if (HasAdjacentSymbol(array, i, j, numberLen, pattern))
            {
                value += number;
            }
            j = j + numberLen;
        }
    }
}

Console.WriteLine(value);
stopWatch.Stop();
TimeSpan ts = stopWatch.Elapsed;
Console.WriteLine("RunTime " + ts.ToString());

static bool HasAdjacentSymbol(string[,] array, int row, int col, int length, string pattern)
{
    for (int i = row - 1; i <= row + 1; i++)
    {
        for (int j = col - 1; j <= col + length; j++)
        {
            if (i > 0 && j > 0 && i < array.GetLength(0) && j < array.GetLength(1))
            {
                MatchCollection matches = Regex.Matches(array[i, j], pattern);
                if (matches.Count > 0)
                {
                    return true;
                }
            }
        }
    }
    return false;
}