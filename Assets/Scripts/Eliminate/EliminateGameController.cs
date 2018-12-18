using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliminateGameController : MonoBehaviour {

    // Use this for initialization
    public Gemstone Gemstone;
    public int rowNum = 7;
    public int columNum = 10;
    public List<List<Gemstone>> gemstoneList;
    List<Gemstone> matchesGemstone;
    Gemstone currentGemstone;

	void Start () {
        gemstoneList = new List<List<Gemstone>>();
        matchesGemstone = new List<Gemstone>();
        for(int rowIndex=0;rowIndex<rowNum;rowIndex++)
        {
            List<Gemstone> temp = new List<Gemstone>();
            for(int columIndex=0;columIndex<columNum;columIndex++)
            {
                Gemstone g = AddGemstone(rowIndex, columIndex);
                temp.Add(g);
            }
            gemstoneList.Add(temp);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //在制定位置生成宝石
    public Gemstone AddGemstone(int rowIndex,int columIndex)
    {
        Gemstone g = Instantiate(Gemstone) as Gemstone;
        g.transform.parent = this.transform;
        g.GetComponent<Gemstone>().RandomCreateGemstoneBg();
        g.GetComponent<Gemstone>().UpdatePosition(rowIndex, columIndex);
        return g;
    }

    //判断是否选中宝石，如果没有选中则选中，如果选中则判断另一个选中的是否可以交换，可以交换则交换，否则将前者变为未选中
    public void Select(Gemstone g)
    {
        if (currentGemstone == null)
        {
            currentGemstone = g;
            currentGemstone.isSelected = true;
            return;
        }
        else
        {
            if(Mathf.Abs(currentGemstone.rowIndex-g.rowIndex)+Mathf.Abs(currentGemstone.columIndex-g.columIndex)==1)
            {
                ExchangeAndMatches(currentGemstone, g);
            }
            currentGemstone.isSelected = false;
            currentGemstone = null;
        }
    }

    //将一个物体赋值给数组中的某个位置
    public void SetGemstone(int rowIndex,int columIndex,Gemstone g)
    {
        List<Gemstone> temp=gemstoneList[rowIndex] as List<Gemstone>;
        temp[columIndex] = g;
    }

    //交换两个宝石位置，首先将两者数组交换，然后两者的坐标交换，然后进行移动
    public void Exchange(Gemstone g1,Gemstone g2)
    {
        SetGemstone(g1.rowIndex, g1.columIndex,g2);
        SetGemstone(g2.rowIndex, g2.columIndex, g1);
        int tempRowIndex;
        tempRowIndex = g1.rowIndex;
        g1.rowIndex = g2.rowIndex;
        g2.rowIndex = tempRowIndex;

        int tempColumIndex;
        tempColumIndex = g1.columIndex;
        g1.columIndex = g2.columIndex;
        g2.columIndex = tempColumIndex;

        g1.TweenToPosition(g1.rowIndex, g1.columIndex);
        g2.TweenToPosition(g2.rowIndex, g2.columIndex);

    }

    //如果交换后连成一条线则消除，如果没有则交换回来
    void ExchangeAndMatches(Gemstone g1,Gemstone g2)
    {
        Exchange(g1, g2);
        if(CheckHorizontalMatches()||CheckVerticalMatches())
        {
            RemoveMatches();
        }
        else
        {
            Exchange(g1, g2);
        }
        
    }

    //得到一个已知位置的宝石物体
    public Gemstone GetGemstone(int rowIndex,int columIndex)
    {
        List<Gemstone> temp = gemstoneList[rowIndex] as List<Gemstone>;
        Gemstone g = temp[columIndex] as Gemstone;
        return g;
    }

    bool CheckHorizontalMatches()
    {
        bool isMatches = false;
        for(int rowIndex=0;rowIndex<rowNum;rowIndex++)
        {
            for(int columIndex=0;columIndex<columNum-2; columIndex++)
            {
                if((GetGemstone(rowIndex,columIndex).gemstoneType==GetGemstone(rowIndex,columIndex+1).gemstoneType)&& (GetGemstone(rowIndex, columIndex).gemstoneType == GetGemstone(rowIndex, columIndex + 2).gemstoneType))
                {
                    AddMatches(GetGemstone(rowIndex, columIndex));
                    AddMatches(GetGemstone(rowIndex, columIndex+1));
                    AddMatches(GetGemstone(rowIndex, columIndex+2));
                    isMatches = true;
                }
            }
        }
        return isMatches;
    }

    bool CheckVerticalMatches()
    {
        bool isMatches = false;
        for (int columIndex = 0; columIndex < columNum; columIndex++)
        {
            for (int rowIndex = 0; rowIndex < rowNum - 2; rowIndex++)
            {
                if ((GetGemstone(rowIndex, columIndex).gemstoneType == GetGemstone(rowIndex+1, columIndex ).gemstoneType) && (GetGemstone(rowIndex, columIndex).gemstoneType == GetGemstone(rowIndex+2, columIndex).gemstoneType))
                {
                    AddMatches(GetGemstone(rowIndex, columIndex));
                    AddMatches(GetGemstone(rowIndex+1, columIndex ));
                    AddMatches(GetGemstone(rowIndex+2, columIndex ));
                    isMatches = true;
                }
            }
        }
        return isMatches;
    }


    //将成三个的物件加入消除队列
    void AddMatches(Gemstone g)
    {
        if(matchesGemstone==null)
        {
            matchesGemstone = new List<Gemstone>();
        }
        int Index = matchesGemstone.IndexOf(g);
        if(Index==-1)
        {
            matchesGemstone.Add(g);
        }
    }

    void RemoveGemstone(Gemstone g)
    {
        g.Dispose();
        for(int i=g.rowIndex+1;i<rowNum;i++)
        {
            Gemstone temGemstone = GetGemstone(i, g.columIndex);
            temGemstone.rowIndex--;
            SetGemstone(temGemstone.rowIndex, temGemstone.columIndex, temGemstone);

            temGemstone.TweenToPosition(temGemstone.rowIndex, temGemstone.columIndex);
        }
        Gemstone newGemstone = AddGemstone(rowNum, g.columIndex);
        newGemstone.rowIndex--;
        SetGemstone(newGemstone.rowIndex, newGemstone.columIndex, newGemstone);

        newGemstone.TweenToPosition(newGemstone.rowIndex, newGemstone.columIndex);
    }

    void RemoveMatches()
    {
        for (int i=0;i<matchesGemstone.Count;i++)
        {
            Gemstone g = matchesGemstone[i] as Gemstone;
            RemoveGemstone(g);
        }
        matchesGemstone = new List<Gemstone>();
        StartCoroutine("WaitForCheckMatchesAgain");
    }

    IEnumerator WaitForCheckMatchesAgain()
    {
        yield return new WaitForSeconds(0.5f);
        if (CheckHorizontalMatches()||CheckVerticalMatches())
        {
            RemoveMatches();
        }
    }
}
