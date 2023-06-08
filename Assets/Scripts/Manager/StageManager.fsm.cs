using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public partial class StageManager : SymbolManager
{
    [SerializeField] TextMeshProUGUI fsmText;
    private string _fsm;
    public string fsm
    {
        get { return _fsm; }
        set { _fsm = value; fsmText.text = value; }
    }

    // Stage의 전체적인 흐름은 
    // 여기서 FSM방식으로 제어합니다.

    public void FSM_Start()
    {
        fsm = "FSM_Start";
        eventAlram.nextEvent = FSM_PayOut;
        RemoveAllSymbols();
        CreateBaseSymbols();
    }

    void FSM_PayOut()
    {
        fsm = "FSM_PayOut";
        bool[,] targetsSymbol;
        if (FindPayOut(out targetsSymbol))
        {
            eventAlram.nextEvent = FSM_FallDown;
            FocusingSymbols(targetsSymbol);
            StartCoroutine(CoDelay(1.6f, () => ExplodeSymbols(targetsSymbol)));
        }
        else
            FSM_WildPayOut();
    }

    void FSM_WildPayOut()
    {
        fsm = "FSM_WildPayOut";
        bool[,] targetsSymbol;
        if (FindWildPayOut(out targetsSymbol))
        {
            eventAlram.nextEvent = FSM_FallDown;
            FocusingSymbols(targetsSymbol);
            StartCoroutine(CoDelay(1.6f, () => ExplodeSymbols(targetsSymbol)));
        }
        else
            FSM_Supply();
    }

    void FSM_FallDown()
    {
        fsm = "FSM_FallDown";
        if (FindFallDownSymbols())
        {
            eventAlram.nextEvent = FSM_SlideDown;
            FallDownSymbols();
        }
        else
            FSM_SlideDown();
    }

    void FSM_SlideDown()
    {
        fsm = "FSM_SlideDown";
        if (FindLeftSlideSymbols())
        {
            eventAlram.nextEvent = FSM_CheckSpecialSymbol;
            LeftSlideDownSymbols();
        } 
        else if (FindRightSlideSymbols())
        {
            eventAlram.nextEvent = FSM_SlideDown;
            RightSlideDownSymbols();
        }
        else
            FSM_PayOut();
    }

    void FSM_CheckSpecialSymbol()
    {
        fsm = "FSM_CheckSpecialSymbol";
        if (FindSpecialSymbol())
        {
            eventAlram.nextEvent = FSM_CheckSpecialSymbol;
            ChangeSpecialSymbol();
        } else
            FSM_SlideDown();
    }

    void FSM_Supply()
    {
        fsm = "FSM_Supply";
        if (FindSupplySymbol())
        {
            eventAlram.nextEvent = FSM_PayOut;
            SupplySymbols();
        }
        else
            FSM_EndSpin();
    }

    void FSM_EndSpin()
    {
        fsm = "FSM_EndSpin";
        endSpinEvent();
    }
}
