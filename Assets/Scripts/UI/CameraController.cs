using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour {
    public Vector3 target; // target position to track
    public Vector3 offset; // offset from bar to display
    public float scrollSpeed = 2f; // how smoothly to transition camera
    public float offsetRight = 2f; // offset from rightmost bar

    int lastMeasure = 0;
    float camWidth;
    float camHeight;

    // Use this for initialization
    void Start () {
        camHeight = GetComponent<Camera>().orthographicSize * 2f;
        camWidth = GetComponent<Camera>().aspect * camHeight;
    }

    // Scroll to target (every measure)
    void Update () {
        if (Song.isPlaying && Song.currentSong.currentMeasure > lastMeasure) {
            lastMeasure = Song.currentSong.currentMeasure;
            List<GameObject> bars = Song.currentSong.chart.bars;
            if (lastMeasure < bars.Count) {
                target = bars[lastMeasure].transform.position;
                float xMax = bars[bars.Count - 1].transform.position.x - camWidth / 2f - offsetRight;
                target.x = Mathf.Clamp(target.x, 0f, xMax);
            }
        }

        Vector3 newPos = transform.position;
        newPos.x = Mathf.Lerp(newPos.x, target.x, scrollSpeed * Time.deltaTime);
        transform.position = newPos + offset;
    }

}
