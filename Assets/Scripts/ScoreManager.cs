using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

/// <summary>
/// 게임의 점수를 관리함. 모든 게임 인스턴스에 하나만 존재함.(마스터 클라이언트)
/// </summary>
public class ScoreManager : MonoBehaviour
{
    #region Singleton
    public static ScoreManager Instance { get; private set; }

    public int ATeamScore { get; private set; }
    public int BTeamScore { get; private set; }

    private PhotonView view;
    private ScoreTextManager scoreTextMgr;

    public System.Action onScoreChanged;

    //아군에게만 적용되는 효과를 구현하기 위한 장치
    public int HomeTeam { get; private set; }


    private void Awake()
    {


        if (Instance != null)
        {
            Debug.Log("more than one ScoreManager");
            return;
        }
        Instance = this;


    }


    #endregion
    // Start is called before the first frame update
    void Start()
    {
        scoreTextMgr = ScoreTextManager.Instance;
        view = GetComponent<PhotonView>();

        ExitGames.Client.Photon.Hashtable properties = PhotonNetwork.LocalPlayer.CustomProperties;
        HomeTeam = (int)properties["team"];
        Debug.Log("홈팀은" + HomeTeam);

    }

    /// <summary>
    /// 팀의 스코어를 1 올리고 모든 인스턴스의 스코어텍스트를 업데이트함
    /// </summary>
    /// <param name="team"></param>
    public void AddScore(int team)
    {   
        
        view.RPC("AddScoreRPC", RpcTarget.AllBuffered, team);

    }

    [PunRPC]
    public void AddScoreRPC(int team)
    {
        if (team == 0)
            ATeamScore++;
        else
            BTeamScore++;

        scoreTextMgr.UpdateScore(ATeamScore, BTeamScore);

        onScoreChanged?.Invoke();
    }

   
}
