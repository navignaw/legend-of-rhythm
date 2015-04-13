using UnityEngine;
using UnityEngine.Audio;
using System.Collections;


[RequireComponent(typeof(AudioSource))]
public class Song : MonoBehaviour {
    public static Song currentSong;

    [System.Serializable]
    public class TimeSignature {
        public int beats; // number of beats per bar
        public NoteType unit; // type of note for one beat
    }

    public AudioSource audioSource; // actual music file
    public string songName; // name of song
    public string artist; // name of artist
    public float bpm = 120f; // beats per minute
    public float beatTime; // time duration of a single beat in seconds
    public float songPos = 0.0f; // position in song in seconds
    public float currentBeat = 0.0f; // position in song in beats
    public int currentMeasure = 0; // current measure of song
    public float offset = 0.0f; // extra time before song begins (due to mp3 metadata)
    public TimeSignature timeSignature;

    public Chart chart;
    public bool isPlaying = false;

    double startTick; // initial dspTime for offsetting

    // Use this for initialization
    void Start () {
        beatTime = 60f / bpm;
        currentSong = this;
    }

    // Update is called once per frame
    void Update () {
        if (isPlaying) {
            songPos = (float)(AudioSettings.dspTime - startTick) * audioSource.pitch - offset;
            currentBeat = songPos / beatTime;
            currentMeasure = Mathf.FloorToInt(currentBeat / timeSignature.beats);
        }
    }

    public void PlaySong() {
        audioSource.Play();
        isPlaying = true;
        startTick = AudioSettings.dspTime;
    }

}
