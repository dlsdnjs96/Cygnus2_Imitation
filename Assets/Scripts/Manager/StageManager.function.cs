using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using TMPro;


public partial class StageManager : SymbolManager
{
    [SerializeField] TextMeshProUGUI winText;
    private float _score;
    public float score
    {
        get { return _score; }
        set { _score = value; winText.text = value.ToString("N"); }
    }
    protected int supplyHeight;

    void CreateBaseSymbols()
    {
        StartCoroutine(CoCreateBaseSymbols());
    }

    public void RemoveAllSymbols()
    {
        for (int i = 1;i <= 6;i++)
        {
            for (int j = 1; j <= 8; j++)
                RemoveSymbol(new Vector2Int(i, j));
        }
    }

    bool FindPayOut(out bool[,] completedSymbols)
    {

        bool isCompleted = false;
        int[,] countingTable = new int[7, 9]; // column, symbolID
        completedSymbols = new bool[7, 9];


        // 각각의 릴에 무슨 종류의 심볼이 몇개씩 있는지 카운트
        for (int i = 1; i <= 6; i++) // column
        {
            for (int j = 1; j <= 8; j++) // row
            {
                if (symbols[i, j] != null && symbols[i, j].IsCountable())
                { // only low, middle, high, wild
                    print("IsWild " + symbols[i, j].name + " " + symbols[i, j].IsWild());
                    if (symbols[i, j].IsWild())
                        countingTable[i, 8]++;
                    else 
                        countingTable[i, symbols[i, j].GetId()]++;
                }
            }
        }


        for (int i = 1; i <= 7; i++) // symbolID
        {
            bool isPossible = false;
            int col;


            // 현재 심볼 + 와일드로 몇줄 까지 이어지는지 확인
            for (col = 1; col <= 6; col++) // column
            {
                if (countingTable[col, i] <= 0 && countingTable[col, 8] <= 0)
                {
                    break;
                }
            }
            // 3줄이 안될 경우 현재 심볼 카운팅 테이블 초기화
            if (col <= 3)
            {
                for (col = 1; col <= 6; col++)
                    countingTable[col, i] = 0;
                continue;
            }



            // 경우의 수에 현재 심볼이 하나 이상 껴있는지
            for (int k = 1; k < col; k++)
            {
                if (countingTable[k, i] > 0)
                    isPossible = true;
            }

            // 와일드 심볼만으로 이루어진 조합일 경우 실패
            if (!isPossible)
            {
                for (col = 1; col <= 6; col++)
                    countingTable[col, i] = 0;
                continue;
            }



            // 점수 계산
            int way = countingTable[1, i] + countingTable[1, 8];

            for (int k = 2; k < col; k++)
            {
                way *= countingTable[k, i] + countingTable[k, 8];
            }

            float scorePlus = ScoreTable.Instance.payTable[col - 1, i] * way;
            score += scorePlus;



            // 터트릴 블록 확인
            for (int k = 1; k < col; k++) // column
            {
                for (int l = 1; l <= 8; l++)
                {
                    if (symbols[k, l] != null && 
                        (symbols[k, l].GetId() == i || symbols[k, l].IsWild()))
                    {
                        completedSymbols[k, l] = true;
                        isCompleted = true;
                    }
                }
            }
        }

        return isCompleted;
    }

    bool FindWildPayOut(out bool[,] completedSymbols)
    {
        Stack<Vector2Int> stack = new Stack<Vector2Int>();
        completedSymbols = new bool[7, 9];
        int maxCol = 1;

        if (symbols[1, 1] == null || !symbols[1, 1].IsWild()) return false;

        stack.Push(new Vector2Int(1, 1));
        while (stack.Count > 0)
        {
            Vector2Int top = stack.Pop();

            if (top.x > 6 || top.x < 1 || top.y > 8 || top.y < 1) continue;
            if (symbols[top.x, top.y] == null) continue;
            if (completedSymbols[top.x, top.y]) continue;
            if (!symbols[top.x, top.y].IsWild()) return false;

            completedSymbols[top.x, top.y] = true;
            maxCol = Mathf.Max(maxCol, top.x);

            if (symbols[top.x, top.y] != null)
            {
                if (top.x % 2 == 0)
                {
                    stack.Push(new Vector2Int(top.x + 1, top.y + 1));
                    stack.Push(new Vector2Int(top.x - 1, top.y + 1));
                }
                else
                {
                    stack.Push(new Vector2Int(top.x + 1, top.y - 1));
                    stack.Push(new Vector2Int(top.x - 1, top.y - 1));
                }
                stack.Push(new Vector2Int(top.x + 1, top.y));
                stack.Push(new Vector2Int(top.x - 1, top.y));
                stack.Push(new Vector2Int(top.x, top.y + 1));
                stack.Push(new Vector2Int(top.x, top.y - 1));
            }

        }
        return maxCol >= 3;
    }

    void FocusingSymbols(bool[,] targetSymbols)
    {

        for (int i = 1; i <= 6; i++)
        {
            for (int j = 1; j <= 8; j++)
            {
                if (!targetSymbols[i, j]) continue;

                ShowFocusEffect(symbols[i, j].transform.position);
            }
        }
    }
    void ExplodeSymbols(in bool[,] targetSymbols)
    {
        StartCoroutine(CoExplodeBlocks(targetSymbols));
    }

