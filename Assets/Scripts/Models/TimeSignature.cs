using UnityEngine;
using System.Collections;

[System.Serializable]
public class TimeSignature {
    public int beats; // number of beats per bar
    public NoteType unit; // type of note for one beat

    public float multiplier {
        get {
            switch (unit) {
                case NoteType.EIGHTH:
                    return 8f;
                case NoteType.QUARTER:
                    return 4f;
                case NoteType.HALF:
                    return 2f;
            }
            Debug.Log("unexpected time signature!");
            return 1f;
        }
    }
}
