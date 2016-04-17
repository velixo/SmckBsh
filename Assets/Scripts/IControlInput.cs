using UnityEngine;

public interface IControlInput{
    float xDir { get; }
    float yDir { get; }

    Vector2 GetAttackDir();
}
