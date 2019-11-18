using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Character", menuName = "Character")]
public class CharacterInfo : ScriptableObject
{

    public string name;
    public GameObject model;
    //클래스(직업)
    public string occupation;
    public string description;
    public int currentLevel;
    public int currentEXP;
    public int maxEXP;
    public Stat Maxhp;
    //탄당 피해량
    public Stat attack;
    public Stat specialAttack;
    //공격 리치
    public Stat range;
    public Stat attackSpeed;
    public Sprite portrait;

    //sp는 다 똑같이 하고 차는 속도만 다르게한다.
    //armor는 삭제하고 hp와 탄당 피해량만으로 가자.


}
