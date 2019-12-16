using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterPour : MonoBehaviour
{
    public GameObject drop;
    public GameObject bottle;
    public float speed = 1000f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Vector3.Dot(bottle.transform.up, Vector3.down) > 0)
        {

            GameObject instDrop = Instantiate(drop, transform.position, Quaternion.identity) as GameObject;
            Rigidbody instDropRigidbody = instDrop.GetComponent<Rigidbody>();
            instDropRigidbody.AddForce(bottle.transform.up * speed);
        }

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Glass")
            Destroy(gameObject);
    }
}

