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
    float shootingTimer = 0;
    Camera mainCamera;
    GameObject player;

    void Awake()
    {
        mainCamera = Camera.main;
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        Vector2 target = GetTargetPosition();
        vectorToTarget = (target - (Vector2)transform.position);
        shootingTimer += Time.deltaTime;
    }

    public void Fire()
    {
        if (CanFire())
        {
            shootingTimer = 0;
            Fire(vectorToTarget);
        }
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
        return shootingTimer >= timeToShoot;
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
