using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightShooter : Shooter
{


    protected override IEnumerator Shoot(GameObject projectilePrefab)
    {
        for (int i = 0; i < firePerClick; i++)
        {
            //발사체 스폰
            GameObject projectile = pooler.SpawnFromPool(projectilePrefab.name, shotPos[0].position, Quaternion.Euler(transform.forward));
            //발사체에게 플레이어 정보 넘겨주기
            projectile.GetComponent<Projectile>().ownerStats = ownerStats;
            //발사
            Rigidbody projectileRB = projectile.GetComponent<Rigidbody>();
            projectileRB.AddForce(transform.forward * shotPower, ForceMode.Force);
            //연속 발사 사이에 텀을 둔다. 
            yield return new WaitForSeconds(shootDelay);
        }
    }

    protected override IEnumerator Ultimate(GameObject projectilePrefab)
    {
        throw new System.NotImplementedException();
    }
}
