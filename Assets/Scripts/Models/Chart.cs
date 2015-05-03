using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[RequireComponent(typeof(Song))]
public class Chart : MonoBehaviour {
    public Vector3 startingPos;
    public float barDisplacement = 1f; // spacing after each bar
    public int totalScore = 0;

    const float NEXT_NOTE_THRESHOLD = 0.15f; // threshold before we register a hit as the next note

    public List<Note> notes; // list of note objects (attached to birb prefab)
    public List<GameObject> bars; // list of bars
    Song song;

    public List<GameObject> Prefabs; // list of note prefabs indexed by NoteType
    public GameObject PrefabBarLine;
    public ScoreReader reader;

    // For keeping track of current note:
    Note currentNote; // currently held note
    float elapsedBeat = 0.0f; // how much of current note has elapsed
    int lastNoteIndex = 0;

    // Use this for initialization
    void Start () {
        song = GetComponent<Song>();
        DrawNotes();
        //song.PlaySong();
    }

    // Update is called once per frame
    void Update () {
    }


    // Instantiate prefabs for each note, parsed from text file
    void DrawNotes()
    {
        float beatCounter = 0.0f;
        float currentBeat = 0.0f;
        float currentPos = 0.0f; // x position of last sprite

        notes = new List<Note>();
        bars = new List<GameObject>();

        // TODO: draw time signature, etc.
        Vector3 pos = new Vector3(currentPos, 0, 0);
        bars.Add(SpawnChild(PrefabBarLine, pos));
        currentPos += barDisplacement;

        foreach (GameObject prefab in ReadNotesFromFile()) {
            // Instantiate sprite
            // TODO: adjust y position based on row on staff
            pos.x = currentPos;
            GameObject birb = SpawnChild(prefab, pos);
            Note note = birb.GetComponent<Note>();
            notes.Add(note);

            // Update beat and position
            currentPos += note.displacement;
            pos.x = currentPos;
            currentBeat = note.beatValue * song.timeSignature.multiplier;
            beatCounter += currentBeat;

            if (beatCounter >= song.timeSignature.beats) {
                // Add bar line
                bars.Add(SpawnChild(PrefabBarLine, pos));
                currentPos += barDisplacement;
                beatCounter %= song.timeSignature.beats;
            }
        }

        // TODO: Add double bar
        pos.x += 0.2f;
        bars.Add(SpawnChild(PrefabBarLine, pos));
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

    // Currently, prefabs are ordered by noteType (see enum).
    // First eight are notes, next eight are rests.
    GameObject GetNotePrefab(NoteType noteType, bool isRest) {
        return Prefabs[(int)noteType + (isRest ? 8 : 0)];
    }

    GameObject SpawnChild(GameObject prefab, Vector3 pos) {
        GameObject go = Instantiate(prefab, pos, Quaternion.identity) as GameObject;
        go.transform.parent = transform;
        return go;
    }

    // Iterator that returns note prefab based off text file
    IEnumerable<GameObject> ReadNotesFromFile()
    {
        foreach (string line in reader.ReadLine()) {
            // TODO: change formatting of strings from Note/Rest: type
            string isRest = line.Substring(0, line.IndexOf(':'));
            string noteTypeStr = line.Substring(line.IndexOf(':') + 2);
            NoteType noteType = NoteType.QUARTER;

            switch (noteTypeStr) {
                case "eighth":
                    noteType = NoteType.EIGHTH;
                    break;
                case "quarter":
                    noteType = NoteType.QUARTER;
                    break;
                case "half":
                    noteType = NoteType.HALF;
                    break;
                case "whole":
                    noteType = NoteType.WHOLE;
                    break;
                case "dotted_eighth":
                    noteType = NoteType.DOTTED_EIGHTH;
                    break;
                case "dotted_quarter":
                    noteType = NoteType.DOTTED_QUARTER;
                    break;
                case "dotted_half":
                    noteType = NoteType.DOTTED_HALF;
                    break;
                case "dotted_whole":
                    noteType = NoteType.DOTTED_WHOLE;
                    break;
            }

            yield return GetNotePrefab(noteType, isRest == "Rest");
        }
    }

    // TODO: refactor to own UI script?
    void OnGUI() {
        GUIStyle style = new GUIStyle();
        style.fontSize = 20;
        GUILayout.Label("Score: " + totalScore.ToString(), style);
    }

}
