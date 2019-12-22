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
    //캐릭터 선택 카드
    public GameObject characterCard;

    //플레이어가 가지고 있는 캐릭터
    public CharacterInfo[] characterInfo;

    private List<GameObject> slots = new List<GameObject>();


    private void OnEnable()
    {
        for (int i = 0; i < characterInfo.Length; i++)
        {
            GameObject slot = Instantiate(characterCard, content.transform);
            slot.GetComponent<CharacterSlot>().AddCharacter(characterInfo[i]);
            slots.Add(slot);

        }
    }

    //창을 다시키면 이미지가 이상해지는 버그가 있으므로 파괴하고 다시 생성한다.
    private void OnDisable()
    {
        foreach (var slot in slots)
        {
            Destroy(slot);
        }
        slots.Clear();
    }

}
