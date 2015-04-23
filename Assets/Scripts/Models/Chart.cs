﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Song))]
public class Chart : MonoBehaviour {
    public Vector3 startingPos;
    public float barDisplacement = 1f; // spacing after each bar
    public int totalScore = 0;

    const float NEXT_NOTE_THRESHOLD = 0.15f; // threshold before we register a hit as the next note

    public List<NoteType> noteTypes; // list of note types
    public List<Note> notes; // list of note objects (attached to birb prefab)
    public List<GameObject> bars; // list of bars
    Song song;

    public GameObject PrefabEighthNote;
    public GameObject PrefabQuarterNote;
    public GameObject PrefabHalfNote;
    public GameObject PrefabWholeNote;
    public GameObject PrefabDottedEighthNote;
    public GameObject PrefabDottedQuarterNote;
    public GameObject PrefabDottedHalfNote;
    public GameObject PrefabDottedWholeNote;
    public GameObject PrefabBarLine;

    // For keeping track of current note:
    Note currentNote; // currently held note
    float elapsedBeat = 0.0f; // how much of current note has elapsed
    int lastNoteIndex = 0;

    // TODO(Roger): parse string or text file to get NoteTypes
    void AddTestNotes() {
        noteTypes = new List<NoteType>();
        for (int i = 0; i < 18; i++) {
            noteTypes.Add(NoteType.QUARTER);
            noteTypes.Add(NoteType.QUARTER);
            noteTypes.Add(NoteType.QUARTER);
            noteTypes.Add(NoteType.QUARTER);
            noteTypes.Add(NoteType.QUARTER);
            noteTypes.Add(NoteType.QUARTER);
            noteTypes.Add(NoteType.HALF);
            noteTypes.Add(NoteType.QUARTER);
            noteTypes.Add(NoteType.QUARTER);
            noteTypes.Add(NoteType.HALF);
            noteTypes.Add(NoteType.WHOLE);
        }
    }

    // Use this for initialization
    void Start () {
        song = GetComponent<Song>();
        AddTestNotes();
        DrawNotes();
        song.PlaySong();
    }

    // Update is called once per frame
    void Update () {
    }


    // Instantiate prefabs for each note in note array
    void DrawNotes()
    {
        float beatCounter = 0.0f;
        float currentBeat = 0.0f;
        float currentPos = 0.0f; // x position of last sprite

        notes = new List<Note>();
        bars = new List<GameObject>();

        // TODO: draw time signature, etc.
        Vector3 pos = new Vector3(currentPos, 0, 0);
        GameObject bar = Instantiate(PrefabBarLine, pos, Quaternion.identity) as GameObject;
        bar.transform.parent = gameObject.transform;
        bars.Add(bar);
        currentPos += barDisplacement;

        foreach (NoteType noteType in noteTypes)
        {
            // Instantiate sprite
            // TODO: adjust y position based on row on staff
            pos.x = currentPos;
            GameObject birb = Instantiate(GetNotePrefab(noteType), pos, Quaternion.identity) as GameObject;
            birb.transform.parent = gameObject.transform;
            Note note = birb.GetComponent<Note>();
            notes.Add(note);

            // Update beat and position
            currentPos += note.displacement;
            pos.x = currentPos;
            currentBeat = note.beatValue * song.timeSignature.multiplier;
            beatCounter += currentBeat;

            if (beatCounter >= song.timeSignature.beats) {
                // Add bar line
                bar = Instantiate(PrefabBarLine, pos, Quaternion.identity) as GameObject;
                bar.transform.parent = gameObject.transform;
                bars.Add(bar);
                currentPos += barDisplacement;
                beatCounter %= song.timeSignature.beats;
            }
        }

        // TODO: Add double bar
        pos.x += 0.2f;
        bar = Instantiate(PrefabBarLine, pos, Quaternion.identity) as GameObject;
        bar.transform.parent = gameObject.transform;
        bars.Add(bar);
    }

    // Return appropriate prefab based on note type
    GameObject GetNotePrefab(NoteType type) {
        switch (type) {
            case NoteType.EIGHTH:
                return PrefabEighthNote;
            case NoteType.QUARTER:
                return PrefabQuarterNote;
            case NoteType.HALF:
                return PrefabHalfNote;
            case NoteType.WHOLE:
                return PrefabWholeNote;
            case NoteType.DOTTED_EIGHTH:
                return PrefabDottedEighthNote;
            case NoteType.DOTTED_QUARTER:
                return PrefabDottedQuarterNote;
            case NoteType.DOTTED_HALF:
                return PrefabDottedHalfNote;
            case NoteType.DOTTED_WHOLE:
                return PrefabDottedWholeNote;
        }

        Debug.Log("invalid note type!");
        return null;
    }

    // Return currently playing note in song
    public Note GetCurrentNote(float deltaBeat) {
        elapsedBeat += deltaBeat;
        float beatValue = notes[lastNoteIndex].beatValue * song.timeSignature.multiplier;

        // Check if song beat has moved on to next note
        if (elapsedBeat >= beatValue && lastNoteIndex < notes.Count - 1) {
            if (currentNote != null && currentNote == notes[lastNoteIndex]) {
                ReleaseNote(); // release held note automatically
            }

            elapsedBeat -= beatValue;
            notes[lastNoteIndex].AnimateBeat(false);
            notes[++lastNoteIndex].AnimateBeat(true);

        // Otherwise, check to miss an unplayed note
        } else if (elapsedBeat > Score.MISS_THRESHOLD) {
            if (!notes[lastNoteIndex].played) {
                notes[lastNoteIndex].AnimateHit(Score.Miss);
            }
        }
        return notes[lastNoteIndex];
    }

    // On button press, hit current note
    public void HitNote() {
        float delay = elapsedBeat;
        float beatValue = notes[lastNoteIndex].beatValue * song.timeSignature.multiplier;

        // If we're really close to next note, hit that one
        if (beatValue - elapsedBeat < NEXT_NOTE_THRESHOLD && lastNoteIndex + 1 < notes.Count) {
            delay = elapsedBeat - beatValue;
            currentNote = notes[lastNoteIndex + 1];
        } else {
            currentNote = notes[lastNoteIndex];
        }

        if (currentNote.played) {
            currentNote = null;
            return;
        }

        // Compute score and play animation
        Score score = Score.ComputeScore(delay, true);
        totalScore += score.value;
        currentNote.AnimateHit(score);

        if (currentNote.duration == 0 || score == Score.Miss) {
            currentNote = null;
        }
    }

    // On button release, check held note
    public void ReleaseNote() {
        if (currentNote == null) {
            return;
        }

        if (currentNote.duration > 0) {
            float delay = elapsedBeat;
            float duration = currentNote.duration * song.timeSignature.multiplier;

            // Compute score and play animation
            Score score = Score.ComputeScore(duration - delay, false);
            totalScore += score.value;
            currentNote.AnimateRelease(score);
        }
        currentNote = null;
    }

    // TODO: refactor to own UI script?
    void OnGUI() {
        GUIStyle style = new GUIStyle();
        style.fontSize = 20;
        GUILayout.Label("Score: " + totalScore.ToString(), style);
    }

}
