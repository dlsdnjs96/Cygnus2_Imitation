using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildSymbol : ISymbol
{
    public override bool IsWild() // Wild �ɺ��� ���
    {
        return true;
    }
}
