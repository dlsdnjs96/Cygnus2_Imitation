using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplySymbol : SpecialSymbol
{
    [SerializeField] public float multiply;
    public override bool IsWild() // Wild �ɺ��� ���
    {
        return false;
    }
    public override bool IsMultifly() // ��Ƽ�ö��� �ɺ�
    {
        return true;
    }
    public override float GetMultifly() // ��Ƽ�ö��� ��ġ
    {
        return multiply;
    }
    public override bool IsCountable() // Low, Middle, High or Wild �ɺ��� ���
    {
        return false;
    }
}
