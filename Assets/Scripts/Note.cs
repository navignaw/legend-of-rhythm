using UnityEngine;
using System.Collections;

public enum NoteType {
    EIGHTH,
    QUARTER,
    HALF,
    WHOLE
};

public class Note : MonoBehaviour {
    public Vector3 startingPos;
    public NoteType noteType = NoteType.QUARTER;
    public bool dotted = false; // dotted notes have 150% duration

    // How long user needs to hold the note.
    // Duration of 0 indicates single press (quarter, eighth notes)
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
            }
            return value * (dotted ? 1.5f : 1.0f);
        }
    }

    bool played = false; // when note has been played
    int row = 0; // which row on the staff the note lines up with

    // Use this for initialization
    void Start () {
    }

    // Update is called once per frame
    void Update () {
    }

}
