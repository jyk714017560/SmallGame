using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class BlockController : MonoBehaviour {

    // Use this for initialization
    public GameObject upgradePrefab;
    public SpriteAtlas Block_1;
    void Start () {
        string spriteFileName = "block_" + GetComponent<Block>().color;
        this.GetComponent<SpriteRenderer>().sprite = Block_1.GetSprite(spriteFileName);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject go = GameObject.Find("Main Camera");
        LevelLoader levelLoader = go.GetComponent<LevelLoader>();
        gameObject.GetComponent<Block>().hits_required -= 1;


        if (gameObject.GetComponent<Block>().hits_required==0)
        {
            Destroy(gameObject);
            levelLoader.block_count--;
        }

        switch (gameObject.GetComponent<Block>().color)
        {
            case "blue":
                gameObject.GetComponent<Block>().color = "green";
                break;
            case "green":
                gameObject.GetComponent<Block>().color = "pink";
                break;
            case "red":
                gameObject.GetComponent<Block>().color = "yellow";
                break;
            case "yellow":
                gameObject.GetComponent<Block>().color = "blue";
                break;
            default:
                Destroy(gameObject);
                break;
        }
        string spriteFileName = "block_" + GetComponent<Block>().color;
        this.GetComponent<SpriteRenderer>().sprite = Block_1.GetSprite(spriteFileName);

        if (Random.value<0.1)
        {
            Instantiate(upgradePrefab, new Vector3(collision.gameObject.transform.position.x, collision.transform.position.y, 0),Quaternion.identity);
        }
    }
}
