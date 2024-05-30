using UnityEngine;

public abstract class FirePattern : MonoBehaviour
{
    public abstract Vector2[] GetDirections(Vector2 shootingDirection);
}