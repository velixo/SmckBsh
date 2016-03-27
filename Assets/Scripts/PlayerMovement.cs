using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
    public float xMaxVel;
    private float xDir;
    private Rigidbody2D rBody;

	// Use this for initialization
	private void Awake () {
        rBody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	private void FixedUpdate () {
        GetUserInput();
        if (xDir != 0) {
            Vector2 vel = rBody.velocity;
            vel.x = xMaxVel * xDir;
	        rBody.velocity = vel;
        }
	}

    private void GetUserInput() {
        xDir = Input.GetAxisRaw("Horizontal");
    }
}
