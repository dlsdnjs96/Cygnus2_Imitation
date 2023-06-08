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
    public override bool IsUndestoryable() // �����Ŀ��� ������� ����
    {
        return false;
    }
    public override bool IsChangable() // ���� ������ ���ϴ� �ɺ�
    {
        return true;
    }

    public override bool IsCountable() // Low, Middle, High or Wild �ɺ��� ���
    {
        return false;
    }
}
