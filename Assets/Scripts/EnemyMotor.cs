using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMotor : MonoBehaviour
{
    public float speed;
    public float stoppingDistance;

    private Transform target;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(target != null)
        {
            if(Vector3.Distance(target.position, transform.position) <= stoppingDistance)
            {
                return;
            }
            Vector3 direction = (target.position - transform.position).normalized;
            rb.MovePosition(transform.position + direction * speed * Time.deltaTime);
        }
    }


    public void ChaseTarget(Transform _target)
    {
        target = _target;
    }

    public void StopChaseTarget()
    {
        target = null;
    }
}
