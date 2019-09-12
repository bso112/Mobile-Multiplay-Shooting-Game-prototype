using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{

    public Stat Maxhp;
    public Stat sp;
    public Stat attack;
    public Stat armor;
    //공격 리치
    public Stat range;
    public Stat attackSpeed;

    int currentHP;

    private void Start()
    {
        currentHP = Maxhp.GetValue();
    }


    public void TakeDamage(int damage)
    {
        //맞는 애니메이션 실행
        damage -= armor.GetValue();
        damage = Mathf.Clamp(damage, 0, damage);
        currentHP -= damage;
        Debug.Log(gameObject.name + "이" + damage +"의 데미지를 받았습니다.");
        if(currentHP <= 0)
        {
            Die();
        }

        

    }

    protected virtual void Die()
    {
        Debug.Log(gameObject.name + "이 죽었습니다!");
    }
}
