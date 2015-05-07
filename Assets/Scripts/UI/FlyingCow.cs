using UnityEngine;
using System.Collections;

public class FlyingCow : MonoBehaviour {

    Vector3 offset;
    private Vector3 target;
    // Use this for initialization

    void Start () {
        offset = new Vector3(0f, 60.0f);
        target = transform.position;
    }

    // Update is called once per frame
    void Update () {

    }

    void FixedUpdate()
    {
        if (transform.position != target)
        {
            Debug.Log("hi");
            float x = target.x - transform.position.x;
            float y = target.y - transform.position.y;
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(x * 5.0f, y * 5.0f);
        }
    }

    public void SetTarget(Transform transformTarget) {
        target = transformTarget.position + offset;
    }
}
