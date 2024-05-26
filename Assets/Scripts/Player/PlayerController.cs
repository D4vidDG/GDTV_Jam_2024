using UnityEngine;

class PlayerController : MonoBehaviour
{
    [SerializeField] Weapon weapon;
    const KeyCode RELOAD_KEY = KeyCode.R;

    PlayerMovement movement;

    private void Awake()
    {
        movement = GetComponent<PlayerMovement>();
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
    }


    private bool WantsToShoot()
    {
        return Input.GetMouseButton(0);
    }

}