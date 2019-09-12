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

        Vector3 direction = Vector3.forward * joyStick.Vertical + Vector3.right * joyStick.Horizontal;
        rb.MovePosition(transform.position + direction * speed * Time.deltaTime);

        transform.LookAt(transform.position + direction);

        //Debug.DrawRay(transform.position, direction* 30, Color.red, 3f);





    }

    private void OnCollisionEnter(Collision collision)
    {
        rb.angularVelocity = Vector3.zero;
    }

}
