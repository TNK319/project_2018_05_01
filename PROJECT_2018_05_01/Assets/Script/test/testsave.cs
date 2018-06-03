using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class testsave : MonoBehaviour {
    public string fileName; // 保存するファイル名
    // Use this for initialization
    void Start () {
        WriteCSV("hello");

    }

// Update is called once per frame
void Update () {
		
	}

    // CSVに書き込む処理
    private void WriteCSV(string txt)
    {
        StreamWriter streamWriter;
        FileInfo fileInfo;
        fileInfo = new FileInfo(Application.dataPath + "/" + fileName + ".csv");
        streamWriter = fileInfo.AppendText();
        streamWriter.WriteLine(txt);
        streamWriter.Flush();
        streamWriter.Close();
    }
}
