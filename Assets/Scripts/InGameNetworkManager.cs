using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class GameManager : MonoBehaviourPunCallbacks
{
    //플레이어 스폰

    public GameObject playerPrefabs;
    public GameObject playerUI;

    public int team;
    public Transform[] spwanPos;

    private void Start()
    {
    }

}
