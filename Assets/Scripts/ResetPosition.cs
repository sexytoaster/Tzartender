//simple script that can be attatched to any object to make it reset its position if it falls
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ResetPosition : MonoBehaviour
{
   
    private Quaternion resetRotation;
    private Vector3 originPosition;

    private void Start()
    {
        //get references to the opsition the object is in at the start of the scene
        resetRotation = transform.rotation;
        originPosition = transform.position;
    }
    private void OnCollisionEnter(Collision collision)
    {
        //if it collides with the ground
        if(collision.gameObject.tag == "Ground")
        {
            //reset
            ResetObjectPosition();
        }
        //if it is a pint glass that is put on the drink mat, signifying the drink was finished
        if(collision.gameObject.tag == "Finished" && gameObject.name == "PintGlass")
        {
            //reset
            ResetObjectPosition();
        }
    }
    public void ResetObjectPosition()
    {
        //transform back to original position and rotation, remove any forces on the bottle so it doesnt keep moving after being reset
        transform.rotation = resetRotation;
        transform.position = originPosition;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }
}
