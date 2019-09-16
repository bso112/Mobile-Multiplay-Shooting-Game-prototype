using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParabolaShooter : Shooter
{


    protected override IEnumerator Shoot(GameObject projectilePrefab)
    {

        GameObject projectile = pooler.SpawnFromPool(projectilePrefab.name, shotPos[0].position, Quaternion.identity);
        projectile.GetComponent<Projectile>().ownerStats = ownerStats;
        ParabolaController controller = projectile.GetComponent<ParabolaController>();
        controller.ParabolaRoot = transform.Find("ParabolaRoots").gameObject;
        yield return new WaitForSeconds(0.1f); //파라볼라루트를 할당하는데 시간이 좀 걸림
        controller.FollowParabola();
        yield return new WaitForSeconds(shootDelay);

    }

    protected override IEnumerator Ultimate(GameObject projectilePrefab)
    {
        throw new System.NotImplementedException();
    }
}
