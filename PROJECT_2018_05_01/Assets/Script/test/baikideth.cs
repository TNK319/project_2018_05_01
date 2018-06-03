using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baikideth : MonoBehaviour {
    float time = 0;
    float dethtime = 5.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        if (time > dethtime)
        {
            Destroy(gameObject);
        }
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bank"||collision.gameObject.tag=="king")
        {
            collision.GetComponent<bank>().hp++;
            Destroy(gameObject);
        }
    }
}
