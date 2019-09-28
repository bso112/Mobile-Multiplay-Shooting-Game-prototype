using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public abstract class Shooter : MonoBehaviour
{
    public GameObject projectilePrefab;
    public GameObject specialProjectilePrefab;
    [Header("")]
    public Transform[] shotPos;
    [Header("한번 공격에 몇번 발사하는가")]
    public float firePerClick = 1f;
    [Header("공격간의 딜레이")]
    public float fireCoolDown;
    [Header("특수공격간의 딜레이")]
    public float UltiCoolDown;
    [Header("발사간의 딜레이")]
    public float shootDelay = 0.2f;
    [Header("발사체를 쏘는 힘")]
    public float shotPower = 1000;
    
    protected GameObject currentProjectile;
    protected ObjectPooler pooler;
    protected CharacterStats ownerStats;

    private float timeStampForAttack;
    private float timeStampForUlti;

    private void Start()
    {
        pooler = ObjectPooler.instance;
        ownerStats = GetComponent<CharacterStats>();
    }

    //자식에서 Start를 쓰면 부모의 Start가 실행이 안됨..
    protected void BaseStart()
    {
        pooler = ObjectPooler.instance;
        ownerStats = GetComponent<CharacterStats>();
    }

    //멀티플레이 할 때 이건 동적으로 할당해야한다.
    public void OnShotButtonClicked()
    {
        if(timeStampForAttack <= Time.time)
        {
            StartCoroutine(Shoot(projectilePrefab));
            timeStampForAttack = Time.time + fireCoolDown;
        }
        
    }

    public void OnUltiButtonClicked()
    {
        if (timeStampForUlti <= Time.time)
        {
            Debug.Log("궁극기!");
            StartCoroutine(Ultimate(specialProjectilePrefab));
            timeStampForUlti = Time.time + UltiCoolDown;
        }
    }

    protected abstract IEnumerator Shoot(GameObject projectilePrefab);

    protected abstract IEnumerator Ultimate(GameObject projectilePrefab);

   






}
