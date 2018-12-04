using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    private float speed = 270.8f;
    public bool executejump = false;

    private void Update()
    {

        if ((Input.GetButton("Jump") || executejump == true) && transform.position.y < 10 && transform.position.y <= 4)
        {
            if (transform.rotation.eulerAngles.y >= 0 && transform.rotation.eulerAngles.y < 45)
            { 
                transform.position += new Vector3(0, 1 * speed, 170) * Time.deltaTime;
            } else if (transform.rotation.eulerAngles.y >= 45 && transform.rotation.eulerAngles.y < 135)
            {
                transform.position += new Vector3(170, 1 * speed, 0) * Time.deltaTime;
            } else if (transform.rotation.eulerAngles.y >= 135 && transform.rotation.eulerAngles.y < 225)
            {
                transform.position += new Vector3(0, 1 * speed, -170) * Time.deltaTime;
            } else if (transform.rotation.eulerAngles.y >= 225 && transform.rotation.eulerAngles.y < 315)
            {
                transform.position += new Vector3(-170, 1 * speed, 0) * Time.deltaTime;
            } else if (transform.rotation.eulerAngles.y > 315)
            {
                transform.position += new Vector3(0, 1 * speed, 170) * Time.deltaTime;
            }
            
        }
        else {
            if (transform.position.y > 4)
            {
                transform.position += Vector3.up * -12.8f * Time.deltaTime;
            }
            executejump = false;
        }


    }
}
