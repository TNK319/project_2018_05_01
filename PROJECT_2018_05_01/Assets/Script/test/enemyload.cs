using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class enemyload : MonoBehaviour {

	// Use this for initialization
	void Start () {
        TextAsset csvFile; // CSVファイル
        csvFile = Resources.Load("Data/enemy00") as TextAsset; /* Resouces/CSV下のCSV読み込み */
        StringReader reader = new StringReader(csvFile.text);
        int count = reader.Peek();
        while (reader.Peek() > -1)
        {
            string line = reader.ReadLine();
            string[] values = line.Split(',');
            if (values[1] != "0")
            {
                GameObject Bank = (GameObject)Resources.Load("prefab/bank" + values[1]);
                Bank.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                Bank.transform.position = new Vector3(float.Parse(values[2]) + 15, float.Parse(values[3]), float.Parse(values[4]));
                Instantiate(Bank);
            }
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
    void Update () {
		
	}
}
