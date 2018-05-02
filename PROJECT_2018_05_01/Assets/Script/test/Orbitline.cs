using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbitline : MonoBehaviour {
    public int ID;
    GameObject Cannon;
	// Use this for initialization
	void Start () {
        Cannon = GameObject.Find("Cannon");
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.position=Cannon.GetComponent<cannon>().getpos(ID);
	}
}
