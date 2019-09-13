using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush : MonoBehaviour
{

    MeshRenderer meshRenderer;
    //나중에 멀티플레이용으로 바꾸기.

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            meshRenderer.material.color = ModifyAlpha(meshRenderer, 0.3f);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {

            meshRenderer.material.color = ModifyAlpha(meshRenderer, 1f);
        }
    }


    Color ModifyAlpha(Renderer renderer, float alpha)
    {
        Color newColor = renderer.material.color;
        newColor.a = alpha;
        return newColor;

    }

}
