using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using System.IO;

public class GameManager : MonoBehaviour
{

    #region Singleton
    public static GameManager Instance { get; private set; }

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

    [Header("mathTablePanel 관련")]
    public GameObject matchTablePanel;

    public Sprite[] portraits;

    public GameObject[] A_TeamProfiles;
    private Text[] A_TeamNames;
    private Image[] A_TeamPortraits;

    public GameObject[] B_TeamProfiles;
    private Image[] B_TeamPortraits;
    private Text[] B_TeamNames;

    //A팀과 B팀의 스폰 포인트
    public Transform[] A_spawnPoints;
    public Transform[] B_spawnPoints;

    public int HomeTeam { get; private set; }

    private PhotonView photonView;
    private Dictionary<string, Sprite> profileDic = new Dictionary<string, Sprite>();

    //게임관련 필드
    [HideInInspector]
    public int HomeScore;
    public Text homeScoreText;
    [HideInInspector]
    public int AwayScore;
    public Text awayScoreText;

    [Header("게임 러닝타임(초)")]
    [SerializeField]
    private float GameTimeLimit;
    public float CurrentGameTime { get; private set; }

    private bool isHomeWin;
    private bool timeOut;
    private bool gotTenCoin;

    //Coin 스크립트에서 인보크함.
    public System.Action OnScoreUpdated;

    
    private void Start()
    {
        ObjectPooler.instance.OnObjectPoolReady += InitGame;
#if UNITY_EDITOR
        //PhotonNetwork.OfflineMode = true;
#endif
    }

    private void InitGame()
    {
        Debug.Log("initGame");
        //프로필을 셋팅한다. (순서대로 들어가지 않음)
        foreach (GameObject A_Profile in A_TeamProfiles)
        {
            A_TeamPortraits = A_Profile.GetComponentsInChildren<Image>();
            A_TeamNames = A_Profile.GetComponentsInChildren<Text>();
        }

        foreach (GameObject B_Profile in B_TeamProfiles)
        {
            B_TeamPortraits = B_Profile.GetComponentsInChildren<Image>();
            B_TeamNames = B_Profile.GetComponentsInChildren<Text>();
        }

        foreach (var profile in portraits)
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

            //로컬 플레이어를 팀에 따라 위치를 정해 스폰한다.
            if ((int)properties["team"] == 0)
            {
                A_index = Mathf.Clamp(A_index, 0, A_spawnPoints.Length);
                GameObject player = PhotonNetwork.Instantiate((string)properties["character"], A_spawnPoints[A_index++].position, Quaternion.identity);
                player.GetComponent<PlayerSetup>().SetTeamRPC(0);
                HomeTeam = 0;
            }
            else
            {
                B_index = Mathf.Clamp(B_index, 0, A_spawnPoints.Length);
                GameObject player = PhotonNetwork.Instantiate((string)properties["character"], B_spawnPoints[B_index++].position, Quaternion.identity);
                player.GetComponent<PlayerSetup>().SetTeamRPC(1);
                HomeTeam = 1;
            }

            //매치 테이블 셋팅
            if (photonView.IsMine)
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


        OnScoreUpdated += EndGame;
        OnScoreUpdated += UpdateScoreUI;
    }


    private void Update()
    {
        CurrentGameTime += Time.deltaTime;

        if (CurrentGameTime >= GameTimeLimit)
        {
            timeOut = true;
        }

        if (HomeScore > 10 || AwayScore > 10)
        {
            gotTenCoin = true;
        }
    }

    private void UpdateScoreUI()
    {
        homeScoreText.text = HomeScore.ToString();
        awayScoreText.text = AwayScore.ToString();

    }


    private void EndGame()
    {
        if(timeOut)
        {
            if (HomeScore > AwayScore)
            {
                isHomeWin = true;
            }

            OnGameEnd();
        }
    }



    void OnGameEnd()
    {
        if (isHomeWin)
        {
            Debug.Log("HomeWin!");
            //승리 판넬 띄우고, 보상주기
            //OnHomeWin?.invoke(); ?? 따로 처리하는 걸 빼는게 좋으려나..
        
        }
        else
        {
            Debug.Log("AwayWin!");
            //패배 판넬 띄우기
            //OnAwayWin?.invoke(); ??
        }
    }


    //매치테이블 셋팅
    private void SetProfile()
    {
        int A_TeamProfileIndex = 0;
        int B_TeamProfileIndex = 0;
        foreach (var player in PhotonNetwork.PlayerList)
        {
            ExitGames.Client.Photon.Hashtable properties = player.CustomProperties;

            if ((int)properties["team"] == 0)
            {
                A_TeamProfileIndex = Mathf.Clamp(A_TeamProfileIndex, 0, A_TeamProfiles.Length);
                A_TeamNames[A_TeamProfileIndex].text = player.NickName;
                A_TeamPortraits[A_TeamProfileIndex++].sprite = profileDic[(string)properties["character"]];


            }
            else
            {
                B_TeamProfileIndex = Mathf.Clamp(B_TeamProfileIndex, 0, B_TeamProfiles.Length);
                B_TeamNames[B_TeamProfileIndex].text = player.NickName;
                B_TeamPortraits[B_TeamProfileIndex++].sprite = profileDic[(string)properties["character"]];
            }
        }
    }

}



