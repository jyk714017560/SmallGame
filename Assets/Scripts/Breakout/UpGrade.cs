using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpGrade : MonoBehaviour {

    // Use this for initialization
    public Sprite[] upgradeSprites;
    public string upgradeName = "";
	void Start () {
        Sprite icon = upgradeSprites[Random.Range(0, upgradeSprites.Length)];
        upgradeName = icon.ToString();
        this.gameObject.GetComponent<SpriteRenderer>().sprite = icon;
	}
	
	// Update is called once per frame
	void Update () {
        this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y - 0.05f, 0);
        if(gameObject.transform.position.y<=-8.0f)
        {
            Destroy(this.gameObject);
        }
	}

}
