using UnityEngine;
using System.Collections;

public class LoadSong : Tutorial {
    public static int SongToLoad = 0;
    GameObject currentMessage;

    // Continue in tutorial
    protected override void ProceedTutorial() {
        Song song = songs[SongToLoad];
        Chart chart = charts[SongToLoad];
        chart.gameObject.SetActive(true);
        chart.DrawNotes();
        song.PlaySong();
        currentMessage = CreateMessage("Now playing: " + song.songName + " by " + song.artist, -1);
    }

}
