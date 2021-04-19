using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TpscameraControl : MonoBehaviour
{
    private Transform tr;
    private float r;
    private float turnSpeed;
    private float turnSpeedvalue = 100.0f;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        turnSpeed = 0.0f;
        tr = GetComponent<Transform>();
        yield return new WaitForSeconds(0.3f);
        turnSpeed = turnSpeedvalue;
    }

    // Update is called once per frame
    void Update()
    {
        r =Input.GetAxis("Mouse Y");
        tr.Rotate(Vector3.right*Time.deltaTime*r*turnSpeed);
    }
}
