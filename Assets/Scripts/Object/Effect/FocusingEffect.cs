using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusingEffect : IBlockEffect
{
    public override void ReturnIt()
    {
        FocusingEffectPool.instance.ReturnObject(this);
    }
}
