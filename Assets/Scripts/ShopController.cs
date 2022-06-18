using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    public CartController cartController;
    public GameController gameController;
    public RampController rampController;

    public bool testUpBF;
    public bool testUpBD;
    public bool testUpB;
    public bool testUpW;
    public bool testUpH;
    public bool testUpL;

    public float[] rampHeightPrices; 
    public float[] rampLenghtPrices;
    public float[] boosterPrices;
    public float[] wingsPrices;
    public float[] bodyPrices;
    public float[] boosterFuelPrices;
    public int boosterLevel = 0;
    public int wingsLevel = 0;
    public int bodyLevel = 0;
    public int boosterFuelLevel = 0;
    public int maxBoosterLevel = 3;
    public int maxWingsLevel = 3;
    public int maxBodyLevel = 3;
    public int maxBoosterFuelLevel = 9;
    public int maxRampHeightLevel = 9;
    public int maxRampLenghtLevel = 9;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (testUpBF)
        {
            UpgradeBoosterFuel();
            testUpBF = false;
        }

        if (testUpBD)
        {
            UpgradeBody();
            testUpBD = false;
        }
        if (testUpB)
        {
            UpgradeBooster();
            testUpB = false;
        }

        if (testUpW)
        {
            UpgradeWings();
            testUpW = false;
        }
        if (testUpH)
        {
            UpgradeRampHeight();
            testUpH = false;
        }

        if (testUpL)
        {
            UpgradeRampLenght();
            testUpL = false;
        }

    }


    public void UpgradeBooster()
    {
        if (canUpgradeBooster())
        {
            gameController.money -= boosterPrices[boosterLevel];
            boosterLevel++;
            cartController.UpgradeCart(1, boosterLevel);
        }
    }
    public bool canUpgradeBooster()
    {
        if (gameController.money >= boosterPrices[boosterLevel] && boosterLevel < maxBoosterLevel)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void UpgradeWings()
    {
        if (canUpgradeWings())
        {
            gameController.money -= wingsPrices[wingsLevel];
            wingsLevel++;
            cartController.UpgradeCart(3, wingsLevel);
        }
    }
    public bool canUpgradeWings()
    {
        if (gameController.money >= wingsPrices[wingsLevel] && wingsLevel < maxWingsLevel)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void UpgradeBody()
    {
        if (canUpgradeBody())
        {
            gameController.money -= bodyPrices[bodyLevel];
            bodyLevel++;
            cartController.UpgradeCart(2, bodyLevel);
        }
    }
    public bool canUpgradeBody()
    {
        if (gameController.money >= bodyPrices[bodyLevel] && bodyLevel < maxBodyLevel)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void UpgradeBoosterFuel()
    {
        if (canUpgradeBoosterFuel())
        {
            gameController.money -= boosterFuelPrices[boosterFuelLevel];
            boosterFuelLevel++;
            cartController.UpgradeCart(4, boosterFuelLevel);
        }
    }
    public bool canUpgradeBoosterFuel()
    {
        if (gameController.money >= boosterFuelPrices[boosterFuelLevel] && boosterFuelLevel < maxBoosterFuelLevel)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void UpgradeRampHeight()
    {
        if (canUpgradeRampHeight())
        {
            gameController.money -= rampHeightPrices[rampController.height];
            rampController.upgradeHeight();
        }
    }
    public bool canUpgradeRampHeight()
    {
        if (gameController.money >= rampHeightPrices[rampController.height] && rampController.height < maxRampHeightLevel)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void UpgradeRampLenght()
    {
        if (canUpgradeRampLenght())
        {
            gameController.money -= rampLenghtPrices[rampController.length];
            rampController.upgradeLength();
        }
    }
    public bool canUpgradeRampLenght()
    {
        if(gameController.money >= rampLenghtPrices[rampController.length] && rampController.length< maxRampLenghtLevel)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
