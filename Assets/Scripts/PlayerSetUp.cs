using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class PlayerSetup : MonoBehaviour
{

    private PlayerMotor motor;
    private Shooter shooter;
    private PhotonView photonView;


    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        motor = GetComponent<PlayerMotor>();
        shooter = GetComponent<Shooter>();

        if (photonView.IsMine)
        {
            Transform canvas = GameObject.Find("Canvas").transform;
            motor.joyStick = canvas.Find("Movement Joystick").GetComponent<Joystick>();
            canvas.Find("AttackButton").GetComponent<Button>().onClick.AddListener(shooter.OnShotButtonClicked);
            canvas.Find("UltiButton").GetComponent<Button>().onClick.AddListener(shooter.OnUltiButtonClicked);
        }
        else
        {
            motor.enabled = false;
            shooter.enabled = false;
        }

        

    }

}
