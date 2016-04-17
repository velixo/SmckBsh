using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class NetworkedCameraFollow : MonoBehaviour {
    public GameObject ground;
    public GameObject lWall;
    public GameObject rWall;
    private GameObject player;
    public float moveSpeed;
    private float vExtent;
    private float hExtent;
    private float xMin;
    private float xMax;
    private float yMin;
	private float yMax;

	private void Awake () {
        vExtent = Camera.main.GetComponent<Camera>().orthographicSize;
        hExtent = vExtent * Screen.width / Screen.height;
        xMin = lWall.GetComponent<BoxCollider2D>().bounds.min.x;
        xMax = rWall.GetComponent<BoxCollider2D>().bounds.max.x;
        yMin = ground.GetComponent<BoxCollider2D>().bounds.min.y;
        yMax = lWall.GetComponent<BoxCollider2D>().bounds.max.y;
	}

	private void Update () {
        if (player == null) {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject p in players) {
                if (p.GetComponent<NetworkIdentity>().isLocalPlayer) {
                    player = p;
                }
            }

        } else {
            Vector2 newPos = Vector2.Lerp(transform.position, player.transform.position, Time.deltaTime * moveSpeed);
            newPos.x = Mathf.Clamp(newPos.x, xMin + hExtent, xMax - hExtent);
            newPos.y = Mathf.Clamp(newPos.y, yMin + vExtent, yMax - vExtent);
            transform.position = newPos;
        }
	}
}
