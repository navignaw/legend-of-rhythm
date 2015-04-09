using UnityEngine;
using System.Collections;

public enum NoteType {
    EIGHTH,
    QUARTER,
    HALF,
    WHOLE,
    DOTTED_EIGHTH,
    DOTTED_QUARTER,
    DOTTED_HALF,
    DOTTED_WHOLE
};

public class Note : MonoBehaviour {
    public Vector3 startingPos;
    public NoteType noteType = NoteType.QUARTER;
    public float displacement = 1.0f; // how far apart the next note is

    // How long user needs to hold the note.
    // Duration of 0 indicates single press (quarter, eighth notes, etc.)
    public float duration {
        get {
            float value = 0.0f;
            switch (noteType) {
                case NoteType.HALF:
                    value = 0.5f;
                    break;
                case NoteType.WHOLE:
                    value = 1.0f;
                    break;
                case NoteType.DOTTED_HALF:
                    value = 0.75f;
                    break;
                case NoteType.DOTTED_WHOLE:
                    value = 1.5f;
                    break;
            }
            return value;
        }
    }

    bool played = false; // when note has been played
    int row = 0; // which row on the staff the note lines up with

    // Use this for initialization
    void Start () {
        // TODO: Calculate displacement based off NoteType
    }

    // Update is called once per frame
    void Update () {
    }

}
