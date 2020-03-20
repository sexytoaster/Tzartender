using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterPour : MonoBehaviour
{
    public GameObject drop;
    public GameObject bottle;
    public float speed = 1000f;
    Vector3 v3Velocity;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        v3Velocity = rb.velocity;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.parent.name == "MixerBottle" || transform.parent.name == "MixerBottle(Clone)")
        {
            bool cap = transform.parent.gameObject.GetComponentInChildren<BottleCap>().capRB.isKinematic;
            if (cap == false)
            {
                if (Vector3.Dot(bottle.transform.up, Vector3.down) > 0)
                {

                    GameObject instDrop = Instantiate(drop, transform.position, Quaternion.identity) as GameObject;
                    Rigidbody instDropRigidbody = instDrop.GetComponent<Rigidbody>();
                    instDropRigidbody.AddForce((bottle.transform.up * speed) + v3Velocity);
                }
            }
        }
        else
        {
            if (Vector3.Dot(bottle.transform.up, Vector3.down) > 0)
            {

                GameObject instDrop = Instantiate(drop, transform.position, Quaternion.identity) as GameObject;
                Rigidbody instDropRigidbody = instDrop.GetComponent<Rigidbody>();
                instDropRigidbody.AddForce((bottle.transform.up * speed) + v3Velocity);
            }
        }

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Glass")
            Destroy(gameObject);
    }
}

