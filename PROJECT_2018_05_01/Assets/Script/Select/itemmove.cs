using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class itemmove : MonoBehaviour {
    public GameObject item;
    [SerializeField]
    bool start=false;

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (start)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = item.GetComponent<SpriteRenderer>().sprite;
            start = false;
        }
	}
    public void set()
    {
        start = true;
    }
}
