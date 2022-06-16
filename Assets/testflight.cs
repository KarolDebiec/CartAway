using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testflight : MonoBehaviour
{
    Rigidbody rb; 
    public float thrust = 0;
    public bool boost=false;
    public bool useLift = false;
    public float angleOfAttack;
    public float angleOfAttackSin;
    public float dragForce;
    public float liftForce;
    public float realLiftForce;
    public float weightForce;
    public float thrustForce;
    public float liftMultiplier;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        angleOfAttack = gameObject.transform.localRotation.x;
        angleOfAttackSin = Mathf.Deg2Rad*angleOfAttack;
        realLiftForce = Mathf.PI * angleOfAttackSin  * liftMultiplier;
        if (boost)
        {
            rb.AddForce(new Vector3(0, 0, thrustForce - dragForce) *thrust);
        }
        if(useLift)
        {
            rb.AddForce(new Vector3(0, realLiftForce - weightForce, 0));
        }
       
    }
}
