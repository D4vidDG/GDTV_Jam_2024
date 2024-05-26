using UnityEngine;

public static class Mouse
{
    public static Vector2 GetVectorToMouse(Vector2 from)
    {
        return (GetScreenPoint() - from).normalized;
    }

    public static Vector2 GetScreenPoint()
    {
        return (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}