using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bird : MonoBehaviour {

    // Use this for initialization
    public float speed = 2f;
    public float force = 300f;

	void Start () {
        GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
            GetComponent<Rigidbody2D>().AddForce(Vector2.up*force);
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        SceneManager.LoadScene("FlappyBird");

    }

}


