using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Attacking : NetworkBehaviour {
    private bool debug = true;
    public float minTimeBetweenHits;
    private float lastArrHit = -Mathf.Infinity;
    private Vector2 attackDir = Vector2.zero;
    private GameObject attackSprite = null;
    private float hitSpriteOffset;
    private int playerLayer;
    private IControlInput attackInput;

    private void Awake () {
        GameObject hitSprite = Instantiate(Resources.Load("Prefabs/HitSprite")) as GameObject;
        hitSpriteOffset = (transform.localScale.x * 10 + hitSprite.transform.localScale.x) / 2;
        Destroy(hitSprite);
        hitSprite = null;
        playerLayer = LayerMask.NameToLayer("Player");
        attackInput = GetComponent<IControlInput>();
    }

	private void Update () {
        if (!isLocalPlayer) return;

        float timeSinceHit = Time.timeSinceLevelLoad - lastArrHit;
        CmdCalcHitSpriteAlpha();
        if (timeSinceHit > minTimeBetweenHits) {
            attackDir = attackInput.GetAttackDir();
            if (attackDir != Vector2.zero) {
                lastArrHit = Time.timeSinceLevelLoad;
                if (attackSprite != null) {
                    Destroy(attackSprite);
                    attackSprite = null;
                }
                CmdSpawnHitSprite();
                CmdDestroyEnemiesInAttackArea();
            }
        }
	}

    [Command]
    void CmdCalcHitSpriteAlpha() {
        print("CmdCalcHitSpriteAlpha()");
        if (attackSprite != null) {
            Color col = attackSprite.GetComponent<SpriteRenderer>().color;
            col.a = 1 - Mathf.Clamp01((Time.timeSinceLevelLoad - lastArrHit) / minTimeBetweenHits);
            attackSprite.GetComponent<SpriteRenderer>().color = col;
        }
    }

    [Command]
    void CmdSpawnHitSprite() {
        print("CmdSpawnHitSprite()");
        Vector2 attackSpritePos = (Vector2) transform.position + attackDir * hitSpriteOffset;
        attackSprite = Instantiate(Resources.Load("Prefabs/HitSprite"), attackSpritePos, Quaternion.identity) as GameObject;
        NetworkServer.Spawn(attackSprite);
        //attackSprite.transform.position = transform.position;
        //attackSprite.transform.Translate(attackDir * hitSpriteOffset);
    }

    [Command]
    void CmdDestroyEnemiesInAttackArea() {
        print("CmdDestroyEnemiesInAttackArea()");
        Vector2 aPos = attackSprite.transform.position;
        Vector2 aScale = attackSprite.transform.localScale;
        Vector2 aMin = new Vector2(aPos.x - aScale.x / 2, aPos.y - aScale.y / 2);
        Vector2 aMax = new Vector2(aPos.x + aScale.x / 2, aPos.y + aScale.y / 2);
        Collider2D[] colls = Physics2D.OverlapAreaAll(aMin, aMax);
        for (int i = 0; i < colls.Length; i++) {
            Collider2D coll = colls[i];
            if (coll.gameObject != gameObject && coll.gameObject.layer == playerLayer) {
                Destroy(coll.gameObject);
            }
        }
    }

    private Bounds debugBounds = new Bounds(Vector2.zero, new Vector2(1, 1));
    private float debugDur = 0.1f;
    public void OnDrawGizmos() {
        if (debug) {
            float timeSinceHit = Time.timeSinceLevelLoad - lastArrHit;
            if (attackDir != Vector2.zero && timeSinceHit > minTimeBetweenHits) {
                debugBounds.center = (Vector2)transform.position + attackDir * hitSpriteOffset;
                DebugExtension.DebugBounds(debugBounds, Color.green, debugDur);
            }
        }
    }
}
