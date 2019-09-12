using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class Shooter : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform shotPos;
    public float shotPower = 1000;

    private ObjectPooler pooler;
    private Rigidbody projectileRB;
    private CharacterStats selfStats;
    private void Start()
    {
        pooler = ObjectPooler.instance;
        selfStats = GetComponent<CharacterStats>();
    }

    public void OnShotButtonClicked()
    {
        //발사체 스폰
        GameObject projectile = pooler.SpawnFromPool(projectilePrefab.name, shotPos.position, Quaternion.Euler(transform.forward));
        //발사체에게 플레이어 정보 넘겨주기
        projectile.GetComponent<Projectile>().ownerStats = selfStats;
        //발사
        projectileRB = projectile.GetComponent<Rigidbody>();
        projectileRB.AddForce(transform.forward * shotPower, ForceMode.Force);

    }





}
