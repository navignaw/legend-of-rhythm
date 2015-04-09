using UnityEngine;
using System.Collections;

public class Chart : MonoBehaviour {
    public Vector3 startingPos;
    public float scrollSpeed = 1.0f;
    public int noteScore = 1; // score per perfect note

    Note[] notes; // array of notes

    // Use this for initialization
    void Start () {
    }

    // Update is called once per frame
    void Update () {
        Vector3 newPos = transform.position;
        newPos.x = startingPos.x - scrollSpeed * Song.currentSong.songPos;
        Debug.Log(newPos);
        transform.position = newPos;
    }

}
