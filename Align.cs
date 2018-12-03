using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Align : BlendedSteering {

    public GameObject ball;
    private float maxRotation = 38f;
    private float maxAngularAcceleration = 80f;
    private float targetRadius = 3f;
    private float slowRadius = 5f;
    private float timetotarget = 0.1f;


    float rotation;
    float rotationSize;
    float rotationSpeed;
    float angularAcceleration;

    float actualRotation = 0;

    float angularSteering = 0;

    bool isFacing = false;
    public float targetrotation;

    public void face() {
        isFacing = true;
        Update();
    }

    public void Update()
    {
        if (isFacing == false)
        {
            rotation = ball.transform.rotation.eulerAngles.y - transform.rotation.eulerAngles.y;
            rotation = rotation % 360;
        }
        else {
            rotation = targetrotation - transform.rotation.eulerAngles.y;
            rotation = rotation % 360;
        }

        if (rotation > 180){
            rotation = rotation - 360;
        }else if (rotation < -180) {
            rotation = rotation + 360;
        }

        rotationSize = Mathf.Abs(rotation);

        if (rotationSize > targetRadius) {
            print(rotationSize);
            if (rotationSize > slowRadius)
            {
                rotationSpeed = maxRotation;
            }
            else {
                rotationSpeed = maxRotation * rotationSize / slowRadius;
            }

            rotationSpeed *= rotation / rotationSize;

            angularSteering = rotationSpeed - transform.rotation.y;
            angularSteering /= timetotarget;

            angularAcceleration = Mathf.Abs(angularSteering);

            if (angularAcceleration > maxAngularAcceleration) {
                angularSteering /= angularAcceleration;
                angularSteering *= maxAngularAcceleration;
            }


            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y + (angularSteering * Time.deltaTime * base.faceWeight), 0);

        } 


     
           
        
   

       
    }
}
