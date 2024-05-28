using UnityEngine;

public class MouseLookAheadTarget : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float lookAheadDistance;

    Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        Vector2 playerPosition = player.transform.position;
        Vector2 fromPlayerToMouse = Mouse.GetVectorToMouse(player.transform.position);
        Vector2 lookAheadPosition = playerPosition + fromPlayerToMouse.normalized * lookAheadDistance;
        transform.position = lookAheadPosition;
    }

}
