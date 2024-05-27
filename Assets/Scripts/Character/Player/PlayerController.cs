using ExtensionMethods;
using UnityEngine;

class PlayerController : MonoBehaviour
{
    [SerializeField] Weapon[] weapons;
    [SerializeField] Transform gunHoldingPoint;
    const KeyCode RELOAD_KEY = KeyCode.R;
    const KeyCode SWITCH_WEAPON_KEY = KeyCode.Z;
    int currentWeaponIndex;
    Weapon currentWeapon => weapons[currentWeaponIndex];

    PlayerMovement movement;


    private void Awake()
    {
        movement = GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        currentWeaponIndex = 0;
        currentWeapon.gameObject.SetActive(true);
        currentWeapon.Equip(gunHoldingPoint);
    }

    void Update()
    {
        if (currentWeapon != null)
        {
            if (WantsToShoot())
            {
                if (currentWeapon.TryToFire())
                {
                    movement.ApplyKnockback(currentWeapon.GetKnockback(), currentWeapon.GetDirection().normalized);
                }
            }

            if (Input.GetKeyDown(RELOAD_KEY))
            {
                currentWeapon.Reload();
            }

            if (Input.GetKeyDown(SWITCH_WEAPON_KEY))
            {
                SwitchWeapon();
            }
        }

        FaceMouse();
    }

    private void FaceMouse()
    {
        Vector2 vectorToMouse = Mouse.GetVectorToMouse(transform.position);
        float angle = vectorToMouse.GetAngle();
        bool isMouseLeft = 90f < angle && angle < 270f;
        float xScale = isMouseLeft ? 1 : -1;
        transform.localScale = new Vector3(xScale, transform.localScale.y, transform.localScale.z);
    }

    private bool WantsToShoot()
    {
        return Input.GetMouseButton(0);
    }

    private void SwitchWeapon()
    {
        if (weapons.Length > 0)
        {
            currentWeapon.UnEquip();
            currentWeapon.gameObject.SetActive(false);

            currentWeaponIndex = (currentWeaponIndex + 1) % weapons.Length;

            currentWeapon.gameObject.SetActive(true);
            currentWeapon.Equip(gunHoldingPoint);
        }
    }

}