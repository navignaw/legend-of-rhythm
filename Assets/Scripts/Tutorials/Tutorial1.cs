using UnityEngine;
using System.Collections;

public class Tutorial1 : Tutorial {
    public CameraController cameraController;
    GameObject currentMessage;

    // Continue in tutorial
    protected override void ProceedTutorial() {
        switch (phase++) {
            case 0:
                CreateMessage("Welcome! It's time to learn the ways of the songbirds.");
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
                // Play quarter note song
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
                charts[0].Reset();
                charts[0].gameObject.SetActive(false);
                CreateMessage(1, "This is a half note. In most songs, the half note lasts for two beats.");
                break;

            case 6:
                CreateMessage("When you see a half note, press and hold the spacebar for two taps of my cowbell.");
                break;

            case 7:
                charts[1].gameObject.SetActive(true);
                charts[1].Reset();
                charts[1].DrawNotes();
                cameraController.Reset();
                CreateMessage("Ready to begin?");
                break;

            case 8:
                // Play half note song
                songs[1].PlaySong();
                currentMessage = CreateMessage("Remember, press and hold the spacebar on half notes for two taps of my cowbell.", -1);
                break;

            case 9:
                Destroy(currentMessage);
                // Proceed if player gets at least 10 goods and 2 oks
                if (charts[1].totalScore >= Score.GOOD_SCORE * 10 + Score.OK_SCORE * 2) {
                    CreateMessage("Great! You've mastered the half note.");
                } else {
                    charts[1].totalScore = 0;
                    CreateMessage("Uh oh! Remember to hold out half notes for two beats, and tap quarter notes for one. Try again!");
                    phase = 7;
                }
                break;

            case 10:
                charts[1].Reset();
                charts[1].gameObject.SetActive(false);
                CreateMessage(2, "This is a whole note. This note must be played for four whole beats.");
                break;

            case 11:
                charts[2].gameObject.SetActive(true);
                charts[2].Reset();
                charts[2].DrawNotes();
                cameraController.Reset();
                CreateMessage("As a recap: whole notes are 4, half notes are 2, and quarter notes are 1 beat. Ready to begin?");
                break;

            case 12:
                // Play whole note song
                songs[2].PlaySong();
                currentMessage = CreateMessage("As a recap: whole notes are 4, half notes are 2, and quarter notes are 1 beat.", -1);
                break;

            case 13:
                Destroy(currentMessage);
                // Proceed if player gets at least 5 goods and 4 oks
                if (charts[2].totalScore >= Score.GOOD_SCORE * 5 + Score.OK_SCORE * 4) {
                    CreateMessage("Excellent work!");
                } else {
                    charts[2].totalScore = 0;
                    CreateMessage("Uh oh! Remember to hold out whole notes for four beats and half notes for two beats. Try again!");
                    phase = 11;
                }
                break;

            case 14:
                CreateMessage("Now that you've mastered the quarter note, half note, and whole note, it's time to tackle a song! Use the spacebar to tap the rhythms you see!");
                break;

            default:
                CompleteTutorial(1);
                break;
        }
    }

}
