using UnityEngine;
using System.Collections;

public class AiControlInput : MonoBehaviour, IControlInput {
    private Movement movement;
    private float _xDir = 0;
    public float xDir {
        get {
            if (movement.TouchingSurface()) {
                Vector2 touchedSide = movement.GetTouchedSide();
                if (_xDir == 1 && touchedSide == Vector2.right) {
                    _xDir = -1;
                } else if (_xDir == -1 && touchedSide == Vector2.left) {
                    _xDir = 1;
                }
            }
            return _xDir;
        }
    }
    public float yDir {
        get {
            return 1;
        }
    }

    private void Awake() {
        movement = GetComponent<Movement>();
        if (movement.TouchingSurface()) {
            Vector2 touchedSide = movement.GetTouchedSide();
            if (touchedSide != Vector2.right) {
                _xDir = 1;
            } else if (touchedSide != Vector2.left) {
                _xDir = -1;
            }
        } else {
            _xDir = 1;
        }
    }

    public Vector2 GetAttackDir() {
        return Vector2.zero;
    }
}
