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
    public float offsetEnd = 0.0f; // extra time after song ends
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

            ReadInput();
        }
    }

    public void PlaySong() {
        audioSource.Play();
        startTick = AudioSettings.dspTime;
        SetAnimatorSpeed();
        currentSong = this;
        Invoke("EndSong", audioSource.clip.length + offsetEnd);
    }

    public void EndSong() {
        Debug.Log("song ended");
        audioSource.Stop();
        currentNote = null;
        currentMeasure = 0;
        currentBeat = 0f;

        if (Tutorial.CurrentTutorial) {
            Tutorial.Proceed();
        } else {
            // TODO: show scores
        }
        currentSong = null;
    }

    void ReadInput() {
        if (Input.GetButtonDown("Hit")) {
            chart.HitNote();
        } else if (Input.GetButtonUp("Hit")) {
            chart.ReleaseNote();
        }
    }

    void SetAnimatorSpeed() {
        Animator anim = Tutorial.CurrentTutorial.cow.GetComponent<Animator>();
        if (anim) {
            anim.speed = beatTime;
        }
    }

}
