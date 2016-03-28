using UnityEngine;
using System.Collections;

public class PlayerAttacking : MonoBehaviour {
    public float hitTimeDiff;
    private float lastArrHit = -Mathf.Infinity;
    private bool debugHitUp = false;
    private bool debugHitRight = false;
    private bool debugHitDown = false;
    private bool debugHitLeft = false;

	// Update is called once per frame
	private void Update () {
        float timeSinceHit = Time.timeSinceLevelLoad - lastArrHit;
        if (timeSinceHit > hitTimeDiff) {
            if (Input.GetKey(KeyCode.UpArrow)) {
                lastArrHit = Time.timeSinceLevelLoad;
                debugHitUp = true;
            } else if (Input.GetKey(KeyCode.RightArrow)) {
                lastArrHit = Time.timeSinceLevelLoad;
                debugHitRight = true;
            } else if (Input.GetKey(KeyCode.DownArrow)) {
                lastArrHit = Time.timeSinceLevelLoad;
                debugHitDown = true;
            } else if (Input.GetKey(KeyCode.LeftArrow)) {
                lastArrHit = Time.timeSinceLevelLoad;
                debugHitLeft = true;
            }
        }
	}

    public void OnDrawGizmos() {
        Vector2 extents = new Vector2(1, 1);
        Vector2 center = transform.position;
        float dur = 0.1f;
        if (debugHitUp) {
            DebugExtension.DebugBounds(new Bounds(center + Vector2.up, extents), Color.green, dur);
            debugHitUp = false;
        }
        if (debugHitRight) {
            DebugExtension.DebugBounds(new Bounds(center + Vector2.right, extents), Color.green, dur);
            debugHitRight = false;
        }
        if (debugHitDown) {
            DebugExtension.DebugBounds(new Bounds(center + Vector2.down, extents), Color.green, dur);
            debugHitDown = false;
        }
        if (debugHitLeft) {
            DebugExtension.DebugBounds(new Bounds(center + Vector2.left, extents), Color.green, dur);
            debugHitLeft = false;
        }
    }
}
