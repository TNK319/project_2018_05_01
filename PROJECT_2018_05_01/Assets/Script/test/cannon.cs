using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cannon : MonoBehaviour {
    //弾丸
    public GameObject bullet;
    public GameObject ball;
    public GameObject axis;
    //軌道座標登録座標変数
    Vector3[] line = new Vector3[15];
    Rigidbody2D rigid;
    //弾丸を飛ばす力
    float maxpower=20.0f;
    float powerx=0.0f;
    float powery = 0.0f;
    float heighty;

    float time=1.0f;

    public bool player;//true:player,false:enemy

    // Use this for initialization
    void Start () {
        
        for(int i = 0; i < line.Length; i++)
        {
            //軌道座標用に球を生成
            Instantiate(ball);
            //生成されたときに順番にIDを登録
            ball.GetComponent<Orbitline>().ID = i;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (player)
        {
            //マウスの座標獲得
            Vector3 s_position = Input.mousePosition;
            //マウス座標をワールド座標に変換
            Vector3 w_position = Camera.main.ScreenToWorldPoint(s_position);
            //大砲の角度の調整
            //マウスが大砲を超えたら
            if (w_position.y >= axis.transform.position.y)
            {
                if (w_position.x >= axis.transform.position.x)
                {
                    heighty = GetAim(axis.transform.position, w_position);
                }
                else
                {
                    heighty = 90;
                }
            }
            else
            {
                heighty = 0;
            }
        }
        //敵だった場合
        else
        {
            time -= Time.deltaTime;
            if (time <= 0)
            {
                time = 1.0f;
                heighty = 45+90;
            }
        }
        
        //角度制御
        if (heighty/90 < 1.00f)
        {
            powery = maxpower * heighty/90;
            powerx = maxpower - powery;
        }
        else
        {
            powery = maxpower * heighty / 90;
            powerx = maxpower - powery;
            powery = maxpower * (180 - heighty) / 90;
        }
        //求めた角度を反映
        axis.transform.rotation = Quaternion.Euler(0.0f, 0.0f, heighty);
        //発射
        if (Input.GetMouseButtonDown(0))
        {
            //弾丸生成座標指定
            bullet.transform.position = gameObject.transform.position;
            //弾丸生成
            GameObject bot = Instantiate(bullet) as GameObject;
            rigid = bot.GetComponent<Rigidbody2D>();
            //飛ばす軌道
            rigid.AddForce(Vector2.right *powerx, ForceMode2D.Impulse);
            rigid.AddForce(Vector2.up *powery, ForceMode2D.Impulse);
        }
        //軌道線座標を演算登録
        for(int i = 0; i < line.Length; i++)
        {
            //軌道の玉の間隔
            float interval = i * 0.2f;
            line[i] = GetPos(interval, powerx,powery, 1);
        }
	}
    //二つのオブジェクトから角度を求めるメソッド
    private float GetAim(Vector2 p1, Vector2 p2)
    {
        Vector2 a = new Vector2(1, 0);
        Vector2 b = p2 - p1; // p1を原点に持ってくる
        return Vector2.Angle(a, b);
    }
    private Vector3 GetPos(float time, float power, float uppower,float mass)
    {
        //軌道上にかかる重力
        float halfGravity = Physics.gravity.y * 0.5f;
        //一定時間後にいるx座標
        float x = time * power / mass;
        //一定時間語にいるy座標と高さにかかる重力
        float y = time * uppower / mass +halfGravity * Mathf.Pow(time, 2);
        //大砲の位置を返り値に渡す
        return new Vector3(x+transform.position.x, y+transform.position.y, 0) ;
    }
    //軌道上にある球に登録した軌道座標を渡す
    public Vector3 getpos(int ID)
    {
        return line[ID];
    }
}
