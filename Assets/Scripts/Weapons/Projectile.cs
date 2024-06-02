
using System;
using ExtensionMethods;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float startSpeed;
    [SerializeField] int penetration;
    [SerializeField] GameObject model;
    [SerializeField] LayerMask enemiesLayer;
    [SerializeField] LayerMask obstaclesLayer;

    Rigidbody2D rigidBody;
    Collider2D collider2d;

    bool launched;
    int targetsHit;
    float maxTravelingDistance;
    Vector2 startPosition;

    public Action<Health> OnTargetHit;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        collider2d = GetComponent<Collider2D>();
        collider2d.enabled = false;
        EnableModel(false);
    }

    private void Start()
    {
        targetsHit = 0;
        startPosition = transform.position;
    }

    private void Update()
    {
        float distanceTraveled = Vector2.Distance(startPosition, transform.position);
        if (maxTravelingDistance < distanceTraveled)
        {
            Destroy();
        }
    }

    public void Launch(Vector2 direction, float maxDistance)
    {
        launched = true;
        EnableModel(true);
        collider2d.enabled = true;
        rigidBody.velocity = direction.normalized * startSpeed;
        transform.right = direction.normalized;
        maxTravelingDistance = maxDistance;
    }

    protected void EnableModel(bool enable)
    {
        model.SetActive(enable);
    }

    protected bool IsShoot()
    {
        return launched;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (LayerMaskExtensions.IsInLayerMask(enemiesLayer, other.gameObject)
                && other.TryGetComponent<Health>(out Health health))
        {
            OnTargetHit?.Invoke(health);
            targetsHit++;
        }

        if (targetsHit >= penetration ||
                LayerMaskExtensions.IsInLayerMask(obstaclesLayer, other.gameObject))
        {
            Destroy();
        }
    }

    private void Destroy()
    {
        Destroy(this.gameObject);
    }
}
