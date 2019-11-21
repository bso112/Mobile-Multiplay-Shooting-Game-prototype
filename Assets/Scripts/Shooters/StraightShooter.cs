using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class StraightShooter : Shooter
{


    protected override IEnumerator Shoot(GameObject projectilePrefab)
    {
        for (int i = 0; i < firePerClick; i++)
        {
            //발사체 스폰
            GameObject projectile = PhotonNetwork.Instantiate(projectilePrefab.name, shotPos[0].position, Quaternion.Euler(transform.forward));
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
