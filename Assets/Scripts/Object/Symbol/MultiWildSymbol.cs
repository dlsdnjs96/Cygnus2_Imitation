using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiWildSymbol : MultiflySymbol
{
    public override bool IsWild() // Wild �ɺ��� ���
    {
        return true;
    }
    public override bool IsMultifly() // ��Ƽ�ö��� �ɺ�
    {
        return true;
    }
    public override bool IsUndestoryable() // �����Ŀ��� ������� ����
    {
        return true;
    }
    public override bool IsCountable() // Low, Middle, High or Wild �ɺ��� ���
    {
        return true;
    }
}
