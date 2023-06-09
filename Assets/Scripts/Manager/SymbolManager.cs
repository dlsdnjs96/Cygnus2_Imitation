using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SymbolManager : MonoBehaviour
{
    public static Symbol[,] symbols = new Symbol[7, 9];
    //public Symbol this[int x, int y]
    //{ 
    //    get { return symbols[x, y]; }
    //    private set { symbols[x, y] = value; }
    //}

    protected Symbol MakeSymbol(Vector2Int point)
    {
        if (symbols[point.x, point.y] != null) RemoveSymbol(point);
        symbols[point.x, point.y] = SymbolPool.instance.GetObject(SymbolBucketPool.Instance.PullOutSymbol(point));
        symbols[point.x, point.y].currentPosition = point;
        symbols[point.x, point.y].transform.localEulerAngles = Vector3.zero;
        return symbols[point.x, point.y];
    }
    protected Symbol MakeSymbol(Vector2Int point, string name)
    {
        if (symbols[point.x, point.y] != null) RemoveSymbol(point);
        symbols[point.x, point.y] = SymbolPool.instance.GetObject(name);
        symbols[point.x, point.y].currentPosition = point;
        symbols[point.x, point.y].transform.localEulerAngles = Vector3.zero;
        return symbols[point.x, point.y];
    }
    protected Symbol MakeWild(Vector2Int point)
    {
        if (symbols[point.x, point.y] != null) RemoveSymbol(point);
        symbols[point.x, point.y] = SymbolPool.instance.GetObject("Wild");
        symbols[point.x, point.y].currentPosition = point;
        return symbols[point.x, point.y];
    }
    protected Symbol MakeUsedScatter(Vector2Int point)
    {
        if (symbols[point.x, point.y] != null) RemoveSymbol(point);
        symbols[point.x, point.y] = SymbolPool.instance.GetObject("UsedScatter");
        symbols[point.x, point.y].currentPosition = point;
        return symbols[point.x, point.y];
    }

    protected void RemoveSymbol(Vector2Int point)
    {
        if (symbols[point.x, point.y] == null) return;

        SymbolBucketPool.Instance.PushBackSymbol(symbols[point.x, point.y].symbolName, point);
        SymbolPool.instance.Return(symbols[point.x, point.y]);
        symbols[point.x, point.y] = null;
    }

    protected void MoveSymbol(Vector2Int from, Vector2Int to)
    {
        if (symbols[to.x, to.y] != null) return;

        symbols[to.x, to.y] = symbols[from.x, from.y];
        symbols[from.x, from.y] = null;
    }

    protected Symbol ChangeSpecialSymbol(Vector2Int point)
    {
        Symbol ChangedSymbol = SymbolPool.instance.GetObject(symbols[point.x, point.y].GetChangeName());
        if (symbols[point.x, point.y] != null) RemoveSymbol(point);

        symbols[point.x, point.y] = ChangedSymbol;
        symbols[point.x, point.y].currentPosition = point;
        return symbols[point.x, point.y];
    }


}
