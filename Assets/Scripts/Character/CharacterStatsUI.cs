using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 외부에서 characterInfo를 받아 캐릭터 스탯을 UI에 보여주는 클래스
/// </summary>
public class CharacterStatsUI : MonoBehaviour
{
    private CharacterInfo character;
    public new Text name;
    public Text maxHP;
    public Text attack;
    public Text specialAttack;
    public Text occupation;
    public Text description;
    public Text level;
    public Text currentEXP;
    public Text maxEXP;



    //캐릭터의 스탯을 보여준다.
    public void SetInfo(CharacterInfo _character)
    {
        character = _character;
        name.text = character.name;
        maxHP.text = character.Maxhp.GetValue().ToString();
        occupation.text = character.occupation;
        description.text = character.description;
        level.text = character.currentLevel.ToString();
        currentEXP.text = character.currentEXP.ToString();
        maxEXP.text = "/ " + character.maxEXP.ToString();
        attack.text = character.attack.GetValue().ToString();
        specialAttack.text = character.specialAttack.GetValue().ToString();
    }


    //캐릭터가 최종적으로 선택됬을때 네트워크 매니저에게 선택된 characterInfo를 넘겨준다.
    public void OnSelectBtnClicked()
    {
        NetworkManager.instance.SetCharacter(character);
        GameObject.Find("Canvas").transform.Find("GameOptionPanel").GetComponent<CharacterPreview>().currentModel = character.model;
    }
}
