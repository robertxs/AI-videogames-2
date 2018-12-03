using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityMatch : MonoBehaviour {

    public GameObject ball;
    private float maxSpeed = 50f;
    private float maxAcceleration = 5f;
    private float timetotarget = 0.5f;

    Vector3 velocity;

    private void Update()
    {
        Vector3 direction = ball.transform.position - transform.position;
       

        Vector3 characterspeed = new Vector3(transform.position.x, 0, transform.position.z);
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
