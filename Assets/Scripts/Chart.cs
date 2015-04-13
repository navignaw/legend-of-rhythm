using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Chart : MonoBehaviour {
    public Vector3 startingPos;
    public int noteScore = 1; // score per perfect note

    public List<Note> notes; // array of notes

    public Transform PrefabEighthNote;
    public Transform PrefabQuarterNote;
    public Transform PrefabHalfNote;
    public Transform PrefabWholeNote;
    public Transform PrefabDottedEighthNote;
    public Transform PrefabDottedQuarterNote;
    public Transform PrefabDottedHalfNote;
    public Transform PrefabDottedWholeNote;

    // TODO: unhardcode
    void AddTestNotes() {
        notes.Add(gameObject.AddComponent<Note>() as Note);
        notes.Add(gameObject.AddComponent<Note>() as Note);
        notes.Add(gameObject.AddComponent<Note>() as Note);
        notes.Add(gameObject.AddComponent<Note>() as Note);
        Note newNote = gameObject.AddComponent<Note>() as Note;
        newNote.noteType = NoteType.HALF;
        newNote.displacement = 1.5f;
        notes.Add(newNote);
        newNote = gameObject.AddComponent<Note>() as Note;
        newNote.noteType = NoteType.HALF;
        newNote.displacement = 1.5f;
        notes.Add(newNote);
    }

    // Use this for initialization
    void Start () {
        AddTestNotes();
        drawNotes();
    }

    // Update is called once per frame
    void Update () {
    }


    // Instantiate prefabs for each note in note array
    void drawNotes()
    {
        float beatCounter = 0.0f;
        float currentBeat = 0.0f;
        float timeSignature = 4.0f; // TODO: don't hardcode this
        float currentPos = 0.0f; // x position of last sprite

        foreach (Note note in notes)
        {
            // Instantiate sprite
            // TODO: adjust y position based on row on staff
            Vector3 pos = new Vector3(currentPos, 0, 0);
            note.sprite = Instantiate(getNotePrefab(note.noteType), pos, Quaternion.identity) as GameObject;

            // Update beat and position
            currentPos += note.displacement;
            currentBeat = note.beatValue * timeSignature;
            beatCounter += currentBeat;

            if (beatCounter >= timeSignature) {
                // TODO: add bar line
                beatCounter %= timeSignature;
            }
        }
    }

    // Return appropriate prefab based on note type
    Transform getNotePrefab(NoteType type) {
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
}
