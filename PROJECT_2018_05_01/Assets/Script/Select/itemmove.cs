using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class itemmove : MonoBehaviour {
    public GameObject item;
    public int ID;
    [SerializeField]
    bool start=false;

    // Use this for initialization
    void Start () {
        Debug.Log("ID:"+ID);
	}
	
	// Update is called once per frame
	void Update () {

	}
}
