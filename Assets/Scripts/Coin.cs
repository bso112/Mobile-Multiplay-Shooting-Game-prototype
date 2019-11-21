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
        Debug.Log(other.name + "과 코인이 부딪힘");
        if (other.tag.Equals("Player"))
        {
            Debug.Log("코인 사라짐");
            RPC_helper.DestroyRequestToMaster(view, gameObject);
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
