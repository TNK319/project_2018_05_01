using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class save : MonoBehaviour {
    public void Onclick()
    {
        GameObject maneger;
        maneger = GameObject.Find("GameManager");
        maneger.GetComponent<itemclick>().banksave();
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
