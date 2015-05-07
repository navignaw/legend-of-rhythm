using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Chart))]
public class Song : MonoBehaviour {
    public static Song currentSong;
    public static bool isPlaying {
        get {
            return (currentSong != null && currentSong.audioSource.isPlaying);
        }
    }

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

    AudioSource audioSource; // actual music file
    double startTick; // initial dspTime for offsetting

    // Use this for initialization
    void Awake () {
        beatTime = 60f / bpm;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update () {
        if (audioSource.isPlaying) {
            songPos = (float)(AudioSettings.dspTime - startTick) * audioSource.pitch - offset;
            currentNote = chart.GetCurrentNote(songPos / beatTime - currentBeat);
            currentBeat = songPos / beatTime;
            currentMeasure = Mathf.FloorToInt(currentBeat / timeSignature.beats);

            // Check if we've reached end of song as determined by notes
            if (currentMeasure >= chart.bars.Count - 1) {
                EndSong();
            }

            ReadInput();
        }
    }

    public void PlaySong() {
        audioSource.Play();
        startTick = AudioSettings.dspTime;
        currentSong = this;
    }

    public void EndSong() {
        audioSource.Stop();
        // TODO: show scores
        currentSong = null;
    }

    void ReadInput() {
        if (Input.GetButtonDown("Hit")) {
            chart.HitNote();
        } else if (Input.GetButtonUp("Hit")) {
            chart.ReleaseNote();
        }
    }

}
