using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStatsUI : MonoBehaviour
{
    private CharacterInfo character;
    public Text name;
    public Text maxHP;
    public Text attack;
    public Text specialAttack;
    public Text occupation;
    public Text description;
    public Text level;
    public Text currentEXP;
    public Text maxEXP;




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

    public void OnSelectBtnClicked()
    {
        NetworkManager.instance.SetCharacter(character);
        NetworkManager.instance.gameOptionPanel.GetComponent<CharacterPreview>().currentModel = character.model;
    }
}
