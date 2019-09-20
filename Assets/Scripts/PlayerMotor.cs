using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    public float speed;
    private Rigidbody rb;
    public Joystick joyStick;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(joyStick != null)
        {
            Vector3 direction = Vector3.forward * joyStick.Vertical + Vector3.right * joyStick.Horizontal;
            rb.MovePosition(transform.position + direction * speed * Time.deltaTime);

            transform.LookAt(transform.position + direction);

        }


    }

    private void OnCollisionEnter(Collision collision)
    {
        if(rb != null)
            rb.angularVelocity = Vector3.zero;
    }

}
