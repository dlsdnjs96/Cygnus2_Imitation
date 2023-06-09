using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

class SymbolProbJson
{
    public List<SymbolProb> list;

    public SymbolProbJson()
    {
        list = new List<SymbolProb>();
    }
}

public class SymbolBucketPool : Singleton<SymbolBucketPool>
{
    SymbolBucket currentBucket;

    SymbolBucket baseSpinBucket;
    SymbolBucket freeSpinBucket;

    private void Start()
    {
        LoadBaseSpinBucket();
        LoadFreeSpinBucket();
        BringBaseSpinBucket();
    }

    void LoadBaseSpinBucket()
    {
        baseSpinBucket = new SymbolBucket();
        baseSpinBucket.probList = new SymbolProb[19];

        string data = File.ReadAllText(Application.dataPath + "/Json/BaseSpinSymbol.json");
        SymbolProbJson symbolProbJson = JsonConvert.DeserializeObject<SymbolProbJson>(data);
        for (int i = 0; i < 19; i++)
            baseSpinBucket.probList[i] = symbolProbJson.list[i].Clone();

        baseSpinBucket.ConnectBucket();
    }

    void LoadFreeSpinBucket()
    {
        freeSpinBucket = new SymbolBucket();
        freeSpinBucket.probList = new SymbolProb[19];

        string data = File.ReadAllText(Application.dataPath + "/Json/FreeSpinSymbol.json");
        SymbolProbJson symbolProbJson = JsonConvert.DeserializeObject<SymbolProbJson>(data);
        for (int i = 0; i < 19; i++)
            freeSpinBucket.probList[i] = symbolProbJson.list[i].Clone();

        freeSpinBucket.ConnectBucket();
    }


    public void BringBaseSpinBucket()
    {
        currentBucket = baseSpinBucket.DeepCopy();
    }
    public void BringFreeSpinBucket()
    {
        currentBucket = freeSpinBucket.DeepCopy();
    }

    public string PullOutSymbol(Vector2Int point)
    {
        return currentBucket.PullOutSymbol(point.x);
    }
    public void PushBackSymbol(string symbolName, Vector2Int point)
    {
        currentBucket.PushBackSymbol(symbolName, point.x);
    }
}
