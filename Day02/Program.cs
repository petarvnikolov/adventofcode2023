int maxRed = 12;
int maxGreen = 13;
int maxBlue = 14;

int GetGameID(string line)
{
    string gameID = "0";

    string idSubstring = line.Substring(5, 3);

    foreach (char character in idSubstring)
    {
        if (char.IsDigit(character))
        {
            gameID += "" + character;
        }
    }

    return int.Parse(gameID);
}

bool GameIsPossible(string line)
{
    string[] sets = line.Split(';');
    int indexOfColon = sets[0].IndexOf(':');
    sets[0] = sets[0].Substring(indexOfColon + 2);

    foreach (string set in sets)
    {
        string[] cubesInSet = set.Split(",");

        for (int i = 0; i < cubesInSet.Length; i++)
        {
            cubesInSet[i] = cubesInSet[i].TrimStart();

            int redIndex = cubesInSet[i].IndexOf("red");
            int greenIndex = cubesInSet[i].IndexOf("green");
            int blueIndex = cubesInSet[i].IndexOf("blue");

            int redCubes = 0;
            int greenCubes = 0;
            int blueCubes = 0;

            if (redIndex != -1)
            {
                cubesInSet[i] = cubesInSet[i].Substring(0, redIndex - 1);

                redCubes = int.Parse(cubesInSet[i]);

                if (redCubes > maxRed)
                {
                    return false;
                }
            }

            if (greenIndex != -1)
            {
                cubesInSet[i] = cubesInSet[i].Substring(0, greenIndex - 1);

                greenCubes = int.Parse(cubesInSet[i]);

                if (greenCubes > maxGreen)
                {
                    return false;
                }
            }

            if (blueIndex != -1)
            {
                cubesInSet[i] = cubesInSet[i].Substring(0, blueIndex - 1);

                blueCubes = int.Parse(cubesInSet[i]);

                if (blueCubes > maxBlue)
                {
                    return false;
                }
            }

        }
    }

    return true;
}

int GetPower(string line)
{
    string[] sets = line.Split(';');
    int indexOfColon = sets[0].IndexOf(':');
    sets[0] = sets[0].Substring(indexOfColon + 2);

    int mostRedCubes = 0;
    int mostGreenCubes = 0;
    int mostBlueCubes = 0;

    foreach (string set in sets)
    {
        string[] cubesInSet = set.Split(",");

        for (int i = 0; i < cubesInSet.Length; i++)
        {
            cubesInSet[i] = cubesInSet[i].TrimStart();

            int redIndex = cubesInSet[i].IndexOf("red");
            int greenIndex = cubesInSet[i].IndexOf("green");
            int blueIndex = cubesInSet[i].IndexOf("blue");

            int redCubes = 0;
            int greenCubes = 0;
            int blueCubes = 0;

            if (redIndex != -1)
            {
                cubesInSet[i] = cubesInSet[i].Substring(0, redIndex - 1);

                redCubes = int.Parse(cubesInSet[i]);

                if (mostRedCubes == 0 || mostRedCubes < redCubes)
                {
                    mostRedCubes = redCubes;
                }
            }

            if (greenIndex != -1)
            {
                cubesInSet[i] = cubesInSet[i].Substring(0, greenIndex - 1);

                greenCubes = int.Parse(cubesInSet[i]);

                if (mostGreenCubes == 0 || mostGreenCubes < greenCubes)
                {
                    mostGreenCubes = greenCubes;
                }
            }

            if (blueIndex != -1)
            {
                cubesInSet[i] = cubesInSet[i].Substring(0, blueIndex - 1);

                blueCubes = int.Parse(cubesInSet[i]);

                if (mostBlueCubes == 0 || mostBlueCubes < blueCubes)
                {
                    mostBlueCubes = blueCubes;
                }
            }

        }
    }

    int power = mostRedCubes * mostGreenCubes * mostBlueCubes;

    return power;
}

int sumOfIds = 0;
int sumOfPowers = 0;

while (Console.ReadLine() is { } line)
{
    if (GameIsPossible(line))
    {
        sumOfIds += GetGameID(line);
    }

    sumOfPowers += GetPower(line);
}

Console.WriteLine("Part 1: " + sumOfIds);
Console.WriteLine("Part 2: " + sumOfPowers);
