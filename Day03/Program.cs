using Tools; // Using code from https://github.com/MartinZikmund/advent-of-code-2023 because I didn't have time...

var input = InputTools.ReadAllLines();

int PartOne()
{
    var width = input[0].Length;
    var height = input.Length;

    var map = new char[width, height];
    for (var x = 0; x < width; x++)
    {
        for (var y = 0; y < height; y++)
        {
            map[x, y] = input[y][x];
        }
    }

    var runningTotal = 0;
    var currentNumber = 0;
    var hasNeighboringSymbol = false;

    for (var y = 0; y < height; y++)
    {
        void EndCurrentNumber()
        {
            if (currentNumber != 0 && hasNeighboringSymbol)
            {
                runningTotal += currentNumber;
            }
            currentNumber = 0;
            hasNeighboringSymbol = false;
        }

        for (var x = 0; x < height; x++)
        {
            var character = map[x, y];
            // check if we are reading a number
            if (char.IsDigit(character))
            {
                var value = character - '0';
                currentNumber = currentNumber * 10 + value;
                foreach (var direction in Directions.WithDiagonals)
                {
                    var neigbhorX = x + direction.X;
                    var neigbhorY = y + direction.Y;
                    if (neigbhorX < 0 || neigbhorX >= width || neigbhorY < 0 || neigbhorY >= height)
                    {
                        continue;
                    }

                    var neighborCharacter = map[neigbhorX, neigbhorY];
                    if (!char.IsDigit(neighborCharacter) && neighborCharacter != '.')
                    {
                        hasNeighboringSymbol = true;
                    }
                }
            }
            else
            {
                EndCurrentNumber();
            }
        }

        EndCurrentNumber();
    }

    return runningTotal;
}

int PartTwo()
{
    var width = input[0].Length;
    var height = input.Length;

    var map = new char[width, height];
    for (var x = 0; x < width; x++)
    {
        for (var y = 0; y < height; y++)
        {
            map[x, y] = input[y][x];
        }
    }

    var runningTotal = 0;
    var currentNumber = 0;
    var asterisks = new Dictionary<Point, List<int>>();
    var neighboringAsterisks = new HashSet<Point>();

    for (var y = 0; y < height; y++)
    {
        void EndCurrentNumber()
        {
            if (currentNumber != 0 && neighboringAsterisks.Count > 0)
            {
                foreach (var neighboringAsterisk in neighboringAsterisks)
                {
                    var x = neighboringAsterisk.X;
                    var y = neighboringAsterisk.Y;
                    if (!asterisks.ContainsKey((x, y)))
                    {
                        asterisks[(x, y)] = [];
                    }

                    asterisks[(x, y)].Add(currentNumber);
                }
            }
            currentNumber = 0;
            neighboringAsterisks.Clear();
        }

        for (var x = 0; x < height; x++)
        {
            var character = map[x, y];
            // check if we are reading a number
            if (char.IsDigit(character))
            {
                var value = character - '0';
                currentNumber = currentNumber * 10 + value;
                foreach (var direction in Directions.WithDiagonals)
                {
                    var neigbhorX = x + direction.X;
                    var neigbhorY = y + direction.Y;
                    if (neigbhorX < 0 || neigbhorX >= width || neigbhorY < 0 || neigbhorY >= height)
                    {
                        continue;
                    }

                    var neighborCharacter = map[neigbhorX, neigbhorY];
                    if (neighborCharacter == '*')
                    {
                        neighboringAsterisks.Add((neigbhorX, neigbhorY));
                    }
                }
            }
            else
            {
                EndCurrentNumber();
            }
        }

        EndCurrentNumber();
    }

    foreach (var (point, numbers) in asterisks)
    {
        if (numbers.Count == 2)
        {
            runningTotal += numbers[0] * numbers[1];
        }
    }

    return runningTotal;
}

Console.WriteLine("Part 1: " + PartOne());
Console.WriteLine("Part 2: " + PartTwo());
