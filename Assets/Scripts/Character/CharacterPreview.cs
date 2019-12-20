using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

/// <summary>
/// 로비화면에서 캐릭터모델을 띄우는 클래스
/// </summary>
public class CharacterPreview : MonoBehaviour
{ 
    [Header("캐릭터가 나타날 위치")]
    public Transform placeHolder;

    //이 CharacterPreview 가 나타낼 모델 (CharacterPreview 은 게임옵션 판넬, 캐릭터스탯판넬 해서 총 두개있음)
    public GameObject currentModel { get; set; }

    private GameObject modelInstance;


    private void OnEnable()
    {
        StartCoroutine(SetPreview());
    }

    private void OnDisable()
    {
        ClearPreview();
    }

    //characterStatsPanel의 characterPalceHodler에 캐릭터 모델을 생성한다.
    private IEnumerator SetPreview()
    {
        //정보의 동기화 시점이 안맞아서 강제로 잠시 대기..(좋은 방법은 아님)
        yield return new WaitForSeconds(0.05f);
        modelInstance = Instantiate(currentModel, placeHolder.position, placeHolder.rotation);

    }

    //창을 닫을 때 캐릭터 모델을 제거한다.
    private void ClearPreview()
    {
        if (modelInstance != null)
            Destroy(modelInstance.gameObject);
        modelInstance = null;

    }
}
