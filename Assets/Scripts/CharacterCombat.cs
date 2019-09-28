using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CharacterCombat : MonoBehaviour
{

    private CharacterStats selfStats;
    public float attackCoolDown;
    private float timeStamp;


    // Start is called before the first frame update
    void Start()
    {
        selfStats = GetComponent<CharacterStats>();
        attackCoolDown = selfStats.attackSpeed.GetValue();
    }

    //이 로직은 슈팅게임에 적합하지 않음.. 사용하지말자 일단.
    public void Attack(CharacterStats _target, float _damage)
    {
        if (timeStamp <= Time.time)
        {
            //어택 에애니메이션 실행


            Debug.Log(gameObject.name + "Attaked" + _target.gameObject.name);
            _target.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.AllBuffered, _damage);
            timeStamp = Time.time + attackCoolDown;
        }
        


    }
}
