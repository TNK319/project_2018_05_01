﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DEBUG : MonoBehaviour {
    GameObject debug;
    static string log;
    bool oc = true;
    public static void debuglog(string a,float b)
    {
        log += a + ":" + b+"\n";
    }
    public static void debuglog(string a,int []b)
    {
        string blog="[";
        log += a + ";";
        for(int i = 0; i < b.Length; i++)
        {
            blog += b[i]+",";
        }
        log += blog + "]\n";
    }
    public static void debuglog(GameObject name)
    {
        if (name == null)
        {
            log +=
                "OJ:null\n";
        }
        else
        {
            log +=
                "OJ:" + name.name +
                "\nx:" + name.transform.position.x +
                "\ny:" + name.transform.position.y +
                "\n";
        }
    }
    public static void debuglog(string OJname,GameObject name)
    {
        if (name == null)
        {
            log +=
                "OJ:"+OJname+"\n";
        }
        else
        {
            log +=
                "OJ:" + OJname +
                "\nx:" + name.transform.position.x +
                "\ny:" + name.transform.position.y +
                "\n";
        }
    }
    public static void debuglog(string a,bool name)
    {
        log += "fg:" + a + ":" + name + "\n";
    }
    public static void debuglog(string a, string b)
    {
        log += "st;" + a + ":" + b + "\n";
    }
    private void opencclose(string a="KyeCode.A")
    {
        if (Input.GetKeyDown(a))
        {

        }
    }

    // Use this for initialization
    void Start () {
        this.debug=GameObject.Find("debug");
        log= "デバック画面\n";
    }
	
	// Update is called once per frame
	void Update () {
        debug.GetComponent<Text>().text =
            log;
        log = "";
    }
}