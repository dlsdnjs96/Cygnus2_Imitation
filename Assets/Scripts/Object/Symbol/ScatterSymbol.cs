using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScatterSymbol : SpecialSymbol
{
    public override bool IsCountable() // Low, Middle, High or Wild 심볼인 경우
    {
        return false;
    }
}
