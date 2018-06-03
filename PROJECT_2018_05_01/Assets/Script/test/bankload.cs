using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class bankload : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        DEBUG.Null(Bankstatc.Bank);
        foreach( Vector4 list in Bankstatc.BankList2)
        {
            Vector3 pos = new Vector3(list.x-8, list.y-1.2f, list.z);
            GameObject bank = (GameObject)Resources.Load("prefab/bank" + list.w);
            bank.transform.position = pos;
            bank.GetComponent<Rigidbody2D>().bodyType=RigidbodyType2D.Dynamic;
            Instantiate(bank);
        }
        GameObject rest = (GameObject)Resources.Load("prefab/bank1");
        rest.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        rest = (GameObject)Resources.Load("prefab/bank2");
        rest.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        rest = (GameObject)Resources.Load("prefab/bank3");
        rest.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        rest = (GameObject)Resources.Load("prefab/bank4");
        rest.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;

    }
    // Update is called once per frame
    void Update()
    {

    }
}