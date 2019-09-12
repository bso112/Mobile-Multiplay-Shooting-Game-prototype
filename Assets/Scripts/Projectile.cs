using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [HideInInspector]
    public CharacterStats ownerStats;
    [HideInInspector]
    public Transform owner;

    // Start is called before the first frame update
    void Start()
    {
        owner = ownerStats.gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (ownerStats == null)
            return;
        //사거리 벗어나면 사라짐
        if(Vector3.Distance(owner.position, transform.position) > ownerStats.range.GetValue())
        {
            gameObject.SetActive(false);
        }

    }

    private void OnTriggerEnter(Collider other)
    {

        CharacterStats target = other.GetComponent<CharacterStats>();
        if(target!=null)
        {
            target.TakeDamage(ownerStats.attack.GetValue());
            gameObject.SetActive(false);
        }
        
    }
}
