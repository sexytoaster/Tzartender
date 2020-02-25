using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleCap : MonoBehaviour
{
    public Rigidbody capRB;
    int speed = 20;
    float torque = 500000;
    // Start is called before the first frame update
    void Start()
    {
        capRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Do something here");
        //Check for a match with the name on colliding object
        if (other.gameObject.name == "BottleOpener")
        {
            capRB.isKinematic = false;
            float turn = Input.GetAxis("Horizontal");
            capRB.AddRelativeForce(transform.position + transform.up * speed, ForceMode.Force);
            capRB.AddTorque(transform.right * torque);
            capRB.AddTorque(transform.up * torque);
        }

    }
}
