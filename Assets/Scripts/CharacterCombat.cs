using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void Attack(CharacterStats _target, int _damage)
    {
        if (timeStamp <= Time.time)
        {
            //어택 에애니메이션 실행
            Debug.Log(gameObject.name + "Attaked" + _target.gameObject.name);
            _target.TakeDamage(_damage);
            timeStamp = Time.time + attackCoolDown;
        }
        


    }
}
