using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
    public float xMaxVel;
    public float jumpVel;
    private Rigidbody2D rBody;
    private float xDir = 0;
    private float yDir = 0;
    private float collRadius = 0.51f;
    private int excludePlayerMask;

	// Use this for initialization
	private void Awake () {
        rBody = GetComponent<Rigidbody2D>();
        excludePlayerMask =  ~ (1 << LayerMask.NameToLayer("Player"));
	}
	
	// Update is called once per frame
	private void FixedUpdate () {
        GetUserInput();
        Vector2 vel = rBody.velocity;
        if (xDir != 0) {
            vel.x = xMaxVel * xDir;
        }
        if (yDir > 0 && TouchingSurface()) {
            vel.y = jumpVel;
        }
	    rBody.velocity = vel;
	}

    private void GetUserInput() {
        xDir = Input.GetAxisRaw("Horizontal");
        yDir = Input.GetAxisRaw("Vertical");
    }

    private bool TouchingSurface() {
        Collider2D coll = Physics2D.OverlapCircle(transform.position, collRadius, excludePlayerMask);
        return coll != null;
    }

    //public void OnDrawGizmos() {
    //    DebugExtension.DebugCircle(transform.position, Vector3.forward, Color.yellow, collRadius);
    //}
}
