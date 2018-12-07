using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Group : MonoBehaviour {

    public float lastFall = 0;
	// Use this for initialization
	void Start () {
        if(!IsValidGridPos())
        {
            Debug.Log("Game Over");
            Destroy(gameObject);
        }
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.LeftArrow)||Input.GetKeyDown(KeyCode.A))
        {
            transform.position += new Vector3(-1, 0, 0);
            if(IsValidGridPos())
            {
                UpdateGrid();
            }
            else transform.position += new Vector3(1, 0, 0);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            transform.position += new Vector3(1, 0, 0);
            if (IsValidGridPos())
            {
                UpdateGrid();
            }
            else transform.position += new Vector3(-1, 0, 0);
        }

        if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            transform.Rotate(0, 0, -90);
            if (IsValidGridPos())
            {
                UpdateGrid();
            }
            else transform.Rotate(0, 0, 90);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)||Time.time-lastFall>1)
        {
            transform.position += new Vector3(0,-1,0);
            if (IsValidGridPos())
            {
                UpdateGrid();
            }
            else
            {
                transform.position += new Vector3(0, 1, 0);
                Grid.DeleteFullRows();
                FindObjectOfType<Spawner>().SpawnerNext();
                enabled = false;
            }
            lastFall = Time.time;
        }

    }

    void UpdateGrid()
    {
        for(int y=0;y<Grid.height;y++)
        {
            for(int x=0;x<Grid.width;x++)
            {
                if(Grid.grid[x,y]!=null)
                {
                    if(Grid.grid[x,y].parent==transform)
                    {
                        Grid.grid[x, y] = null;
                    }
                }
            }
        }

        foreach(Transform child in transform)
        {
            Vector2 v = Grid.RoundVec2(child.position);
            Grid.grid[(int)v.x, (int)v.y] = child;
        }
    }

    bool IsValidGridPos()
    {
        foreach(Transform child in transform)
        {
            Vector2 v = Grid.RoundVec2(child.position);
            if(!Grid.InsideBorder(v))
            {
                return false;
            }
            if(Grid.grid[(int)v.x,(int)v.y]!=null&&Grid.grid[(int)v.x,(int)v.y].parent!=transform)
            {
                return false;
            }

        }
        return true;
    }


}
