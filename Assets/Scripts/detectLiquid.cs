using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectLiquid : MonoBehaviour
{
    Renderer rend;
    public float fillAmount;
    public float fillOffset;
    public DrinkInstructions currentValues;

    private void Start()
    {
        fillAmount = rend.material.GetFloat("fillAmount");
        
    }
    private void Update()
    {
        //for testing
        currentValues.Vodka += .1f;
    }
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Do something here");
        //Check for a match with the name on colliding object
        if (collision.gameObject.name == "DropVodka")
        {
            currentValues.Vodka++;
            fillAmount = (float)fillAmount - (float)fillOffset;
            //If the GameObject's name matches, send changing fill variable to the shader to output visual representation
            rend.material.SetFloat("fillAmount", fillAmount);
            
        }
        
    }
}
