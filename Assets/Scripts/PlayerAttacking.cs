using UnityEngine;
using System.Collections;

public class PlayerAttacking : MonoBehaviour {
    private bool debug = true;
    public float minTimeBetweenHits;
    private float lastArrHit = -Mathf.Infinity;
    private Vector2 hitDir = Vector2.zero;
    private GameObject hitSprite = null;
    private float hitSpriteOffset;

    private void Awake () {
        hitSpriteOffset = 1f;
    }

	private void Update () {
        float timeSinceHit = Time.timeSinceLevelLoad - lastArrHit;
        if (timeSinceHit > minTimeBetweenHits) {
            if (Input.GetKey(KeyCode.UpArrow)) {
                lastArrHit = Time.timeSinceLevelLoad;
                hitDir = Vector2.up;
            } else if (Input.GetKey(KeyCode.RightArrow)) {
                lastArrHit = Time.timeSinceLevelLoad;
                hitDir = Vector2.right;
            } else if (Input.GetKey(KeyCode.DownArrow)) {
                lastArrHit = Time.timeSinceLevelLoad;
                hitDir = Vector2.down;
            } else if (Input.GetKey(KeyCode.LeftArrow)) {
                lastArrHit = Time.timeSinceLevelLoad;
                hitDir = Vector2.left;
            }

            if (hitDir != Vector2.zero) {
                print("start hit sprite");
                hitSprite = Instantiate(Resources.Load("Prefabs/HitSprite")) as GameObject;
                hitSprite.transform.position = transform.position;
                hitSprite.transform.Translate(hitDir * hitSpriteOffset);
                hitDir = Vector2.zero;
            }
        }

        CalcHitSpriteAlpha();
	}

    private void CalcHitSpriteAlpha() {
        if (hitSprite != null) {
            Color col = hitSprite.GetComponent<SpriteRenderer>().color;
            col.a = 1 - Mathf.Clamp01((Time.timeSinceLevelLoad - lastArrHit) / minTimeBetweenHits);
            if (col.a > 0) {
                hitSprite.GetComponent<SpriteRenderer>().color = col;
            } else {
                Destroy(hitSprite);
                hitSprite = null;
            }
        }
    }

    private Bounds debugBounds = new Bounds(Vector2.zero, new Vector2(1, 1));
    private float debugDur = 0.1f;
    public void OnDrawGizmos() {
        if (debug) {
            float timeSinceHit = Time.timeSinceLevelLoad - lastArrHit;
            if (hitDir != Vector2.zero && timeSinceHit > minTimeBetweenHits) {
                debugBounds.center = (Vector2)transform.position + hitDir * hitSpriteOffset;
                DebugExtension.DebugBounds(debugBounds, Color.green, debugDur);
            }
        }
    }
}
