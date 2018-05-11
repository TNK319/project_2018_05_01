using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Positioning : MonoBehaviour {
    public GameObject Bank;
    float bigwidth;
    float bigheight;
    float smallwidth;
    float smallheight;
    float widthfree;
    float heightfree;
    float halfwidth;
    float halfheight;
    public bool start=false;
    public bool big = false;
    // Use this for initialization
    void Start () {
        AutoAdjustment();
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            //整数で位置
            int intx = 0;
            int inty = 0;
            float x = transform.position.x;
            float y = transform.position.y;
            Debug.Log("float座標:x," + x + ":y," + y);
            //x座標が正の数か負の数
            if (x >= 0)
            {
                intx = (int)((x + smallwidth / 2) / smallwidth + widthfree);
                Debug.Log("座標,x:" + intx);
            }
            else if (x < 0)
            {
                intx = (int)((x - smallwidth / 2) / smallwidth + widthfree);
                Debug.Log("座標,x:" + intx);
            }
            //y座標が正の数か負の数
            if (y >= 0)
            {
                inty = (int)((y + smallheight / 2) / smallheight + heightfree);
                Debug.Log("座標,y:" + inty);
            }
            else if (y < 0)
            {
                inty = (int)((y - smallheight / 2) / smallheight + heightfree);
                Debug.Log("座標,y:" + inty);
            }
            Debug.Log("座標,x:"+ intx * (smallwidth + widthfree)+",y:"+ inty * (smallheight + heightfree));
            if (big)
            {
                gameObject.transform.position = new Vector2(intx * (smallwidth + widthfree) + halfwidth, inty * (smallheight + heightfree) + halfheight); 
            }
            else
            {
                gameObject.transform.position = new Vector2(intx * (smallwidth + widthfree), inty * (smallheight + heightfree));
            }
            start = false;
        }
    }
    private void AutoAdjustment()
    {
        //二つのbankの大きさを比べてそれぞれに代入
        if (Bank.GetComponent<SpriteRenderer>().bounds.size.x < gameObject.GetComponent<SpriteRenderer>().bounds.size.x)
        {
            bigwidth = gameObject.GetComponent<SpriteRenderer>().bounds.size.x;
            bigheight = gameObject.GetComponent<SpriteRenderer>().bounds.size.y;
            smallwidth = Bank.GetComponent<SpriteRenderer>().bounds.size.x;
            smallheight = Bank.GetComponent<SpriteRenderer>().bounds.size.y;
            big = true;
        }
        else
        {
            bigwidth = Bank.GetComponent<SpriteRenderer>().bounds.size.x;
            bigheight = Bank.GetComponent<SpriteRenderer>().bounds.size.y;
            smallwidth = gameObject.GetComponent<SpriteRenderer>().bounds.size.x;
            smallheight = gameObject.GetComponent<SpriteRenderer>().bounds.size.y;
        }
        //座標の空き間隔
        widthfree = bigwidth - smallwidth * 2;
        heightfree = bigheight - smallheight * 2;
        halfwidth = (smallwidth+widthfree)/2;
        halfheight = (smallheight+heightfree)/2;
        Debug.Log("bigwidth," + bigwidth);
        Debug.Log("smallwidth," + smallwidth);
        Debug.Log("widthfree" + widthfree);
        Debug.Log("bigheight," + bigheight);
        Debug.Log("smallheight," + smallheight);
        Debug.Log("heightfree" + heightfree);
        Debug.Log("halfwidth"+halfwidth);
        Debug.Log("halfheight" + halfheight);
    }
    public void dataset(float smallwidth_set,float widthfree_set,float smallheight_set,float heightfree_Set)
    {
        smallwidth=smallwidth_set;
        widthfree=widthfree_set;
        smallheight=smallheight_set;
        heightfree=heightfree_Set;
        start = true;
    }
}
