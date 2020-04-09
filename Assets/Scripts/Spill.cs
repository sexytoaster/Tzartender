using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spill : MonoBehaviour
{
    public List<GameObject> Emitters;
    public List<GameObject> bottomOfGlass;
    private DrinkInstructions currentValues;
    private float totalValues = 0;
    private float fillChange = 0;
    private float fillAmount;
    private float minFillAmount;
    // Start is called before the first frame update
    void Start()
    {
        //overfilling stuff
        foreach (GameObject emitter in Emitters)
        {
            emitter.GetComponent<ParticleSystem>().Stop();
        }
        minFillAmount = GameObject.FindGameObjectWithTag("liquidInGlass").GetComponent<wobble>().minFillAmount;
        currentValues = GameObject.FindGameObjectWithTag("liquidInGlass").GetComponent<wobble>().currentValues;
        
    }

    // Update is called once per frame
    void Update()
    {
        fillChange = GameObject.FindGameObjectWithTag("liquidInGlass").GetComponent<wobble>().fillChange;
        fillAmount = GameObject.FindGameObjectWithTag("liquidInGlass").GetComponent<wobble>().fillAmount;
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
            if (emitter.transform.position.y > currentBottom.transform.position.y + fillChange || fillAmount >= minFillAmount)
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
        GameObject.FindGameObjectWithTag("liquidInGlass").GetComponent<wobble>().fillChange = fillChange;
        GameObject.FindGameObjectWithTag("liquidInGlass").GetComponent<wobble>().fillAmount = fillAmount;

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
}
