using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DynamicFlee : MonoBehaviour
{

    public GameObject ball;
    private float maxSpeed = 40f;
    private float maxAcceleration = 10f;

    Vector3 velocity;

    private void Update()
    {

        Vector3 acceleration = transform.position - ball.transform.position;

        acceleration = acceleration.normalized;
        acceleration = acceleration * maxAcceleration;
        velocity += acceleration * Time.deltaTime;

        if (velocity.magnitude > maxSpeed)
        {
            velocity = velocity.normalized;
            velocity = velocity * maxSpeed;
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(velocity), 0.02F);
        transform.Translate(velocity * Time.deltaTime, Space.World);
    }

}