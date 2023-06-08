using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public partial class ISymbol : MonoBehaviour
{
    public Vector2Int currentPosition;
    protected Vector2 departurePoint;
    protected Vector2 destination;

    private AnimationCurve landingCurve;
    private AnimationCurve slideCurve;
    private AnimationCurve fallDownCurve;


    public void MoveToLocation()
    {
        transform.localPosition = Constant.GetSymbolLocation(currentPosition);
    }
    public void FallDown(float _duration, Delegate nextEvent)
    {
        destination = Constant.GetSymbolLocation(currentPosition);
        departurePoint = destination + (Vector2.up * 1000.0f);

        StartCoroutine(CoFallDown(_duration, nextEvent));
    }
    public void FallDown(Vector2Int nextPoint, float _duration, Delegate nextEvent)
    {
        currentPosition = nextPoint;
        destination = Constant.GetSymbolLocation(currentPosition);
        departurePoint = destination + (Vector2.up * 1000.0f);

        StartCoroutine(CoFallDown(_duration, nextEvent));
    }
    public void FallDownFromHere(Vector2Int nextPoint, float _duration, Delegate nextEvent)
    {
        currentPosition = nextPoint;
        destination = Constant.GetSymbolLocation(currentPosition);
        departurePoint = transform.localPosition;

        StartCoroutine(CoFallDown(_duration, nextEvent));
    }
    public virtual void SlideDown(Vector2Int nextPoint, Delegate nextEvent)
    {
        currentPosition = nextPoint;
        destination = Constant.GetSymbolLocation(nextPoint);
        departurePoint = transform.localPosition;

        StartCoroutine(CoSlideDown(Constant.slidingDuration, Mathf.Clamp(departurePoint.x - destination.x, -1.0f, 1.0f), nextEvent));
    }
    public void Explode(Delegate nextEvent)
    {
        // 0.7초간 폭파 이펙트 실행
        BangEffect blockEffect = BangEffectPool.instance.GetObject();
        blockEffect.transform.position = transform.position;
        blockEffect.TurnOn(0.7f);

        // 0.7초 후 심볼 삭제
        StartCoroutine(CoDelay(0.7f,
            () => { if (nextEvent != null) nextEvent();
                SymbolBucketPool.Instance.PushBackSymbol(symbolName, currentPosition);
                SymbolPool.instance.Return(this);
            }
            ));
    }




    IEnumerator CoDelay(float delay, Delegate nextEvent)
    {
        yield return new WaitForSeconds(delay);
        if (nextEvent != null) nextEvent();
    }
    // duration : 옆으로 미끄러지는 시간
    // directon : 미끄러지는 방향 (-1.0f or 1.0f)
    // nextEvent : 동작 종료후 이벤트
    IEnumerator CoSlideDown(float duration, float direction, Delegate nextEvent) // 심볼이 회전하며 미끄러지는 코루틴
    {
        // 미끄러지면서 심볼이 얼마나 회전하는지 계산용 변수
        Quaternion prevQuaternion = transform.localRotation;
        Quaternion postQuaternion = Quaternion.Euler(0.0f, 0.0f, transform.localRotation.eulerAngles.z + (direction * 30.0f));

        float passedTime = 0.0f;
        float curve;
        Vector3 lerpedPosition = new Vector3(0, 0, 0);

        while (passedTime < duration)
        {
            // 진행된 시간 계산
            passedTime += Time.deltaTime;

            // 심볼 마찰 때문에 천천히 움직이다 빨리 떨어지는걸 curve로 표현
            curve = slideCurve.Evaluate(Mathf.Clamp01(passedTime / duration));

            // 원형에 맞게 이동하는 위치 계산
            lerpedPosition.x = Mathf.Lerp(departurePoint.x, destination.x, curve);
            lerpedPosition.y = departurePoint.y + Util.GetYofCircle(Constant.blockDiameter, lerpedPosition.x - departurePoint.x) - Constant.blockDiameter;

            transform.localPosition = lerpedPosition;
            transform.localRotation = Quaternion.Lerp(prevQuaternion, postQuaternion, curve);


            yield return new WaitForEndOfFrame();
        }

        if (nextEvent != null) nextEvent();
    }
    // duration : 옆으로 미끄러지는 시간
    // nextEvent : 동작 종료후 이벤트
    protected IEnumerator CoSlideDown(float duration, Delegate nextEvent) // 심볼이 회전없이 미끄러지는 코루틴
    {
        float passedTime = 0.0f;
        float curve;
        Vector3 lerpedPosition = new Vector3(0, 0, 0);

        while (passedTime < duration)
        {
            // 진행된 시간 계산
            passedTime += Time.deltaTime;

            // 심볼 마찰 때문에 천천히 움직이다 빨리 떨어지는걸 curve로 표현
            curve = slideCurve.Evaluate(Mathf.Clamp01(passedTime / duration));

            // 원형에 맞게 이동하는 위치 계산
            lerpedPosition.x = Mathf.Lerp(departurePoint.x, destination.x, curve);
            lerpedPosition.y = departurePoint.y + Util.GetYofCircle(Constant.blockDiameter, lerpedPosition.x - departurePoint.x) - Constant.blockDiameter;

            transform.localPosition = lerpedPosition;


            yield return new WaitForEndOfFrame();
        }

        if (nextEvent != null) nextEvent();
    }
    // departurePoint에서 부터 destination으로 떨어지는 모션을 표현하는 함수
    IEnumerator CoFallDown(float duration, Delegate nextEvent)
    {
        transform.localPosition = departurePoint;

        float passedTime = 0.0f;

        while (passedTime < duration)
        {
            passedTime += Time.deltaTime;

            transform.localPosition = Vector2.Lerp(departurePoint, destination, -fallDownCurve.Evaluate(Mathf.Clamp01(passedTime / duration)));
            yield return new WaitForEndOfFrame();
        }

        StartCoroutine(CoLandingMove(0.1f, 15.0f, nextEvent));
    }
    // 추락 후 약간의 반동을 표현해주는 함수
    IEnumerator CoLandingMove(float duration, float range, Delegate nextEvent)
    {
        Vector2 from = transform.localPosition;

        float passedTime = 0.0f;

        while (passedTime < duration)
        {
            passedTime += Time.deltaTime;

            transform.localPosition = from + (Vector2.up * range * landingCurve.Evaluate(Mathf.Clamp01(passedTime / duration)));
            yield return new WaitForEndOfFrame();
        }
        if (nextEvent != null) nextEvent();
    }
}
