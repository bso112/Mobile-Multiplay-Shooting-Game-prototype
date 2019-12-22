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

    /// <summary>
    /// SetPreview에서 생성할 캐릭터 모델을 받는 시점을 정하기 위해 사용된다.
    /// </summary>
    public System.Action onEnable;

    private void OnEnable()
    {
        onEnable?.Invoke();
        SetPreview();
        onEnable = null;
    }

    private void OnDisable()
    {
        ClearPreview();
    }

    //characterStatsPanel의 characterPalceHodler에 캐릭터 모델을 생성한다.
    private void SetPreview()
    {
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
