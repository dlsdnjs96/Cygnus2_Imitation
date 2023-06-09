using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using TMPro;

public class GageBar : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI gageNumber;
    [SerializeField] GameObject scatterLight;
    [SerializeField] GameObject scatterSymbol;
    [SerializeField] RectTransform gageAnchor;
    [SerializeField] RectTransform gradation;
    [SerializeField] RectTransform gradationNumber;

    Delegate endEvent;
    private float _gage;
    public float gage {
        get { return _gage; }
        private set { _gage = value; gageNumber.text = value.ToString(); }
    }
    public float maxGageNumber { get; private set; }


    private void Start()
    {
        maxGageNumber = 1;
        if (maxGageNumber < 4)
        {
            for (int i = (int)maxGageNumber; i <= 4; i++)
                MakeGradation(i);
        }
        gageNumber.text = "1";
        gageAnchor.anchoredPosition = Vector2.zero;
        gage = 1;
    }

    public void AddGage(float _gage)
    {
        if (maxGageNumber < gage + _gage + 4)
        {
            for (int i = (int)maxGageNumber; i <= gage + _gage + 4; i++)
                MakeGradation(i);
        }
        maxGageNumber = Mathf.Max(maxGageNumber, gage + _gage);
        StartCoroutine(CoSlidingReel(_gage));
    }

    public void ResetGage()
    {
        gageAnchor.anchoredPosition = Vector2.zero;
        gageNumber.text = "1";
    }

    public void TurnOnScatter(Vector3 from, Delegate _endEvent)
    {
        endEvent = _endEvent;
        StartCoroutine(CoTurnOnScatter(from));
    }
    void MakeGradation(int num)
    {
        num--;
        Instantiate(gradation, gageAnchor.transform, false).anchoredPosition = new Vector2(0, num * 200.0f);
        RectTransform temp = Instantiate(gradationNumber, gageAnchor.transform, false);
        temp.anchoredPosition = new Vector2(0, (num * 200.0f) + 20.0f);
        temp.GetComponent<TextMeshProUGUI>().text = (num + 1).ToString();
    }

    IEnumerator CoTurnOnScatter(Vector3 from)
    {
        float passedTime = 0.0f;
        Vector2 lerppedPosition;

        scatterLight.SetActive(true);
        while (passedTime < 2.0f)
        {
            passedTime += Time.deltaTime;
            lerppedPosition = Vector2.Lerp(from, scatterSymbol.transform.position, Mathf.Clamp01(passedTime / 2.0f));
            scatterLight.transform.position = new Vector3(lerppedPosition.x, lerppedPosition.y, scatterLight.transform.position.z);

            yield return new WaitForEndOfFrame();
        }
        scatterSymbol.SetActive(true);
        scatterLight.SetActive(false);

        if (endEvent != null) endEvent();
    }
    IEnumerator CoSlidingReel(float addGage)
    {
        float fromY, toY;
        float passedTime = 0.0f, duration;

        fromY = gageAnchor.anchoredPosition.y;
        toY = ((addGage + gage - 1) * -200.0f);


        duration = addGage / 10.0f;

        while (passedTime < duration)
        {
            passedTime += Time.deltaTime;

            gageAnchor.anchoredPosition = Vector2.up * Mathf.Lerp(fromY, toY, Mathf.Clamp01(passedTime / duration));
            gageNumber.text = ((int)(gageAnchor.anchoredPosition.y / -200.0f)).ToString();

            yield return new WaitForEndOfFrame();
        }
        gage += addGage;
        gageNumber.text =  gage.ToString();

        if (endEvent != null) endEvent();
    }

}
