using System.Collections;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public bool controlEnabled;
    [SerializeField] float fireRate;
    [SerializeField] float reloadTime;
    [SerializeField] float knockback;
    [SerializeField] int maxAmmo;
    [SerializeField] protected float range;
    [SerializeField] protected float damage;
    [SerializeField] protected Projectile projectile;
    [SerializeField] protected Transform gunTip;
    [SerializeField] float mouseDeadZone;

    Vector2 shootingDirection;
    float shootingTimer;
    float reloadTimer;
    int currentAmmo;
    bool reloading;
    bool equipped;
    Coroutine reloadRoutine;

    public SoundName fireSound, reloadSound;

    private void Start()
    {
        controlEnabled = true;
        reloadTimer = 0;
        shootingTimer = 0;
        currentAmmo = maxAmmo;
        reloading = false;
    }

    void Update()
    {
        if (controlEnabled)
        {
            shootingDirection = GetShootingDirection();
            shootingTimer += Time.deltaTime;

            if (equipped)
            {
                FaceDirection(shootingDirection);
            }
        }
    }

    public bool TryToFire()
    {
        if (CanFire())
        {
            shootingTimer = 0;
            AudioManager.instance.PlaySound(fireSound);
            Fire(shootingDirection);
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
        if (reloading) CancelReload();
        transform.right = Vector2.right;
        equipped = false;
    }

    public void Reload()
    {
        if (CanReload())
        {
            AudioManager.instance.PlaySound(reloadSound);
            reloadRoutine = StartCoroutine(ReloadCoroutine());
        }
    }


    public float GetKnockback()
    {
        return knockback;
    }

    public Vector2 GetDirection()
    {
        return shootingDirection;
    }

    public int GetAmmoLeft()
    {
        return currentAmmo;
    }

    public float GetReloadPercentage()
    {
        return (reloadTimer / reloadTime) * 100f;
    }

    public bool IsReloading()
    {
        return reloading;
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

    private Vector2 GetShootingDirection()
    {
        return Mouse.GetVectorToMouse(gunTip.position).normalized;
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
        return shootingCooldownElapsed && hasEnoughAmmo && equipped && !reloading;
    }

    private bool CanReload()
    {
        return !reloading && currentAmmo < maxAmmo;
    }

    private void CancelReload()
    {
        if (reloadRoutine != null)
        {
            StopCoroutine(reloadRoutine);
            AudioManager.instance.StopSound(reloadSound);
            reloadRoutine = null;
            reloading = false;
            reloadTimer = 0;
        }
    }


    private IEnumerator ReloadCoroutine()
    {
        reloading = true;
        reloadTimer = 0;
        while (reloadTimer < reloadTime)
        {
            reloadTimer += Time.deltaTime;
            yield return null;
        }

        reloadTimer = reloadTime;
        reloading = false;
        ResetAmmo();
        reloadRoutine = null;
    }

    private void ResetAmmo()
    {
        currentAmmo = maxAmmo;
    }


    private void OnDrawGizmosSelected()
    {
        DrawGizmos();
    }

    protected virtual void DrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(gunTip.position, range);
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(gunTip.position, mouseDeadZone);
    }
}
