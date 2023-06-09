using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplySymbol : SpecialSymbol
{
    [SerializeField] public float multiply;
    public override bool IsWild() // Wild 심볼인 경우
    {
        return false;
    }
    public override bool IsMultifly() // 멀티플라이 심볼
    {
        return true;
    }
    public override float GetMultifly() // 멀티플라이 수치
    {
        return multiply;
    }
    public override bool IsCountable() // Low, Middle, High or Wild 심볼인 경우
    {
        return false;
    }
}
