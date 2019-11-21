using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush : MonoBehaviour
{

    MeshRenderer meshRenderer;
    private TeamManager teamManager;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        teamManager = TeamManager.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            PlayerSetup setup = other.GetComponent<PlayerSetup>();
            //아군이면 부쉬 속에서 투명하게 보인다.              
            if (teamManager != null && teamManager.HomeTeam == setup.Team)
            {
                meshRenderer.material.color = ModifyAlpha(meshRenderer, 0.3f);
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            PlayerSetup setup = other.GetComponent<PlayerSetup>();

            //다시 원래 투명도로 바꿔줌
            if (teamManager.HomeTeam == setup.Team)
            {
                meshRenderer.material.color = ModifyAlpha(meshRenderer, 1f);
            }
        }
    }


    Color ModifyAlpha(Renderer renderer, float alpha)
    {
        Color newColor = renderer.material.color;
        newColor.a = alpha;
        return newColor;

    }

}
