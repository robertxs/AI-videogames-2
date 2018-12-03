using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ObstacleAvoidance : Seek {

    public List<GameObject> obstacles;
    public GameObject Dragon1;
    public GameObject Dragon2;
    public GameObject Dragon3;
 

    GameObject mostThreatening;

    // Use this for initialization
    void Start () {
        obstacles = new List<GameObject>();
        obstacles.Add(Dragon1);
        obstacles.Add(Dragon2);
        obstacles.Add(Dragon3);

    }

    float obstacleRadius = 10f;
    float maxVelocity = 15f;

    // Update is called once per frame
    void Update () {

        float dynamic_length = base.velocity.magnitude / maxVelocity;
        Vector3 ahead = transform.position + base.velocity.normalized * maxVelocity * dynamic_length;
        Vector3 ahead2 = transform.position + base.velocity.normalized * (maxVelocity / 2) * dynamic_length;
        GameObject obstacle = findMostThreateningObstacle(ahead, ahead2);

        if (obstacle!= null) {
            Vector3 avoidance_force = ahead - obstacle.transform.position;
            avoidance_force = avoidance_force.normalized * 9;
            transform.Translate(avoidance_force * Time.deltaTime);
        } 

    }

    GameObject findMostThreateningObstacle(Vector3 ahead, Vector3 ahead2)

    {
        mostThreatening = null;
        for (int i = 0; i< obstacles.Count; i++) {

            float distance = HandleUtility.DistancePointLine(obstacles[i].transform.position, transform.position, ahead);
            float distance2 = HandleUtility.DistancePointLine(obstacles[i].transform.position, transform.position, ahead2);
            float distance3 = HandleUtility.DistancePointLine(obstacles[i].transform.position, transform.position, transform.position);

            if ((distance <= obstacleRadius || distance2 <= obstacleRadius || distance3 <= obstacleRadius) && (mostThreatening == null || distance < HandleUtility.DistancePointLine(mostThreatening.transform.position, transform.position, ahead))) {
                mostThreatening = obstacles[i];
            }
        }
        return mostThreatening;
    }

}
