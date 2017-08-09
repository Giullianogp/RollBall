using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{


    public float speed = 120;
    private Rigidbody rb;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    private void Main()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    void Update()
    {

        if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            // Exit condition for Desktop devices
            if (Input.GetKey("escape"))
                Application.Quit();
        }
        else
        {
            // Exit condition for mobile devices
            //if (Input.GetKeyDown(KeyCode.Escape))
            //    Application.Quit();
        }
    }


    void FixedUpdate()
    {
        if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            // Player movement in desktop devices
            // Definition of force vector X and Y components
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            // Building of force vector
            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
            // Adding force to rigidbody
            rb.AddForce(movement * speed * Time.deltaTime);
        }
        else
        {
            // Player movement in mobile devices
            // Building of force vector 
            Vector3 movement = new Vector3(Input.acceleration.x, 0.0f, Input.acceleration.y);
            // Adding force to rigidbody
            rb.AddForce(movement * speed * Time.deltaTime);
        }


    }


}
