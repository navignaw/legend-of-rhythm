using UnityEngine;
using System.Collections;

public class Tutorial2 : Tutorial {
    public CameraController cameraController;
    GameObject currentMessage;

    // Continue in tutorial
    protected override void ProceedTutorial() {
        switch (phase++) {
            case 0:
                CreateMessage("Next, we will be looking at rests, which indicate pauses or silence in songs.");
                break;

            case 1:
                CreateMessage(3, "This is a quarter rest. Like a quarter note, it lasts for one beat.");
                break;

            case 2:
                CreateMessage(4, "This is a half rest. Like a half note, it lasts for two beats.");
                break;

            case 3:
                CreateMessage(5, "Finally, the whole rest lasts for 4 whole beats (one measure).");
                break;

            case 4:
                charts[0].Reset();
                charts[0].DrawNotes();
                cameraController.Reset();
                CreateMessage("Let's practice! Tap the notes and follow the birds on the fence, but don't hit the rests or they will get angry.");
                break;

            case 5:
                // Play quarter note song
                songs[0].PlaySong();
                AnimateCowbell(true);
                currentMessage = CreateMessage("Let's practice! Tap the notes and follow the birds on the fence, but don't hit the rests or they will get angry.", -1);
                break;

            case 6:
                Destroy(currentMessage);
                AnimateCowbell(false);
                // Proceed if player gets at least 12 goods
                if (charts[0].totalScore >= Score.GOOD_SCORE * 12) {
                    CreateMessage("Nicely done!");
                } else {
                    charts[0].totalScore = 0;
                    CreateMessage("Uh oh! We missed a couple of notes that time. Try again!");
                    phase = 2;
                }
                break;

            case 7:
                charts[0].Reset();
                charts[0].gameObject.SetActive(false);
                CreateMessage("Let's try another song with notes and rests! Use the spacebar to tap the rhythms you see.");
                break;

            default:
                CompleteTutorial(2);
                break;
        }
    }

}
