using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangingEffect : IBlockEffect
{

    public override void ReturnIt()
    {
        ChangingEffectPool.instance.ReturnObject(this);
    }
}
