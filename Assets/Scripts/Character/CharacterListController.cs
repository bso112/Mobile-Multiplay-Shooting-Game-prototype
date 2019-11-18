using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterListController : MonoBehaviour
{
    //캐릭터 프로필들의 부모오브젝트
    public GameObject content;

    //플레이어가 가지고 있는 캐릭터
    public CharacterInfo[] characterInfo;

    List<CharacterSlot> characterSlots = new List<CharacterSlot>();

    private void OnEnable()
    {
        int i = 0;
        foreach (var slot in content.GetComponentsInChildren<CharacterSlot>())
        {
            if (i < characterInfo.Length)
                slot.AddCharacter(characterInfo[i++]);
        }
    }

}
