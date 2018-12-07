using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyControl : MonoBehaviour {

    // Use this for initialization
    public HeadControl Head;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Food")
        {
            Destroy(collision.gameObject);
            Head.CreatFood();
        }
    }
}
