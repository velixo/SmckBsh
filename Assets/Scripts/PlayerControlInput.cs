using UnityEngine;

public class PlayerControlInput : MonoBehaviour, IControlInput {
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

    public Vector2 GetAttackDir() {
        if (Input.GetKey(KeyCode.UpArrow)) {
            return Vector2.up;
        } else if (Input.GetKey(KeyCode.RightArrow)) {
            return Vector2.right;
        } else if (Input.GetKey(KeyCode.DownArrow)) {
            return Vector2.down;
        } else if (Input.GetKey(KeyCode.LeftArrow)) {
            return Vector2.left;
        } else {
            return Vector2.zero;
        }
    }
}
