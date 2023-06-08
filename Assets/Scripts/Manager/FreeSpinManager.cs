using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FreeSpinManager : StageManager
{
    [SerializeField] GameObject spinNumberNotice;
    [SerializeField] GameObject bonusStageNotice;
    [SerializeField] TextMeshProUGUI spinNumberText;
    [SerializeField] TextMeshProUGUI maxSpinText;

    private int _currentSpinNumber;
    public int currentSpinNumber
    {
        get { return _currentSpinNumber; }
        set { _currentSpinNumber = value; spinNumberText.text = value.ToString(); }
    }

    private int _maxSpinNumber;
    public int maxSpinNumber
    {
        get { return _maxSpinNumber; }
        set { _maxSpinNumber = value; maxSpinText.text = value.ToString(); }
    }


    public override void StartSpin()
    {
        isScattered = true;

        spinNumberNotice.SetActive(true);
        bonusStageNotice.SetActive(false);

        SymbolBucketPool.Instance.BringFreeSpinBucket();
        supplyHeight = 3;

        FSM_Start();
    }
    public override int GetSpinType() { return 2; }
    public bool NextStage()
    {
        if (currentSpinNumber >= maxSpinNumber)
        {
            ResetStage();
            spinNumberNotice.SetActive(false);
            return false; 
        }
        currentSpinNumber++;
        return true;
    }

    public void ResetStage()
    {
        maxSpinNumber = 7;
        currentSpinNumber = 1;
        bonusStageNotice.SetActive(false);
    }
}
