using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ParabolaShooter : Shooter
{


    protected override IEnumerator Shoot(GameObject _projectilePrefab)
    {

        GameObject projectile = PhotonNetwork.Instantiate(_projectilePrefab.name, shotPos[0].position, Quaternion.LookRotation(transform.forward));
        if(projectile == null)
        {
            Debug.LogError("프로젝타일이 null임");
        }
        yield return new WaitForSeconds(shootDelay);

    }

    protected override IEnumerator Ultimate(GameObject _projectilePrefab)
    {
        StartCoroutine(Shoot(_projectilePrefab));
        yield return null;
    }
}
