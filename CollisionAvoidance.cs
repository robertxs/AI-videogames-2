using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionAvoidance : MonoBehaviour {
   
    public List<GameObject> targets;
    public GameObject Bigdragon;
    public GameObject ball;

    float maxAcceleration = 1f;
    float radius = 40f;

    // Use this for initialization
    void Start () {
        targets = new List<GameObject>();
        targets.Add(Bigdragon);
        targets.Add(ball);
    }
	
	// Update is called once per frame
	void Update () {

        float shortestTime = Mathf.Infinity;

        GameObject firstTarget = null;
        float firstMinSeparation = 0;
        float firstDistance = 0;
        Vector3 firstRelativePos = new Vector3(0, 0, 0);
        Vector3 firstRelativeVel = new Vector3(0, 0, 0);

        for (int i = 0; i < targets.Count; i++)
        {
            Vector3 relativePos = targets[i].transform.position - transform.position;
            Vector3 relativeVel = targets[i].transform.forward * maxAcceleration - transform.forward * maxAcceleration;
            float relativeSpeed = relativeVel.magnitude;

            float timeToCollision = Vector3.Dot(relativePos, relativeVel) / (relativeSpeed * relativeSpeed);

            float distance = relativePos.magnitude;
            float minSeparation = distance - relativeSpeed * shortestTime;

            if (minSeparation > 2 * radius) {
                continue;
            }

            if (timeToCollision>0 && timeToCollision<shortestTime) {

                shortestTime = timeToCollision;
                firstTarget = targets[i];
                firstMinSeparation = minSeparation;
                firstDistance = distance;
                firstRelativePos = relativePos;
                firstRelativeVel = relativeVel;

            }

            if (firstTarget != null) {
                if (firstMinSeparation <= 0 || distance < 2 * radius)
                {
                    relativePos = firstTarget.transform.position - transform.position;
                }
                else {
                    relativePos = firstRelativePos + firstRelativeVel*shortestTime;
                } 
            }

            relativePos = relativePos.normalized;
            transform.Translate(relativePos * maxAcceleration, Space.World);


        }

    }
}
 