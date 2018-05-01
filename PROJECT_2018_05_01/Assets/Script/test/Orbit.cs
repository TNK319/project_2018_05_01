using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour {
    public GameObject BlockMan;
    Rigidbody2D rigid;
    float weightpower=10.0f;
    float heightpower = 10.0f;

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        DEBUG.debuglog("高さ:",heightpower);
        DEBUG.debuglog("横:",weightpower);
        
        //高さの強さ調整
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            heightpower += 1.0f;
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            heightpower += -1.0f;
        }
        //横の強さ調整
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            weightpower += 1.0f;
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            weightpower += -1.0f;
        }
        //発射
        if (Input.GetMouseButtonDown(0))
        {
            GameObject bot = Instantiate(BlockMan) as GameObject;
            rigid = bot.GetComponent<Rigidbody2D>();
            rigid.bodyType = RigidbodyType2D.Dynamic;
            rigid.AddForce(Vector2.right *weightpower, ForceMode2D.Impulse);
            rigid.AddForce(Vector2.up *heightpower, ForceMode2D.Impulse);
        }
	}
    Vector3 GetPos(float time, float power, float mass)
    {
        float halfGravity = Physics.gravity.y * 0.5f;

        float x = time * power / mass;
        float y = halfGravity * Mathf.Pow(time, 2);
        return new Vector3(x, y, 0);
    }
}
