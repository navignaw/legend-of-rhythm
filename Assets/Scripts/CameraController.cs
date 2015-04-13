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
        if (Song.currentSong == null || !Song.currentSong.isPlaying) {
            return;
        }

        if (Song.currentSong.currentMeasure > lastMeasure) {
            lastMeasure = Song.currentSong.currentMeasure;
            if (lastMeasure < Song.currentSong.chart.bars.Count) {
                target = Song.currentSong.chart.bars[lastMeasure].transform.position;
            }
        }

        Vector3 newPos = transform.position;
        newPos.x = Mathf.Lerp(newPos.x, target.x, scrollSpeed * Time.deltaTime);
        transform.position = newPos;
    }

}
