using System.Collections;
using UnityEngine;
using Common;
using System;


public class EventAlram : MonoBehaviour
{
    private int coroutineCount = 0;
    public Common.Delegate nextEvent;

    public Common.Delegate GetEndEvent()
    {
        coroutineCount++;
        return EndCoroutine;
    }

    public void EndCoroutine()
    {
        coroutineCount--;

        if (coroutineCount == 0)
        {
            StartCoroutine(CoDelay(0.3f));
        }
    }




    IEnumerator CoDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (nextEvent!= null)
        {
            nextEvent();
        }
    }
}
