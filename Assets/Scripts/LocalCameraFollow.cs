using UnityEngine;
using System.Collections;

public class LocalCameraFollow : MonoBehaviour {
    public GameObject player;
    public GameObject ground;
    public GameObject lWall;
    public GameObject rWall;
    public float moveSpeed;
    private float vExtent;
    private float hExtent;
    private float xMin;
    private float xMax;
    private float yMin;
	private float yMax;

	// Use this for initialization
	private void Awake () {
        vExtent = Camera.main.GetComponent<Camera>().orthographicSize;
        hExtent = vExtent * Screen.width / Screen.height;
        xMin = lWall.GetComponent<BoxCollider2D>().bounds.min.x;
        xMax = rWall.GetComponent<BoxCollider2D>().bounds.max.x;
        yMin = ground.GetComponent<BoxCollider2D>().bounds.min.y;
        yMax = lWall.GetComponent<BoxCollider2D>().bounds.max.y;
	}

	// Update is called once per frame
	private void Update () {
        Vector2 newPos = Vector2.Lerp(transform.position, player.transform.position, Time.deltaTime * moveSpeed);
        newPos.x = Mathf.Clamp(newPos.x, xMin + hExtent, xMax - hExtent);
        newPos.y = Mathf.Clamp(newPos.y, yMin + vExtent, yMax - vExtent);
        transform.position = newPos;
	}
}
