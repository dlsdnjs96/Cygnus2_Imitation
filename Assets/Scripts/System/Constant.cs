using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constant
{
    public const float root3 = 1.73205f;
    public const float blockDiameter = 110.0f;
    public const float blockDistance = blockDiameter * root3 / 2.0f;

    public const float slidingDuration = 0.65f;


    public static Vector2[,] symbolLocation = new Vector2[7, 9];


    public static void Initialize()
    {
        CalculateSymbolLocation();
    }

    public static Vector2 GetSymbolLocation(Vector2Int vector2Int)
    {
        return symbolLocation[vector2Int.x, vector2Int.y];
    }
    static void CalculateSymbolLocation()
    {
        for (int i = 1; i <= 6; i++)
        {
            for (int j = 1; j <= 8; j++)
            {
                if (i % 2 == 0)
                    symbolLocation[i, j] = new Vector2(i * blockDistance, j * blockDiameter);
                else
                    symbolLocation[i, j] = new Vector2(i * blockDistance, (j * blockDiameter) - (blockDiameter / 2.0f));
                symbolLocation[i, j] -= new Vector2(4 * blockDistance, 4.5f * blockDiameter);
            }
        }
    }

    public static string GetSymbolName(int index)
    {
        string[] names = new string[] { "", "Low1", "Low2", "Low3", "Middle1", "Middle2", "Middle3", "High1", "Wild", "Scatter", "Multi2",
            "Multi3", "Multi5", "Multi10", "UsedScatter", "MultiWild2", "MultiWild3", "MultiWild5", "MultiWild10" };

        return names[index];
    }

    public static int GetSymbolIndex(string name)
    {
        Dictionary<string, int> indicies = new Dictionary<string, int>()
        {
            { "Low1", 1 },
            { "Low2", 2 },
            { "Low3", 3 },
            { "Middle1", 4 },
            { "Middle2", 5 },
            { "Middle3", 6 },
            { "High1", 7 },
            { "Wild", 8 },
            { "Scatter", 9 },
            { "Multi2", 10},
            { "Multi3", 11 },
            { "Multi5", 12 },
            { "Multi10", 13 },
            { "UsedScatter", 14 },
            { "MultiWild2", 15 },
            { "MultiWild3", 16 },
            { "MultiWild5", 17 },
            { "MultiWild10", 18}
        };

        return indicies[name];
    }

    public static int GetSymbolCountableIndex(string name)
    {
        Dictionary<string, int> indicies = new Dictionary<string, int>()
        {
            { "Low1", 1 },
            { "Low2", 2 },
            { "Low3", 3 },
            { "Middle1", 4 },
            { "Middle2", 5 },
            { "Middle3", 6 },
            { "High1", 7 },
            { "Wild", 8 },
            { "Scatter", 9 },
            { "Multi2", 10},
            { "Multi3", 11 },
            { "Multi5", 12 },
            { "Multi10", 13 },
            { "UsedScatter", 14 },
            { "MultiWild2", 8 },
            { "MultiWild3", 8 },
            { "MultiWild5", 8 },
            { "MultiWild10", 8}
        };

        return indicies[name];
    }
}
