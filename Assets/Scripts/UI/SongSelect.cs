using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SongSelect : MonoBehaviour {
    public int currentSong = 0;
    List<GameObject> songs = new List<GameObject>();

    // Use this for initialization
    void Start () {
        foreach (Transform child in transform) {
            songs.Add(child.gameObject);
        }
    }

    // Update is called once per frame
    void Update () {

    }

    public void PrevSong() {
        songs[currentSong].SetActive(false);
        currentSong = (currentSong - 1 + songs.Count) % songs.Count;
        songs[currentSong].SetActive(true);
    }

    public void NextSong() {
        songs[currentSong].SetActive(false);
        currentSong = (currentSong + 1) % songs.Count;
        songs[currentSong].SetActive(true);
    }

    public void PlaySong() {
        LoadSong.SongToLoad = currentSong;
        LoadSong.ReturnToLevel = "songSelect";
        Application.LoadLevel("game");
    }
}
