using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : Align {

    private float wanderOffset = 1f;
    private float wanderRadius = 1.5f;
    private float wanderRate = 5f;
    private Vector3 wanderOrientation = new Vector3 (0,0,0);
    private float maxAcceleration = 2.9f;
    Vector3 target;


    static bool turn = true;

    public void Update() {
        if (turn == true)
        {
            wanderOrientation += new Vector3(0, Random.Range(0f, 360f), 0) * wanderRate;
            Vector3 targetOrientation = wanderOrientation + transform.rotation.eulerAngles;

            target = transform.position + (wanderOffset * new Vector3(Mathf.Sin(transform.rotation.eulerAngles.y), 0, Mathf.Cos(transform.rotation.eulerAngles.y)));
            target += wanderRadius * targetOrientation;

            Vector3 Direction = targetOrientation - transform.position;
            if (Direction.magnitude > 0)
            {
                base.targetrotation = Mathf.Atan2(Direction.x, Direction.z) * Mathf.Rad2Deg;
                base.face();
                
            }

            StartCoroutine(Wait());
        }
        
        Vector3 speedDirection = new Vector3(Mathf.Sin(transform.rotation.eulerAngles.y), 0, Mathf.Cos(transform.rotation.eulerAngles.y)).normalized;
        transform.Translate(speedDirection * Time.deltaTime * maxAcceleration, Space.World);
            

    }

    IEnumerator Wait()
    {
        turn = false;
        yield return new WaitForSeconds(0.5f);
        turn = true;
     
    }

}
