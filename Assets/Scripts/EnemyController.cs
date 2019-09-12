using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public float aggroRadius;
    public SphereCollider aggroZone;

    private CharacterStats selfStats;
    private CharacterCombat selfCombat;
    private EnemyMotor motor;

    private Transform player;
    private CharacterStats playerStats;



    // Start is called before the first frame update
    void Start()
    {
        selfStats = GetComponent<CharacterStats>();
        selfCombat = GetComponent<CharacterCombat>();
        motor = GetComponent<EnemyMotor>();
        aggroZone.radius = aggroRadius;
    }


    private void Update()
    {
        if(player != null)
        {
            selfCombat.Attack(playerStats, selfStats.attack.GetValue());
        }

    }


    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            motor.ChaseTarget(other.transform);

            if(Vector3.Distance(other.transform.position, transform.position) <= selfStats.range.GetValue())
            {
                player = other.transform;
                playerStats = other.GetComponent<CharacterStats>();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        motor.StopChaseTarget();
        player = null;
        playerStats = null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, aggroRadius);
    }
}
