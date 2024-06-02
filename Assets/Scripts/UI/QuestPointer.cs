using UnityEngine;
using UnityEngine.UI;

public class QuestPointer : MonoBehaviour
{
    [SerializeField] GameObject pointer;
    [SerializeField] RectTransform screenCanvas;
    [SerializeField] Camera targetCamera;
    [SerializeField] float border = 0;
    [SerializeField] float offScreenTolerance = 0.1f;
    [SerializeField] QuestTarget target;

    bool m_enabled;

    private void Start()
    {
        Enable(true);
    }

    public void SetTarget(QuestTarget newTarget)
    {
        if (target != null) target.ShowIndicator(false);
        this.target = newTarget;
        newTarget.ShowIndicator(true);
    }

    public void Enable(bool enable)
    {
        ShowPointer(enable);
        if (target != null) target.ShowIndicator(enable);
        m_enabled = enable;
        Update();
    }

    private void Update()
    {
        if (!m_enabled) return;
        Vector2 targetWorldPos = target.transform.position;
        if (!IsPointOnScreen(targetWorldPos))
        {
            ShowPointer(true);
            target.ShowIndicator(false);
            UpdatePointerTransform();
        }
        else
        {
            ShowPointer(false);
            target.ShowIndicator(true);
        }
    }

    private void ShowPointer(bool show)
    {
        pointer.SetActive(show);
    }

    private void UpdatePointerTransform()
    {
        Vector2 pointerDirection = GetPointerDirection();
        Vector2 pointerPosition = GetPointerPosition(pointerDirection);

        transform.localPosition = pointerPosition;
        transform.right = pointerDirection;

    }

    private Vector2 GetPointerPosition(Vector2 pointerDirection)
    {
        float halfScreenHeight = screenCanvas.rect.height / 2 - border;
        float halfScreenWidth = screenCanvas.rect.width / 2 - border;

        float screenX = 0;
        float screenY = 0;

        if (pointerDirection.x == 0)
        {
            screenX = 0;
            screenY = pointerDirection.y >= 0 ? halfScreenHeight : -halfScreenHeight;
        }
        else
        {
            float slope = Mathf.Abs(pointerDirection.y / pointerDirection.x);
            float heightAtHalfWidth = slope * halfScreenWidth;
            float widthAtHalfHeight = (halfScreenHeight / slope);

            if (heightAtHalfWidth <= halfScreenHeight) //It means the pointer direction intersects with one side of the canvas.
            {
                screenY = heightAtHalfWidth * Mathf.Sign(pointerDirection.y);
                screenX = halfScreenWidth * Mathf.Sign(pointerDirection.x);
            }
            else //It means the pointer direction intersects with the top or the bottom of the canvas.
            {
                screenY = halfScreenHeight * Mathf.Sign(pointerDirection.y);
                screenX = widthAtHalfHeight * Mathf.Sign(pointerDirection.x);
            }


        }

        return new Vector2(screenX, screenY);
    }

    private Vector2 GetPointerDirection()
    {
        Vector2 fromCameraToTarget = target.transform.position - targetCamera.transform.position;
        // Vector2 turnVector = fromCameraToTarget - (Vector2)targetCamera.transform.forward;
        // Vector2 pointerDirection = targetCamera.transform.InverseTransformVector(turnVector).normalized;
        // pointerDirection.z = 0;
        return fromCameraToTarget;
    }

    private bool IsPointOnScreen(Vector2 worldPoint)
    {
        Vector3 viewPortPoint = targetCamera.WorldToViewportPoint(worldPoint);
        bool isInFront = viewPortPoint.z > 0;
        bool isOnScreen = (viewPortPoint.x + offScreenTolerance >= 0 && viewPortPoint.x - offScreenTolerance <= 1)
                            && (viewPortPoint.y + offScreenTolerance >= 0 && viewPortPoint.y - offScreenTolerance <= 1);
        return isInFront && isOnScreen;
    }
}
