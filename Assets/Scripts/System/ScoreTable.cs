using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTable : Singleton<ScoreTable>
{
    public float[,] payTable = new float[7, 10]; // column, blockID


    private void Start()
    {
        payTable[3, 1] = 0.05f;
        payTable[4, 1] = 0.1f;
        payTable[5, 1] = 0.15f;
        payTable[6, 1] = 0.2f;

        payTable[3, 2] = 0.05f;
        payTable[4, 2] = 0.1f;
        payTable[5, 2] = 0.15f;
        payTable[6, 2] = 0.2f;

        payTable[3, 3] = 0.05f;
        payTable[4, 3] = 0.1f;
        payTable[5, 3] = 0.15f;
        payTable[6, 3] = 0.2f;

        payTable[3, 4] = 0.1f;
        payTable[4, 4] = 0.2f;
        payTable[5, 4] = 0.3f;
        payTable[6, 4] = 0.4f;

        payTable[3, 5] = 0.2f;
        payTable[4, 5] = 0.4f;
        payTable[5, 5] = 0.6f;
        payTable[6, 5] = 0.8f;

        payTable[3, 6] = 0.3f;
        payTable[4, 6] = 0.6f;
        payTable[5, 6] = 0.9f;
        payTable[6, 6] = 1.2f;

        payTable[3, 7] = 2.5f;
        payTable[4, 7] = 5.0f;
        payTable[5, 7] = 10.0f;
        payTable[6, 7] = 15.0f;

        payTable[3, 8] = 5.0f;
        payTable[4, 8] = 10.0f;
        payTable[5, 8] = 20.0f;
        payTable[6, 8] = 30.0f;
    }
}
