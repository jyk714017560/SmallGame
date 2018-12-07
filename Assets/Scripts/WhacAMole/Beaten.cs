using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beaten : MonoBehaviour {

    public int id;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Destroy(gameObject, 0.35f);
        FindObjectOfType<GameControl>().holes[id].isAppear = false;
	}
}
