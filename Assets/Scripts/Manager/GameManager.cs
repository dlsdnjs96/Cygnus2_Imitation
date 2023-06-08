using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GameManager : MonoBehaviour
{
    [SerializeField] BaseSpinManager baseSpinManager;
    [SerializeField] FreeSpinManager freeSpinManager;
    [SerializeField] TextMeshProUGUI moneyText;
    [SerializeField] TextMeshProUGUI betText;
    [SerializeField] BaseWindow notEnoughMoneyWindow;
    [SerializeField] BaseWindow freeSpinWindow;
    [SerializeField] CoinNotice coinNotice;
    [SerializeField] GameObject allButtons;
    StageManager currentManager;



    private float _money;
    private float _bet;
    public float money { 
        get { return _money; } 
        set { _money = value; moneyText.text = value.ToString("N"); }
    }
    public float bet { 
        get { return _bet; }
        set { _bet = value; betText.text = value.ToString("N"); }
    }

    private void Awake()
    {
        Constant.Initialize();
        money = 10000;
        bet = 10;
    }
    private void Start()
    {
        baseSpinManager.endSpinEvent = EndSpin;
        freeSpinManager.endSpinEvent = EndSpin;

        currentManager = baseSpinManager;
        Time.timeScale = 1.5f;
    }

    public void StartSpin()
    {
        allButtons.SetActive(false);
        coinNotice.TurnOffNotice();
        if (money < bet)
        {
            notEnoughMoneyWindow.OpenWindow();
            return;
        }
        if (currentManager.GetSpinType() == 1)
            money -= bet;

        currentManager.StartSpin();
    }

    public void EndSpin()
    {
        ChangeManager();
    }

    void ChangeManager()
    {
        if (currentManager.isScattered)
        { // scatter가 작동했을 때
            if (currentManager.GetSpinType() == 1) // BaseSpin일 경우
            {
                print("ChangeManager 1");
                freeSpinManager.ResetStage();
                freeSpinWindow.OpenWindow();
                currentManager = freeSpinManager;
                currentManager.score = baseSpinManager.score;
            }
            else if (currentManager.GetSpinType() == 2) // FreeSpin일 경우
            {
                if (!freeSpinManager.NextStage())
                {
                    print("ChangeManager 2");
                    currentManager = baseSpinManager;
                    money += currentManager.score;
                    WinMoneyNotice();
                }
                else
                {
                    print("ChangeManager 3");
                    freeSpinManager.maxSpinNumber += 3;
                    currentManager.StartSpin();
                }
            }
        }
        else // scatter가 작동 안했을 때
        {
            if (currentManager.GetSpinType() == 1) // BaseSpin일 경우
            {
                print("ChangeManager 4");
                money += currentManager.score;
                WinMoneyNotice();
            }
            else if (currentManager.GetSpinType() == 2) // FreeSpin일 경우
            {
                if (!freeSpinManager.NextStage())
                {
                    print("ChangeManager 5");
                    money += currentManager.score;
                    WinMoneyNotice();
                    currentManager = baseSpinManager;
                }
                else
                {
                    print("ChangeManager 6");
                    currentManager.StartSpin();
                }
            }
        }

        currentManager.isScattered = false;
    }

    void WinMoneyNotice()
    {
        coinNotice.TurnOnNotice(currentManager.score, () =>
        {
            allButtons.SetActive(true);
        });
    }
}
