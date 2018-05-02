using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cannon : MonoBehaviour {
    public GameObject BlockMan;
    public GameObject ball;
    Vector3[] line = new Vector3[10];
    Rigidbody2D rigid;
    float power=10.0f;
    float height = 10.0f;

    // Use this for initialization
    void Start () {
        for(int i = 0; i < 10; i++)
        {
            Instantiate(ball);
            ball.GetComponent<Orbitline>().ID = i;
        }
	}
	
	// Update is called once per frame
	void Update () {
        DEBUG.debuglog("高さ:",height);
        DEBUG.debuglog("横:",power);
        
        //高さの強さ調整
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            height += 1.0f;
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            height += -1.0f;
        }
        //発射
        if (Input.GetMouseButtonDown(0))
        {
            GameObject bot = Instantiate(BlockMan) as GameObject;
            rigid = bot.GetComponent<Rigidbody2D>();
            rigid.AddForce(Vector2.right *power, ForceMode2D.Impulse);
            rigid.AddForce(Vector2.up *height, ForceMode2D.Impulse);
        }
        //軌道線座標を演算登録
        for(int i = 0; i < 10; i++)
        {
            float a = i * 0.2f;
            line[i] = GetPos(a, power,height, 1);
        }
	}
    Vector3 GetPos(float time, float power, float uppower,float mass)
    {
        float halfGravity = Physics.gravity.y * 0.5f;

        float x = time * power / mass;
        float y = time * uppower / mass +halfGravity * Mathf.Pow(time, 2);
        return new Vector3(x+transform.position.x, y+transform.position.y, 0) ;
    }
    public Vector3 getpos(int ID)
    {
        return line[ID];
    }
}
