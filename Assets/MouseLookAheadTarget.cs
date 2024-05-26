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
        Vector2 mousePosition = (Vector2)mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 playerPosition = player.transform.position;
        Vector2 fromPlayerToMouse = mousePosition - playerPosition;
        Vector2 lookAheadPosition = playerPosition + fromPlayerToMouse.normalized * lookAheadDistance;
        transform.position = lookAheadPosition;
    }

}
