using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
    public Vector3 target; // target position to track
    public float scrollSpeed = 2.0f; // how smoothly to transition camera

    int lastMeasure = 0;

    // Use this for initialization
    void Start () {
    }

    // Scroll to target (every measure)
    void Update () {
        if (Song.currentSong == null) {
            return;
        }

        if (Song.currentSong.currentMeasure > lastMeasure) {
            target = new Vector3(target.x + 5, target.y, target.z); // TODO: scroll to next note
            lastMeasure = Song.currentSong.currentMeasure;
        }

        Vector3 newPos = transform.position;
        newPos.x = Mathf.Lerp(newPos.x, target.x, scrollSpeed * Time.deltaTime);
        transform.position = newPos;
    }

}
