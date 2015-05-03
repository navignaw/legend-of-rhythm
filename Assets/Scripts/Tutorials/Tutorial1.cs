using UnityEngine;
using System.Collections;

public class Tutorial1 : Tutorial {
    GameObject currentMessage;

    // Continue in tutorial
    protected override void ProceedTutorial() {
        Vector3 pos = new Vector3(0f, 0f, 0f); // position of text message
        switch (phase++) {
            case 0:
                CreateMessage(Vector3.zero, "HELLO FROM THE COW");
                break;


            default:
                CompleteTutorial(1);
                break;
        }
    }

}
