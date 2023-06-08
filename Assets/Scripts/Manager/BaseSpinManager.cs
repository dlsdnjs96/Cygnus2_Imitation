using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSpinManager : StageManager
{
    public override void StartSpin()
    {
        isScattered = true;
        SymbolBucketPool.Instance.BringBaseSpinBucket();

        score = 0.0f;
        supplyHeight = 3;

        FSM_Start();
    }
    public override int GetSpinType() { return 1; }
}
