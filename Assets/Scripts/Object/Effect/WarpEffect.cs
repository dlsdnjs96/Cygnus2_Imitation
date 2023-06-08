using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpEffect : IBlockEffect
{
    public override void ReturnIt()
    {
        WarpEffectPool.instance.ReturnObject(this);
    }
}

