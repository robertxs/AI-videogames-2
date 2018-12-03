using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evade : Flee {

    private float maxPrediction = 50f;
    private float prediction;
    private Vector3 targetPosition = new Vector3(0, 0, 0);


    private void Update()
    {

        Vector3 Direction = transform.position - ball.transform.position;
        float distance = Direction.magnitude;

        if (maxSpeed <= distance / maxPrediction)
        {
            prediction = maxPrediction;
        }
        else
        {
            prediction = distance / maxSpeed;
        }


        targetPosition += Direction * (prediction);

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetPosition), 0.15F);

        targetPosition = targetPosition.normalized;
        transform.Translate(targetPosition * Time.deltaTime * maxSpeed, Space.World);


    }
}
