using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class SpecialSymbol : ISymbol
{
    [SerializeField] public string changeName;

    public override void SlideDown(Vector2Int nextPoint, Delegate nextEvent)
    {
        currentPosition = nextPoint;
        destination = Constant.GetSymbolLocation(nextPoint);
        departurePoint = transform.localPosition;

        StartCoroutine(CoSlideDown(Constant.slidingDuration, nextEvent));
    }
    public override string GetChangeName()
    {
        return changeName;
    }
    public override bool IsUndestoryable() // 조합후에도 사라지지 않음
    {
        return false;
    }
    public override bool IsChangable() // 벽에 닿으면 변하는 심볼
    {
        return true;
    }

    public override bool IsCountable() // Low, Middle, High or Wild 심볼인 경우
    {
        return false;
    }
}
