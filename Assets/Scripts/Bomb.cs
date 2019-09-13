using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float power;
    public float radius;
    public float upForce;

    private ParabolaController controller;
    private Vector3 startPos;
    private void Start()
    {
        startPos = transform.position;
        controller = GetComponent<ParabolaController>();
    }
    private void OnDisable()
    {
        transform.position = startPos;
        //멈춰줘야됨..
        if (controller != null)
        {
            controller.StopFollow();
        }

    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("LocalPlayer"))
            return;

        //부딪힌 곳을 시작으로 radius만큼 주변에 있는 콜라이더들을 가져움
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (var col in colliders)
        {
            //로컬플레이어 혹은 움직일 수 없는 오브젝트면 패스.
            if (col.CompareTag("LocalPlayer") || !col.CompareTag("Moveable"))
                continue;

            Rigidbody rib = col.GetComponent<Rigidbody>();
            if (rib != null)
                rib.AddExplosionForce(power, transform.position, radius, upForce, ForceMode.Force);
        }

        gameObject.SetActive(false);
    }

}
