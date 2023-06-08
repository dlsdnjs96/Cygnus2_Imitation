using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class ISymbol : MonoBehaviour
{
    [SerializeField] public string symbolName;
    public virtual bool IsWild() // Wild �ɺ��� ���
    {
        return false;
    }
    public virtual bool IsCountable() // Low, Middle, High or Wild �ɺ��� ���
    {
        return true;
    }
    public bool IsNormal() // Low, Middle or High �ɺ��� ���
    {
        return symbolID >= 1 && symbolID <= 7;
    }

    public bool IsWarppable() // �ٴڿ� ������ �����ϴ� �ɺ�
    {
        return symbolID >= 9 && symbolID <= 13;
    }
    public virtual bool IsChangable() // ���� ������ ���ϴ� �ɺ�
    {
        return false;
    }
    public virtual bool IsMultifly() // ��Ƽ�ö��� �ɺ�
    {
        return false;
    }
    public virtual bool IsUndestoryable() // �����Ŀ��� ������� ����
    {
        return false;
    }
    public bool IsScatter() // ������ �ɺ�
    {
        return symbolID == 9;
    }
    public virtual float GetMultifly() // ��Ƽ�ö��� ��ġ
    {
        return 0;
    }
    public virtual string GetChangeName()
    {
        return "";
    }
}
