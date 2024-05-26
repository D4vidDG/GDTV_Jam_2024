using UnityEngine;

class PlayerController : MonoBehaviour
{
    [SerializeField] Weapon weapon;
    [SerializeField] KeyCode reloadKey;


    void Update()
    {
        if (WantsToShoot())
        {
            weapon.Fire();
        }

        if (Input.GetKeyDown(reloadKey))
        {
            weapon.Reload();
        }
    }


    private bool WantsToShoot()
    {
        return Input.GetMouseButton(0);
    }

}