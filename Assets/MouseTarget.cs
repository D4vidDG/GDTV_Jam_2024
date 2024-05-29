using UnityEngine;

public class MouseTarget : MonoBehaviour
{
    [SerializeField] float minDistanceFromPlayer;

    GameObject player;
    Vector2 lastPosition;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        Vector2 playerToMouse = Mouse.GetVectorToMouse(player.transform.position);
        if (minDistanceFromPlayer < playerToMouse.magnitude)
        {
            lastPosition = Mouse.GetScreenPoint();
        }

        transform.position = lastPosition;
    }
}