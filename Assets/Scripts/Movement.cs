using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
    public bool debug;
    public float xMaxVel;
    public float jumpVel;
    public float xAirDrag;
    public float xGroundDrag;
    public float wallJumpVel;
    private IMovementInput movementInput;
    private Rigidbody2D rBody;
    private BoxCollider2D bColl;
    private float xDir = 0;
    private float yDir = 0;
    private float collRadius = 0.52f;
    private int excludePlayerMask;

	// Use this for initialization
	private void Awake () {
        rBody = GetComponent<Rigidbody2D>();
        bColl = GetComponent<BoxCollider2D>();
        excludePlayerMask =  ~ (1 << LayerMask.NameToLayer("Player"));
        movementInput = GetComponent<IMovementInput>();
	}
	
	// Update is called once per frame
	private void FixedUpdate () {
        GetMovementInput();
        Move();
	}

    private void Move() {
        Vector2 vel = rBody.velocity;
        bool touchingSurface = TouchingSurface();
        if (touchingSurface) {
            vel.x *= xGroundDrag;
        } else {
            vel.x *= xAirDrag;
        }

        if (xDir != 0) {
            vel.x = xMaxVel * xDir;
        }
        if (yDir > 0 && touchingSurface) {
            vel.y = jumpVel;
            Vector2 touchedSide = GetTouchedSide();
            if (touchedSide == Vector2.left) {
                vel.x = wallJumpVel;
            }
            else if (touchedSide == Vector2.right) {
                vel.x = -wallJumpVel;
            }
        }
        rBody.velocity = vel;
    }

    private void GetMovementInput() {
        xDir = movementInput.xDir;
        yDir = movementInput.yDir;
    }

    private bool TouchingSurface() {
        //TODO change this to a box check
        Collider2D coll = Physics2D.OverlapCircle(transform.position, collRadius, excludePlayerMask);
        return coll != null;
    }

    private Vector2 GetTouchedSide() {
        Vector2 center = bColl.bounds.center;
        Vector2 top = new Vector2(center.x, bColl.bounds.max.y);
        Vector2 bottom = new Vector2(center.x, bColl.bounds.min.y);

        RaycastHit2D tHitLeft = Physics2D.Raycast(top, Vector2.left, collRadius, excludePlayerMask);
        RaycastHit2D bHitLeft = Physics2D.Raycast(bottom, Vector2.left, collRadius, excludePlayerMask);
        if (tHitLeft.collider != null || bHitLeft.collider != null) {
            return Vector2.left;
        }

        RaycastHit2D tHitRight = Physics2D.Raycast(top, Vector2.right, collRadius, excludePlayerMask);
        RaycastHit2D bHitRight = Physics2D.Raycast(bottom, Vector2.right, collRadius, excludePlayerMask);
        if (tHitRight.collider != null || bHitRight.collider != null) {
            return Vector2.right;
        }

        return Vector2.zero;
    }

    public void OnDrawGizmos() {
        if (Application.isPlaying && debug) {
            Vector2 center = bColl.bounds.center;
            Vector2 top = new Vector2(center.x, bColl.bounds.max.y);
            Vector2 bottom = new Vector2(center.x, bColl.bounds.min.y);

            // draw left side touch detection rays
            Debug.DrawRay(top, Vector2.left * collRadius, Color.cyan);
            Debug.DrawRay(bottom, Vector2.left * collRadius, Color.red);

            // draw right side touch detection
            Debug.DrawRay(top, Vector2.right * collRadius, Color.green);
            Debug.DrawRay(bottom, Vector2.right * collRadius, Color.magenta);

            // draw surface detection circle
            DebugExtension.DebugCircle(transform.position, Vector3.forward, Color.yellow, collRadius);
        }
    }
}
