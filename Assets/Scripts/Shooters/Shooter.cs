using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public abstract class Shooter : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform shotPos;
    [Header("공격 당 발사체 수")]
    public float firePerClick = 1f;
    [Header("공격간의 딜레이")]
    public float fireCoolDown;
    [Header("발사간의 딜레이")]
    public float shootDelay = 0.2f;
    [Header("ParabolaShooter에서는 안씀")]
    public float shotPower = 1000;


    protected GameObject currentProjectile;
    protected ObjectPooler pooler;
    protected CharacterStats selfStats;
    private float timeStamp;

    private void Start()
    {
        pooler = ObjectPooler.instance;
        selfStats = GetComponent<CharacterStats>();
    }

    //멀티플레이 할 때 이건 동적으로 할당해야한다.
    public void OnShotButtonClicked()
    {
        if(timeStamp <= Time.time)
        {
            StartCoroutine(Shoot());
            timeStamp = Time.time + fireCoolDown;
        }
        
    }

    protected abstract IEnumerator Shoot();





}
