using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collider : MonoBehaviour {
    public bool hit;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bank"||collision.gameObject.tag=="king")
        {
            hit = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bank" || collision.gameObject.tag == "king")
        {
            hit = false;
        }
    }
}
