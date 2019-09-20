using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using System.IO;

public class GameManager : MonoBehaviour
{

    public GameObject matchTablePanel;

    public Sprite[] profiles;
    public Image[] A_TeamProfiles;
    public Image[] B_TeamProfiles;
    //A팀과 B팀의 스폰 포인트
    public Transform[] A_spawnPoints;
    public Transform[] B_spawnPoints;

    private PhotonView photonView;
    private Dictionary<string, Sprite> profileDic = new Dictionary<string, Sprite>();

    // Start is called before the first frame update
    void Start()
    {

        foreach(var profile in profiles)
        {
            profileDic.Add(profile.name, profile);
        }

        //네트워크가 준비되면
        if (PhotonNetwork.IsConnectedAndReady)
        {
            
            photonView = GetComponent<PhotonView>(); 

            //캐릭터 스폰
            int A_index = 0;
            int B_index = 0;
            ExitGames.Client.Photon.Hashtable properties = PhotonNetwork.LocalPlayer.CustomProperties;
            if ((int)properties["team"] == 0)
            {
                A_index = Mathf.Clamp(A_index, 0, A_spawnPoints.Length);
                PhotonNetwork.Instantiate("Characters/" + (string)properties["character"], A_spawnPoints[A_index++].position, Quaternion.identity);
            }
            else
            {
                B_index = Mathf.Clamp(B_index, 0, A_spawnPoints.Length);
                PhotonNetwork.Instantiate("Characters/" + (string)properties["character"], B_spawnPoints[B_index++].position, Quaternion.identity);
            }

            //매치 테이블 셋팅
            if(photonView.IsMine)
            {
                SetProfile();
            }


        }
        else
        {
            Debug.Log("not ready");
        }

        //3초후 게임시작하는 걸 가정함.
        Destroy(matchTablePanel, 3f);

    }

    void SetProfile()
    {
        int A_TeamProfileIndex = 0;
        int B_TeamProfileIndex = 0;
        foreach(var player in PhotonNetwork.PlayerList)
        {
            ExitGames.Client.Photon.Hashtable properties = player.CustomProperties;

            if((int)properties["team"] == 0)
            {
                A_TeamProfileIndex = Mathf.Clamp(A_TeamProfileIndex, 0, A_TeamProfiles.Length);
                A_TeamProfiles[A_TeamProfileIndex++].sprite = profileDic[(string)properties["character"]];

            }
            else
            {
                B_TeamProfileIndex = Mathf.Clamp(B_TeamProfileIndex, 0, B_TeamProfiles.Length);
                B_TeamProfiles[B_TeamProfileIndex++].sprite = profileDic[(string)properties["character"]];
            }
        }
    }

}



