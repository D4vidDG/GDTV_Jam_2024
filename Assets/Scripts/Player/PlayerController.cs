using ExtensionMethods;
using UnityEngine;

class PlayerController : MonoBehaviour
{
    [SerializeField] Weapon weapon;
    [SerializeField] Transform gunHoldingPoint;
    const KeyCode RELOAD_KEY = KeyCode.R;

    PlayerMovement movement;

    private void Awake()
    {
        movement = GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        weapon.Equip(gunHoldingPoint);
    }

    void Update()
    {
        if (WantsToShoot())
        {
            if (weapon.TryToFire())
            {
                movement.ApplyKnockback(weapon.GetKnockback(), weapon.GetDirection().normalized);
            }
        }

        if (Input.GetKeyDown(RELOAD_KEY))
        {
            weapon.Reload();
        }

        FaceMouse();
    }

    private void FaceMouse()
    {
        Vector2 vectorToMouse = Mouse.GetVectorToMouse(transform.position);
        float angle = vectorToMouse.GetAngle();
        bool isMouseLeft = 90f < angle && angle < 270f;
        float xScale = isMouseLeft ? 1 : -1;
        Debug.Log(xScale);
        transform.localScale = new Vector3(xScale, transform.localScale.y, transform.localScale.z);
    }

    private bool WantsToShoot()
    {
        return Input.GetMouseButton(0);
    }

}