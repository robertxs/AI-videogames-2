using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicArrive : MonoBehaviour
{

    public GameObject ball;
    private float maxSpeed = 24f;
    private float maxAcceleration = 6f;
    private float targetRadius = 2f;
    private float slowRadius = 7f;
    private float timetotarget = 4;

    Vector3 velocity;

    private void Update()
    {
        Vector3 direction = ball.transform.position - transform.position;
        float distance = direction.magnitude;

        if (distance >= targetRadius)
        {
            if (distance > slowRadius)
            {
                direction = direction.normalized;
                direction = direction * maxSpeed;
            }
            else
            {
                direction = direction.normalized;
                direction = (direction * maxSpeed * distance) / slowRadius;
            }
            Vector3 characterspeed = new Vector3 (transform.position.x, 0, transform.position.z);
            Vector3 acceleration = direction - characterspeed;
            acceleration /= timetotarget;

            if (acceleration.magnitude > maxAcceleration)
            {
                acceleration = acceleration.normalized;
                acceleration = acceleration * maxAcceleration;
            }

            velocity += acceleration * Time.deltaTime;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(velocity), 0.035F);
            transform.Translate(velocity * Time.deltaTime, Space.World);
        }
    }
}