using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildSymbol : Symbol
{
    public override bool IsWild() // Wild 심볼인 경우
    {
        return true;
    }
}
