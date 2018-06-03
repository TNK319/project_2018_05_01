using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class itemclick : MonoBehaviour
{
    public GameObject test;
    public GameObject[] xy = new GameObject[2];　//座標登録
    public GameObject []Bank = new GameObject[2];//クリック予定先
    [SerializeField] GameObject holditem;        //運搬用オブジェクト
    [SerializeField] GameObject Translucent;     //表示先を参照するよう

    Vector3 mouse_position; //マウスのポジション
    Vector3 world_position; //マウスポジションをワールド座標に変換する用

    Vector3[] ID = new Vector3[20];
    int []IDBank = new int [20];
    int IDhold=0;

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
    bool being = false;
    [SerializeField]
    bool king = false;
    [SerializeField]
    bool isking = false;
    //-----------------------------------------------------
    //----------------------初期化処理-----------------------
    //-----------------------------------------------------


    // Use this for initialization
    void Start()
    {
        //それぞれのオブジェクトが保有しているオブジェクトから画像を読み取ってコライダーサイズを調整に反映
        Bank[0].GetComponent<BoxCollider2D>().size = new Vector2(Bank[0].GetComponent<bankhold>().bank.GetComponent<SpriteRenderer>().bounds.size.x, Bank[0].GetComponent<bankhold>().bank.GetComponent<SpriteRenderer>().bounds.size.y);
        Bank[1].GetComponent<BoxCollider2D>().size = new Vector2(Bank[1].GetComponent<bankhold>().bank.GetComponent<SpriteRenderer>().bounds.size.x, Bank[1].GetComponent<bankhold>().bank.GetComponent<SpriteRenderer>().bounds.size.y);
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
        DEBUG.Null(Bankstatc.Bank);
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
        world_position.z = 0.0f;

        //クリックした瞬間
        if (Input.GetMouseButtonDown(0))
        {

            //コライダーをつけてclick座標の判定を獲得
            Collider2D collition2d = Physics2D.OverlapPoint(world_position);

            //コライダーに当たった場合でタグがbankの場合
            if (collition2d)
            {
                if (collition2d.gameObject.tag == "uibank" || collition2d.gameObject.tag == "uiking"&&!isking)
                {


                    //SpriteRendererを宣言し、略して見やすくする
                    SpriteRenderer bank1 = collition2d.GetComponent<bankhold>().bank.GetComponent<SpriteRenderer>();
                    SpriteRenderer bank2 = Bank[1].GetComponent<bankhold>().bank.GetComponent<SpriteRenderer>();

                    //獲得したコライダーがbank2と同じだった場合
                    if (bank1.bounds.size.x == bank2.bounds.size.x)
                    {
                        //クリック先が大きくない
                        big = false;
                        Translucent.GetComponent<SpriteRenderer>().color = new Vector4(1.0f, 1.0f, 1.0f, 0.5f);
                    }
                    else
                    {
                        //クリック先が大きい
                        big = true;
                        Translucent.GetComponent<SpriteRenderer>().color = new Vector4(1.0f, 1.0f, 1.0f, 0.5f);
                    }

                    if (collition2d.gameObject.tag == "uiking")
                    {
                        king = true;
                    }
                    //識別ID獲得
                    IDhold = collition2d.GetComponent<bankhold>().bankID;
                    //生成座標先の画像反映
                    Translucent.GetComponent<SpriteRenderer>().sprite = collition2d.GetComponent<SpriteRenderer>().sprite;
                    Translucent.GetComponent<BoxCollider2D>().size = new Vector2(collition2d.GetComponent<bankhold>().bank.GetComponent<SpriteRenderer>().bounds.size.x, collition2d.GetComponent<bankhold>().bank.GetComponent<SpriteRenderer>().bounds.size.y);
                    //クリックしてるのでtrue
                    isClicked = true;
                    //クリック先の保有してるオブジェクトにアクセスし、オブジェクトをholditemに保有させる
                    holditem.GetComponent<itemmove>().item = collition2d.GetComponent<bankhold>().bank;
                    //クリック先の画像を読み込み画像を運搬用のitemholdに反映
                    holditem.GetComponent<SpriteRenderer>().sprite = collition2d.GetComponent<SpriteRenderer>().sprite;

                }
                if (collition2d.gameObject.tag == "bank"||collition2d.gameObject.tag=="king")
                {
                    if (collition2d.gameObject.tag == "king")
                    {
                        isking = false;
                    }
                    king = false;
                    Debug.Log("デストロイ");
                    int destroyID = collition2d.GetComponent<bank>().IDget();
                    IDBank[destroyID] = 0;
                    ID[destroyID] = new Vector3();
                    Bankstatc.Bank[destroyID] = null;
                    Bankstatc.BankList[destroyID] = null;
                    Destroy(collition2d.gameObject);
                }
            }
        }

        //クリックしている間
        if (isClicked)
        {

            //ドラッグターゲットをマウスカーソルに合わせて移動
            holditem.transform.position = world_position;
            Translucent.transform.position = Posotion();

            if (xy[0].transform.position.x < Translucent.transform.position.x && Translucent.transform.position.x < xy[1].transform.position.x)
            {
                //コライダーが当たってる場合
                
                //範囲制限y座標
                if (xy[0].transform.position.y < Translucent.transform.position.y && Translucent.transform.position.y < xy[1].transform.position.y)
                {
                    if (Translucent.GetComponent<collider>().hit==false)
                    {
                        being = true;
                        Translucent.GetComponent<SpriteRenderer>().color = new Vector4(1.0f, 1.0f, 1.0f, 0.5f);
                    }
                    else
                    {
                        being = false;
                        Translucent.GetComponent<SpriteRenderer>().color = new Vector4(1.0f, 0.5f, 0.5f, 0.5f);
                    }
                }
                else
                {
                    being = false;
                    Translucent.GetComponent<SpriteRenderer>().color = new Vector4(1.0f, 0.5f, 0.5f, 0.5f);
                }
            }
            else
            {
                being = false;
                Translucent.GetComponent<SpriteRenderer>().color = new Vector4(1.0f, 0.5f, 0.5f, 0.5f);
            }
        }

        //クリックしなくなった場合
        if (Input.GetMouseButtonUp(0))
        {

            if (holditem.GetComponent<itemmove>().item != null && being)
            {
                if (isking==false&&king||!king)
                {
                    //生成前に運搬用オブジェクトの座標を調整
                    holditem.GetComponent<itemmove>().item.transform.position = Posotion();
                    IDorganize(holditem.GetComponent<itemmove>().item.transform.position);
                    //Bankの生成
                    GameObject Bank = Instantiate(holditem.GetComponent<itemmove>().item);
                    if (king)
                    {
                        king = false;
                        isking = true;
                    }
                }
            }

            holditem.GetComponent<SpriteRenderer>().sprite = null;
            holditem.GetComponent<itemmove>().item = null;
            Translucent.GetComponent<SpriteRenderer>().sprite = null;
            isClicked = false;
            king = false;
        }
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
        float width = 0;
        float height = 0;
        if (big)
        {
            width = smallwidth + (widthfree);
            height = smallheight + (heightfree);

            if (x >= 0)
            {

                //x座標から一定の幅を１とした正の計算
                intx = (int)((x + width / 2) / (width));
            }
            else if (x < 0)
            {

                //x座標から一定の幅を１とした正の計算
                intx = (int)((x - width / 2) / width);
            }

            //y座標が正の数か負の数
            if (y >= 0)
            {

                //y座標から一定の幅を１とした負の計算
                inty = (int)((y + height / 2) / height);
            }
            else if (y < 0)
            {

                //y座標から一定の幅を１とした負の計算
                inty = (int)((y - height / 2) / height);
            }
        }
        else
        {
            width = smallwidth + (widthfree);
            height = smallheight + (heightfree);
            if (x >= 0)
            {

                //x座標から一定の幅を１とした正の計算
                intx = (int)(((x+width/2) + width / 2) / width);
            }
            else if (x < 0)
            {

                //x座標から一定の幅を１とした正の計算
                intx = (int)(((x+width/2) - width / 2) / width);
            }

            //y座標が正の数か負の数
            if (y >= 0)
            {

                //y座標から一定の幅を１とした負の計算
                inty = (int)(((y+height/2) + height / 2) / height);
            }
            else if (y < 0)
            {

                //y座標から一定の幅を１とした負の計算
                inty = (int)(((y+height/2) - height / 2) / height);
            }
        }
        //x座標が正の数か負の数

        //大きかった場合
        if (big)
        {
            return new Vector3(intx * width, inty * height, 0);
        }
        else
        {
            return new Vector3(intx * width-width/2, inty * height-height/2, 0);
        }
    }
    

    private void IDorganize(Vector3 pos)
    {
        //Debug.Log(IDBank[])
        for (int i = 0; i < ID.Length; i++)
        {

            if (IDBank[i] == 0)
            {
                IDBank[i] = IDhold;
                ID[i] = pos;
                holditem.GetComponent<itemmove>().item.GetComponent<bank>().IDset(i);
                Bankstatc.Bank[i] = (GameObject)Resources.Load("prefab/bank" + IDBank[i]);
                Bankstatc.Bank[i].transform.position = pos;
                Bankstatc.BankList.Add((GameObject)Resources.Load("prefab/bank" + IDBank[i]));
                Bankstatc.BankList[i].transform.position = pos;
                break;
            }
        }
        Bankstatc.BankList2.Add(new Vector4( pos.x, pos.y, pos.z, IDhold));
    }
    public void banksave()
    {
        Debug.Log("書き出し");
        StreamWriter sw = new StreamWriter("./Assets/Resources/Data/BankData.txt", false, System.Text.Encoding.UTF8); //true=追記 false=上書き
        string bankdata="";
        for (int i=0;i<ID.Length;i++)
        {
            bankdata +=  i + "," + IDBank[i] + "," + ID[i].x + "," + ID[i].y + "," + ID[i].z;
            if (ID.Length - 1 != i)
            {
                bankdata += "\n";
            }
        }
        sw.WriteLine(bankdata);
        sw.Flush();
        sw.Close();
        SceneManager.LoadScene("play");
    }
}