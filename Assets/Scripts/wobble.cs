﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wobble : MonoBehaviour
{
    Renderer rend;
    float wobbleX;
    float wobbleZ;
    float wobbleAddX;
    float wobbleAddZ;
    float sine;
    float time = 0f;
    public float fillAmount;
    public float MaxWobble = 0.07f;
    public float WobbleSpeed = 1f;
    public float Recovery = 1f;
    Vector3 lastPos;
    Vector3 velocity;
    Vector3 lastRot;
    Vector3 angularVelocity;

    public DrinkInstructions currentValues;

    // Use this for initialization
    void Start()
    {
        rend = GetComponent<Renderer>();
        fillAmount = rend.material.GetFloat("fillAmount");
    }
    private void Update()
    {
        //currentValues.Vodka += 1f;
        //currentValues.Coke += 1f;
        time += Time.deltaTime;
        // decrease wobble 
        wobbleAddX = Mathf.Lerp(wobbleAddX, 0, Time.deltaTime * (Recovery));
        wobbleAddZ = Mathf.Lerp(wobbleAddZ, 0, Time.deltaTime * (Recovery));
        sine = 2 * Mathf.PI * WobbleSpeed;
        wobbleX = wobbleAddX * Mathf.Sin(sine * time);
        wobbleZ = wobbleAddZ * Mathf.Sin(sine * time);

        // send to shader
        rend.material.SetFloat("wobbleX", wobbleX);
        rend.material.SetFloat("wobbleZ", wobbleZ);

        // velocity
        velocity = (lastPos - transform.position) / Time.deltaTime;
        angularVelocity = transform.rotation.eulerAngles - lastRot;


        //wobble + velocity
        wobbleAddX += Mathf.Clamp((velocity.x + (angularVelocity.z * 0.2f)) * MaxWobble, -MaxWobble, MaxWobble);
        wobbleAddZ += Mathf.Clamp((velocity.z + (angularVelocity.x * 0.2f)) * MaxWobble, -MaxWobble, MaxWobble);

        // store last position
        lastPos = transform.position;
        lastRot = transform.rotation.eulerAngles;
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "DropVodka(Clone)")
        {
            currentValues.Vodka += .1f;
            fillAmount = fillAmount - (float).000025;
        }
        if (collision.gameObject.name == "DropRum(Clone)")
        {
            currentValues.Rum += .1f;
            fillAmount = fillAmount - (float).000025;
        }
        if (collision.gameObject.name == "DropTequila(Clone)")
        {
            currentValues.Tequila += .1f;
            fillAmount = fillAmount - (float).000025;
        }
        if (collision.gameObject.name == "DropGin(Clone)")
        {
            currentValues.Gin += .1f;
            fillAmount = fillAmount - (float).000025;
        }
        if (collision.gameObject.name == "DropWhiskey(Clone)")
        {
            currentValues.Whiskey += .1f;
            fillAmount = fillAmount - (float).000025;
        }
        if (collision.gameObject.name == "DropCoke(Clone)")
        {
            currentValues.Coke += 1f;
            fillAmount = fillAmount - (float).00025;
        }
        if (collision.gameObject.name == "DropTonic(Clone)")
        {
            currentValues.Tonic += 1f;
            fillAmount = fillAmount - (float).00025;
        }
        if (collision.gameObject.name == "DropSoda(Clone)")
        {
            currentValues.Soda += 1f;
            fillAmount = fillAmount - (float).00025;
        }
        if (collision.gameObject.name == "DropGinger(Clone)")
        {
            currentValues.Ginger += 1f;
            fillAmount = fillAmount - (float).00025;
        }
        if (collision.gameObject.name == "DropOrange(Clone)")
        {
            currentValues.Orange += 1f;
            fillAmount = fillAmount - (float).00025;
        }
        if (collision.gameObject.name == "DropLime(Clone)")
        {
            currentValues.LimeJuice += 1f;
            fillAmount = fillAmount - (float).00025;
        }
        if (collision.gameObject.name == "DropLemon(Clone)")
        {
            currentValues.LemonJuice += 1f;
            fillAmount = fillAmount - (float).00025;
        }
        if (collision.gameObject.tag == "Liquid")
        {
            
            //If the GameObject's name matches the one you suggest, decrease ammount hidden
            rend.material.SetFloat("fillAmount", fillAmount);
            Destroy(collision.gameObject);
        }

    }


}