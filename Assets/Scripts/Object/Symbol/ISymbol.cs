using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class ISymbol : MonoBehaviour
{
    [SerializeField] private int symbolID;

    RectTransform rectTransform;

    public int GetId() { return symbolID; }
    void Awake()
    {
        // �ɺ� ũ�� ����
        rectTransform = GetComponent<RectTransform>();
        rectTransform.sizeDelta = Vector2.one * Constant.blockDiameter;

        landingCurve    = CommonCurve.Instance.landingCurve;
        slideCurve      = CommonCurve.Instance.slideCurve;
        fallDownCurve   = CommonCurve.Instance.fallDownCurve;
    }
}
