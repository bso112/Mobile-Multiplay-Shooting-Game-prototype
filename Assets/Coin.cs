using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public float flySpeed;
    public float destroyDistance;
    public GameObject destroyEffect;
    private GameManager gm;
    
   

    private void Start()
    {
        gm = GameManager.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") || other.CompareTag("LocalPlayer"))
        {
            PlayerSetup setup = other.GetComponent<PlayerSetup>();
            if(setup != null)
            {
                if (setup.Team == gm.HomeTeam)
                    gm.HomeScore++;
                else
                    gm.AwayScore++;

            }
            gm.OnScoreUpdated?.Invoke();
            StartCoroutine(MoveToTargetAndDestory(other.transform.position, flySpeed));
        }
    }

    IEnumerator MoveToTargetAndDestory(Vector3 position, float speed)
    {
        while (Vector3.Distance(position, transform.position) > destroyDistance)
        {
            transform.position = Vector3.Slerp(transform.position, position, speed * Time.deltaTime);
            yield return null;
        }

        ParticleSystem particle = Photon.Pun.PhotonNetwork.Instantiate(destroyEffect.name, transform.position, Quaternion.identity).GetComponent<ParticleSystem>();
        Destroy(gameObject, particle.main.startLifetime.constant);
        Destroy(particle.transform.gameObject, particle.main.startLifetime.constant);
        foreach(var renderer in GetComponentsInChildren<Renderer>())
        {
            renderer.enabled = false;
        }

    }
}
