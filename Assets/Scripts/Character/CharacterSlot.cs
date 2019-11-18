using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSlot : MonoBehaviour
{
    private CharacterInfo character;
    public CharacterStatsUI statsUI;
    public Image portrait;
    public Text name;
    public Text level;
    public Text currentEXP;
    public Text maxEXP;

    public void AddCharacter(CharacterInfo _character)
    {
        character = _character;
        portrait.sprite = character.portrait;
        name.text = character.name;
        level.text = character.currentLevel.ToString();
        currentEXP.text = character.currentEXP.ToString();
        maxEXP.text = "/ " + character.maxEXP.ToString();
    }

    public void ClearSlot()
    {
        character = null;
        portrait.sprite = null;
        name.text = "";
        level.text = "";
        currentEXP.text = "";
        maxEXP.text = "";
    }

    public void OnSlotClicked()
    {
        statsUI.SetInfo(character);
        statsUI.transform.GetComponent<CharacterPreview>().currentModel = character.model;
    }

}
