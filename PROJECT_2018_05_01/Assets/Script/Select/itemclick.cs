using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class itemclick : MonoBehaviour
{
    public GameObject[] xy = new GameObject[2];//座標登録
    public GameObject []Bank = new GameObject[2];//クリック予定先
    [SerializeField] GameObject holditem;        //運搬用オブジェクト

    Vector3 mouse_position; //マウスのポジション
    Vector3 world_position; //マウスポジションをワールド座標に変換する用

    Vector3[] ID = new Vector3[20];
    int []IDBank = new int [20];

    //大きさを獲得する変数
    float bigwidth;     //横の長い幅
    float bigheight;    //縦の長い高さ
    float smallwidth;   //横の短い幅
    float smallheight;  //縦の短い高さ
    float widthfree;    //横の隙間の幅
    float heightfree;   //縦の隙間の長さ
    float halfwidth;    //横の長い幅の半分
    float halfheight;   //縦の長い高さの半分

    bool isClicked=false;   //クリックしている間をtrue
    bool big = false;       //幅高さが大きければtrue


    //-----------------------------------------------------
    //----------------------初期化処理-----------------------
    //-----------------------------------------------------


    // Use this for initialization
    void Start()
    {

        Debug.Log(ID[0]);
        //それぞれのオブジェクトが保有しているオブジェクトから画像を読み取ってUIに反映
        Bank[0].GetComponent<SpriteRenderer>().sprite = Bank[0].GetComponent<bankhold>().bank.GetComponent<SpriteRenderer>().sprite;
        Bank[1].GetComponent<SpriteRenderer>().sprite = Bank[1].GetComponent<bankhold>().bank.GetComponent<SpriteRenderer>().sprite;
        //それぞれの高さと幅を求める
        AutoAdjustment();
        //運搬オブジェクトデータを読み込み
        holditem = GameObject.Find("item");
    }


    //-----------------------------------------------------
    //--------------------フレーム処理-----------------------
    //-----------------------------------------------------


    // Update is called once per frame
    void Update()
    {
        //メソッド呼び出し
        itemhold();
    }


    //-----------------------------------------------------
    //-------------------クリック時の処理--------------------
    //-----------------------------------------------------


    private void itemhold()
    {
        //マウスの座標獲得
        mouse_position = Input.mousePosition;
        //マウス座標をワールド座標に変換
        world_position = Camera.main.ScreenToWorldPoint(mouse_position);
        world_position.z = 10f;

        //クリックした瞬間
        if (Input.GetMouseButtonDown(0))
        {

            //コライダーをつけてclick座標の判定を獲得
            Collider2D collition2d = Physics2D.OverlapPoint(world_position);

            //コライダーに当たった場合でタグがbankの場合
            if (collition2d)
            {
                if (collition2d.gameObject.tag == "uibank")
                {


                    //SpriteRendererを宣言し、略して見やすくする
                    SpriteRenderer bank1 = collition2d.GetComponent<bankhold>().bank.GetComponent<SpriteRenderer>();
                    SpriteRenderer bank2 = Bank[1].GetComponent<bankhold>().bank.GetComponent<SpriteRenderer>();

                    //獲得したコライダーがbank2と同じだった場合
                    if (bank1.bounds.size.x == bank2.bounds.size.x)
                    {
                        //クリック先が大きくない
                        big = false;
                    }
                    else
                    {
                        //クリック先が大きい
                        big = true;
                    }

                    //クリックしてるのでtrue
                    isClicked = true;
                    //クリック先の保有してるオブジェクトにアクセスし、オブジェクトをholditemに保有させる
                    holditem.GetComponent<itemmove>().item = collition2d.GetComponent<bankhold>().bank;
                    //クリック先の画像を読み込み画像を運搬用のitemholdに反映
                    holditem.GetComponent<SpriteRenderer>().sprite = collition2d.GetComponent<SpriteRenderer>().sprite;

                }

                if (collition2d.gameObject.tag == "bank")
                {
                    Debug.Log("デストロイ");
                    int destroyID = collition2d.GetComponent<bank>().IDget();
                    IDBank[destroyID] = 0;
                    ID[destroyID] = new Vector3();
                    Destroy(collition2d.gameObject);
                }
            }
        }

        //クリックしている間
        if (isClicked)
        {

            //ドラッグターゲットをマウスカーソルに合わせて移動
            holditem.transform.position = world_position;
        }

        //クリックしなくなった場合
        if (Input.GetMouseButtonUp(0))
        {
            if (xy[0].transform.position.x < holditem.transform.position.x && holditem.transform.position.x < xy[1].transform.position.x)
            {
                Debug.Log("x0," + xy[0].transform.position.x);
                Debug.Log("x1," + xy[1].transform.position.x);
                if (xy[0].transform.position.y < holditem.transform.position.y&&holditem.transform.position.y<xy[1].transform.position.y)
                {
                    Debug.Log("y0," + xy[0].transform.position.y);
                    Debug.Log("y1," + xy[1].transform.position.y);
                    if (holditem.GetComponent<itemmove>().item != null)
                    {
                        //生成前に運搬用オブジェクトの座標を調整
                        holditem.GetComponent<itemmove>().item.transform.position = Posotion();
                        IDorganize(holditem.GetComponent<itemmove>().item.transform.position);
                        //Bankの生成
                        GameObject Bank = Instantiate(holditem.GetComponent<itemmove>().item);
                    }
                }
            }
            holditem.GetComponent<SpriteRenderer>().sprite = null;
            holditem.GetComponent<itemmove>().item = null;
            isClicked = false;
        }
        DEBUG.debuglog("IDBank",IDBank);
    }


    //-----------------------------------------------------
    //----------------呼び出し先の大きさを比較-----------------
    //-----------------------------------------------------


    private void AutoAdjustment()
    {
        //呼び出し予定先を読み込み略して見やするくする
        SpriteRenderer bank1 = Bank[0].GetComponent<bankhold>().bank.GetComponent<SpriteRenderer>();
        SpriteRenderer bank2 = Bank[1].GetComponent<bankhold>().bank.GetComponent<SpriteRenderer>();

        //二つのbankの大きさを比べてそれぞれに代入
        if (bank1.bounds.size.x < bank2.bounds.size.x)
        {

            bigwidth = bank2.bounds.size.x;     //大きい幅に代入
            bigheight = bank2.bounds.size.y;    //大きい高さに代入
            smallwidth = bank1.bounds.size.x;   //小さい幅に代入
            smallheight = bank1.bounds.size.y;  //小さい高さに代入
        }
        else
        {

            bigwidth = bank1.bounds.size.x;     //大きい幅に代入
            bigheight = bank1.bounds.size.y;    //大きい高さに代入
            smallwidth = bank2.bounds.size.x;   //小さい幅に代入
            smallheight = bank2.bounds.size.y;  //小さい高さに代入
        }

        //座標の空き間隔
        widthfree = bigwidth - smallwidth * 2;      //横幅の隙間を計算
        heightfree = bigheight - smallheight * 2;   //縦幅の隙間を計算
        halfwidth = (smallwidth + widthfree) / 2;   //短い横幅と横の隙間から中間を求める
        halfheight = (smallheight + heightfree) / 2;//短い縦幅と縦の隙間から中間を求める
    }


    //-----------------------------------------------------
    //-------------------出力先の座標調整---------------------
    //-----------------------------------------------------


    private Vector3 Posotion()
    {
        //整数で位置を管理
        int intx = 0;
        int inty = 0;
        //詳細な位置を獲得
        float x = world_position.x;
        float y = world_position.y;

        //x座標が正の数か負の数
        if (x >= 0)
        {

            //x座標から一定の幅を１とした正の計算
            intx = (int)((x + smallwidth / 2) / smallwidth + widthfree);
        }
        else if (x < 0)
        {

            //x座標から一定の幅を１とした正の計算
            intx = (int)((x - smallwidth / 2) / smallwidth + widthfree);
        }

        //y座標が正の数か負の数
        if (y >= 0)
        {

            //y座標から一定の幅を１とした負の計算
            inty = (int)((y + smallheight / 2) / smallheight + heightfree);
        }
        else if (y < 0)
        {

            //y座標から一定の幅を１とした負の計算
            inty = (int)((y - smallheight / 2) / smallheight + heightfree);
        }

        //このオブジェクトが大きかった場合の配置調整
        if (big)
        {

            //オブジェクトが大きかった場合の調整
            return new Vector3(intx * (smallwidth + widthfree) + halfwidth, inty * (smallheight + heightfree) + halfheight, 10);
        }
        else
        {

            //オブジェクトが小さかった場合の調整
            return new Vector3(intx * (smallwidth + widthfree), inty * (smallheight + heightfree), 10);
        }
    }

    private void IDorganize(Vector3 pos)
    {
        for (int i = 0; i < ID.Length; i++)
        {

            if (IDBank[i] == 0)
            {

                if (big)
                {
                    IDBank[i] = 2;
                }
                else
                {
                    IDBank[i] = 1;
                }
                ID[i] = pos;
                holditem.GetComponent<itemmove>().item.GetComponent<bank>().IDset(i);
                break;
            }
        }
    }

}