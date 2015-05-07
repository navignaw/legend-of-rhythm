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
    void Awake () {
        CurrentTutorial = this; // there should only be one tutorial per scene
    }

    void Start () {
        WaitAndProceed(1f);
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
            case 1:
                LoadSong.SongToLoad = 0; // TODO: play tutorial song
                levelToLoad = "game";
                break;
            /*case 2:
                levelToLoad = "story3";
                break;*/
            default:
                levelToLoad = "titleScreen";
                break;
        }
        Application.LoadLevel(levelToLoad);
    }

    // Create text message
    protected GameObject CreateMessage(int imageSprite, string text, float duration) {
        GameObject message = Instantiate(messagePrefab) as GameObject;
        message.transform.SetParent(transform, false);
        message.transform.localPosition = cow.localPosition + messageOffset;
        UIMessage uiMessage = message.GetComponent<UIMessage>();
        uiMessage.messageText = text;
        uiMessage.duration = duration;
        uiMessage.proceedTutorialOnClose = (duration >= 0);

        // Show image and displace text
        if (imageSprite >= 0) {
            uiMessage.image.enabled = true;
            uiMessage.image.sprite = uiMessage.sprites[imageSprite];
            if (imageSprite == 2) {
                Vector2 newHeight = (uiMessage.image.transform as RectTransform).sizeDelta;
                newHeight.y *= 0.5f;
                (uiMessage.image.transform as RectTransform).sizeDelta = newHeight;
            }

            float delta = (uiMessage.image.transform as RectTransform).sizeDelta.x * 1.5f;
            Vector3 newPos = uiMessage.message.transform.position;
            newPos.x += delta;
            uiMessage.message.transform.position = newPos;
            Vector2 newSize = (uiMessage.message.transform as RectTransform).sizeDelta;
            newSize.x -= delta;
            (uiMessage.message.transform as RectTransform).sizeDelta = newSize;
        }
        return message;
    }
    protected GameObject CreateMessage(int imageSprite, string text) {
        return CreateMessage(imageSprite, text, 0f);
    }
    protected GameObject CreateMessage(string text, float duration) {
        return CreateMessage(-1, text, duration);
    }
    protected GameObject CreateMessage(string text) {
        return CreateMessage(-1, text, 0f);
    }

}
