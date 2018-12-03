using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Separation : MonoBehaviour {

    public List<GameObject> targets;
    public GameObject Bigdragon;
    public GameObject ball;

    float threshold = 2f;
    float decayCoefficient = 1f;
    float maxAcceleration = 3f;

    // Use this for initialization
    void Start () {
        targets = new List<GameObject>();
        targets.Add(Bigdragon);
        targets.Add(ball);
    }
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < targets.Count; i++)
        {
            Vector3 direction = targets[i].transform.position - transform.position;
            float distance = direction.magnitude;

            if (distance < threshold) {

                float strength = Mathf.Min(decayCoefficient / (distance * distance), maxAcceleration);

                transform.Translate(direction.normalized * strength, Space.World);

            }
        }
    }
}
