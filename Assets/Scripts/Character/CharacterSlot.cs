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

    //이 오브젝트에 붙어있는 버튼 컴포넌트
    private Button btn_self;

    private void Start()
    {
        GameObject characterStatsPanel = GameObject.Find("Canvas").transform.Find("CharacterStatsPanel").gameObject;
        statsUI = characterStatsPanel.GetComponent<CharacterStatsUI>();
        preview = characterStatsPanel.GetComponent<CharacterPreview>();
        btn_self = GetComponent<Button>();
        btn_self.onClick.AddListener(()=>{ NetworkManager.instance.ShowOnlyOnePanel("CharacterStatsPanel"); });
    }
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

    //클릭된 슬롯만이 등록한다.(슬롯의 onclick이벤트에 연결함)
    public void SubscribePreviewEnable()
    {
        preview.onEnable += OnSlotClicked;
    }

    /// <summary>
    /// 
    /// </summary>
    public void OnSlotClicked()
    {
        //스탯UI와 priview에 슬롯에 있는 캐릭터 정보를 넘겨준다.
        statsUI.SetInfo(character);
        preview.currentModel = character.model;
    }

}
