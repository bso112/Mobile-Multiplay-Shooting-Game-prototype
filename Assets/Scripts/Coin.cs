using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Coin : MonoBehaviour
{
    public float flySpeed;
    public float destroyDistance;
    public GameObject destroyEffect;
    private GameManager gm;
    private PhotonView view;
   

    //private void Start()
    //{   
    //    view = GetComponent<PhotonView>();
    //    if(view.Owner != PhotonNetwork.MasterClient)
    //    {
    //        view.enabled = false;
    //    }
    //    gm = GameManager.Instance;
    //}

    //private void OnTriggerEnter(Collider other)
    //{   

    //    if(other.CompareTag("LocalPlayer"))
    //    {
    //        //로컬플레이어에게 먹힌 코인은 gm에게 알린다. gm은 자신의 갱신된 점수를 RPC한다.
    //        PlayerSetup setup = other.GetComponent<PlayerSetup>();
    //        if(setup != null)
    //        {
    //            //가까운 플레이어가 홈팀이면 홈팀 스코어를 높이고, 아니면 어웨이팀 스코어를 높인다.
    //            if (setup.Team == gm.HomeTeam)
    //                gm.AddHomeScore();
    //            else
    //                gm.AddAwayScore();

    //        }
    //        StartCoroutine(MoveToTargetAndDestory(other.transform.position, flySpeed));
    //    }
    //}

    ////가까운 플레이어로 이동하고, destroyDistance만큼 가까워지면 스스로를 파괴한다.
    //IEnumerator MoveToTargetAndDestory(Vector3 position, float speed)
    //{
    //    while (Vector3.Distance(position, transform.position) > destroyDistance)
    //    {
    //        transform.position = Vector3.Slerp(transform.position, position, speed * Time.deltaTime);
    //        yield return null;
    //    }

    //    ParticleSystem particle = PhotonNetwork.Instantiate(destroyEffect.name, transform.position, Quaternion.identity).GetComponent<ParticleSystem>();
    //    PhotonNetwork.Destroy(gameObject);

    //}
}
