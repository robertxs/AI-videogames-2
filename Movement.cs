using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    private int speed = 20;

    private void Update()
    {

        Vector3 Movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (Movement != new Vector3(0, 0, 0))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Movement), 0.20F);
        }
        transform.position += Movement * speed * Time.deltaTime;

    }
}
