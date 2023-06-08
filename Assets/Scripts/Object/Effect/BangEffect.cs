using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BangEffect : IBlockEffect
{
    public override void ReturnIt()
    {
        BangEffectPool.instance.ReturnObject(this);
    }
}
