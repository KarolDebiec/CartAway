using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public bool test= false;
    public bool test2 = false;
    public bool launchCart = false;
    public bool reset = false;
    public float LaunchForce;
    public GameObject cart;
    private Rigidbody cartRB;
    public GameObject startPos;
    public GameObject targetPos;
    private float distance;
    public float maxDistance;
    private bool landed = false;

    public GameObject ramp;
    public GameObject fork;
    public Vector3 forkStartingPos;
    public GameObject modificationSpot;
    void Start()
    {
        cartRB = cart.GetComponent<Rigidbody>();
        SetupStart();
    }

    void FixedUpdate()
    {
        if(test)
        {
            StartLaunch();
            test = false;
        }
        if (test2)
        {
            SetupModPhase();
            test2 = false;
        }
        if (launchCart)
        {
            LaunchCart();
        }
        if (reset)
        {
            SetupStart();
            reset = false;
        }
    }
    private void Update()
    {
        distance = cart.transform.position.z;
        if(cart.transform.position.y <= 0 && !landed)
        {
            maxDistance = distance;
            landed = true;
        }
        Debug.Log(distance);
    }
    public void LaunchCart()// the process of launching cart with turned off gravity
    {
        if (targetPos.transform.position.z > cart.transform.position.z)
        { 
            cartRB.AddForce((targetPos.transform.position - cart.transform.position) * LaunchForce); 
        }
        else
        {
            Launched();
            launchCart = false;
        }
    }
    public void StartLaunch() // used to start the process of launching
    {
        cartRB.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        launchCart = true;
    }
    public void Launched() // called once the cart reaches target pos at the ramp
    {
        cartRB.useGravity = true;
        fork.transform.parent = ramp.transform;
    }
    public void SetupStart() // setups cart before the launch
    {
        cartRB.useGravity = false;
        cartRB.constraints = RigidbodyConstraints.FreezeAll;
        cart.transform.position = startPos.transform.position;
        cart.transform.LookAt(targetPos.transform.position);
        landed = false;
        fork.SetActive(true);
        fork.transform.parent = cart.transform;
        fork.transform.localPosition = forkStartingPos;
    }
    public void SetupModPhase()// setups modification phase for the cart
    {
        fork.SetActive(false);
        cart.transform.position = modificationSpot.transform.position;
        cart.transform.rotation = new Quaternion(0,0,0,0);
    }
}
