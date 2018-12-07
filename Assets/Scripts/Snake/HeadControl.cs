using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HeadControl : MonoBehaviour {

    public string Direction="";
    public float Speed = 0.1f;
    public GameObject RedFood;
    List<Transform> BodyList = new List<Transform>();
    bool isAte=false;
    public BodyControl Body;


    // Use this for initialization
    void Start () {

        CreatFood();
        InvokeRepeating("move", 0, 0.5f);
		
	}
	
	// Update is called once per frame
	void Update () {

            if (Input.GetKeyDown(KeyCode.UpArrow)&&Direction!="Down")
            {
                Direction = "Up";
            }
            if (Input.GetKeyDown(KeyCode.DownArrow) && Direction != "Up")
            {
                Direction = "Down";
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow) && Direction != "Right")
            {
                Direction = "Left";
            }
            if (Input.GetKeyDown(KeyCode.RightArrow) && Direction != "Left")
            {
                Direction = "Right";
            }

	}

    void move()
    {

        Vector3 v = this.transform.position;
        switch (Direction)
        {
            case "Up":
                transform.position = new Vector3(transform.position.x, transform.position.y + Speed, 0);
                break;
            case "Down":
                transform.position = new Vector3(transform.position.x, transform.position.y - Speed, 0);
                break;
            case "Left":
                transform.position = new Vector3(transform.position.x - Speed, transform.position.y, 0);
                break;
            case "Right":
                transform.position = new Vector3(transform.position.x + Speed, transform.position.y, 0);
                break;
            default:
                break;

        }

        if(isAte)
        {
            BodyControl b = Instantiate(Body, v, Quaternion.identity);
            BodyList.Insert(0, b.transform);
            isAte = false;
            CreatFood();
        }
        if(BodyList.Count>0)
        {
            BodyList.Last().position = v;
            BodyList.Insert(0, BodyList.Last());
            BodyList.RemoveAt(BodyList.Count - 1);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Food")
        {
            isAte = true;
            Destroy(collision.gameObject);
        }
        else
        {
            Debug.Log("hello");
        }


    }

    public void CreatFood()
    {
        Vector3 v = new Vector3(Random.Range(-1.95f, 1.95f), Random.Range(-2.0f, 2.0f), 0);
        Instantiate(RedFood, v, Quaternion.identity);
    }
}
