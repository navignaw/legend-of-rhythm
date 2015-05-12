using UnityEngine;
using System.Collections;

public class LoadSong : Tutorial {
    public static int SongToLoad = 0;
    GameObject currentMessage;

    void Awake() {
        currentMessage = CreateMessage("Loading song...", -1);
        CurrentTutorial = this;
    }

    // Continue in tutorial
    protected override void ProceedTutorial() {
        Song song = songs[SongToLoad];
        Chart chart = charts[SongToLoad];
        switch (phase++) {
            case 0:
                chart.gameObject.SetActive(true);
                chart.DrawNotes();
                song.PlaySong();
                Destroy(currentMessage);
                CreateMessage("Now playing:\n" + song.songName + " by " + song.artist, 5);
                break;

            case 1:
                break;

            // Song completed
            case 2:
                CreateMessage("Song complete!\nYour score: " + chart.totalScore.ToString());
                break;

            default:
                Application.LoadLevel("levelSelect");
                break;

        }
    }

}
