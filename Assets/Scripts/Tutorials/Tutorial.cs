using UnityEngine;
using System.Collections;

public abstract class Tutorial : MonoBehaviour {
    public static Tutorial CurrentTutorial;
    public GameObject messagePrefab;
    public Transform cow;
    public Song[] songs;
    public Chart[] charts;
    public Vector3 messageOffset = Vector3.zero;
    protected int phase = 0;

    public static void Proceed() {
        CurrentTutorial.ProceedTutorial();
    }

    // Use this for initialization
    void Start () {
        CurrentTutorial = this; // there should only be one tutorial per scene
        ProceedTutorial();
    }

    // Update is called once per frame
    void Update () {

    }

    protected abstract void ProceedTutorial();

    protected void WaitAndProceed(float t) {
        Invoke("ProceedTutorial", t);
    }

    protected void CompleteTutorial(int level) {
        if (PlayerPrefs.GetInt("levelUnlocked", 0) < level) {
            PlayerPrefs.SetInt("levelUnlocked", level);
        }
        CurrentTutorial = null;
        string levelToLoad;
        switch (level) {
            /*case 1:
                levelToLoad = "story2";
                break;
            case 2:
                levelToLoad = "story3";
                break;*/
            default:
                levelToLoad = "title";
                break;
        }
        Application.LoadLevel(levelToLoad);
    }

    // Create text message
    protected GameObject CreateMessage(Vector3 offset, string text, float duration) {
        GameObject message = Instantiate(messagePrefab) as GameObject;
        message.transform.SetParent(transform, false);
        message.transform.localPosition = cow.localPosition + messageOffset;
        message.GetComponent<UIMessage>().messageText = text;
        message.GetComponent<UIMessage>().duration = duration;
        message.GetComponent<UIMessage>().proceedTutorialOnClose = (duration >= 0);
        return message;
    }
    protected GameObject CreateMessage(string text, float duration) {
        return CreateMessage(Vector3.zero, text, duration);
    }
    protected GameObject CreateMessage(string text) {
        return CreateMessage(Vector3.zero, text, 0);
    }

}
