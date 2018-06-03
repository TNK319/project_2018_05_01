using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bank : MonoBehaviour {
    public float hp=0;
    public float hpmax=10;
    public float damage=0;
    [SerializeField] int ID;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        damage = 1.0f - (hp / hpmax);
        gameObject.GetComponent<SpriteRenderer>().color = new Vector4(1.0f, damage, damage,1.0f);
        if (hp >= hpmax)
        {
            Destroy(gameObject);
        }
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
