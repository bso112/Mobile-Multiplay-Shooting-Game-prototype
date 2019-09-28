using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [HideInInspector]
    //슈터로부터 사용자 스텟정보를 받아야함.(데미지 적용을 위함)
    public CharacterStats ownerStats;
    [HideInInspector]
    protected Transform owner;

    [Header("기본공격에 붙는 추가데미지")]
    public float damage;

    void Start()
    {
        if(ownerStats != null)
            owner = ownerStats.gameObject.transform;
    }

    

}
