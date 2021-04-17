using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBullet : MonoBehaviour
{
    public GameObject sparkEffect;

    void OnCollisionEnter(Collision coll)
    {
        ContactPoint cont = coll.GetContact(0);
        Vector3 normal = cont.normal;
        Quaternion rot = Quaternion.LookRotation(-normal);
        
        
        if (coll.collider.CompareTag("BULLET"))
        {
            GameObject spark = Instantiate(sparkEffect, cont.point, rot);
            Destroy(spark ,0.8f);
            Destroy(coll.collider.gameObject);
        }

    }
}
