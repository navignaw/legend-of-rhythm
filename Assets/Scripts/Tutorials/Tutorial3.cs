using UnityEngine;
using System.Collections;

public class Tutorial3 : Tutorial {
    public CameraController cameraController;
    GameObject currentMessage;

    // Continue in tutorial
    protected override void ProceedTutorial() {
        switch (phase++) {
            case 0:
                CreateMessage("We've covered all the basic notes. Now let's try something a little more challenging!");
                break;

            case 1:
                CreateMessage(6, "Here is an eighth note. In most songs, it lasts for half a beat, so it needs to be tapped twice as fast!");
                break;

            case 2:
                CreateMessage(7, "The corresponding rest, an eighth rest, looks like this.");
                break;

            case 3:
                CreateMessage("Eighth rests allow for very interesting beats, including syncopation, or rhyhthms that fall on the off-beat.");
                break;

            case 4:
                charts[0].Reset();
                charts[0].DrawNotes();
                cameraController.Reset();
                CreateMessage("Let's practice! Tap the notes and follow the birds on the fence. Remember to hit eighth notes twice as fast.");
                break;

            case 5:
                // Play quarter note song
                songs[0].PlaySong();
                AnimateCowbell(true);
                currentMessage = CreateMessage("Let's practice! Tap the notes and follow the birds on the fence. Remember to hit eighth notes twice as fast.", -1);
                break;

            case 6:
                Destroy(currentMessage);
                AnimateCowbell(false);
                // Proceed if player gets at least 20 goods
                if (charts[0].totalScore >= Score.GOOD_SCORE * 20) {
                    CreateMessage("Good job!");
                } else {
                    charts[0].totalScore = 0;
                    CreateMessage("Uh oh! We missed a couple of notes that time. Try again!");
                    phase = 2;
                }
                break;

            case 7:
                charts[0].Reset();
                charts[0].gameObject.SetActive(false);
                CreateMessage("That's it for now! You're ready to go out and practice on all sorts of birds now.");
                break;

            default:
                CompleteTutorial(3);
                break;
        }
    }

}
