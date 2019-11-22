using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Coin : MonoBehaviour
{
    //public float flySpeed;
    //public float destroyDistance;
    //public GameObject destroyEffect;
    private PhotonView view;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            MasterClientAgent.DestroyRequestToMaster(gameObject);
        }
    }
    private void Start()
    {
        view = GetComponent<PhotonView>();
    }


  



    //가까운 플레이어로 이동하고, destroyDistance만큼 가까워지면 스스로를 파괴한다.
    //IEnumerator MoveToTargetAndDestory(Vector3 position, float speed)
    //{
    //    while (Vector3.Distance(position, transform.position) > destroyDistance)
    //    {
    //        transform.position = Vector3.Slerp(transform.position, position, speed * Time.deltaTime);
    //        yield return null;
    //    }

    //    ParticleSystem particle = PhotonNetwork.Instantiate(destroyEffect.name, transform.position, Quaternion.identity).GetComponent<ParticleSystem>();
    //    RPC_helper.DestroyRequestToMaster(view, gameObject);

    //}
}
