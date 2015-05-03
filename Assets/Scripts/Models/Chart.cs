using UnityEngine;
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

    public List<GameObject> Prefabs; // list of note prefabs indexed by NoteType
    public GameObject PrefabBarLine;

    // For keeping track of current note:
    Note currentNote; // currently held note
    float elapsedBeat = 0.0f; // how much of current note has elapsed
    int lastNoteIndex = 0;

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
    /** TODO(Roger): parse string or text file to get NoteTypes
     *  replace noteTypes (and AddTestNotes) with notes read from a file
     *  might want to make a utility script that just reads and returns notetypes and rests?
     */
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

        foreach (NoteType noteType in noteTypes)
        {
            // Instantiate sprite
            // TODO: adjust y position based on row on staff
            pos.x = currentPos;
            // TODO: assign rests based off of random probability. un-hardcode this
            GameObject birb = SpawnChild(GetNotePrefab(noteType, Random.value >= 0.8), pos);
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

    // TODO: refactor to own UI script?
    void OnGUI() {
        GUIStyle style = new GUIStyle();
        style.fontSize = 20;
        GUILayout.Label("Score: " + totalScore.ToString(), style);
    }

}
