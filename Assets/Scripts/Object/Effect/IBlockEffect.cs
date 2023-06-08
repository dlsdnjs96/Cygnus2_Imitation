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


    // ObjectPool을 Singleton으로 접근하기 때문에 
    // 가상함수로 상속받아서
    // 오브젝트를 되돌려줄 Pool을 따로 지정해줍니다.
    public virtual void ReturnIt() { }
}
