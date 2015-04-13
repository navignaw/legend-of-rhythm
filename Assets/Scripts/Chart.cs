using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Song))]
public class Chart : MonoBehaviour {
    public Vector3 startingPos;
    public float barDisplacement = 1f; // spacing after each bar
    public int totalScore = 0;

    public List<Note> notes; // list of note objects
    public List<GameObject> bars; // list of bars

    public GameObject PrefabEighthNote;
    public GameObject PrefabQuarterNote;
    public GameObject PrefabHalfNote;
    public GameObject PrefabWholeNote;
    public GameObject PrefabDottedEighthNote;
    public GameObject PrefabDottedQuarterNote;
    public GameObject PrefabDottedHalfNote;
    public GameObject PrefabDottedWholeNote;
    public GameObject PrefabBarLine;

    Song song;

    // For keeping track of current note:
    float elapsedBeat = 0.0f; // how much of current note has elapsed
    int lastNoteIndex = 0;

    // TODO: unhardcode
    void AddTestNotes() {
        for (int i = 0; i < 2; i++) {
            notes.Add(gameObject.AddComponent<Note>() as Note);
            notes.Add(gameObject.AddComponent<Note>() as Note);
            notes.Add(gameObject.AddComponent<Note>() as Note);
            notes.Add(gameObject.AddComponent<Note>() as Note);
            notes.Add(gameObject.AddComponent<Note>() as Note);
            notes.Add(gameObject.AddComponent<Note>() as Note);
            Note newNote = gameObject.AddComponent<Note>() as Note;
            newNote.noteType = NoteType.HALF;
            newNote.displacement = 1.5f;
            notes.Add(newNote);
        }
    }

    // Use this for initialization
    void Start () {
        song = GetComponent<Song>();
        notes = new List<Note>();

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
        float timeSignature = 4.0f; // TODO: don't hardcode this
        float currentPos = 0.0f; // x position of last sprite

        // TODO: draw time signature, etc.
        Vector3 pos = new Vector3(currentPos, 0, 0);
        bars = new List<GameObject>();
        bars.Add(Instantiate(PrefabBarLine, pos, Quaternion.identity) as GameObject);
        currentPos += barDisplacement;

        foreach (Note note in notes)
        {
            // Instantiate sprite
            // TODO: adjust y position based on row on staff
            pos.x = currentPos;
            note.sprite = Instantiate(GetNotePrefab(note.noteType), pos, Quaternion.identity) as GameObject;

            // Update beat and position
            currentPos += note.displacement;
            pos.x = currentPos;
            currentBeat = note.beatValue * timeSignature;
            beatCounter += currentBeat;

            if (beatCounter >= timeSignature) {
                // Add bar line
                bars.Add(Instantiate(PrefabBarLine, pos, Quaternion.identity) as GameObject);
                currentPos += barDisplacement;
                beatCounter %= timeSignature;
            }
        }

        // TODO: Add double bar
        pos.x += 0.2f;
        bars.Add(Instantiate(PrefabBarLine, pos, Quaternion.identity) as GameObject);
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
        // TODO: don't hardcode time signature value
        float beatValue = notes[lastNoteIndex].beatValue * 4;
        if (elapsedBeat >= beatValue && lastNoteIndex < notes.Count - 1) {
            elapsedBeat -= beatValue;
            AnimateNote(lastNoteIndex++, false);
            AnimateNote(lastNoteIndex, true);
        }
        return notes[lastNoteIndex];
    }

    void AnimateNote(int index, bool on) {
        notes[index].AnimateBeat(on);
    }

    // On button press, hit current note
    public void HitNote() {
        if (notes[lastNoteIndex].played) {
            return;
        }
        /* TODO: elapsedBeat only captures how long since last beat;
         * but if you press early (just before the next beat), we need to
         * check for that as well
         */
        Score score = Score.ComputeScore(elapsedBeat);
        totalScore += score.value;
        notes[lastNoteIndex].AnimateHit(score);
    }

    // TODO: refactor to own UI script?
    void OnGUI() {
        GUIStyle style = new GUIStyle();
        style.fontSize = 20;
        GUILayout.Label("Score: " + totalScore.ToString(), style);
    }

}
