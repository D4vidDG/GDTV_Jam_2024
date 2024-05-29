using UnityEngine;

public static class Mouse
{
    static Camera mainCamera = Camera.main;

    public static Vector2 GetVectorToMouse(Vector2 from)
    {
        return (GetScreenPoint() - from);
    }

    public static Vector2 GetScreenPoint()
    {
        return (Vector2)mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }
}