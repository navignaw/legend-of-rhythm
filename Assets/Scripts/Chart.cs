using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Chart : MonoBehaviour {
    public Vector3 startingPos;
    public int noteScore = 1; // score per perfect note

    Note[] notes; // array of notes

    // Use this for initialization
    void Start () {
    }

    // Update is called once per frame
    void Update () {
    }


    //Need to take my array of notes and create an array of the correct prefabs
    List<GameObject> drawNotes()
    {
        List<GameObject> noteSprites = new List<GameObject>();

        float beatCounter = 0.0f;
        float currentBeat = 0.0f;
        float timeSignature = 4.0f;
        
        for (int i = 0; i < notes.Length; i++)
        {
            switch (notes[i].noteType)
            {
                case NoteType.EIGHTH:
                    currentBeat = 0.5f;
                    break;
                case NoteType.DOTTED_EIGHTH:
                    currentBeat = 0.75f;
                    break;
                case NoteType.QUARTER:
                    currentBeat = 1.0f;
                    break;
                case NoteType.DOTTED_QUARTER:
                    currentBeat = 1.5f;
                    break;
                case NoteType.HALF:
                    currentBeat = 2f;
                    break;
                case NoteType.DOTTED_HALF:
                    currentBeat = 2.5f;
                    break;
                case NoteType.WHOLE:
                    currentBeat = 4.0f;
                    break;
                case NoteType.DOTTED_WHOLE:
                    currentBeat = 6.0f;
                    break;
            }
            beatCounter += currentBeat;

            if (beatCounter > timeSignature)
            {
                noteSprites.Add(getNote(currentBeat - (beatCounter - timeSignature)));
                noteSprites.Add(getNote(beatCounter - timeSignature));
            }
            else
            {
                noteSprites.Add(getNote(beatCounter - timeSignature));
            }

            if (beatCounter >= timeSignature)
                beatCounter = beatCounter - timeSignature;
        }
        return noteSprites;
    }

    GameObject getNote(float time)
    {
        
        /* Need to create all of the other note prefabs
         * if(time == 0.5) 
            return new GameObject("EighthNote")
        */
         if(time == 1.0) 
            return new GameObject("QuarterNote");
         else if (time == 2.0)
             return new GameObject("HalfNote");
         else
         {
             Debug.Log("EVERYTHING IS BROKEN");
             return new GameObject("HalfNote");
         }
    }
}
