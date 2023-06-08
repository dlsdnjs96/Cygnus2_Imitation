using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IBlockEffect : MonoBehaviour
{
    Animator anim;
    [SerializeField] public string animName;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void TurnOn(float duration)
    {
        anim.enabled = true;
        anim.Play(animName);
        StartCoroutine(CoTurnOff(duration));
    }

    IEnumerator CoTurnOff(float duration)
    {
        yield return new WaitForSeconds(duration);
        anim.enabled = false;
        ReturnIt();
    }


    // ObjectPool�� Singleton���� �����ϱ� ������ 
    // �����Լ��� ��ӹ޾Ƽ�
    // ������Ʈ�� �ǵ����� Pool�� ���� �������ݴϴ�.
    public virtual void ReturnIt() { }
}
