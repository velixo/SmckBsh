using UnityEngine;
using System.Collections;

public class AIMovementInput : MonoBehaviour, IMovementInput {
    private Movement movement;
    private float _xDir = 0;
    private float _yDir = 0;
    public float xDir {
        get {
            return CalculateXDir();
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

    private float CalculateXDir() {
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
