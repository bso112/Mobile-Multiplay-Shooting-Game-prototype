using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParabolaMovement : MonoBehaviour
{
    //파라볼라 슈터에 합치기

    new protected float animation; //움직이는 정도
    public float reach;
    public float height;
    public float inversedSpeed; //작을수록 빠르게 움직임.
    private Vector3 initialPos;

    private void OnEnable()
    {
        initialPos = transform.position;
        animation = 0;
    }

    // Update is called once per frame
    void Update()
    {
        animation += Time.deltaTime;

        //(animation / speed)는 0 과 1 사이의 보간계수이므로 animation은 speed를 넘지 않아야한다.
        if (animation > inversedSpeed)
        {
            //목표지점에서 물리를 무시하고 멈추고 싶다면 animation = inversedSpeed; 를 쓰자.
            return;
        }

        transform.position = MathParabola.Parabola(initialPos, initialPos + transform.forward * reach, height, animation / inversedSpeed);
        
    }
}
