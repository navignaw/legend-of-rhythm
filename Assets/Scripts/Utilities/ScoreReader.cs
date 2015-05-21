using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;


public class ScoreReader : MonoBehaviour {
    public string score;
    string[] lines;

    // Use this for initialization
    void Awake () {
        TextAsset textAsset = Resources.Load("Scores/" + score) as TextAsset;
        if (textAsset == null) {
            Debug.Log("failed to load score: " + score);
            return;
        }
        lines = textAsset.text.Split("\n"[0]);
    }

    // Update is called once per frame
    void Update () {
    }

    public IEnumerable<char> ReadNote() {
        foreach (string line in lines) {
            foreach (char c in line) {
                if (!char.IsWhiteSpace(c)) {
                    yield return c;
                }
            }
        }
    }

}
