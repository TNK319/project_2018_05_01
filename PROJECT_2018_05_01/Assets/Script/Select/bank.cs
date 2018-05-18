using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bank : MonoBehaviour {
    [SerializeField] int ID;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}
    public void IDset(int id)
    {
        ID = id;
    }
    public int IDget()
    {
        return ID;
    }
}
