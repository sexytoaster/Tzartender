using System.Collections;
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
    public float fillChange;
    public float MaxWobble = 0.07f;
    public float WobbleSpeed = 1f;
    public float Recovery = 1f;
    public float minFillAmount = .575f;
    public float totalValues = 0;
    Vector3 lastPos;
    Vector3 velocity;
    Vector3 lastRot;
    Vector3 angularVelocity;

    //overfilling stuff
    public List<GameObject> Emitters;
    public List<GameObject> bottomOfGlass;
    public GameObject drinkMat;
    public DrinkInstructions currentValues;

    // Use this for initialization
    void Start()
    {
        drinkMat = GameObject.FindGameObjectWithTag("Finished");
        rend = GetComponent<Renderer>();
        rend.enabled = false;
        fillAmount = rend.material.GetFloat("fillAmount");
        fillChange = 0;
        
        //overfilling stuff
        foreach (GameObject emitter in Emitters)
        {
            emitter.GetComponent<ParticleSystem>().Stop();
        }
    }
    private void Update()
    {
        if (drinkMat.GetComponent<DrinkFinished>().EnteredTrigger == true)
        { 
            StartCoroutine("EmptyDrink");
        }
        if (fillAmount >= minFillAmount)
        {
            rend.enabled = false;
        }
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

        
        //overfilling and spilling code, use a list of points on the bottom of the glass
        GameObject currentBottom = bottomOfGlass[0];
        //loop over this list and determine the lowest point of the glass. make object at that point current bottom
        foreach (GameObject bottom in bottomOfGlass)
        {
            if (bottom.transform.position.y < currentBottom.transform.position.y)
            {
                currentBottom = bottom;
            }
        }
        //find out which emitters are below the true bottom of the glass, if they are below the bottom begin emitting
        foreach (GameObject emitter in Emitters)
        {
            if(emitter.transform.position.y > currentBottom.transform.position.y + fillChange || fillAmount >= minFillAmount)
            {
                //dont emitt
                emitter.GetComponent<ParticleSystem>().Play();
            }
            else
            {
                //emitt
                //go to script that deals with values in glasses when spilling
                adjustValuesOnPour();
            }
        }
        rend.material.SetFloat("fillAmount", fillAmount);
    }

    void OnTriggerEnter(Collider collision)
    {
        rend.enabled = true;
        //colour shit to come back to
        Color colour = collision.GetComponent<TrailRenderer>().material.color;
        if (colour.a > rend.material.GetColor("tint").a)
        {
            StartCoroutine(changeColour(colour)); //
        }
        
        
       
        if (collision.gameObject.name == "DropVodka(Clone)")
        {
            currentValues.Vodka += .1f;
            fillAmount = fillAmount - (float).000025;
            fillChange += (float).000025;
        }
        if (collision.gameObject.name == "DropRum(Clone)")
        {
            currentValues.Rum += .1f;
            fillAmount = fillAmount - (float).000025;
            fillChange += (float).000025;
        }
        if (collision.gameObject.name == "DropTequila(Clone)")
        {
            currentValues.Tequila += .1f;
            fillAmount = fillAmount - (float).000025;
            fillChange += (float).000025;
        }
        if (collision.gameObject.name == "DropGin(Clone)")
        {
            currentValues.Gin += .1f;
            fillAmount = fillAmount - (float).000025;
            fillChange += (float).000025;
        }
        if (collision.gameObject.name == "DropWhiskey(Clone)")
        {
            currentValues.Whiskey += .1f;
            fillAmount = fillAmount - (float).000025;
            fillChange += (float).000025;
        }
        if (collision.gameObject.name == "DropCoke(Clone)")
        {
            currentValues.Coke += 1f;
            fillAmount = fillAmount - (float).00025;
            fillChange += (float).00025;
        }
        if (collision.gameObject.name == "DropTonic(Clone)")
        {
            currentValues.Tonic += 1f;
            fillAmount = fillAmount - (float).00025;
            fillChange += (float).00025;
        }
        if (collision.gameObject.name == "DropSoda(Clone)")
        {
            currentValues.Soda += 1f;
            fillAmount = fillAmount - (float).00025;
            fillChange += (float).00025;
        }
        if (collision.gameObject.name == "DropGinger(Clone)")
        {
            currentValues.Ginger += 1f;
            fillAmount = fillAmount - (float).00025;
            fillChange += (float).00025;
        }
        if (collision.gameObject.name == "DropOrange(Clone)")
        {
            currentValues.Orange += 1f;
            fillAmount = fillAmount - (float).00025;
            fillChange += (float).00025;
        }
        if (collision.gameObject.name == "DropLime(Clone)")
        {
            currentValues.LimeJuice += 1f;
            fillAmount = fillAmount - (float).00025;
            fillChange += (float).00025;
        }
        if (collision.gameObject.name == "DropLemon(Clone)")
        {
            currentValues.LemonJuice += 1f;
            fillAmount = fillAmount - (float).00025;
            fillChange += (float).00025;
        }
        if (collision.gameObject.tag == "Liquid")
        {
            
            //If the GameObject's name matches the one you suggest, decrease ammount hidden
            rend.material.SetFloat("fillAmount", fillAmount);
            Destroy(collision.gameObject);
        }

    }

    void adjustValuesOnPour()
    {
        totalValues = currentValues.Vodka + currentValues.Rum + currentValues.Tequila + currentValues.Gin + currentValues.Whiskey + currentValues.Coke + currentValues.Tonic + currentValues.Soda + currentValues.Orange + currentValues.Ginger + currentValues.LimeJuice + currentValues.LemonJuice;
        currentValues.Vodka -= currentValues.Vodka / totalValues * .4f;
        currentValues.Rum -= currentValues.Rum / totalValues * .4f;
        currentValues.Tequila -= currentValues.Tequila / totalValues * .4f;
        currentValues.Gin -= currentValues.Gin / totalValues * .4f;
        currentValues.Whiskey -= currentValues.Whiskey / totalValues * .4f;
        currentValues.Coke -= currentValues.Coke / totalValues * .4f;
        currentValues.Tonic -= currentValues.Tonic / totalValues * .4f;
        currentValues.Soda -= currentValues.Soda / totalValues * .4f;
        currentValues.Ginger -= currentValues.Ginger / totalValues * .4f;
        currentValues.Orange -= currentValues.Orange / totalValues * .4f;
        currentValues.LimeJuice -= currentValues.LimeJuice / totalValues * .4f;
        currentValues.LemonJuice -= currentValues.LemonJuice / totalValues * .4f;
        fillAmount += (float).0001;
        fillChange -= (float).0001;
    }

    IEnumerator changeColour(Color colour)
    {
        float t = 0;
        float duration = 1.5f;
        float smoothness = .002f;
        Color initialColour = rend.material.GetColor("tint"); // = ;
        Color changingColour;
        Debug.Log("Initial " + initialColour);
        Debug.Log("Lerping to " + colour);
        while (t < 1)
        {
            changingColour = Color.Lerp(initialColour, colour, t);
            t += Time.deltaTime / duration;
            rend.material.SetColor("tint", changingColour);
            Emitters[1].GetComponent<ParticleSystemRenderer>().material.SetColor("_Color", changingColour);
            yield return new WaitForSeconds(smoothness);
        }
       
    }
    IEnumerator EmptyDrink()
    {
        currentValues.Vodka = 0;
        currentValues.Rum = 0;
        currentValues.Tequila = 0;
        currentValues.Gin = 0;
        currentValues.Whiskey = 0;
        currentValues.Coke = 0;
        currentValues.Tonic = 0;
        currentValues.Soda = 0;
        currentValues.Ginger = 0;
        currentValues.Orange = 0;
        currentValues.LimeJuice = 0;
        currentValues.LemonJuice = 0;

        fillAmount = minFillAmount;
        fillChange = 0;
        yield return null;
    }

}