using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour {

    public GameObject[] Blocks;
    public Sprite[] sprites;
    public static bool isFirst = true;
    public static int current = 0;
    public static int next = 0;

	// Use this for initialization
	void Start () {
        SpawnerNext();
	}
	
	// Update is called once per frame
	void Update () {

		
	}
    
    public void SpawnerNext()
    {
        if(isFirst)
        {
            isFirst = false;
            current = Random.Range(0, Blocks.Length);
            next = Random.Range(0, Blocks.Length);
        }
        else
        {
            current = next;
            next = Random.Range(0, Blocks.Length);
        }

        Instantiate(Blocks[current], transform.position, Quaternion.identity);
        GameObject.Find("Image").GetComponent<Image>().sprite = sprites[next];
    }
}
