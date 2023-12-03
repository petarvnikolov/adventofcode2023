Dictionary<string, string> replacements = new()
{
    {"one", "o1e"},
    {"two", "t2o"},
    {"three", "t3e"},
    {"four", "4"},
    {"five", "5e"},
    {"six", "6"},
    {"seven", "7n"},
    {"eight", "e8t"},
    {"nine", "n9e"}
};

int partOneResult = 0;
int partTwoResult = 0;

string[] wholeLines = File.ReadAllLines(@"./PuzzleInput.txt");

for (int i = 0; i < wholeLines.Length; i++)
{
    int firstDigitIndex = wholeLines[i].IndexOfAny("123456789".ToCharArray());
    int lastDigitIndex = wholeLines[i].LastIndexOfAny("123456789".ToCharArray());

    wholeLines[i] = "" + wholeLines[i][firstDigitIndex] + wholeLines[i][lastDigitIndex];

    partOneResult += int.Parse(wholeLines[i]);
}

wholeLines = File.ReadAllLines(@"./PuzzleInput.txt");

for (int i = 0; i < wholeLines.Length; i++)
{

    foreach (var replacement in replacements)
    {
        wholeLines[i] = wholeLines[i].Replace(replacement.Key, replacement.Value);
    }

    int firstDigitIndex = wholeLines[i].IndexOfAny("123456789".ToCharArray());
    int lastDigitIndex = wholeLines[i].LastIndexOfAny("123456789".ToCharArray());

    wholeLines[i] = "" + wholeLines[i][firstDigitIndex] + wholeLines[i][lastDigitIndex];

    partTwoResult += int.Parse(wholeLines[i]);
}

string[] results = { "Part 1: " + partOneResult, "Part 2: " + partTwoResult };

File.WriteAllLines(@"./PuzzleOutput.txt", results);
