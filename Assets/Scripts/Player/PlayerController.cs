using UnityEngine;

class PlayerController : MonoBehaviour
{
    [SerializeField] Weapon weapon;


    void Update()
    {
        if (WantsToShoot())
        {
            weapon.Fire();
        }
    }


    private bool WantsToShoot()
    {
        return Input.GetMouseButton(0);
    }

}