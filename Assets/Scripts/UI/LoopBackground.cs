using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class LoopBackground : MonoBehaviour {
    GameObject clone;
    float imageWidth;
    bool isClone = false;

    // Use this for initialization
    void Start () {
        imageWidth = GetComponent<SpriteRenderer>().sprite.bounds.size.x;
        if (!isClone) {
            Vector3 newPosition = transform.position;
            newPosition.x += imageWidth * transform.localScale.x;
            clone = Instantiate(gameObject, newPosition, Quaternion.identity) as GameObject;
            clone.transform.parent = gameObject.transform.parent;
            clone.GetComponent<LoopBackground>().isClone = true;
            clone.GetComponent<LoopBackground>().clone = gameObject;
        }
    }

    void Update () {
    }

    void OnBecameInvisible() {
        if (clone != null) {
            Vector3 newPosition = transform.position;
            newPosition.x = clone.transform.position.x + imageWidth * transform.localScale.x;
            transform.position = newPosition;
        }
    }

}
