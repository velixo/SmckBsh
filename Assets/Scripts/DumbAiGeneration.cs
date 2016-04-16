using UnityEngine;
using System.Collections;

public class DumbAiGeneration : MonoBehaviour {
    public GameObject leftWall;
    public GameObject rightWall;
    public GameObject ground;
    public float spawnRate;
    private float lastTimeSpawned;

	private void Awake() {
        lastTimeSpawned = Time.timeSinceLevelLoad;
	}
	
	private void Update () {
        float currTime = Time.timeSinceLevelLoad;
        if (currTime - spawnRate > lastTimeSpawned) {
            lastTimeSpawned = currTime;
            GameObject dumbAi = Instantiate(Resources.Load("Prefabs/DumbAi")) as GameObject;
            dumbAi.transform.parent = transform;
            dumbAi.transform.position = GetRandomSpawnPos();
        }
	}

    private Vector2 GetRandomSpawnPos() {
        return new Vector2(0, 5);
    }
}
