using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obs : MonoBehaviour
{
    public Rigidbody rb;
    public Transform player;
    public Vector3 offset;
    [HideInInspector]
    public Vector3 posLeft;
    [HideInInspector]
    public Vector3 posRight;

    public float Force;

    void Update()
    {
        posLeft = transform.position + offset;
        posRight = transform.position + -offset;
        Ray rayLeft = new Ray(posLeft, Vector3.down);
        Ray rayRight = new Ray(posRight, Vector3.down);

        if (!Physics.Raycast(rayLeft, 0.7f) && Physics.Raycast(rayRight, 0.7f))
        {
            rb.AddForce(-Force * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }
        else if (!Physics.Raycast(rayRight, 0.7f) && Physics.Raycast(rayLeft, 0.7f))
        {
            rb.AddForce(Force * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }

    }
}
