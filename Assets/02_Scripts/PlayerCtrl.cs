using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{

    private float h;
    private float v;
    private float rx;
    private float ry;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        rx = Input.GetAxis("Mouse X");
        ry = Input.GetAxis("Mouse Y");
        

    }
}
