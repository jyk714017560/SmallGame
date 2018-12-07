using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gophers : MonoBehaviour {

    public GameObject beaten;
    public int id;
	// Use this for initialization
	void Start ()
    {

		
	}
	
	// Update is called once per frame
	void Update ()
    {
        Destroy(this.gameObject, 3f);
        FindObjectOfType<GameControl>().holes[id].isAppear = false;

	}

    private void OnMouseDown()
    {
        GameObject g;
        g = Instantiate(beaten, gameObject.transform.position, Quaternion.identity);
        Destroy(this.gameObject, 0.1f);
        g.GetComponent<Beaten>().id = id;
        FindObjectOfType<GameControl>().score++;
        int scores = FindObjectOfType<GameControl>().score;
        GameObject.Find("Score").gameObject.GetComponent<TextMesh>().text= "Score:" + scores.ToString();
    }
}
