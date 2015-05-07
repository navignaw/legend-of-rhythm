using UnityEngine;
using System.Collections;

public class ScoreText : MonoBehaviour {
    public Vector3 target;
    Vector3 offset = new Vector3(-0.5f, 2f, 0f);
    const float delta = 1f;

    GUIText text;

    void Awake() {
        text = GetComponent<GUIText>();
        text.font = Resources.Load("Fonts/Gotham") as Font;
        text.fontSize = 25;
    }

    // Rise up every frame and update position based on camera
    void Update() {
        offset.y += delta * Time.deltaTime;
        transform.position = Camera.main.WorldToViewportPoint(target + offset);

        // fade opacity
        Color newColor = text.material.color;
        newColor.a -= delta * Time.deltaTime;
        text.material.color = newColor;
    }

}
