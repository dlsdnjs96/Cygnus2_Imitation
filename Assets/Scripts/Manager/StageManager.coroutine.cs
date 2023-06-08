using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public partial class StageManager : SymbolManager
{
    IEnumerator CoCreateBaseSymbols()
    {
        string[,] table;
        CreateSymbolTable(out table, Random.Range(0.0f, 10.0f) < 3.0f);

        for (int j = 1; j <= 4; j++)
        {
            for (int i = 1; i <= 6; i++)
            {
                MakeSymbol(new Vector2Int(i, j), table[i, j]);
                symbols[i, j].FallDown(0.3f, eventAlram.GetEndEvent());
                yield return new WaitForSeconds(0.05f);
            }
        }
    }
    IEnumerator CoExplodeBlocks(bool[,] targetSymbols)
    {
        for (int i = 1; i <= 6; i++)
        {
            for (int j = 1; j <= 8; j++)
            {
                if (!targetSymbols[i, j]) continue;
                if (symbols[i, j] != null && symbols[i, j].IsUndestoryable()) continue;

                symbols[i, j].Explode(eventAlram.GetEndEvent());
                symbols[i, j] = null;

                yield return new WaitForSeconds(0.1f);
            }
        }
    }
    IEnumerator CoSupplyBlocks()
    {
        for (int i = 1; i <= 6; i++)
        {
            for (int j = 1; j <= supplyHeight; j++)
            {
                if (symbols[i, j] != null) continue;

                MakeSymbol(new Vector2Int(i, j));
                symbols[i, j].FallDown(0.3f, eventAlram.GetEndEvent());
                yield return new WaitForSeconds(0.01f);
            }
        }
    }

    IEnumerator CoDelay(float delay, Delegate nextEvent)
    {
        yield return new WaitForSeconds(delay);
        if (nextEvent != null) nextEvent();
    }
}
