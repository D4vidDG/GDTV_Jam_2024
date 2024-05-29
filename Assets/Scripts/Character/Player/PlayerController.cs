using UnityEngine;

class PlayerController : MonoBehaviour
{

    const KeyCode RELOAD_KEY = KeyCode.R;
    const KeyCode SWITCH_WEAPON_KEY = KeyCode.Z;

    [SerializeField] Weapon[] weapons;
    [SerializeField] Transform gunHoldingPoint;

    Health health;
    PlayerMovement movement;
    Animator animator;
    CharacterFacer facer;

    int currentWeaponIndex;
    public Weapon currentWeapon => weapons[currentWeaponIndex];
    bool controlEnabled;

    private void Awake()
    {
        movement = GetComponent<PlayerMovement>();
        animator = GetComponentInChildren<Animator>();
        health = GetComponent<Health>();
        facer = GetComponent<CharacterFacer>();
    }

    private void Start()
    {
        controlEnabled = true;
        currentWeaponIndex = 0;
        currentWeapon.gameObject.SetActive(true);
        currentWeapon.Equip(gunHoldingPoint);
    }

    void Update()
    {
        if (!controlEnabled) return;
        if (health.IsDead()) return;

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