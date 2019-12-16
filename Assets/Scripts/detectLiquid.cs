using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectLiquid : MonoBehaviour
{
    Renderer rend;
    public float fillAmount;

    private void Start()
    {
        fillAmount = rend.material.GetFloat("fillAmount");
    }
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Do something here");
        //Check for a match with the name on colliding object
        if (collision.gameObject.name == "DropVodka")
        {
            fillAmount = (float)fillAmount - (float).00001;
            //If the GameObject's name matches, send changing fill variable to the shader to output visual representation
            rend.material.SetFloat("fillAmount", fillAmount);
            
        }
        
    }
}
