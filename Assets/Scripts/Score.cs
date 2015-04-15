using UnityEngine;
using System.Collections;

public class Score {
    public string text;
    public int value = 0;

    // Delay thresholds for hitting notes
    // TODO: finetune
    const float ALMOST_THRESHOLD = -0.05f;
    const float PERFECT_THRESHOLD = 0.25f;
    const float WONDERFUL_THRESHOLD = 0.3f;
    const float GOOD_THRESHOLD = 0.4f;
    const float POOR_THRESHOLD = 0.5f;

    // Score for hitting notes
    const int PERFECT_SCORE = 5;
    const int WONDERFUL_SCORE = 4;
    const int GOOD_SCORE = 3;
    const int ALMOST_SCORE = 2;
    const int POOR_SCORE = 1;
    const int MISS_SCORE = 0;

    private static Score perfect = new Score("PERFECT", PERFECT_SCORE);
    private static Score wonderful = new Score("WONDERFUL", WONDERFUL_SCORE);
    private static Score good = new Score("GOOD", GOOD_SCORE);
    private static Score almost = new Score("ALMOST", ALMOST_SCORE);
    private static Score poor = new Score("POOR", POOR_SCORE);
    private static Score miss = new Score("MISS", MISS_SCORE);

    public static Score Perfect {
        get { return perfect; }
    }

    public static Score Wonderful {
        get { return wonderful; }
    }

    public static Score Good {
        get { return good; }
    }

    public static Score Almost {
        get { return almost; }
    }

    public static Score Poor {
        get { return poor; }
    }

    public static Score Miss {
        get { return miss; }
    }

    Vector3 target;

    // Update score based on timing of hit
    public static Score ComputeScore(float delay) {
        if (delay < ALMOST_THRESHOLD) {
            return Almost;
        } else if (delay < PERFECT_THRESHOLD) {
            return Perfect;
        } else if (delay < WONDERFUL_THRESHOLD) {
            return Wonderful;
        } else if (delay < GOOD_THRESHOLD) {
            return Good;
        } else if (delay < POOR_THRESHOLD) {
            return Poor;
        }
        return Miss;
    }

    // Constructor
    public Score(string text, int value) {
        this.text = text;
        this.value = value;
    }

    public void ShowText(Vector3 pos) {
        GameObject textObject = new GameObject();
        textObject.AddComponent<GUIText>();
        textObject.GetComponent<GUIText>().text = text;
        textObject.GetComponent<GUIText>().fontSize = 20;
        textObject.GetComponent<GUIText>().color = Color.black;
        textObject.AddComponent<ScoreText>();
        textObject.GetComponent<ScoreText>().target = pos;
        Object.Destroy(textObject, 1f); // destroy after a second
    }

}
