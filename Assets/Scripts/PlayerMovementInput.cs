using UnityEngine;

public class PlayerMovementInput : MonoBehaviour, IMovementInput {
    private float mXDir;
    private float mYDir;

    public float xDir {
        get {
            mXDir = 0;
            if (Input.GetKey(KeyCode.D)) mXDir++;
            if (Input.GetKey(KeyCode.A)) mXDir--;
            return mXDir;
        }
    }
    public float yDir {
        get {
            mYDir = 0;
            if (Input.GetKey(KeyCode.W)) mYDir++;
            if (Input.GetKey(KeyCode.S)) mYDir--;
            return mYDir;
        }
    }
}
