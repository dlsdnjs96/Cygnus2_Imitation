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
        // 심볼 크기 지정
        rectTransform = GetComponent<RectTransform>();
        rectTransform.sizeDelta = Vector2.one * Constant.blockDiameter;

        landingCurve    = CommonCurve.Instance.landingCurve;
        slideCurve      = CommonCurve.Instance.slideCurve;
        fallDownCurve   = CommonCurve.Instance.fallDownCurve;
    }
}
