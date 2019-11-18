using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPreview : MonoBehaviour
{
    [Header("캐릭터가 나타날 위치")]
    public Transform placeHolder;

    public GameObject currentModel { get; set; }

    private GameObject modelInstance;

    private void OnEnable()
    {
        SetPreview();
    }

    private void OnDisable()
    {
        ClearPreview();
    }

    private void SetPreview()
    {
        ClearPreview();
        if (currentModel != null)
            modelInstance = Instantiate(currentModel, placeHolder.position, placeHolder.rotation);
        else
            Debug.LogError("currentModel이 null임");
    }

    private void ClearPreview()
    {
        if(modelInstance != null)
            Destroy(modelInstance.gameObject);
        modelInstance = null;

    }
}
