using UnityEngine;
using System.Collections;

public class Tutorial1 : Tutorial {
    public CameraController cameraController;
    GameObject currentMessage;

    // Continue in tutorial
    protected override void ProceedTutorial() {
        switch (phase++) {
            case 0:
                CreateMessage("Welcome to the farm! It's time to collect the songs of the birds.");
                break;

            case 1:
                CreateMessage(0, "This is a quarter note. In most songs, the quarter note lasts for one beat.");
                break;

            case 2:
                charts[0].Reset();
                charts[0].DrawNotes();
                cameraController.Reset();
                CreateMessage("Let's practice! Tap the spacebar once for each tap of my cowbell. Try to follow along with the birds on the fence!");
                break;

            case 3:
                // Play song
                songs[0].PlaySong();
                currentMessage = CreateMessage("Let's practice! Tap the spacebar once for each tap of my cowbell. Try to follow along with the birds on the fence!", -1);
                break;

            case 4:
                Destroy(currentMessage);
                // Proceed if player gets at least 12 goods
                if (charts[0].totalScore >= Score.GOOD_SCORE * 12) {
                    CreateMessage("Nicely done!");
                } else {
                    charts[0].totalScore = 0;
                    CreateMessage("Uh oh! We missed a couple of notes that time. Try again!");
                    phase = 2;
                }
                break;

            case 5:
                break;

            default:
                CompleteTutorial(1);
                break;
        }
    }

}
