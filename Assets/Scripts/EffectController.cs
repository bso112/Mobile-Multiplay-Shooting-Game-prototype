using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class EffectController : MonoBehaviour
{
    private ParticleSystem ps;

    private void OnEnable()
    {
        if(ps != null)
            StartCoroutine(DelayDestroy());
    }
    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();  
    }



    private IEnumerator DelayDestroy()
    {
        yield return new WaitForSeconds(ps.main.startLifetime.constant);
        Photon.Pun.PhotonNetwork.Destroy(gameObject);
    }
}
