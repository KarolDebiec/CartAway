using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartController : MonoBehaviour
{
    public bool isStatic = false; // used while in upgrade shop

    public GameController gameController;
    public float Debug2;
    public float Debug3;
    public bool flying = false;
    public GameObject tiltLewer;
    public float tilt;
    public float tiltMultiplier;
    private Rigidbody rb;
    private Vector3 rotationOffset; // amount to rotate every frame
    public bool rocketBoost;
    public float rocketBoostForce;
    public float rocketBoostFuel = 10f;
    public float maxRocketBoostFuel = 10f;
    public GameObject tiltDisplay;
    public float speed;
    public GameObject speedometr;
    public float maxSpeed; // used to set max Speed of speedometr
    public float maxSpeedAchieved;
    public float maxFrontAngle; //from 0 to 360 this should be about 80
    public float maxBackAngle; //this should be about 280

    public LeverController leverController;
    public float altitude;
    public float maxAltitude; // max altitude achieved in the last jump
    public float overallMaxAltitude; // max altitude achieved in the whole game
    public TextMesh altitudeText;


    private float cartAngle;
    public float realCartAngle;
    private float calcLift;
    public float liftMultiplier = 10;
    public bool haveWings = false;
    public float dragValue = 0.3f;
    public float additionalDragValue = 0.3f;
    public float additionalForceOnGoingDownValue = 0.3f;
    public float additionalForceOnGoingUpValue = 0.3f;
    public float speedRelation;
    public Vector3 staticLiftForce;
    public Vector3 liftForce;

    public List<GameObject> wings;
    public List<GameObject> bodies;
    public List<GameObject> boosters;
    public GameObject activeWing;
    public GameObject activeBody;
    public GameObject activeBooster;
    public GameObject fuelDisplay;
    public GameObject fuelDisplayTop;
    public GameObject fuelDisplayBottom;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        altitudeText.text = "0 m";
        UpgradeCart(1, 0);
        UpgradeCart(2, 0);
        UpgradeCart(3, 0);
        rocketBoostFuel = maxRocketBoostFuel;
        //load jesli dodamy zapis
    }

    private void FixedUpdate()
    {
        if (!isStatic)
        {
            if (rocketBoost && flying)
            {
                rb.AddForce(gameObject.transform.forward * rocketBoostForce);
                SetFuelDisplay(rocketBoostFuel, maxRocketBoostFuel);
                rocketBoostFuel -= Time.deltaTime;
                if (rocketBoostFuel <= 0)
                {
                    StopRocketBoost();
                }
            }
            cartAngle = gameObject.transform.localEulerAngles.x;
            if (gameObject.transform.localRotation.x > 0)
            {
                realCartAngle = cartAngle;
            }
            else
            {
                realCartAngle = -(360 - cartAngle);
            }
            calcLift = (realCartAngle / 90) * Mathf.PI;
            if (flying)
            {
                altitudeText.text = altitude.ToString("F1") + " m";
            }
            if (haveWings && flying) // to do mostly
            {
                if (calcLift <= 0)
                {
                    //liftForce = (Vector3.up * liftMultiplier * -calcLift * speed / speedRelation) + staticLiftForce;
                    liftForce = (Vector3.up * liftMultiplier * -calcLift) + staticLiftForce;
                    Debug.Log("here goes up");
                    //rb.AddForce(Vector3.forward * Mathf.Abs(calcLift) * additionalForceOnGoingUpValue * speed / speedRelation);
                    if(rb.velocity.z > 5)
                    {
                        rb.AddForce((-Vector3.forward * Mathf.Abs(calcLift) * additionalForceOnGoingUpValue)/10);
                    }
                }
                else if (calcLift > 0)
                {
                    //liftForce = (Vector3.up * liftMultiplier * -calcLift * speed / speedRelation);
                    liftForce = (Vector3.up * liftMultiplier * -calcLift );
                    //rb.AddForce(Vector3.forward * Mathf.Abs(calcLift) * additionalForceOnGoingDownValue * speed / speedRelation);
                    rb.AddForce((Vector3.forward * Mathf.Abs(calcLift) * additionalForceOnGoingUpValue) / 10);
                    
                }
                rb.AddForce(liftForce);
            }
        }
    }
    void Update()
    {
        if(!isStatic)
        {
            altitude = gameObject.transform.position.y;
            if (altitude > maxAltitude)
            {
                maxAltitude = altitude;
            }
            /*if(tiltLewer.transform.eulerAngles.x - gameObject.transform.eulerAngles.x>180f)
            {
                tilt = tiltLewer.transform.eulerAngles.x - gameObject.transform.eulerAngles.x - 360;
            }
            else
            {
                tilt = tiltLewer.transform.eulerAngles.x - gameObject.transform.eulerAngles.x;
            }*/
            leverController.WhileTracking();
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
                rotationOffset = new Vector3(tilt * tiltMultiplier * Time.deltaTime, 0, 0);
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
            //rb.AddTorque(transform.right * tiltMultiplier * tilt); this or the line beneath
            //gameObject.transform.Rotate(rotationOffset);
            tiltDisplay.transform.localRotation = Quaternion.Euler(gameObject.transform.rotation.eulerAngles.x - 90, -90, 90);
            speed = rb.velocity.magnitude;
            if (speed >= maxSpeed)
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
        rocketBoostFuel = maxRocketBoostFuel;
        tiltLewer.transform.localRotation = Quaternion.Euler(0, 0, 0);
        maxSpeedAchieved = 0;
        SetFuelDisplay(1, 1);
    }
    public void StartRocketBoost()
    {
        rocketBoost = true;
        //zalaczyc particle i dzwiek
    }
    public void StopRocketBoost()
    {
        gameController.NoBoostFuel();
        rocketBoost = false;
        //wylaczyc particle i dzwiek
    }
    public void OnCartLanded()
    {
        altitudeText.text = "0 m";
        if(maxAltitude > overallMaxAltitude)
        {
            overallMaxAltitude = maxAltitude;
        }
        if(rocketBoost)
        {
            StopRocketBoost();
        }
    }

    public void SetFuelDisplay(float fuel,float maxFuel)
    {
        if (fuel < maxFuel)
        {
            float BottomScale = (fuel / maxFuel) * 2;
            fuelDisplayTop.transform.localScale = new Vector3(1, 1, 2f - BottomScale);
            fuelDisplayBottom.transform.localScale = new Vector3(1, 1, BottomScale);
        }
        else
        {
            fuelDisplayTop.transform.localScale = new Vector3(1, 1, 0);
            fuelDisplayBottom.transform.localScale = new Vector3(1, 1, 2);
        }
    }
    public void UpgradeCart(int upgradeType,int upgradeLevel) //by default there are 4 upgrades for cart(booster,body,wings,booster fuel) and each have 4 levels of upgrade (none,lvl1,lvl2,lvl3) and 10 levels for fuel
    {
        switch (upgradeType)
        {
            case 1:
                switch (upgradeLevel)
                {
                    case 0:
                        if(activeBooster != null)
                        {
                            activeBooster.SetActive(false);
                        }
                        fuelDisplay.SetActive(false);
                        //change parameters here

                        break; 

                    case 1:
                        if (activeBooster != null)
                        {
                            activeBooster.SetActive(false);
                        }
                        activeBooster = boosters[0];
                        activeBooster.SetActive(true);
                        fuelDisplay.SetActive(true);
                        //change parameters here
                        rocketBoostForce = 20;
                        break;

                    case 2:
                        if (activeBooster != null)
                        {
                            activeBooster.SetActive(false);
                        }
                        activeBooster = boosters[1];
                        activeBooster.SetActive(true);
                        fuelDisplay.SetActive(true);
                        //change parameters here
                        rocketBoostForce = 40;
                        break;

                    case 3:
                        if (activeBooster != null)
                        {
                            activeBooster.SetActive(false);
                        }
                        activeBooster = boosters[2];
                        activeBooster.SetActive(true);
                        fuelDisplay.SetActive(true);
                        //change parameters here
                        rocketBoostForce = 100;
                        break;

                    default:
                        Debug.Log("wrong level in upgrade cart function");
                        break;
                }
                break;

            case 2:
                switch (upgradeLevel)
                {
                    case 0:
                        if (activeBody != null)
                        {
                            activeBody.SetActive(false);
                        }
                        dragValue = 0.30f;
                        break;

                    case 1:
                        if (activeBody != null)
                        {
                            activeBody.SetActive(false);
                        }
                        activeBody = bodies[0];
                        activeBody.SetActive(true);
                        //change parameters here
                        dragValue = 0.25f;
                        break;

                    case 2:
                        if (activeBody != null)
                        {
                            activeBody.SetActive(false);
                        }
                        activeBody = bodies[1];
                        activeBody.SetActive(true);
                        //change parameters here
                        dragValue = 0.20f;
                        break;

                    case 3:
                        if (activeBody != null)
                        {
                            activeBody.SetActive(false);
                        }
                        activeBody = bodies[2];
                        activeBody.SetActive(true);
                        //change parameters here
                        dragValue = 0.10f;
                        break;

                    default:
                        Debug.Log("wrong level in upgrade cart function");
                        break;
                }
                break;

            case 3:
                switch (upgradeLevel)
                {
                    case 0:
                        if (activeWing != null)
                        {
                            activeWing.SetActive(false);
                        }
                        haveWings = false;
                        break;

                    case 1:
                        if (activeWing != null)
                        {
                            activeWing.SetActive(false);
                        }
                        activeWing = wings[0];
                        activeWing.SetActive(true);
                        //change parameters here
                        haveWings = true;
                        liftMultiplier = 5;
                        break;

                    case 2:
                        if (activeWing != null)
                        {
                            activeWing.SetActive(false);
                        }
                        activeWing = wings[1];
                        activeWing.SetActive(true);
                        //change parameters here
                        haveWings = true;
                        liftMultiplier = 5.5f;
                        break;

                    case 3:
                        if (activeWing != null)
                        {
                            activeWing.SetActive(false);
                        }
                        activeWing = wings[2];
                        activeWing.SetActive(true);
                        //change parameters here
                        haveWings = true;
                        liftMultiplier = 6;
                        break;

                    default:
                        Debug.Log("wrong level in upgrade cart function");
                        break;
                }
                break;
            case 4:
                switch (upgradeLevel)
                {
                    case 0:
                        maxRocketBoostFuel = 3;
                        break;

                    case 1:
                        maxRocketBoostFuel = 4;
                        break;

                    case 2:
                        maxRocketBoostFuel = 5;
                        break;

                    case 3:
                        maxRocketBoostFuel = 6;
                        break;

                    case 4:
                        maxRocketBoostFuel = 7;
                        break;

                    case 5:
                        maxRocketBoostFuel = 8;
                        break;

                    case 6:
                        maxRocketBoostFuel = 9;
                        break;

                    case 7:
                        maxRocketBoostFuel = 10;
                        break;

                    case 8:
                        maxRocketBoostFuel = 12;
                        break;

                    case 9:
                        maxRocketBoostFuel = 14;
                        break;

                    case 10:
                        maxRocketBoostFuel = 18;
                        break;

                    default:
                        Debug.Log("wrong level in upgrade cart function");
                        break;
                }
                break;
            default:
                Debug.Log("wrong type in upgrade cart function");
                break;
        }
    }
    public void SetStatic(bool value)
    {
        isStatic = value;
    }
}
