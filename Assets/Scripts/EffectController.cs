using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(ParticleSystem))]
public class EffectController : MonoBehaviour
{
    private ParticleSystem ps;
    private PhotonView pw;

    private void OnEnable()
    {
        if(ps != null && pw.IsMine)
            StartCoroutine(DelayDestroy());
    }
    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        pw = GetComponent<PhotonView>();
    }



    private IEnumerator DelayDestroy()
    {
        yield return new WaitForSeconds(ps.main.startLifetime.constant);
        PhotonNetwork.Destroy(gameObject);
    }

}
