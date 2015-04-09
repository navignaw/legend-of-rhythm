using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class Song : MonoBehaviour {
    public static Song currentSong;

    public AudioSource audio; // actual music file
    public string name; // name of song
    public string artist; // name of artist
    public float bpm = 120f; // beats per minute
    public float beatTime; // time duration of a single beat in seconds
    public float songPos = 0.0f; // position in song
    public float offset = 0.0f; // extra time before song begins (due to mp3 metadata)

    public Chart chart;

    double startTick;

    // Use this for initialization
    void Start () {
        beatTime = 60f / bpm;
        startTick = AudioSettings.dspTime;
    }

    // Update is called once per frame
    void Update () {
        songPos = (float)(AudioSettings.dspTime - startTick) * audio.pitch - offset;
        Debug.Log(songPos);
    }

}
