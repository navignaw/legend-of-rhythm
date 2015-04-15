using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

[System.Serializable]
public class TimeSignature {
    public int beats; // number of beats per bar
    public NoteType unit; // type of note for one beat
}

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Chart))]
public class Song : MonoBehaviour {
    public static Song currentSong;
    public static bool isPlaying {
        get {
            return (currentSong != null && currentSong._isPlaying);
        }
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
    public Note currentNote;

    double startTick; // initial dspTime for offsetting
    bool _isPlaying = false;

    // Use this for initialization
    void Start () {
        beatTime = 60f / bpm;
        chart = GetComponent<Chart>();
    }

    // Update is called once per frame
    void Update () {
        if (_isPlaying) {
            songPos = (float)(AudioSettings.dspTime - startTick) * audioSource.pitch - offset;
            currentNote = chart.GetCurrentNote(songPos / beatTime - currentBeat);
            currentBeat = songPos / beatTime;
            currentMeasure = Mathf.FloorToInt(currentBeat / timeSignature.beats);

            ReadInput();
        }
    }

    public void PlaySong() {
        audioSource.Play();
        _isPlaying = true;
        startTick = AudioSettings.dspTime;
        currentSong = this;
    }

    void ReadInput() {
        if (Input.GetButtonDown("Hit")) {
            chart.HitNote();
        } else if (Input.GetButtonUp("Hit")) {
            chart.ReleaseNote();
        }
    }

}
