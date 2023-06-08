using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public partial class StageManager : SymbolManager
{
    [SerializeField] EventAlram eventAlram;
    [SerializeField] GageBar gageBar;

    public bool isScattered;

    public Delegate endSpinEvent;


    public virtual void StartSpin() { }
    public virtual int GetSpinType() { return 0; }
}
