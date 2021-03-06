﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Grid : MonoBehaviour {

    public static int width = 10;
    public static int height = 20;
    public static Transform[,] grid = new Transform[width, height];
    public static int score = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static Vector2 RoundVec2(Vector2 v)
    {
        return new Vector2(Mathf.Round(v.x), Mathf.Round(v.y));
    }

    public static bool InsideBorder(Vector2 pos)
    {
        return ((int)pos.x >= 0 && (int)pos.x < width && (int)pos.y >= 0);
    }

    public static bool IsRowFull(int y)
    {
        for(int x=0;x<width;x++)
        {
            if(grid[x,y]==null)
            {
                return false;
            }
        }
        return true;
    }

    public static void DeleteRow(int y)
    {
        for(int x=0;x<width;x++)
        {
            Destroy(grid[x, y].gameObject);
            grid[x, y] = null;
        }
    }

    public static void DecreaseRow(int y)
    {
        for(int x=0;x<width;x++)
        {
            if(grid[x,y]!=null)
            {
                grid[x, y - 1] = grid[x, y];
                //grid[x, y].gameObject.transform.position -= new Vector3(0, -1, 0);
                grid[x, y] = null;
                grid[x, y-1].position += new Vector3(0, -1, 0);
            }
        }
    }

    public static void DecreaseRowAbove(int y)
    {
        for(int i=y;i<height;i++)
        {
            DecreaseRow(i);
        }
    }

    public static void DeleteFullRows()
    {
        for(int y=0;y<height;y++)
        {
            if(IsRowFull(y))
            {
                DeleteRow(y);
                score++;
                SetScore(score);
                DecreaseRowAbove(y + 1);
                y--;
            }
        }
    }

    public static void SetScore(int s)
    {
        GameObject.Find("Score").GetComponent<Text>().text = "" + s;
    }
}
