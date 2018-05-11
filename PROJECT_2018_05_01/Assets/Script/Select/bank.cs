using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bank : MonoBehaviour {
    public GameObject []Bank=new GameObject[2];
    GameObject banksave;
    GameObject BankPL;
    Vector3 s_position;
    Vector3 w_position;

    float bigwidth;
    float smallwidth;
    float widthfree;
    float bigheight;
    float smallheight;
    float heightfree;
    // Use this for initialization
    void Start () {
        this.BankPL = GameObject.Find("BankPl");
        AutoAdjustment();
	}

    // Update is called once per frame
    void Update()
    {
        selecttouch();
    }
    private void selecttouch()
    {
        //マウスの座標獲得
        s_position = Input.mousePosition;
        //マウス座標をワールド座標に変換
        w_position = Camera.main.ScreenToWorldPoint(s_position);
        w_position.z = 10;
        //マウスクリック時
        if (Input.GetMouseButtonDown(0))
        {
            // 左クリックされた場所のオブジェクトを取得
            Collider2D collition2d = Physics2D.OverlapPoint(w_position);
            if (collition2d&&collition2d.gameObject.tag=="bank")
            {
                banksave = collition2d.transform.gameObject;
            }
            Instantiate(banksave);
        }
        else if (Input.GetMouseButton(0))
        {
            banksave.transform.position = w_position;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            banksave.GetComponent<Positioning>().dataset(smallwidth, widthfree,smallheight,heightfree);
        }
    }
    private void AutoAdjustment()
    {
        //二つのbankの大きさを比べてそれぞれに代入
        if (Bank[0].GetComponent<Renderer>().bounds.size.x < Bank[1].GetComponent<Renderer>().bounds.size.x)
        {
            bigwidth = Bank[1].GetComponent<SpriteRenderer>().bounds.size.x;
            bigheight = Bank[1].GetComponent<SpriteRenderer>().bounds.size.y;
        }
        else
        {
            smallwidth = Bank[0].GetComponent<SpriteRenderer>().bounds.size.x;
            smallheight = Bank[0].GetComponent<SpriteRenderer>().bounds.size.y;
        }
        //座標の空き間隔
        widthfree = bigwidth - smallwidth * 2;
        heightfree = bigheight - smallheight * 2;
        Debug.Log("smallwidth," + smallwidth);
        Debug.Log("smallheight," + smallheight);
        Debug.Log("widthfree" + widthfree);
        Debug.Log("heightfree" + heightfree);

    }

    public void saveprefab()
    {
        // Assetへ保存
        UnityEditor.PrefabUtility.CreatePrefab("Assets/Resources/PieceParts/" + name + ".prefab", BankPL);

        // これをしないとMeshが保存されない！
        UnityEditor.AssetDatabase.SaveAssets();
    }
}
