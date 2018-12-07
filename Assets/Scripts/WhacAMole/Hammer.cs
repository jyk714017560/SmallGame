using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Hammer : MonoBehaviour {

    public SpriteAtlas Hammer_1;
    // Use this for initialization
    void Start () {
        GetComponent<SpriteRenderer>().sprite = Hammer_1.GetSprite("Hammer");
		
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<Transform>().position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
        if(Input.GetMouseButtonDown(0))
        {
            //GetComponent<SpriteRenderer>().sprite = Hammer_1.GetSprite("Hammer_Hit");
            StopAllCoroutines();
            StartCoroutine("Knock");
        }

    }

    IEnumerator Knock()
    {
        GetComponent<SpriteRenderer>().sprite = Hammer_1.GetSprite("Hammer_Hit");
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().sprite = Hammer_1.GetSprite("Hammer");
    }


}
