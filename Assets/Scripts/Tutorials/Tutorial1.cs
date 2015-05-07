using UnityEngine;
using System.Collections;

public class Tutorial1 : Tutorial {
    GameObject currentMessage;

    // Continue in tutorial
    protected override void ProceedTutorial() {
        Vector3 pos = new Vector3(0f, -1f, 0f); // position of text message
        switch (phase++) {
            case 0:
                CreateMessage("Hello from your friendly cow!");
                break;

            case 1:
                CreateMessage("pls halp me collect the birbs");
                break;

            case 2:
                songs[0].PlaySong();
                break;

            default:
                CompleteTutorial(1);
                break;
        }
    }

}
