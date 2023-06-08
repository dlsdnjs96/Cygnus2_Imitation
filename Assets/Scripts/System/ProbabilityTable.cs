using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class BlockProb
{
    public string name;
    public float prob;
    public BlockProb()
    {
        name = "";
        prob = 0.0f;
    }
    public BlockProb(string _name, float _prob)
    {
        name = _name;
        prob = _prob;
    }
}

public class BlockReel
{
    public List<BlockProb> list;
    float totalProb;
    public BlockReel()
    {
        list = new List<BlockProb>();
    }
    public void CalculateProb()
    {
        totalProb = 0.0f;
        foreach (var blockProb in list)
            totalProb += blockProb.prob;
    }
    void Add(BlockProb blockProb)
    {
        list.Add(blockProb);
        CalculateProb();
    }
    public string GetRandom()
    {
        float prob = Random.Range(0.0f, totalProb);

        foreach (var blockProb in list)
        {
            if (prob <= blockProb.prob) return blockProb.name;
            prob -= blockProb.prob;
        }
        return "";
    }
}


public class ProbabilityTable : Singleton<ProbabilityTable>
{
    public BlockReel normalReel;
    public BlockReel col6Reel;

    private void Start()
    {
        Load();
    }

    private void Load()
    {
        string data = File.ReadAllText(Application.dataPath + "/Json/NormalReel.json");
        normalReel = JsonConvert.DeserializeObject<BlockReel>(data);
        normalReel.CalculateProb();

        data = File.ReadAllText(Application.dataPath + "/Json/Col6Reel.json");
        col6Reel = JsonConvert.DeserializeObject<BlockReel>(data);
        col6Reel.CalculateProb();
    }
    


    public string GetRandomBlock(Vector2Int currentPoint)
    {
        if (currentPoint.x == 6) 
            return col6Reel.GetRandom();
        return normalReel.GetRandom();
    }

}
