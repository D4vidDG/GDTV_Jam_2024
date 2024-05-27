using ExtensionMethods;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] float fireRate;
    [SerializeField] float reloadTime;
    [SerializeField] float knockback;
    [SerializeField] int ammo;
    [SerializeField] protected float range;
    [SerializeField] protected float damage;
    [SerializeField] protected Projectile projectile;
    [SerializeField] protected Transform gunTip;

    Vector2 vectorToTarget;
    float shootingTimer;
    int currentAmmo;
    bool reloading;
    bool equipped;

    Camera mainCamera;
    GameObject player;

    void Awake()
    {
        mainCamera = Camera.main;
        player = GameObject.FindWithTag("Player");
    }

    private void Start()
    {
        shootingTimer = 0;
        currentAmmo = ammo;
        reloading = false;
    }

    void Update()
    {
        vectorToTarget = Mouse.GetVectorToMouse(gunTip.position);
        shootingTimer += Time.deltaTime;

        if (equipped)
        {
            FaceDirection(vectorToTarget);
        }
    }

    public bool TryToFire()
    {
        if (CanFire())
        {
            shootingTimer = 0;
            Fire(vectorToTarget);
            currentAmmo--;
            return true;
        }

        return false;
    }

    public void Equip(Transform holdingPoint)
    {
        equipped = true;
        transform.SetParent(holdingPoint, false);
        transform.localPosition = Vector2.zero;
    }

    public void UnEquip()
    {
        transform.right = Vector2.right;
        equipped = false;
    }

    public void Reload()
    {
        if (reloading) return;
        reloading = true;
        Invoke(nameof(ResetAmmo), reloadTime);
    }

    public float GetKnockback()
    {
        return knockback;
    }

    public Vector2 GetDirection()
    {
        return vectorToTarget;
    }

    protected abstract void Fire(Vector2 shootingDirection);

    protected void LaunchProjectle(Vector2 direction)
    {
        Projectile instance = Instantiate<Projectile>(
                                  projectile,
                                  gunTip.position,
                                  Quaternion.identity,
                                  null);
        instance.Launch(direction, range);
        instance.OnTargetHit += OnTargetHit;
    }

    private void FaceDirection(Vector2 direction)
    {
        float globalXScale = transform.lossyScale.x;
        if (globalXScale > 0)
        {
            transform.right = -direction.normalized;
        }
        else
        {
            transform.right = direction.normalized;
        }

    }

    private void OnTargetHit(Health target)
    {
        target.TakeDamage(damage);
    }

    private bool CanFire()
    {
        float timeToShoot = 1 / fireRate;
        bool hasEnoughAmmo = currentAmmo > 0;
        bool shootingCooldownElapsed = shootingTimer >= timeToShoot;
        return shootingCooldownElapsed && hasEnoughAmmo && equipped;
    }

    private void ResetAmmo()
    {
        currentAmmo = ammo;
        reloading = false;
    }

    private Vector2 GetTargetPosition()
    {
        return (Vector2)mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnDrawGizmosSelected()
    {
        DrawGizmos();
    }

    protected virtual void DrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(gunTip.position, range);
    }
}
