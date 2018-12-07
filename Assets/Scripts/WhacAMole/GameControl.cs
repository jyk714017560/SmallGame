using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class GameControl : MonoBehaviour {

    public GameObject Gophers;
    public int PosX, PosY;
    public TextMesh timeLabel;
    public float time = 30f;
    public int score = 0;
	// Use this for initialization

    public class Hole
    {
        public bool isAppear;
        public int HoleX;
        public int HoleY;
    }

    public Hole[] holes;

    private void Awake()
    {
        PosX = -2;
        PosY = -2;
        holes = new Hole[9];
        for(int i=0; i<3; i++)
        {

            for(int j=0; j<3; j++)
            {
                holes[i * 3 + j] = new Hole();
                holes[i * 3 + j].HoleX = PosX;
                holes[i * 3 + j].HoleY = PosY;
                holes[i * 3 + j].isAppear = false;
                PosY++;
            }
            PosY = -2;
            PosX=PosX+2;
        }
        Cursor.visible = false;
    }
    void Start ()
    {

        //Instantiate(Gophers, new Vector3(0, 0+0.4f,-0.1f),Quaternion.identity);
        InvokeRepeating("CanAppear", 0, 10);
        
	}
	
	// Update is called once per frame
	void Update () {
        time -= Time.deltaTime;
        timeLabel.text = "Time:" + time.ToString("F1");

        if(time<0)
        {
            gameover();
        }
		
	}

    public void Appear()
    {
        int i = Random.Range(0, 9);
        while(holes[i].isAppear==true)
        {
            i = Random.Range(0, 9);
        }
        Debug.Log(holes[i].HoleX + "," + holes[i].HoleY);
        Instantiate(Gophers, new Vector3(holes[i].HoleX, holes[i].HoleY + 0.4f, -0.1f), Quaternion.identity);
        Gophers.GetComponent<Gophers>().id = i;
        holes[i].isAppear = true;

    }

    void CanAppear()
    {
        InvokeRepeating("Appear", 0, 1);
    }

    void gameover()
    {
        time = 0;
        timeLabel.text = "Time:0";
        CancelInvoke();
        Cursor.visible = true;
        FindObjectOfType<Hammer>().GetComponent<SpriteRenderer>().sprite = null;
    }
}
