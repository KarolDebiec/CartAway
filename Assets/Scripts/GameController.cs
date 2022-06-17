using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public float money; // games currency

    public bool changeGravity = false;
    public float force;
    public bool test= false;
    public bool test2 = false;
    public bool launchCart = false;
    public bool reset = false;
    public float LaunchForce;
    public GameObject cart;
    private Rigidbody cartRB;
    private CartController cartControl;
    public GameObject startPos;
    public GameObject targetPos;
    private float distance;
    public float maxDistance = 0; // max distance achieved in the last jump
    public float overallMaxDistance = 0; // max distance achieved in the whole game
    private bool landed = false;

    public GameObject ramp;
    public GameObject fork;
    public Vector3 forkStartingPos;
    public GameObject modificationSpot;
    public bool canModify = false;

    public TextMesh recordAltitudeText;
    public TextMesh recordDistanceText;
    public TextMesh lastAltitudeText;
    public TextMesh lastDistanceText;

    public List<ButtonController> buttons;// all the buttons in the scene(including cart and shop)

    public GameObject VRRigCartPlace;
    public GameObject VRRigShopPlace;
    public GameObject VRRig;
    void Start()
    {
        cartRB = cart.GetComponent<Rigidbody>();
        cartControl = cart.GetComponent<CartController>();
        SetScoreDisplay(0,0,0,0);
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
        if (changeGravity)
        {
            ChangeGravity(force);
            changeGravity = false;
        }
    }
    private void Update()
    {
        distance = cart.transform.position.z;
        if(cart.transform.position.y <= 0 && !landed)
        {
            OnLanded();
        }
        if(landed)
        {
            if(cartRB.velocity.magnitude < 4 && cartRB.velocity.magnitude != 0)
            {
                cartRB.velocity = new Vector3(0,0,0);
            }
        }
        //Debug.Log(distance);
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
        cartControl.flying = true;
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
        canModify = false;
        cartControl.ResetCart();
        buttons[0].SetAvailable();
        buttons[1].SetUnavailable();
        maxDistance = 0;
        cartControl.maxAltitude = 0;
        cartRB.drag = cartControl.dragValue;
    }
    public void SetupModPhase()// setups modification phase for the cart
    {
        cartRB.useGravity = false;
        cartRB.constraints = RigidbodyConstraints.FreezeAll;
        landed = false;
        fork.SetActive(false);
        cart.transform.position = modificationSpot.transform.position;
        //cart.transform.rotation = new Quaternion(0,0,0,0);
        cart.transform.rotation = Quaternion.Euler(0, 0, 0);
        canModify = true;
    }
    public void ChangeGravity(float down)
    {
        Physics.gravity = new Vector3(0, down, 0);
    }
    public void OnLanded()
    {
        cartRB.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY; ;
        cart.transform.position = new Vector3(0,0, cart.transform.position.z);
        cart.transform.rotation = Quaternion.Euler(0, 0, 0);
        if(maxDistance < distance)
        {
            maxDistance = distance;
        }
        if (maxDistance > overallMaxDistance)
        {
            overallMaxDistance = maxDistance;
        }
        landed = true;
        cartControl.flying = false;
        buttons[1].SetAvailable();
        buttons[0].SetUnavailable();
        cartControl.OnCartLanded();
        cartRB.drag = 2;
        SetScoreDisplay(overallMaxDistance, cartControl.overallMaxAltitude, maxDistance, cartControl.maxAltitude);
    }
    public void SetScoreDisplay(float recordDist, float recordAlt, float dist, float alt)
    {
        recordAltitudeText.text = recordAlt.ToString("F2") + " m";
        recordDistanceText.text = recordDist.ToString("F2") + " m";
        lastAltitudeText.text = alt.ToString("F2") + " m";
        lastDistanceText.text = dist.ToString("F2") + " m";
    }

    public void ChangeModeToShop()
    {
        VRRig.transform.parent = cart.transform.parent;
        VRRig.transform.position = VRRigShopPlace.transform.position;
        VRRig.transform.rotation = VRRigShopPlace.transform.rotation;
        Debug.Log("jump to shop");
    }
    public void ChangeModeToCart()
    {
        VRRig.transform.parent = cart.transform;
        VRRig.transform.position = VRRigCartPlace.transform.position;
        VRRig.transform.rotation = VRRigCartPlace.transform.rotation;
        Debug.Log("jump to cart");
    }
}
