using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class LevelLoader : MonoBehaviour {

    // Use this for initialization
    public Block block;
    public int block_count = 0;
	void Start () {
        string level = getRandomLevelName();
        //Debug.Log(level);
        LoadLevel(level);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    string getRandomLevelName()
    {
        int level = Random.Range(1, 5);
        return "Assets/Levels/level_" + level+".txt";
    }
    public void LoadLevel(string levelName)
    {
        try
        {
            string line;
            StreamReader reader = new StreamReader(levelName,Encoding.Default);
            using (reader)
            {
                float pos_x = -5f;
                float pos_y = 5.8f;
                line = reader.ReadLine();
                while(line!=null)
                {
                    char[] characters = line.ToCharArray();
                    foreach(char character in characters)
                    {
                        if(character=='X')
                        {
                            pos_x += 0.87f;
                            continue;
                        }
                        Vector2 b_pos = new Vector2(pos_x, pos_y);
                        Block b = Instantiate(block, b_pos, Quaternion.identity);
                        b.GetComponent<BoxCollider2D>().size = new Vector2(0.8f, 0.4f);
                        switch(character)
                        {
                            case 'B':
                                b.GetComponent<Block>().color = "blue";
                                b.GetComponent<Block>().hits_required = 3;
                                block_count++;
                                break;
                            case 'G':
                                b.GetComponent<Block>().color = "green";
                                b.GetComponent<Block>().hits_required = 2;
                                block_count++;
                                break;
                            case 'P':
                                b.GetComponent<Block>().color = "pink";
                                b.GetComponent<Block>().hits_required = 1;
                                block_count++;
                                break;
                            case 'R':
                                b.GetComponent<Block>().color = "red";
                                b.GetComponent<Block>().hits_required = 5;
                                block_count++;
                                break;
                            case 'Y':
                                b.GetComponent<Block>().color = "yellow";
                                b.GetComponent<Block>().hits_required = 4;
                                block_count++;
                                break;
                            default:
                                Destroy(b);
                                break;
                        }
                        pos_x += 0.87f;
                    }
                    pos_x = -5.5f;
                    pos_y -= 0.45f;
                    line = reader.ReadLine();
                }
                reader.Close();
            }

        }  
        catch(IOException e)
        {
            Debug.Log(e.Message);
        }
    }
}
