using ExtensionMethods;
using UnityEngine;

public class CharacterFacer : MonoBehaviour
{
    [SerializeField] Transform target;

    bool isEnabled;
    GameObject model;

    private void Update()
    {
        if (isEnabled && target != null) FacePoint(target.position);
    }

    private void Awake()
    {
        model = transform.GetChild(0).gameObject;
    }

    public void Enable()
    {
        isEnabled = true;
    }

    public void Disable()
    {
        isEnabled = false;
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    public void FacePoint(Vector2 position)
    {
        Vector2 vectorToPoint = position - (Vector2)transform.position;
        float angle = vectorToPoint.GetAngle();
        float xScale = 90 < angle && angle < 270 ? 1 : -1;
        model.transform.localScale = new Vector3(xScale, transform.localScale.y, transform.localScale.z);
    }
}