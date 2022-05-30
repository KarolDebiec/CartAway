using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartController : MonoBehaviour
{
    public GameObject tiltLewer;
    public float tilt;
    public float tiltMultiplier;
    private Rigidbody rb;
    private Vector3 rotationOffset; // amount to rotate every frame
    public bool rocketBoost;
    public float rocketBoostForce;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (rocketBoost)
        {
            rb.AddForce(gameObject.transform.forward * rocketBoostForce);
        }
    }
    void Update()
    {
        if(tiltLewer.transform.eulerAngles.x - gameObject.transform.eulerAngles.x>180f)
        {
            tilt = tiltLewer.transform.eulerAngles.x - gameObject.transform.eulerAngles.x - 360;
        }
        else
        {
            tilt = tiltLewer.transform.eulerAngles.x - gameObject.transform.eulerAngles.x;
        }
        //rb.AddTorque(transform.right * tiltMultiplier * tilt);
        rotationOffset = new Vector3((tilt/360)*tiltMultiplier,0,0);
        gameObject.transform.Rotate(rotationOffset);
    }

    public void AddBoost(float force)
    {
        rb.AddForce(gameObject.transform.forward * force);
    }
    public void AddBoost(float force, Vector3 dir)
    {
        rb.AddForce((dir) * force);
    }
}
