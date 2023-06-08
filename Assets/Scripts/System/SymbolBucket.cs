using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;


public class SymbolProb
{
    public string symbol;
    public int ea;

    public SymbolProb()
    {
        symbol = "";
        ea = 0;
    }
    public SymbolProb(string _symbol, int _ea)
    {
        symbol = _symbol;
        ea = _ea;
    }

    public SymbolProb Clone()
    {
        return new SymbolProb(symbol, ea);
    }
}


public class SymbolBucket : MonoBehaviour
{
    public SymbolProb[] probList;
    List<SymbolProb>[] reelBucket;

    private void Start()
    {
        ConnectBucket();



    }

    public string PullOutSymbol(int reelNumber)
    {
        int totalCount = 0;

        foreach (var it in reelBucket[reelNumber])
            totalCount += it.ea;
        int rnd = Random.Range(0, totalCount);

        foreach (var it in reelBucket[reelNumber])
        {
            if (rnd <= it.ea)
            {
                it.ea--;
                return it.symbol;
            }
            rnd -= it.ea;
        }
        return "";
    }

    public void PushBackSymbol(string symbolName, int reelNumber)
    {
        foreach (var it in reelBucket[reelNumber])
        {
            if (it.symbol == symbolName)
            {
                it.ea++;
                return;
            }
        }
    }

    public void ConnectBucket()
    {
        reelBucket = new List<SymbolProb>[7];

        for (int i = 1; i <= 6; i++)
            reelBucket[i] = new List<SymbolProb>();

        for (int i = 1; i <= 3; i++)
        {
            reelBucket[i].Add(probList[Util.GetBucketIndex("frontLow1")]);
            reelBucket[i].Add(probList[Util.GetBucketIndex("frontLow2")]);
            reelBucket[i].Add(probList[Util.GetBucketIndex("frontLow3")]);
            reelBucket[i].Add(probList[Util.GetBucketIndex("frontMiddle1")]);
            reelBucket[i].Add(probList[Util.GetBucketIndex("frontMiddle2")]);
            reelBucket[i].Add(probList[Util.GetBucketIndex("frontMiddle3")]);
        }

        for (int i = 4; i <= 6; i++)
        {
            reelBucket[i].Add(probList[Util.GetBucketIndex("frontLow1")]);
            reelBucket[i].Add(probList[Util.GetBucketIndex("frontLow2")]);
            reelBucket[i].Add(probList[Util.GetBucketIndex("frontLow3")]);
            reelBucket[i].Add(probList[Util.GetBucketIndex("frontMiddle1")]);
            reelBucket[i].Add(probList[Util.GetBucketIndex("frontMiddle2")]);
            reelBucket[i].Add(probList[Util.GetBucketIndex("frontMiddle3")]);
        }

        reelBucket[6].Add(probList[Util.GetBucketIndex("high1")]);

        for (int i = 1; i <= 6; i++)
            reelBucket[i].Add(probList[Util.GetBucketIndex("wild")]);

        for (int i = 2; i <= 6; i++)
        {
            reelBucket[i].Add(probList[Util.GetBucketIndex("multifly2")]);
            reelBucket[i].Add(probList[Util.GetBucketIndex("multifly3")]);
            reelBucket[i].Add(probList[Util.GetBucketIndex("multifly5")]);
            reelBucket[i].Add(probList[Util.GetBucketIndex("multifly10")]);
            reelBucket[i].Add(probList[Util.GetBucketIndex("scatter")]);
        }
    }
    public void ResetBucket()
    {
        probList = new SymbolProb[19];


        probList[0] = new SymbolProb();

        probList[0] = new SymbolProb("Low1", 15);
        probList[1] = new SymbolProb("Low2", 15);
        probList[2] = new SymbolProb("Low3", 15);
        probList[3] = new SymbolProb("Middle1", 15);
        probList[4] = new SymbolProb("Middle2", 15);
        probList[5] = new SymbolProb("Middle3", 15);

        probList[6] = new SymbolProb("Low1", 50);
        probList[7] = new SymbolProb("Low2", 50);
        probList[8] = new SymbolProb("Low3", 50);
        probList[9] = new SymbolProb("Middle1", 50);
        probList[10] = new SymbolProb("Middle2", 50);
        probList[11] = new SymbolProb("Middle3", 50);

        probList[12] = new SymbolProb("High1", 50);

        probList[13] = new SymbolProb("Wild", 5);

        probList[14] = new SymbolProb("Multi2", 5);
        probList[15] = new SymbolProb("Multi3", 5);
        probList[16] = new SymbolProb("Multi5", 5);
        probList[17] = new SymbolProb("Multi10", 5);

        probList[18] = new SymbolProb("Scatter", 1);
    }

    public SymbolBucket DeepCopy()
    {
        SymbolBucket clone = new SymbolBucket();

        clone.probList = new SymbolProb[19];
        for (int i = 0; i < 19; i++)
            clone.probList[i] = probList[i].Clone();
        clone.ConnectBucket();
        return clone;
    }
}
