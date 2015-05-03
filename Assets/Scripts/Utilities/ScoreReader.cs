using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;


public class ScoreReader : MonoBehaviour {
    public string score;
    StreamReader reader;

    // Use this for initialization
    void Awake () {
        reader = new StreamReader("Assets/Scores/" + score, Encoding.Default);
    }

    // Update is called once per frame
    void Update () {
    }

    public IEnumerable<string> ReadLine() {
        string line;
        using (reader) {
            line = reader.ReadLine();
            while (line != null) {
                yield return line;
                line = reader.ReadLine();
            }

            reader.Close();
        }
    }

}
