using System;
using UnityEngine;
class HitScanBullet
{
    // public Bullet()
    // {

    // }

    // public void Shoot(Vector2 origin, Vector2 shootingDirection, float distance, int penetration, Action<Health> OnTargetHit)
    // {
    //     RaycastHit2D[] hits = Physics2D.RaycastAll(
    //         origin,
    //         shootingDirection,
    //         distance,
    //         Physics.AllLayers);

    //     if (hits.Length < 1) return;
    //     Debug.Log("hits:" + hits.Length);


    //     int hitsChecked = 0;
    //     int targetsHit = 0;
    //     while (targetsHit < penetration && hitsChecked < hits.Length)
    //     {
    //         RaycastHit2D hit = hits[hitsChecked];
    //         Health target = hit.collider.GetComponentInChildren<Health>();
    //         if (target != null)
    //         {
    //             OnTargetHit(target);
    //             targetsHit++;
    //         }
    //         hitsChecked++;
    //     }
    // }
}