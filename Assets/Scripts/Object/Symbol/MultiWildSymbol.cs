using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiWildSymbol : MultiflySymbol
{
    public override bool IsWild() // Wild 심볼인 경우
    {
        return true;
    }
    public override bool IsMultifly() // 멀티플라이 심볼
    {
        return true;
    }
    public override bool IsUndestoryable() // 조합후에도 사라지지 않음
    {
        return true;
    }
    public override bool IsCountable() // Low, Middle, High or Wild 심볼인 경우
    {
        return true;
    }
}
