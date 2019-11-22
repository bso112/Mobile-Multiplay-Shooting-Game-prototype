using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(PhotonView))]
public class MasterClientAgent : MonoBehaviour
{
    PhotonView view;

    private void Awake()
    {
        view = GetComponent<PhotonView>();
    }
    // Start is called before the first frame update
    void Start()
    {
        view.TransferOwnership(PhotonNetwork.MasterClient);
    }

    public static void DestroyRequestToMaster(GameObject obj)
    {
        PhotonNetwork.Destroy(obj);
    }

}
