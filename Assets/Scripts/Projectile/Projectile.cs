using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    protected CharacterStats ownerStats;
    protected Transform owner;
    private GameManager gm;

    [Header("기본공격에 붙는 추가데미지")]
    [SerializeField]
    protected float damage;

    void Start()
    {
        gm = GameManager.Instance;
        owner = gm.localPlayer.transform;
        ownerStats = gm.localPlayer.GetComponent<CharacterStats>();
    }

    

}
