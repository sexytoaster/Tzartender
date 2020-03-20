/*This script controls the movement of the buttons in the scene. There are only 2 types of buttons in the game, those that move 
 along the x axis and ones that move in the y axis.*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VRButton : MonoBehaviour
{ 
    [System.Serializable]
    public class ButtonEvent : UnityEvent { }

    //create float for constant that controls amount button needs to be pressed to activate, as well as a flag and a buttonevent
    public float pressLength;
    public bool pressed;
    public ButtonEvent downEvent;

    //vector3 for the starting position of the button and an empty reference for the rigidbody of the button
    Vector3 startPos;
    Rigidbody rb;

    void Start()
    {
        //set startpos and get reference to rb
        startPos = transform.position;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //Playbutton is our button that moves in the y axis
        if (this.gameObject.name == "PlayButton")
        {
            //if distance moved is more than what was specified as a press
            // set it to our max distance and register a press if we haven't already
            float dist = Mathf.Abs(transform.position.y - startPos.y);
            if (dist >= pressLength)
            {
                // Prevent the button from going past the pressLength
                transform.position = new Vector3(transform.position.x, startPos.y - pressLength, transform.position.z);
                if (!pressed)
                {
                    pressed = true;
                    // If we have an event, invoke it
                    downEvent?.Invoke();
                }
            }
            else
            {
                // If we aren't all the way down, reset our press
                pressed = false;
            }
            // Prevent button from moving past original position
            if (transform.position.y > startPos.y)
            {
                transform.position = new Vector3(transform.position.x, startPos.y, transform.position.z);
            }
        }
        else
        {
            //for the buttons that move in x axis
            //same code as above, but variables moved around
            float dist = Mathf.Abs(transform.position.x - startPos.x);
            if (dist >= pressLength)
            {
                transform.position = new Vector3(startPos.x + pressLength, transform.position.y, transform.position.z);
                if (!pressed)
                {
                    pressed = true;
                    downEvent?.Invoke();
                }
            }
            else
            {
                pressed = false;
            }
            if (transform.position.x < startPos.x)
            {
                transform.position = new Vector3(startPos.x, transform.position.y, transform.position.z);
            }
        }
    }
}
