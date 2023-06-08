using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWindow : MonoBehaviour
{
    // 상호작용할 윈도우
    [SerializeField] public BaseWindow connectedWindow;
    // 윈도우 뒷배경
    [SerializeField] public BaseWindow backgroundWindow;
    public virtual void OpenWindow() { 
        // 이미 활성화 상태일 경우 리턴
        if (gameObject.activeSelf) return; 
        // 뒷배경 윈도우가 있으면 활성화
        if (backgroundWindow != null) backgroundWindow.OpenWindow();
        // 윈도우 활성화
        gameObject.SetActive(true); 
        // 시작할 때 윈도우창이 커졌다 작아지는 효과
        BounceWindow(); 
    }
    public virtual void CloseWindow()
    {
        // 뒷배경 윈도우가 있으면 비활성화
        if (backgroundWindow != null) backgroundWindow.CloseWindow();
        // 윈도우 비활성화
        gameObject.SetActive(false);
    }

    public virtual void HorizontalScrollWindow() { StartCoroutine(CoHorizontalScrollWindow()); }
    public virtual void VerticalScrollWindow() { StartCoroutine(CoVerticalScrollWindow()); }
    public virtual void BounceWindow() { StartCoroutine(CoBounceWindow()); }

    IEnumerator CoHorizontalScrollWindow()
    {
        float passedTime = 0f;

        while (passedTime < 0.2f)
        {
            passedTime += Time.deltaTime;
            transform.localScale = Vector3.right + (Vector3.up * Mathf.Lerp(0f, 1f, passedTime / 0.2f)); 
            yield return new WaitForEndOfFrame();
        }
    }
    IEnumerator CoVerticalScrollWindow()
    {
        float passedTime = 0f;

        while (passedTime < 0.2f)
        {
            passedTime += Time.deltaTime;
            transform.localScale = Vector3.right + (Vector3.up * Mathf.Lerp(0f, 1f, passedTime / 0.2f)); 
            yield return new WaitForEndOfFrame();
        }
    }
    IEnumerator CoBounceWindow()
    {
        float passedTime = 0f;

        while (passedTime < 0.2f)
        {
            passedTime += Time.deltaTime;
            transform.localScale = Vector3.one * Mathf.Lerp(1f, 1.05f, passedTime / 0.2f); 
            yield return new WaitForEndOfFrame();
        }

        passedTime = 0f;
        while (passedTime < 0.2f)
        {
            passedTime += Time.deltaTime;
            transform.localScale = Vector3.one * Mathf.Lerp(1.05f, 1f, passedTime / 0.2f); 
            yield return new WaitForEndOfFrame();
        }
    }
}
