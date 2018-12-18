using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gemstone : MonoBehaviour {

    // Use this for initialization
    public float xOffset = -5.5f;
    public float yOffset = -2.0f;
    public int rowIndex = 0;
    public int columIndex = 0;
    public GameObject[] gemstoneBgs;
    public int gemstoneType;
    GameObject gemstoneBg;
    SpriteRenderer SpriteRenderer;
    EliminateGameController gameController;

	void Start () {
        gameController = GameObject.Find("GameController").GetComponent<EliminateGameController>();
        SpriteRenderer = gemstoneBg.GetComponent<SpriteRenderer>();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RandomCreateGemstoneBg()
    {
        if(gemstoneBg!=null)
        {
            return;
        }
        gemstoneType = Random.Range(0, gemstoneBgs.Length);
        gemstoneBg = Instantiate(gemstoneBgs[gemstoneType]) as GameObject;
        gemstoneBg.transform.parent = this.transform;
    }

    public void UpdatePosition(int _rowIndex,int _columIndex)
    {
        rowIndex = _rowIndex;
        columIndex = _columIndex;
        this.transform.position = new Vector3(columIndex + xOffset, rowIndex + yOffset, 0);
    }

    public bool isSelected
    {
        set
        {
            if (value)
            {
                SpriteRenderer.color = Color.red;
            }
            else
            {
                SpriteRenderer.color = Color.white;
            }
        }   

    }

    public void OnMouseDown()
    {
        gameController.Select(this);
    }

    public void TweenToPosition(int _rowIndex,int _columIndex)
    {
        rowIndex = _rowIndex;
        columIndex = _columIndex;
        iTween.MoveTo(this.gameObject, iTween.Hash("x", columIndex + xOffset, "y", rowIndex + yOffset, "time", 0.5f));
    }

    public void Dispose()
    {
        Destroy(this.gameObject);
        Destroy(gemstoneBg.gameObject);
        gameController = null;
    }
}
