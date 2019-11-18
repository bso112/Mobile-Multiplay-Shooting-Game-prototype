using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ItemPickup : MonoBehaviour
{
    //동전을 줍고, 게임매니저(마스터클라이언트)에게 스코어를 획득했음을 알린다.
    int score = 0;
    PlayerSetup setup;
    PhotonView view;


    private void Start()
    {
        setup = GetComponent<PlayerSetup>();
        view = GetComponent<PhotonView>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(view.IsMine)
        {
            Coin coin = other.GetComponent<Coin>();
            if (coin != null)
            {
                score++;
                OnPickCoinRPC();
            }
        }
        
    }

    /// <summary>
    /// 마스터클라이언트의 스코어매니저에게 자기 팀 점수를 올려달라고 요청함.
    /// </summary>
    private void OnPickCoinRPC()
    {
        //오직 실제 플레이어의 의사로 코인을 먹을때만 스코어를 올린다.
        if (PhotonNetwork.IsMasterClient && view.IsMine)
        {
            ScoreManager scoreMgr = ScoreManager.Instance;
            scoreMgr.AddScore(setup.Team);
        }
        else if (!PhotonNetwork.IsMasterClient && view.IsMine)
        {
            view.RPC("OnPickCoin", RpcTarget.MasterClient);
        }
        else
            return;
           
    }
    [PunRPC]
    private void OnPickCoin()
    {
        //마스터클라이언트 게임 인스턴스에 있는 플레이어의 분신이 실행한다.
        ScoreManager scoreMgr = ScoreManager.Instance;
        if(scoreMgr != null)
        {
            scoreMgr.AddScore(setup.Team);
            
        }
    }
}
