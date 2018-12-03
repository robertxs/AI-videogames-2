using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Face : Align {

    public GameObject faceball;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 Direction = faceball.transform.position - transform.position;
        if (Direction.magnitude > 0) {
            base.targetrotation = Mathf.Atan2(Direction.x, Direction.z) * Mathf.Rad2Deg;
            base.face();
        }

    }
}
