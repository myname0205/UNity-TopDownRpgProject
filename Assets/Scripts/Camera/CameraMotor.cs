using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{
    private Transform lookAtTarget;
    
    [Range(0, 1)]
    public float boundX = 0.15f;
    [Range(0, 1)]
    public float boundY = 0.05f;

    private void Start()
    {
        lookAtTarget = GameObject.Find("Player").transform;
    }

    private void LateUpdate()
    {
        Vector3 delta = Vector3.zero;

        // To check if we are inside the bounds on the x axis
        float deltaX = lookAtTarget.position.x - transform.position.x;
        if (deltaX > boundX || deltaX < -boundX)
        {
            if(transform.position.x < lookAtTarget.position.x)
            {
                delta.x = deltaX - boundX;
            }
            else
            {
                delta.x = deltaX + boundX;
            }
        }

        // To check if we are inside the bounds on the y axis
        float deltaY = lookAtTarget.position.y - transform.position.y;
        if (deltaY > boundY || deltaY < -boundY)
        {
            if (transform.position.y < lookAtTarget.position.y)
            {
                delta.y = deltaY - boundY;
            }
            else
            {
                delta.y = deltaY + boundY;
            }
        }

        transform.position += new Vector3(delta.x, delta.y, 0);
    }
}
