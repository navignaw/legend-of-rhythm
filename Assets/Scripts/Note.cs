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

    // How long user needs to hold the note. Normalized so whole note = 1.
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

    // How long note actually lasts. Normalized so whole note = 1.
    public float beatValue {
        get {
            switch (noteType) {
                case NoteType.EIGHTH:
                    return 0.125f;
                case NoteType.QUARTER:
                    return 0.25f;
                case NoteType.HALF:
                    return 0.5f;
                case NoteType.WHOLE:
                    return 1.0f;
                case NoteType.DOTTED_EIGHTH:
                    return 0.1875f;
                case NoteType.DOTTED_QUARTER:
                    return 0.375f;
                case NoteType.DOTTED_HALF:
                    return 0.75f;
                case NoteType.DOTTED_WHOLE:
                    return 1.5f;
            }
            return 0.0f;
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
