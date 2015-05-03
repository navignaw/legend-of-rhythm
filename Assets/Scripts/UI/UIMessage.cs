using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIMessage : MonoBehaviour {
    public Text message;
    public GameObject continueText;
    public string messageText
    {
        get { return message.text; }
        set { message.text = value; }
    }
    public bool proceedTutorialOnClose = false;

    /** How long message will last before fading.
     *  0 = player must click to continue
     * -1 = lasts forever; creator is responsible for destroying object
     */
    public float duration = 0;

    // Use this for initialization
    void Start () {
        if (duration == 0) {
            continueText.SetActive(true);
        } else if (duration > 0) {
            Invoke("OnCloseMessage", duration);
        }
    }

    // Update is called once per frame
    void Update () {
    }

    public void CloseMessage() {
        if (duration == 0) {
            OnCloseMessage();
        }
    }

    void OnCloseMessage() {
        Destroy(gameObject); // TODO: fade before killing message
        if (proceedTutorialOnClose) {
            Tutorial.Proceed();
        }
    }
}
