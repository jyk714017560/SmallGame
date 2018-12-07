using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Racket : MonoBehaviour {

    // Use this for initialization
    public float speed = 10.0f;
	void Start () {

		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (transform.position.x > -5.2)
            {
                transform.Translate(Vector3.left * Time.deltaTime * speed);
            }
            else
            {
                return;
            }
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            if (transform.position.x < 5.2)
            {
                transform.Translate(Vector3.right * Time.deltaTime * speed);
            }
            else
            {
                return;
            }
        }
    }

    void performUpgrade(string name)
    {
        name = name.Remove(name.Length - 21);
        float x;
        Ball ballController = GameObject.Find("ball").GetComponent<Ball>();
        switch(name)
        {
            case "ball_speed_up":
                if(ballController.BallSpeed<27)
                {
                    ballController.BallSpeed += 3;
                }
                break;
            case "ball_speed_down":
                if (ballController.BallSpeed >18)
                {
                    ballController.BallSpeed -= 3;
                }
                break;
            case "paddle_size_up":
                x = this.gameObject.transform.localScale.x;
                if (x < 8.0f)
                    this.gameObject.transform.localScale = new Vector3(x += 0.25f, this.gameObject.transform.localScale.y, 1.0f);
                break;
            case "paddle_size_down":
                x = this.gameObject.transform.localScale.x;
                if (x < 8.0f)
                    this.gameObject.transform.localScale = new Vector3(x -= 0.25f, this.gameObject.transform.localScale.y, 1.0f);
                break;
            case "paddle_speed_up":
                speed += 3;
                break;
            case "paddle_speed_down":
                if(speed>7)
                {
                    speed -= 3;
                }
                
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="upgrade")
        {
            string name = collision.gameObject.GetComponent<UpGrade>().upgradeName;
            performUpgrade(name);
            Destroy(collision.gameObject);
        }
    }
}
