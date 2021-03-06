using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverController : MonoBehaviour
{
    /*
    obiekt ktory jest pivotem 
    dzwignia ma collider i jak go zlapie reka to liczy odlegosc reki od
    albo look at albo odlgelosc punktu od pivota
    */
    public GameObject target;
    public Vector3 targetPosition;
    public Vector3 diffRotation;
    public float MaxRotationBound;
    public float MinRotationBound;
    public bool tracking = false;

    public void RotateLever()
    {
        targetPosition = new Vector3(gameObject.transform.position.x, target.transform.position.y, target.transform.position.z);
        gameObject.transform.LookAt(targetPosition, Vector3.up);
        gameObject.transform.eulerAngles = gameObject.transform.eulerAngles - diffRotation;
    }

    public void StartTrackHand(GameObject trackTarget)
    {
        target = trackTarget;
        tracking = true;
    }
    public void StopTrackHand()
    {
        tracking = false;
    }
    public void WhileTracking()
    {
        if (tracking)
        {
            RotateLever();
        }
    }
}
