using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 캐릭터 선택화면에서 각 슬롯에 캐릭터를 배치하는 클래스
/// </summary>
public class CharacterListController : MonoBehaviour
{
    //캐릭터 프로필들의 부모오브젝트
    public GameObject content;

    //플레이어가 가지고 있는 캐릭터
    public CharacterInfo[] characterInfo;


    private void OnEnable()
    {
        CharacterSlot[] slots = content.GetComponentsInChildren<CharacterSlot>();

        for (int i =0; i<characterInfo.Length; i++)
        {
            slots[i].AddCharacter(characterInfo[i]);
        }

    }

}
