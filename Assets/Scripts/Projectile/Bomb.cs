using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Projectile
{
    [Header("폭탄이 밀어내는 힘")]
    public float power;
    [Header("피해반경")]
    public float radius;
    [Header("대상이 위로 뜨는 정도")]
    public float upForce;
    [Header("폭탄이 터지기까지의 딜레이")]
    public float explosionDelay;

    public GameObject effect;

    private ParabolaController controller;
    private Vector3 startPos;
    private ObjectPooler pooler;
    private ParticleSystem particle;
    private Renderer[] renderers;

    private void Start()
    {
        startPos = transform.position;
        controller = GetComponent<ParabolaController>();
        pooler = ObjectPooler.instance;
        particle = effect.GetComponent<ParticleSystem>();
        renderers = GetComponentsInChildren<Renderer>();
    }

    private void OnEnable()
    {
        if(renderers != null)
        {
            foreach (var renderer in renderers)
            {
                renderer.enabled = true;
            }
        }
        

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

        Debug.Log("bomb Collided with" + collision.transform.name);

        StartCoroutine(Explosion());


    }

    IEnumerator Explosion()
    {
        yield return new WaitForSeconds(explosionDelay);

        Debug.Log("폭발!");

        if (ownerStats == null)
        {
            Debug.Log("ownerStat이 없습니다");
        }

        //부딪힌 곳을 시작으로 radius만큼 주변에 있는 콜라이더들을 가져움
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (var col in colliders)
        {
            //로컬플레이어 혹은 움직일 수 없는 오브젝트면 패스.
            if (col.CompareTag("LocalPlayer") || !col.CompareTag("Moveable"))
                continue;

            Rigidbody rib = col.GetComponent<Rigidbody>();
            if (rib != null)
            {
                rib.AddExplosionForce(power, transform.position, radius, upForce, ForceMode.Force);
                CharacterStats targetStats = col.GetComponent<CharacterStats>();
                if (targetStats != null)
                {
                    targetStats.TakeDamage(ownerStats.attack.GetValue() + damage);
                }

            }
        }

        //파티클 시스템 재생
        GameObject effectObj = pooler.SpawnFromPool(effect.name, transform.position, Quaternion.identity);
        //파티클 끝날 때 까지 잠깐 숨기기
        foreach(var renderer in renderers)
        {
            renderer.enabled = false;
        }
        yield return new WaitForSeconds(particle.main.duration);
        effectObj.SetActive(false);
        gameObject.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

}
