using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// 점수를 UI에 표시함. 모든 게임인스턴스에 하나씩 존재함.
/// </summary>
public class TeamManager : MonoBehaviour
{
    
    #region Singleton
    public static TeamManager Instance { get; private set; }

    //아군에게만 적용되는 효과를 구현하기 위한 장치
    public int HomeTeam { get; private set; }

    //점수관련 필드
    [SerializeField]
    private Text ATeamScoreText;
    [SerializeField]
    private Text BTeamScoreText;

    private PhotonView view;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("more than one gameManager");
            return;
        }
        Instance = this;

    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        ExitGames.Client.Photon.Hashtable properties = PhotonNetwork.LocalPlayer.CustomProperties;
        HomeTeam = (int)properties["team"];
        view = GetComponent<PhotonView>();
    }

    /// <summary>
    /// 모든 인스턴스의 스코어 텍스트를 업데이트함
    /// </summary>
    /// <param name="ATeamScore"></param>
    /// <param name="BTeamScore"></param>
    public void UpdateScoreRPC(int ATeamScore, int BTeamScore)
    {
        view.RPC("UpdateScore", RpcTarget.AllBuffered, ATeamScore, BTeamScore);
    }
    #region RPCs
    [PunRPC]
    private void UpdateScore(int ATeamScore, int BTeamScore)
    {
        ATeamScoreText.text = ATeamScore.ToString();
        BTeamScoreText.text = BTeamScore.ToString();

    }
    #endregion

}
