using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrive : MonoBehaviour {

    public GameObject ball;
    private float maxSpeed = 8f;
    private float radius = 4f;
    private float timetotarget = 4;

    private void Update()
    {
  
        Vector3 velocity = ball.transform.position - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(velocity), 0.15F);

        if (velocity.magnitude > radius)
        {
            velocity = velocity / timetotarget;
            if (velocity.magnitude > maxSpeed) {
                velocity = velocity.normalized;
                velocity = velocity * maxSpeed;
            }

            transform.Translate(velocity * Time.deltaTime, Space.World);
        }
    }
}
