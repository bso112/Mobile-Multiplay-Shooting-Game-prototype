using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class GameResultManager : MonoBehaviour
{

    [HideInInspector]
    private int winner;

    //R은 레드팀, B는 블루팀
    [SerializeField]
    private Transform[] R_spawnPoints;
    [SerializeField]
    private Transform[] B_spawnPoints;
    [SerializeField]
    private GameObject R_winUI;
    [SerializeField]
    private GameObject B_winUI;
    [SerializeField]
    private Text[] R_nameTexts;
    [SerializeField]
    private Text[] B_nameTexts;



    // Start is called before the first frame update
    void Start()
    {
        
        //오브젝트 풀을 디폴트 풀로 바꿔놓는다.
        PhotonNetwork.PrefabPool = new DefaultPool();

        winner = DataPipeline.Instance.winner;

        //각 플레이어의 캐릭터를 스폰한다.
        int A_index = 0;
        int B_index = 0;

        //A팀과 B팀 플레이어 이름을 지정할 텍스트 컴포넌트를 나타내기 위한 인덱스
        int i = 0;
        int j = 0;


        foreach (Player player in PhotonNetwork.PlayerList)
        {

            Hashtable properties = player.CustomProperties;

            Debug.Log("플레이어 이름: " + player.NickName + "팀 :" + (int)properties["team"]);
            //0이면 레드팀, 1이면 블루팀
            if ((int)properties["team"] == 0)
            {   
                //플레이어가 나가도 안없어지게 씬오브젝트로 설정
                if(PhotonNetwork.IsMasterClient)
                    PhotonNetwork.InstantiateSceneObject((string)properties["character"] + "Model", R_spawnPoints[A_index++].transform.position, R_spawnPoints[A_index].rotation);
                R_nameTexts[i++].text = player.NickName;
                
            }
            else
            {   
                if(PhotonNetwork.IsMasterClient)
                    PhotonNetwork.InstantiateSceneObject((string)properties["character"] + "Model", B_spawnPoints[B_index++].transform.position, B_spawnPoints[B_index].rotation);
                B_nameTexts[j++].text = player.NickName;
            }


        }

        

        //승자에게만 보여지는 UI를 표시한다.
        if (winner == 0)
        {
            R_winUI.SetActive(true);
            B_winUI.SetActive(false);
        }
        else
        {
            B_winUI.SetActive(true);
            R_winUI.SetActive(false);
        }

        //따로따로 로비 씬으로 나갈 수 있게 설정
        PhotonNetwork.AutomaticallySyncScene = false;

    }

    public void LoadLobby()
    {
        PhotonNetwork.LocalPlayer.CustomProperties.Clear();
        DataPipeline.Instance.Clear();
        PhotonNetwork.LoadLevel("Lobby");
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.LeaveRoom();
            Debug.Log("방을 나갑니다");
        }
        else
            Debug.Log("룸 밖에 있습니다.");
    }

}