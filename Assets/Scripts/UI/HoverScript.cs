using UnityEngine;
using System.Collections;

public class HoverScript : MonoBehaviour {

    private Vector3 target;
	// Use this for initialization
	void Start () {
        target = new Vector3(0, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate()
    {
        if (transform.position != target)
        {
            float x = (target - transform.position).x;
            float y = (target - transform.position).y;
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(x * 5.0f, y * 5.0f);
        }
    }

    public void SetTargetX(float x_position){
        target.x = x_position;
    }

    public void SetTargetY(float y_position)
    {
        target.y = y_position;
    }
}
