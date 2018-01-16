using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Controls : MonoBehaviour
{
    public float speed = 120;
    public Text countText;

    private Rigidbody rb;
    private int count;
    private float distToGround;
    Collider m_ObjectCollider;
    float moveHorizontal;
    float moveVertical;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        m_ObjectCollider = gameObject.GetComponent<Collider>();
        count = 0;
        SetCountText();
        //Get the distance to ground.
        
        distToGround = m_ObjectCollider.bounds.extents.y;
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

        if (Input.GetKeyDown("space") && IsGrounded())
        {
            Vector3 jump = new Vector3(0.0f, 6000.0f, 0.0f);
            GetComponent<Rigidbody>().AddForce(jump);
        }
    }


    void FixedUpdate()
    {
        if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            // Player movement in desktop devices
            // Definition of force vector X and Y components
            if(IsGrounded())
            {
                moveHorizontal = Input.GetAxis("Horizontal");
                moveVertical = Input.GetAxis("Vertical");
            }
            
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp")) other.gameObject.SetActive(false);
        count++;
        SetCountText();

        //Destroy(other.gameObject);
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }

}
