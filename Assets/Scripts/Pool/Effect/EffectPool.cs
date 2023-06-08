using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPool : ObjectPool<IBlockEffect>
{

    public override IBlockEffect GetObject()
    {
        if (pooling.Count > 0)
        {
            IBlockEffect obj = pooling.Dequeue();
            obj.gameObject.SetActive(true);
            obj.transform.SetParent(parentObj.transform, false);
            return obj;
        }
        else
        {
            IBlockEffect newObj = CreateNewObject();
            newObj.gameObject.SetActive(true);
            newObj.transform.SetParent(parentObj.transform, false);
            return newObj;
        }
    }
    public override IBlockEffect CreateNewObject()
    {
        IBlockEffect newObj = Instantiate(poolingPrefab, transform, false).GetComponent<IBlockEffect>();
        newObj.gameObject.SetActive(false);
        return newObj;
    }
}
