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
        // 0.7�ʰ� ���� ����Ʈ ����
        BangEffect blockEffect = BangEffectPool.instance.GetObject();
        blockEffect.transform.position = transform.position;
        blockEffect.TurnOn(0.7f);

        // 0.7�� �� �ɺ� ����
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
    // duration : ������ �̲������� �ð�
    // directon : �̲������� ���� (-1.0f or 1.0f)
    // nextEvent : ���� ������ �̺�Ʈ
    IEnumerator CoSlideDown(float duration, float direction, Delegate nextEvent) // �ɺ��� ȸ���ϸ� �̲������� �ڷ�ƾ
    {
        // �̲������鼭 �ɺ��� �󸶳� ȸ���ϴ��� ���� ����
        Quaternion prevQuaternion = transform.localRotation;
        Quaternion postQuaternion = Quaternion.Euler(0.0f, 0.0f, transform.localRotation.eulerAngles.z + (direction * 30.0f));

        float passedTime = 0.0f;
        float curve;
        Vector3 lerpedPosition = new Vector3(0, 0, 0);

        while (passedTime < duration)
        {
            // ����� �ð� ���
            passedTime += Time.deltaTime;

            // �ɺ� ���� ������ õõ�� �����̴� ���� �������°� curve�� ǥ��
            curve = slideCurve.Evaluate(Mathf.Clamp01(passedTime / duration));

            // ������ �°� �̵��ϴ� ��ġ ���
            lerpedPosition.x = Mathf.Lerp(departurePoint.x, destination.x, curve);
            lerpedPosition.y = departurePoint.y + Util.GetYofCircle(Constant.blockDiameter, lerpedPosition.x - departurePoint.x) - Constant.blockDiameter;

            transform.localPosition = lerpedPosition;
            transform.localRotation = Quaternion.Lerp(prevQuaternion, postQuaternion, curve);


            yield return new WaitForEndOfFrame();
        }

        if (nextEvent != null) nextEvent();
    }
    // duration : ������ �̲������� �ð�
    // nextEvent : ���� ������ �̺�Ʈ
    protected IEnumerator CoSlideDown(float duration, Delegate nextEvent) // �ɺ��� ȸ������ �̲������� �ڷ�ƾ
    {
        float passedTime = 0.0f;
        float curve;
        Vector3 lerpedPosition = new Vector3(0, 0, 0);

        while (passedTime < duration)
        {
            // ����� �ð� ���
            passedTime += Time.deltaTime;

            // �ɺ� ���� ������ õõ�� �����̴� ���� �������°� curve�� ǥ��
            curve = slideCurve.Evaluate(Mathf.Clamp01(passedTime / duration));

            // ������ �°� �̵��ϴ� ��ġ ���
            lerpedPosition.x = Mathf.Lerp(departurePoint.x, destination.x, curve);
            lerpedPosition.y = departurePoint.y + Util.GetYofCircle(Constant.blockDiameter, lerpedPosition.x - departurePoint.x) - Constant.blockDiameter;

            transform.localPosition = lerpedPosition;


            yield return new WaitForEndOfFrame();
        }

        if (nextEvent != null) nextEvent();
    }
    // departurePoint���� ���� destination���� �������� ����� ǥ���ϴ� �Լ�
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
    // �߶� �� �ణ�� �ݵ��� ǥ�����ִ� �Լ�
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
