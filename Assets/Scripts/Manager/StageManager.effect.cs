using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class StageManager : SymbolManager
{
    void ShowFocusEffect(Vector3 vector3)
    {
        IBlockEffect blockEffect = FocusingEffectPool.instance.GetObject();
        blockEffect.transform.position = vector3;
        blockEffect.TurnOn(1.5f);
    }
    void ShowChangingEffect(Vector3 vector3)
    {
        IBlockEffect blockEffect;
        for (int i = 0; i < 4; i++)
        {
            blockEffect = ChangingEffectPool.instance.GetObject();
            blockEffect.transform.position = vector3;
            blockEffect.TurnOn(1.0f);
        }
    }
}
