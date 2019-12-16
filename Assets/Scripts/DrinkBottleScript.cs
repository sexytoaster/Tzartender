using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkBottleScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit hit;

        int layerMask = 1 << 11;
        Vector3 up = transform.TransformDirection(Vector3.up);
        //if the bottle is tilted more than 90 degrees from upright
        if (Mathf.Abs(this.gameObject.transform.rotation.eulerAngles.x) > 90 || Mathf.Abs(this.gameObject.transform.rotation.eulerAngles.z) > 90)
        {
            //if the raycast hits
            if (Physics.Raycast(transform.position, up, out hit, 10, layerMask))
            {
                Debug.DrawRay(transform.position, up, Color.green);
                print("hitting");
                hit.transform.gameObject.GetComponent<Glass>().vodka = 1;
            }
        }
    }
}
