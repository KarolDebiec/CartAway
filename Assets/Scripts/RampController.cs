using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RampController : MonoBehaviour
{
    public bool testH;
    public bool testL;

    public GameObject rampTarget;
    public GameObject rampOrigin;
    public int height = 0;
    public int maxHeight = 9;
    public int length = 0;
    public int maxLength = 9;
    public GameObject activeRamp;
    public List<GameObject> rampTypes;// there should be 100 ramps here length is upgraded every 10 heights
    public List<Vector3> rampTargetPositions;// there should be 11 positions here
    public List<Vector3> rampOriginPositions;// there should be 11 positions here

    
    void Update()
    {
        if(testH)
        {
            upgradeHeight();
            testH = false;
        }

        if (testL)
        {
            upgradeLength();
            testL = false;
        }
    }

    public void upgradeHeight()
    {
        if(height < maxHeight)
        {
            height++;
            rampTarget.transform.position = rampTargetPositions[height];
            rampOrigin.transform.position = rampOriginPositions[length];
            upgradeDisplayedRamp(length, height);
        }
    }
    public void upgradeLength()
    {
        if(length < maxLength)
        {
            length++;
            rampTarget.transform.position = rampTargetPositions[height];
            rampOrigin.transform.position = rampOriginPositions[length];
            upgradeDisplayedRamp(length, height);
        }
    }
    public void upgradeDisplayedRamp(int x, int y)
    {
        activeRamp.SetActive(false);
        activeRamp = rampTypes[x*10+y];
        activeRamp.SetActive(true);
    }
}
