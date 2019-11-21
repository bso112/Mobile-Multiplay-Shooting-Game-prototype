using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RPC_helper : MonoBehaviour
{
    /// <summary>
    /// 마스터클라이언트에게 오브젝트 파괴를 요청한다.
    /// </summary>
    /// <param name="view"></param>
    /// <param name="obj"></param>
    public static void DestroyRequestToMaster(PhotonView view, GameObject obj)
    {
        if (!PhotonNetwork.IsMasterClient)
            view.RPC("DestroyObjOnMaster", RpcTarget.MasterClient, obj);
        else
        {
            PhotonNetwork.Destroy(obj);
        }
    }

    [PunRPC]
    private void DestroyObjOnMaster(GameObject obj)
    {
        PhotonNetwork.Destroy(obj);
    }

    
}
