using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 캐릭터를 선택하는 화면에서 캐릭터를 나타내는 슬롯 하나를 관리하는 클래스
/// </summary>
public class CharacterSlot : MonoBehaviour
{
    private CharacterInfo character;
    public CharacterStatsUI statsUI;
    public CharacterPreview preview;
    public Image portrait;
    public Text name;
    public Text level;
    public Text currentEXP;
    public Text maxEXP;

    /// <summary>
    /// 슬롯에 캐릭터를 나타낸다.
    /// </summary>
    /// <param name="_character"></param>
    public void AddCharacter(CharacterInfo _character)
    {
        character = _character;
        portrait.sprite = character.portrait;
        name.text = character.name;
        level.text = character.currentLevel.ToString();
        currentEXP.text = character.currentEXP.ToString();
        maxEXP.text = "/ " + character.maxEXP.ToString();
    }

    /// <summary>
    /// 슬롯을 비운다
    /// </summary>
    public void ClearSlot()
    {
        character = null;
        portrait.sprite = null;
        name.text = "";
        level.text = "";
        currentEXP.text = "";
        maxEXP.text = "";
    }

    /// <summary>
    /// 슬롯이 클릭됬을때. 오브젝트 이벤트에 등록되어있다.
    /// </summary>
    public void OnSlotClicked()
    {
        statsUI.SetInfo(character);
        preview.currentModel = character.model;
    }

}
