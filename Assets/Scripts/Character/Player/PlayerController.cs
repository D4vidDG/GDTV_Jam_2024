using UnityEngine;

class PlayerController : MonoBehaviour
{

    const KeyCode RELOAD_KEY = KeyCode.R;
    const KeyCode SWITCH_WEAPON_KEY = KeyCode.Z;

    Health health;
    PlayerMovement movement;
    Animator animator;
    CharacterFacer facer;
    WeaponInventory weaponInventory;

    bool controlEnabled;

    private void Awake()
    {
        movement = GetComponent<PlayerMovement>();
        animator = GetComponentInChildren<Animator>();
        health = GetComponent<Health>();
        facer = GetComponent<CharacterFacer>();
        weaponInventory = GetComponent<WeaponInventory>();
    }

    private void Start()
    {
        controlEnabled = true;
    }

    void Update()
    {
        if (!controlEnabled)
        {
            if (weaponInventory.currentWeapon != null) weaponInventory.currentWeapon.enabled = false;
            return;
        }
        else
        {
            if (weaponInventory.currentWeapon != null) weaponInventory.currentWeapon.enabled = true;
        }

        if (health.IsDead()) return;


        Weapon currentWeapon = weaponInventory.currentWeapon;
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
                weaponInventory.SwitchToNextWeapon();
            }
        }

        facer.FacePoint(Mouse.GetScreenPoint());

    }

    public void Enable(bool enabled)
    {
        controlEnabled = enabled;
    }

    private void LateUpdate()
    {
        animator.SetBool("Moving", movement.IsMoving());
        animator.SetBool("Dead", health.IsDead());
    }

    private bool WantsToShoot()
    {
        return Input.GetMouseButton(0);
    }

}