using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util
{
    public static void Vector2IntToVector2(Vector2Int _vector2Int, out Vector2 _vector2)
    {
        _vector2.x = (float)_vector2Int.x;
        _vector2.y = (float)_vector2Int.y;
    }


    public static float GetYofCircle(float _diameter, float _x)
    {
        return Mathf.Sqrt(Mathf.Pow(_diameter, 2.0f) - Mathf.Pow(_x, 2.0f));
    }
    //public static void Vector2ToVector2Int(Vector2 _vector2, out Vector2Int _vector2Int)
    //{
    //    _vector2Int.x = (int)_vector2.x;
    //    _vector2Int.y = (int)_vector2.y;
    //}

    public static int GetBucketIndex(string name)
    {
        switch (name)
        {
            case "frontLow1": return 0;
            case "frontLow2": return 1;
            case "frontLow3": return 2;
            case "frontMiddle1": return 3;
            case "frontMiddle2": return 4;
            case "frontMiddle3": return 5;

            case "backLow1": return 6;
            case "backLow2": return 7;
            case "backLow3": return 8;
            case "backMiddle1": return 9;
            case "backMiddle2": return 10;
            case "backMiddle3": return 11;

            case "high": return 12;

            case "wild": return 13;

            case "multifly2": return 14;
            case "multifly3": return 15;
            case "multifly5": return 16;
            case "multifly10": return 17;
        }
        return 0;
    }
}
