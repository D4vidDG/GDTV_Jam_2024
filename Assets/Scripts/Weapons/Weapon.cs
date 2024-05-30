using System;
using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public bool controlEnabled;
    [SerializeField] WeaponType weaponType;
    [SerializeField] FirePattern firePattern;
    [SerializeField] float fireRate;
    [SerializeField] float reloadTime;
    [SerializeField] float range;
    [SerializeField] Projectile projectile;
    [SerializeField] Transform gunTip;
    [SerializeField] float mouseDeadZone;
    [SerializeField] SoundName fireSound, reloadSound;

    Vector2 shootingDirection;
    float shootingTimer;
    float reloadTimer;
    int currentAmmo;
    bool reloading;
    bool equipped;
    Coroutine reloadRoutine;

    WeaponStats myStats;

    float damage => myStats.GetStat(WeaponStat.Damage);
    float knockback => myStats.GetStat(WeaponStat.Knockback);
    int maxAmmo => (int)myStats.GetStat(WeaponStat.Ammo);

    private void Awake()
    {
        myStats = GetComponent<WeaponStats>();
    }

    private void Start()
    {
        controlEnabled = true;
        reloadTimer = 0;
        shootingTimer = 0;
        currentAmmo = maxAmmo;
        reloading = false;
    }

    private void OnEnable()
    {
        myStats.OnLevelUp += OnLevelUp;
    }

    private void OnDisable()
    {
        myStats.OnLevelUp -= OnLevelUp;
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
            Fire();
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

    public WeaponType GetWeaponType()
    {
        return weaponType;
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

    private void Fire()
    {
        shootingTimer = 0;
        AudioManager.instance.PlaySound(fireSound);
        Vector2[] firingDirections = firePattern.GetDirections(shootingDirection);
        foreach (Vector2 direction in firingDirections)
        {
            LaunchProjectle(direction);
        }
        currentAmmo--;
    }

    private void LaunchProjectle(Vector2 direction)
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



    private void OnLevelUp()
    {
        ResetAmmo();
    }

    private void ResetAmmo()
    {
        currentAmmo = maxAmmo;
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(gunTip.position, range);
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(gunTip.position, mouseDeadZone);
    }
}
