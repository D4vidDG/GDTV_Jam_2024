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
        Vector2 target = GetTargetPosition();
        vectorToTarget = (target - (Vector2)transform.position);
        shootingTimer += Time.deltaTime;
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
        projectile.OnTargetHit += OnTargetHit;
    }

    private void OnTargetHit(Health target)
    {
        Debug.Log("target hit:" + target.gameObject.name);
        target.TakeDamage(damage);
    }

    private bool CanFire()
    {
        float timeToShoot = 1 / fireRate;
        bool hasEnoughAmmo = currentAmmo > 0;
        bool shootingCooldownElapsed = shootingTimer >= timeToShoot;
        return shootingCooldownElapsed && hasEnoughAmmo;
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
        Gizmos.DrawWireSphere(transform.position, range);
    }
}