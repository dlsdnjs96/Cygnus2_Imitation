using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Common;



public class WinMention
{
    public string text;
    public float requireGold;

    public WinMention(string _text, float _gold)
    {
        text = _text;
        requireGold = _gold;
    }
}



public class CoinNotice : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    [SerializeField] TMP_Text winNotice;
    [SerializeField] AnimationCurve showUpCurve;
    [SerializeField] AnimationCurve scaleUpDownCurve;
    [SerializeField] GameObject winBackground;
    [SerializeField] ParticleSystem coinFalls;
    [SerializeField] StageManager stageManager;

    WinMention[] winMentions;
    Delegate endEvent;

    public void Awake()
    {
        LoadMentions();
    }

    public void TurnOnNotice(float coin, Delegate _endEvent)
    {
        endEvent = _endEvent;
        text.transform.localScale = Vector3.one;
        winNotice.transform.localScale = Vector3.one;
        gameObject.SetActive(true);

        if (coin == 0.0f)
        {
            TurnOffNotice();
            endEvent();
            return;
        }
        else if (coin > winMentions[0].requireGold) // big win ÀÌ»ף
        {
            winNotice.gameObject.SetActive(true);
            coinFalls.gameObject.SetActive(true);
            winBackground.SetActive(true);
            StartCoroutine(CoWinNotice(0.0f, coin, 0));
        }
        else
            StartCoroutine(CoIncreaseCoin(coin));
        //StartCoroutine(CoWinBackground());
    }
    public void TurnOffNotice()
    {
        text.text = "0.00";
        gameObject.SetActive(false);
        winNotice.gameObject.SetActive(false);
        winBackground.SetActive(false);
        coinFalls.gameObject.SetActive(false);
    }

    IEnumerator CoIncreaseCoin(float _coin)
    {
        float passedTime = 0.0f;

        while(passedTime < 2.0f)
        {
            passedTime += Time.deltaTime;
            text.text = string.Format("{0:F2}", Mathf.Lerp(0.0f, _coin, Mathf.Clamp01(passedTime / 2.0f)));

            yield return new WaitForEndOfFrame();
        }
        StartCoroutine(CoShowUpNotice());
    }
    IEnumerator CoShowUpNotice()
    {
        float passedTime = 0.0f;
        if (endEvent != null) endEvent();

        while (passedTime < 1.0f)
        {
            passedTime += Time.deltaTime;
            text.transform.localScale = Vector3.one * showUpCurve.Evaluate(passedTime);
            winNotice.transform.localScale = Vector3.one * showUpCurve.Evaluate(passedTime);

            yield return new WaitForEndOfFrame();
        }
        StartCoroutine(CoScaleUpDownNotice());
    }
    IEnumerator CoScaleUpDownNotice() {

        float passedTime = 0.0f;

        while (true)
        {
            passedTime += Time.deltaTime;
            text.transform.localScale = Vector3.one * scaleUpDownCurve.Evaluate(passedTime / 3.0f);
            winNotice.transform.localScale = Vector3.one * scaleUpDownCurve.Evaluate(passedTime / 3.0f);

            yield return new WaitForEndOfFrame();
        }
    }




    IEnumerator CoWinNotice(float fromCoin, float toCoin, int index)
    {
        float toGoldInThis = Mathf.Min(toCoin, winMentions[index].requireGold);
        float passedTime = 0.0f;

        winNotice.text = winMentions[index].text;

        while (passedTime < 2.0f)
        {
            passedTime += Time.deltaTime;

            winNotice.gameObject.transform.localScale = Vector3.one;
            winNotice.gameObject.transform.localScale += Vector3.one * Mathf.Clamp01(passedTime / 2.0f) / 3.0f;
            winBackground.transform.rotation = Quaternion.Euler(0.0f, 0.0f, winBackground.transform.rotation.eulerAngles.z + (100.0f * Time.deltaTime));
            text.text = string.Format("{0:F1}", Mathf.Lerp(fromCoin, toGoldInThis, Mathf.Clamp01(passedTime / 2.0f)));

            yield return new WaitForEndOfFrame();
        }

        if (toCoin > winMentions[index].requireGold)
            StartCoroutine(CoWinNotice(toGoldInThis, toCoin, index + 1));
        else
            StartCoroutine(CoShowUpNotice());
    }
    void LoadMentions()
    {
        winMentions = new WinMention[10];

        winMentions[0] = new WinMention("BIG WIN", 10.0f);
        winMentions[1] = new WinMention("SUPER WIN", 50.0f);
        winMentions[2] = new WinMention("MEGA WIN", 100.0f);
        winMentions[3] = new WinMention("EPIC WIN", 300.0f);
        winMentions[4] = new WinMention("ULTRA WIN", 1000.0f);
        winMentions[5] = new WinMention("ULTIMATE WIN", 100000.0f);
    }
}
