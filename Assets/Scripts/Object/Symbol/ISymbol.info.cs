using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class ISymbol : MonoBehaviour
{
    [SerializeField] public string symbolName;
    public virtual bool IsWild() // Wild 심볼인 경우
    {
        return false;
    }
    public virtual bool IsCountable() // Low, Middle, High or Wild 심볼인 경우
    {
        return true;
    }
    public bool IsNormal() // Low, Middle or High 심볼인 경우
    {
        return symbolID >= 1 && symbolID <= 7;
    }

    public bool IsWarppable() // 바닥에 닿으면 워프하는 심볼
    {
        return symbolID >= 9 && symbolID <= 13;
    }
    public virtual bool IsChangable() // 벽에 닿으면 변하는 심볼
    {
        return false;
    }
    public virtual bool IsMultifly() // 멀티플라이 심볼
    {
        return false;
    }
    public virtual bool IsUndestoryable() // 조합후에도 사라지지 않음
    {
        return false;
    }
    public bool IsScatter() // 스케터 심볼
    {
        return symbolID == 9;
    }
    public virtual float GetMultifly() // 멀티플라이 수치
    {
        return 0;
    }
    public virtual string GetChangeName()
    {
        return "";
    }
}