    bool FindFallDownSymbols()
    {
        for (int i = 1; i <= 6; i++)
        {
            for (int j = 2; j <= 8; j++)
            {
                if (symbols[i, j] != null && symbols[i, j - 1] == null) return true;
            }
        }
        return false;
    }
    void FallDownSymbols()
    {
        for (int i = 1; i <= 6; i++)
        {
            for (int j = 1; j <= 8; j++)
            {
                if (symbols[i, j] != null) continue;

                for (int k = j + 1; k <= 8; k++)
                {
                    if (symbols[i, k] != null)
                    {
                        MoveSymbol(symbols[i, k].currentPosition, new Vector2Int(i, j));
                        symbols[i, j].FallDownFromHere(new Vector2Int(i, j), 0.3f, eventAlram.GetEndEvent());
                        break;
                    }
                }
            }
        }
    }

    bool FindLeftSlideSymbols()
    {
        for (int i = 1; i <= 6; i++)
        {
            for (int j = 1 + i % 2; j <= 8; j++)
            {
                if (symbols[i, j] == null) break;

                if (i > 1 && symbols[i - 1, j - (i % 2)] == null)
                {
                    for (int k = j; k <= 8; k++)
                    {
                        if (symbols[i, k] == null) break;

                        return true;
                    }
                    break;
                }

            }
        }
        return false;
    }
    bool FindRightSlideSymbols()
    { 
        for (int i = 6; i >= 1; i--)
        {
            for (int j = 1 + i % 2; j <= 8; j++)
            {
                if (symbols[i, j] == null) break;

                if (i < 6 && symbols[i + 1, j - (i % 2)] == null)
                {
                    for (int k = j; k <= 8; k++)
                    {
                        if (symbols[i, k] == null) break;

                        return true;
                    }
                    i++;
                    break;
                }
            }
        }
        return false;
    }

    void LeftSlideDownSymbols()
    {
        for (int i = 1; i <= 6; i++)
        {
            for (int j = 1 + i % 2; j <= 8; j++)
            {
                if (symbols[i, j] == null) break;

                if (i > 1 && symbols[i - 1, j - (i % 2)] == null)
                {
                    for (int k = j; k <= 8; k++)
                    {
                        if (symbols[i, k] == null) break;

                        symbols[i, k].SlideDown(new Vector2Int(i - 1, k - (i % 2)), eventAlram.GetEndEvent());
                        MoveSymbol(new Vector2Int(i, k), new Vector2Int(i - 1, k - (i % 2)));
                    }
                    break;
                }

            }
        }
    }

    void RightSlideDownSymbols() { 
        for (int i = 6; i >= 1; i--)
        {
            for (int j = 1 + i % 2; j <= 8; j++)
            {
                if (symbols[i, j] == null) break;

                if (i < 6 && symbols[i + 1, j - (i % 2)] == null)
                {
                    for (int k = j; k <= 8; k++)
                    {
                        if (symbols[i, k] == null) break;

                        symbols[i, k].SlideDown(new Vector2Int(i + 1, k - (i % 2)), eventAlram.GetEndEvent());
                        MoveSymbol(new Vector2Int(i, k), new Vector2Int(i + 1, k - (i % 2)));
                    }
                    i++;
                    break;
                }
            }
        }
    }

    bool FindSupplySymbol()
    {
        for (int i = 1; i <= 6; i++)
        {
            for (int j = 1; j <= supplyHeight; j++)
            {
                if (symbols[i, j] == null) return true;
            }
        }

        return false;
    }

    void SupplySymbols()
    {
        supplyHeight = Mathf.Min(supplyHeight + 1, 8);
        StartCoroutine(CoSupplyBlocks());
    }

    bool FindSpecialSymbol()
    {
        for (int i = 1; i <= 8; i++)
        {
            if (symbols[1, i] != null && symbols[1, i].IsChangable())
                return true;
        }
        return false;
    }
    void ChangeSpecialSymbol()
    {
        for (int i = 1; i <= 8; i++)
        {
            if (symbols[1, i] != null && symbols[1, i].IsChangable())
            {
                if (symbols[1, i].IsMultifly())
                {
                    ShowChangingEffect(symbols[1, i].transform.position);
                    StartCoroutine(CoDelay(1.1f, eventAlram.GetEndEvent()));
                    gageBar.AddGage(symbols[1, i].GetMultifly());
                    

                } else if (symbols[1, i].IsScatter())
                {
                    isScattered = true;
                    gageBar.TurnOnScatter(symbols[1, i].transform.position, eventAlram.GetEndEvent());

                }
                ChangeSpecialSymbol(new Vector2Int(1, i)).MoveToLocation();
                break;
            }
        }
    }

    void CreateSymbolTable(out string[,] table, bool isHitted)
    {
        table = new string[7, 5];

        while (true)
        {
            for (int j = 1; j <= 4; j++)
            {
                for (int i = 1; i <= 6; i++)
                {
                    table[i, j] = SymbolBucketPool.Instance.PullOutSymbol(new Vector2Int(i, j));
                }
            }
            if (CheckIsHitted(table) == isHitted) return;

            for (int j = 1; j <= 4; j++)
            {
                for (int i = 1; i <= 6; i++)
                    SymbolBucketPool.Instance.PushBackSymbol(table[i, j], new Vector2Int(i, j));
            }
        }
    }

    bool CheckIsHitted(in string[,] table)
    {
        bool[,] countingTable = new bool[4, 19];

        for (int i = 1;i <= 3;i++) // column
        {
            for (int j = 1;j <= 4;j++) // row
            {
                countingTable[i, Constant.GetSymbolCountableIndex(table[i, j])] = true;
            }
        }

        int col;
        for (int i = 1; i <= 7; i++) // symbol type
        {
            for (col = 1; col <= 3; col++) // column
            {
                if (!countingTable[col, i] && !countingTable[col, 8]) break;
            }
            if (col == 4)
                return true;
        }
        return false;
    }
}
