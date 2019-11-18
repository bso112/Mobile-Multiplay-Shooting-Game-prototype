using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;

public class PlayerSetup : MonoBehaviour
{

    public TextMeshProUGUI nameText;
    private PlayerMotor motor;
    private Shooter shooter;
    private PhotonView photonView;
    private FollowCam followCam;
    private ItemPickup pickup;
    [HideInInspector]
    public int Team { get; private set; }


    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
        motor = GetComponent<PlayerMotor>();
        shooter = GetComponent<Shooter>();
        followCam = Camera.main.transform.GetComponent<FollowCam>();

    }

    // Start is called before the first frame update
    void Start()
    {

        if (photonView.IsMine)
        {
            followCam.target = gameObject;
            Transform canvas = GameObject.Find("Canvas").transform;
            motor.joyStick = canvas.Find("Movement Joystick").GetComponent<Joystick>();
            canvas.Find("AttackButton").GetComponent<Button>().onClick.AddListener(shooter.OnShotButtonClicked);
            canvas.Find("UltiButton").GetComponent<Button>().onClick.AddListener(shooter.OnUltiButtonClicked);
            transform.tag = "LocalPlayer";
        }
        else
        {
            motor.enabled = false;
            shooter.enabled = false;
        }

        SetPlayerName();

        

    }

    void SetPlayerName()
    {
        nameText.text = photonView.Owner.NickName;
    }

    public void SetTeamRPC(int _team)
    {
        if (photonView == null)
            Debug.LogError("포톤 뷰가 없습니다");
        photonView.RPC("SetTeam", RpcTarget.AllBuffered, _team);
        Debug.Log(PhotonNetwork.LocalPlayer.NickName + "의 팀은" + Team);
    }

    [PunRPC]
    private void SetTeam(int _team)
    {
        Team = _team;
    }
}
