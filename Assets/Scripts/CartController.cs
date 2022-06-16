using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartController : MonoBehaviour
{
    public float Debug1;
    public float Debug2;
    public bool flying = false;
    public GameObject tiltLewer;
    public float tilt;
    public float tiltMultiplier;
    private Rigidbody rb;
    private Vector3 rotationOffset; // amount to rotate every frame
    public bool rocketBoost;
    public float rocketBoostForce;
    public GameObject tiltDisplay;
    public float speed;
    public GameObject speedometr;
    public float maxSpeed;
    public float maxSpeedAchieved;
    public float maxFrontAngle; //from 0 to 360 this should be about 80
    public float maxBackAngle; //this should be about 280
    public float dragForce;
    public float cartAngle;
    public LeverController leverController;
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
        cartAngle = gameObject.transform.localEulerAngles.x;
        if(flying)
        {
            //rb.AddForce(-transform.forward * dragForce);
        }
    }
    void Update()
    {
        /*if(tiltLewer.transform.eulerAngles.x - gameObject.transform.eulerAngles.x>180f)
        {
            tilt = tiltLewer.transform.eulerAngles.x - gameObject.transform.eulerAngles.x - 360;
        }
        else
        {
            tilt = tiltLewer.transform.eulerAngles.x - gameObject.transform.eulerAngles.x;
        }*/
        leverController.WhileTracking();
        Debug1 = tiltLewer.transform.localEulerAngles.y;
        if (flying)
        {
            if (tiltLewer.transform.localEulerAngles.y == 180)
            {
                tilt = tiltLewer.transform.localEulerAngles.x;
                if (tilt > 180 && tilt <= 360)
                {
                    tilt = -(360 - tilt);
                }
                tilt = -tilt;
            }
            else
            {
                tilt = tiltLewer.transform.localEulerAngles.x;
                if (tilt > 180 && tilt <= 360)
                {
                    tilt = -(360 - tilt);
                }
            }
            rotationOffset = new Vector3(tilt * tiltMultiplier*Time.deltaTime, 0, 0);
            if (gameObject.transform.localEulerAngles.x < maxFrontAngle || gameObject.transform.localEulerAngles.x > maxBackAngle)
            {
                gameObject.transform.Rotate(rotationOffset);
            }
            else if (gameObject.transform.localEulerAngles.x >= maxFrontAngle && rotationOffset.x < 0 && gameObject.transform.localEulerAngles.x < 180)
            {
                gameObject.transform.Rotate(rotationOffset);
            }
            else if (gameObject.transform.localEulerAngles.x <= maxBackAngle && rotationOffset.x > 0 && gameObject.transform.localEulerAngles.x > 180)
            {
                gameObject.transform.Rotate(rotationOffset);
            }
        }
        Debug2 = tiltLewer.transform.localEulerAngles.x;
        //rb.AddTorque(transform.right * tiltMultiplier * tilt); this or the line beneath
        //gameObject.transform.Rotate(rotationOffset);
        tiltDisplay.transform.localRotation = Quaternion.Euler(gameObject.transform.rotation.eulerAngles.x-90, -90, 90);
        speed = rb.velocity.magnitude;
        if(speed >= maxSpeed)
        {
            speedometr.transform.localRotation = Quaternion.Euler(0, 0, -360);
        }
        else
        {
            speedometr.transform.localRotation = Quaternion.Euler(0, 0, (speed / maxSpeed) * -360);
        }
        if (speed >= maxSpeedAchieved)
        {
            maxSpeedAchieved = speed;
        }
    }

    public void AddBoost(float force)
    {
        rb.AddForce(gameObject.transform.forward * force);
    }
    public void AddBoost(float force, Vector3 dir)
    {
        rb.AddForce((dir) * force);
    }
    public void ResetCart()
    {
        tiltLewer.transform.localRotation = Quaternion.Euler(0, 0, 0);
        maxSpeedAchieved = 0;
    }
}
