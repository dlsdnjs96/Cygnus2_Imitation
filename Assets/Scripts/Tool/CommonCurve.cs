using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonCurve : Singleton<CommonCurve>
{
    [SerializeField] public AnimationCurve landingCurve;
    [SerializeField] public AnimationCurve slideCurve;
    [SerializeField] public AnimationCurve fallDownCurve;
}
