/*This script is attatched to the bottlecaps on the mixers,
Its purpose it to remove the bottlecap from the mixerbottle
by causing the bottlecap to fly off*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleCap : MonoBehaviour
{
    //create a rigidbody type so we can assign the rigidbody of the cap to it so we can manipulate it. Also need a speed a torque constant 
    public Rigidbody capRB;
    int speed = 20;
    float torque = 500000;
    // Start is called before the first frame update
    void Start()
    {
        //assign rigidbody
        capRB = GetComponent<Rigidbody>();
    }
    
    void OnTriggerEnter(Collider other)
    {
        //Check for a match with the name on colliding object
        if (other.gameObject.name == "BottleOpener")
        {
            //make it so position can bemanipulated, add force upwards and torque to make it rotate to mimic bottle opening
            capRB.isKinematic = false;
            float turn = Input.GetAxis("Horizontal");
            capRB.AddRelativeForce(transform.position + transform.up * speed, ForceMode.Force);
            capRB.AddTorque(transform.right * torque);
            capRB.AddTorque(transform.up * torque);
        }
    }
}
