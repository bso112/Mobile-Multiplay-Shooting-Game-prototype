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

    private void Awake()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            this.enabled = false;
            return;
        }

        if (Instance != null)
        {
            Debug.Log("more than one ScoreManager");
            return;
        }
        Instance = this;

    }

    private PhotonView view;
    private TeamManager teamMgr;

    #endregion
    // Start is called before the first frame update
    void Start()
    {

        teamMgr = TeamManager.Instance;
        
    }

    /// <summary>
    /// 팀의 스코어를 1 올리고 모든 인스턴스의 스코어텍스트를 업데이트함
    /// </summary>
    /// <param name="team"></param>
    public void AddScore(int team)
    {
        if (team == 0)
            ATeamScore++;
        else
            BTeamScore++;

        teamMgr.UpdateScoreRPC(ATeamScore, BTeamScore);

        
    }

   
}
