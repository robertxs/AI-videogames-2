using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flee : MonoBehaviour{

    public GameObject ball;
    protected float maxSpeed = 8f;


    private void Update()
    {
        Vector3 velocity = transform.position - ball.transform.position;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(velocity), 0.15F);

        velocity = velocity.normalized;
        transform.Translate(velocity * Time.deltaTime * maxSpeed, Space.World);
    }

}
