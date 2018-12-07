using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraFollow : MonoBehaviour {

    // Use this for initialization
    public Transform target;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
       transform.position = new Vector3(target.position.x,transform.position.y,transform.position.z);
	}
}
