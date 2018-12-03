using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : BlendedSteering
{

    public GameObject target;
    protected float maxSpeed = 15f;
    protected Vector3 velocity;
  
    public void Update()
    {

        Vector3 vel = target.transform.position - transform.position;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(vel), 0.15F);

        velocity = vel;
        vel = vel.normalized;
        transform.Translate(vel * Time.deltaTime * maxSpeed * base.seekWeight, Space.World);
    }

}
