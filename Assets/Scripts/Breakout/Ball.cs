using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour {

    // Use this for initialization
    public float BallSpeed = 10f;
    int num = 0;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.anyKey&&num==0)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.up * BallSpeed;
            num=1;
        }
        
        if(transform.position.y<-8)
        {
            SceneManager.LoadScene("BreakOut");
        }
	}

    //检测小球位置（-0.5→0.5）
    float HitFactor(Vector2 ballPos,Vector2 racketPos,float racketWidth)
    {

        return (ballPos.x-racketPos.x)/racketWidth;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name=="racket"&&num==1)
        {
            float x = HitFactor(transform.position, collision.transform.position, collision.collider.bounds.size.x);
            Vector2 dir = new Vector2(x, 1).normalized;
            GetComponent<Rigidbody2D>().velocity = dir * BallSpeed;
        }

    }
}
