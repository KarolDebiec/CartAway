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
    public float[] rampLengthPrices;
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
    public MeshRenderer[] boosterIndicators;
    public TextMesh boosterPriceDisplay;
    public MeshRenderer[] wingsIndicators;
    public TextMesh wingsPriceDisplay;
    public MeshRenderer[] bodyIndicators;
    public TextMesh bodyPriceDisplay;
    public MeshRenderer[] boosterFuelIndicators;
    public TextMesh boosterFuelPriceDisplay;
    public MeshRenderer[] rampHeightIndicators;
    public TextMesh rampHeightPriceDisplay;
    public MeshRenderer[] rampLengthIndicators;
    public TextMesh rampLengthPriceDisplay;

    public Material upgradedMaterial;
    public Material canUpgradeMaterial;
    public Material cantUpgradeMaterial;

    public TextMesh moneyDisplay;

    public ShopButton[] shopButtons;
    void Start()
    {
        CheckAllButtons();
        CheckAllDisplays();
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
            CheckAllButtons();
            testUpL = false;
        }

    }

    public void CheckAllDisplays()
    {
        moneyDisplay.text = gameController.money.ToString("F2") + " $";
        if (boosterLevel >= 0 && boosterLevel < 3)
        {
            boosterPriceDisplay.text = boosterPrices[boosterLevel].ToString("F2") + " $";
        }
        if(boosterLevel == 0)
        {
            boosterIndicators[0].material = cantUpgradeMaterial;
            boosterIndicators[1].material = cantUpgradeMaterial;
            boosterIndicators[2].material = cantUpgradeMaterial;
            if(boosterPrices[boosterLevel] <= gameController.money)
            {
                boosterIndicators[0].material = canUpgradeMaterial;
            }
        }
        else if(boosterLevel == 1)
        {
            boosterIndicators[0].material = upgradedMaterial;
            boosterIndicators[1].material = cantUpgradeMaterial;
            boosterIndicators[2].material = cantUpgradeMaterial;
            if (boosterPrices[boosterLevel] <= gameController.money)
            {
                boosterIndicators[1].material = canUpgradeMaterial;
            }
        }
        else if (boosterLevel == 2)
        {
            boosterIndicators[0].material = upgradedMaterial;
            boosterIndicators[1].material = upgradedMaterial;
            boosterIndicators[2].material = cantUpgradeMaterial;
            if (boosterPrices[boosterLevel] <= gameController.money)
            {
                boosterIndicators[2].material = canUpgradeMaterial;
            }
        }
        else if (boosterLevel == 3)
        {
            boosterIndicators[0].material = upgradedMaterial;
            boosterIndicators[1].material = upgradedMaterial;
            boosterIndicators[2].material = upgradedMaterial;
        }
        ///////////////////////////////////////////////////////
        if (wingsLevel >= 0 && wingsLevel < 3)
        {
            wingsPriceDisplay.text = wingsPrices[wingsLevel].ToString("F2") + " $";
        }
        if (wingsLevel == 0)
        {
            wingsIndicators[0].material = cantUpgradeMaterial;
            wingsIndicators[1].material = cantUpgradeMaterial;
            wingsIndicators[2].material = cantUpgradeMaterial;
            if (wingsPrices[wingsLevel] <= gameController.money)
            {
                wingsIndicators[0].material = canUpgradeMaterial;
            }
        }
        else if (wingsLevel == 1)
        {
            wingsIndicators[0].material = upgradedMaterial;
            wingsIndicators[1].material = cantUpgradeMaterial;
            wingsIndicators[2].material = cantUpgradeMaterial;
            if (wingsPrices[wingsLevel] <= gameController.money)
            {
                wingsIndicators[1].material = canUpgradeMaterial;
            }
        }
        else if (wingsLevel == 2)
        {
            wingsIndicators[0].material = upgradedMaterial;
            wingsIndicators[1].material = upgradedMaterial;
            wingsIndicators[2].material = cantUpgradeMaterial;
            if (wingsPrices[wingsLevel] <= gameController.money)
            {
                wingsIndicators[2].material = canUpgradeMaterial;
            }
        }
        else if (wingsLevel == 3)
        {
            wingsIndicators[0].material = upgradedMaterial;
            wingsIndicators[1].material = upgradedMaterial;
            wingsIndicators[2].material = upgradedMaterial;
        }
        ///////////////////////////////////////////////////////
        if (bodyLevel >= 0 && bodyLevel < 3)
        {
            bodyPriceDisplay.text = bodyPrices[bodyLevel].ToString("F2") + " $";
        }
        if (bodyLevel == 0)
        {
            bodyIndicators[0].material = cantUpgradeMaterial;
            bodyIndicators[1].material = cantUpgradeMaterial;
            bodyIndicators[2].material = cantUpgradeMaterial;
            if (bodyPrices[bodyLevel] <= gameController.money)
            {
                bodyIndicators[0].material = canUpgradeMaterial;
            }
        }
        else if (bodyLevel == 1)
        {
            bodyIndicators[0].material = upgradedMaterial;
            bodyIndicators[1].material = cantUpgradeMaterial;
            bodyIndicators[2].material = cantUpgradeMaterial;
            if (bodyPrices[bodyLevel] <= gameController.money)
            {
                bodyIndicators[1].material = canUpgradeMaterial;
            }
        }
        else if (bodyLevel == 2)
        {
            bodyIndicators[0].material = upgradedMaterial;
            bodyIndicators[1].material = upgradedMaterial;
            bodyIndicators[2].material = cantUpgradeMaterial;
            if (bodyPrices[bodyLevel] <= gameController.money)
            {
                bodyIndicators[2].material = canUpgradeMaterial;
            }
        }
        else if (bodyLevel == 3)
        {
            bodyIndicators[0].material = upgradedMaterial;
            bodyIndicators[1].material = upgradedMaterial;
            bodyIndicators[2].material = upgradedMaterial;
        }
        ///////////////////////////////////////////////////////
        if (boosterFuelLevel >= 0 && boosterFuelLevel < 9)
        {
            boosterFuelPriceDisplay.text = boosterFuelPrices[boosterFuelLevel].ToString("F2") + " $";
        }
        if (boosterFuelLevel == 0)
        {
            boosterFuelIndicators[0].material = cantUpgradeMaterial;
            boosterFuelIndicators[1].material = cantUpgradeMaterial;
            boosterFuelIndicators[2].material = cantUpgradeMaterial;
            boosterFuelIndicators[3].material = cantUpgradeMaterial;
            boosterFuelIndicators[4].material = cantUpgradeMaterial;
            boosterFuelIndicators[5].material = cantUpgradeMaterial;
            boosterFuelIndicators[6].material = cantUpgradeMaterial;
            boosterFuelIndicators[7].material = cantUpgradeMaterial;
            boosterFuelIndicators[8].material = cantUpgradeMaterial;
            if (boosterFuelPrices[boosterFuelLevel] <= gameController.money)
            {
                boosterFuelIndicators[0].material = canUpgradeMaterial;
            }
        }
        else if (boosterFuelLevel == 1)
        {
            boosterFuelIndicators[0].material = upgradedMaterial;
            boosterFuelIndicators[1].material = cantUpgradeMaterial;
            boosterFuelIndicators[2].material = cantUpgradeMaterial;
            boosterFuelIndicators[3].material = cantUpgradeMaterial;
            boosterFuelIndicators[4].material = cantUpgradeMaterial;
            boosterFuelIndicators[5].material = cantUpgradeMaterial;
            boosterFuelIndicators[6].material = cantUpgradeMaterial;
            boosterFuelIndicators[7].material = cantUpgradeMaterial;
            boosterFuelIndicators[8].material = cantUpgradeMaterial;
            if (boosterFuelPrices[boosterFuelLevel] <= gameController.money)
            {
                boosterFuelIndicators[1].material = canUpgradeMaterial;
            }
        }
        else if (boosterFuelLevel == 2)
        {
            boosterFuelIndicators[0].material = upgradedMaterial;
            boosterFuelIndicators[1].material = upgradedMaterial;
            boosterFuelIndicators[2].material = cantUpgradeMaterial;
            boosterFuelIndicators[3].material = cantUpgradeMaterial;
            boosterFuelIndicators[4].material = cantUpgradeMaterial;
            boosterFuelIndicators[5].material = cantUpgradeMaterial;
            boosterFuelIndicators[6].material = cantUpgradeMaterial;
            boosterFuelIndicators[7].material = cantUpgradeMaterial;
            boosterFuelIndicators[8].material = cantUpgradeMaterial;
            if (boosterFuelPrices[boosterFuelLevel] <= gameController.money)
            {
                boosterFuelIndicators[2].material = canUpgradeMaterial;
            }
        }
        else if (boosterFuelLevel == 3)
        {
            boosterFuelIndicators[0].material = upgradedMaterial;
            boosterFuelIndicators[1].material = upgradedMaterial;
            boosterFuelIndicators[2].material = upgradedMaterial;
            boosterFuelIndicators[3].material = cantUpgradeMaterial;
            boosterFuelIndicators[4].material = cantUpgradeMaterial;
            boosterFuelIndicators[5].material = cantUpgradeMaterial;
            boosterFuelIndicators[6].material = cantUpgradeMaterial;
            boosterFuelIndicators[7].material = cantUpgradeMaterial;
            boosterFuelIndicators[8].material = cantUpgradeMaterial;
            if (boosterFuelPrices[boosterFuelLevel] <= gameController.money)
            {
                boosterFuelIndicators[3].material = canUpgradeMaterial;
            }
        }
        else if (boosterFuelLevel == 4)
        {
            boosterFuelIndicators[0].material = upgradedMaterial;
            boosterFuelIndicators[1].material = upgradedMaterial;
            boosterFuelIndicators[2].material = upgradedMaterial;
            boosterFuelIndicators[3].material = upgradedMaterial;
            boosterFuelIndicators[4].material = cantUpgradeMaterial;
            boosterFuelIndicators[5].material = cantUpgradeMaterial;
            boosterFuelIndicators[6].material = cantUpgradeMaterial;
            boosterFuelIndicators[7].material = cantUpgradeMaterial;
            boosterFuelIndicators[8].material = cantUpgradeMaterial;
            if (boosterFuelPrices[boosterFuelLevel] <= gameController.money)
            {
                boosterFuelIndicators[4].material = canUpgradeMaterial;
            }
        }
        else if (boosterFuelLevel == 5)
        {
            boosterFuelIndicators[0].material = upgradedMaterial;
            boosterFuelIndicators[1].material = upgradedMaterial;
            boosterFuelIndicators[2].material = upgradedMaterial;
            boosterFuelIndicators[3].material = upgradedMaterial;
            boosterFuelIndicators[4].material = upgradedMaterial;
            boosterFuelIndicators[5].material = cantUpgradeMaterial;
            boosterFuelIndicators[6].material = cantUpgradeMaterial;
            boosterFuelIndicators[7].material = cantUpgradeMaterial;
            boosterFuelIndicators[8].material = cantUpgradeMaterial;
            if (boosterFuelPrices[boosterFuelLevel] <= gameController.money)
            {
                boosterFuelIndicators[5].material = canUpgradeMaterial;
            }
        }
        else if (boosterFuelLevel == 6)
        {
            boosterFuelIndicators[0].material = upgradedMaterial;
            boosterFuelIndicators[1].material = upgradedMaterial;
            boosterFuelIndicators[2].material = upgradedMaterial;
            boosterFuelIndicators[3].material = upgradedMaterial;
            boosterFuelIndicators[4].material = upgradedMaterial;
            boosterFuelIndicators[5].material = upgradedMaterial;
            boosterFuelIndicators[6].material = cantUpgradeMaterial;
            boosterFuelIndicators[7].material = cantUpgradeMaterial;
            boosterFuelIndicators[8].material = cantUpgradeMaterial;
            if (boosterFuelPrices[boosterFuelLevel] <= gameController.money)
            {
                boosterFuelIndicators[6].material = canUpgradeMaterial;
            }
        }
        else if (boosterFuelLevel == 7)
        {
            boosterFuelIndicators[0].material = upgradedMaterial;
            boosterFuelIndicators[1].material = upgradedMaterial;
            boosterFuelIndicators[2].material = upgradedMaterial;
            boosterFuelIndicators[3].material = upgradedMaterial;
            boosterFuelIndicators[4].material = upgradedMaterial;
            boosterFuelIndicators[5].material = upgradedMaterial;
            boosterFuelIndicators[6].material = upgradedMaterial;
            boosterFuelIndicators[7].material = cantUpgradeMaterial;
            boosterFuelIndicators[8].material = cantUpgradeMaterial;
            if (boosterFuelPrices[boosterFuelLevel] <= gameController.money)
            {
                boosterFuelIndicators[7].material = canUpgradeMaterial;
            }
        }
        else if (boosterFuelLevel == 8)
        {
            boosterFuelIndicators[0].material = upgradedMaterial;
            boosterFuelIndicators[1].material = upgradedMaterial;
            boosterFuelIndicators[2].material = upgradedMaterial;
            boosterFuelIndicators[3].material = upgradedMaterial;
            boosterFuelIndicators[4].material = upgradedMaterial;
            boosterFuelIndicators[5].material = upgradedMaterial;
            boosterFuelIndicators[6].material = upgradedMaterial;
            boosterFuelIndicators[7].material = upgradedMaterial;
            boosterFuelIndicators[8].material = cantUpgradeMaterial;
            if (boosterFuelPrices[boosterFuelLevel] <= gameController.money)
            {
                boosterFuelIndicators[8].material = canUpgradeMaterial;
            }
        }
        else if (boosterFuelLevel == 9)
        {
            boosterFuelIndicators[0].material = upgradedMaterial;
            boosterFuelIndicators[1].material = upgradedMaterial;
            boosterFuelIndicators[2].material = upgradedMaterial;
            boosterFuelIndicators[3].material = upgradedMaterial;
            boosterFuelIndicators[4].material = upgradedMaterial;
            boosterFuelIndicators[5].material = upgradedMaterial;
            boosterFuelIndicators[6].material = upgradedMaterial;
            boosterFuelIndicators[7].material = upgradedMaterial;
            boosterFuelIndicators[8].material = upgradedMaterial;
        }
        ///////////////////////////////////////////////////////
        if (rampController.height >= 0 && rampController.height < 9)
        {
            rampHeightPriceDisplay.text = rampHeightPrices[rampController.height].ToString("F2") + " $";
        }
        if (rampController.height == 0)
        {
            rampHeightIndicators[0].material = cantUpgradeMaterial;
            rampHeightIndicators[1].material = cantUpgradeMaterial;
            rampHeightIndicators[2].material = cantUpgradeMaterial;
            rampHeightIndicators[3].material = cantUpgradeMaterial;
            rampHeightIndicators[4].material = cantUpgradeMaterial;
            rampHeightIndicators[5].material = cantUpgradeMaterial;
            rampHeightIndicators[6].material = cantUpgradeMaterial;
            rampHeightIndicators[7].material = cantUpgradeMaterial;
            rampHeightIndicators[8].material = cantUpgradeMaterial;
            if (rampHeightPrices[rampController.height] <= gameController.money)
            {
                rampHeightIndicators[0].material = canUpgradeMaterial;
            }
        }
        else if (rampController.height == 1)
        {
            rampHeightIndicators[0].material = upgradedMaterial;
            rampHeightIndicators[1].material = cantUpgradeMaterial;
            rampHeightIndicators[2].material = cantUpgradeMaterial;
            rampHeightIndicators[3].material = cantUpgradeMaterial;
            rampHeightIndicators[4].material = cantUpgradeMaterial;
            rampHeightIndicators[5].material = cantUpgradeMaterial;
            rampHeightIndicators[6].material = cantUpgradeMaterial;
            rampHeightIndicators[7].material = cantUpgradeMaterial;
            rampHeightIndicators[8].material = cantUpgradeMaterial;
            if (rampHeightPrices[rampController.height] <= gameController.money)
            {
                rampHeightIndicators[1].material = canUpgradeMaterial;
            }
        }
        else if (rampController.height == 2)
        {
            rampHeightIndicators[0].material = upgradedMaterial;
            rampHeightIndicators[1].material = upgradedMaterial;
            rampHeightIndicators[2].material = cantUpgradeMaterial;
            rampHeightIndicators[3].material = cantUpgradeMaterial;
            rampHeightIndicators[4].material = cantUpgradeMaterial;
            rampHeightIndicators[5].material = cantUpgradeMaterial;
            rampHeightIndicators[6].material = cantUpgradeMaterial;
            rampHeightIndicators[7].material = cantUpgradeMaterial;
            rampHeightIndicators[8].material = cantUpgradeMaterial;
            if (rampHeightPrices[rampController.height] <= gameController.money)
            {
                rampHeightIndicators[2].material = canUpgradeMaterial;
            }
        }
        else if (rampController.height == 3)
        {
            rampHeightIndicators[0].material = upgradedMaterial;
            rampHeightIndicators[1].material = upgradedMaterial;
            rampHeightIndicators[2].material = upgradedMaterial;
            rampHeightIndicators[3].material = cantUpgradeMaterial;
            rampHeightIndicators[4].material = cantUpgradeMaterial;
            rampHeightIndicators[5].material = cantUpgradeMaterial;
            rampHeightIndicators[6].material = cantUpgradeMaterial;
            rampHeightIndicators[7].material = cantUpgradeMaterial;
            rampHeightIndicators[8].material = cantUpgradeMaterial;
            if (rampHeightPrices[rampController.height] <= gameController.money)
            {
                rampHeightIndicators[3].material = canUpgradeMaterial;
            }
        }
        else if (rampController.height == 4)
        {
            rampHeightIndicators[0].material = upgradedMaterial;
            rampHeightIndicators[1].material = upgradedMaterial;
            rampHeightIndicators[2].material = upgradedMaterial;
            rampHeightIndicators[3].material = upgradedMaterial;
            rampHeightIndicators[4].material = cantUpgradeMaterial;
            rampHeightIndicators[5].material = cantUpgradeMaterial;
            rampHeightIndicators[6].material = cantUpgradeMaterial;
            rampHeightIndicators[7].material = cantUpgradeMaterial;
            rampHeightIndicators[8].material = cantUpgradeMaterial;
            if (rampHeightPrices[rampController.height] <= gameController.money)
            {
                rampHeightIndicators[4].material = canUpgradeMaterial;
            }
        }
        else if (rampController.height == 5)
        {
            rampHeightIndicators[0].material = upgradedMaterial;
            rampHeightIndicators[1].material = upgradedMaterial;
            rampHeightIndicators[2].material = upgradedMaterial;
            rampHeightIndicators[3].material = upgradedMaterial;
            rampHeightIndicators[4].material = upgradedMaterial;
            rampHeightIndicators[5].material = cantUpgradeMaterial;
            rampHeightIndicators[6].material = cantUpgradeMaterial;
            rampHeightIndicators[7].material = cantUpgradeMaterial;
            rampHeightIndicators[8].material = cantUpgradeMaterial;
            if (rampHeightPrices[rampController.height] <= gameController.money)
            {
                rampHeightIndicators[5].material = canUpgradeMaterial;
            }
        }
        else if (rampController.height == 6)
        {
            rampHeightIndicators[0].material = upgradedMaterial;
            rampHeightIndicators[1].material = upgradedMaterial;
            rampHeightIndicators[2].material = upgradedMaterial;
            rampHeightIndicators[3].material = upgradedMaterial;
            rampHeightIndicators[4].material = upgradedMaterial;
            rampHeightIndicators[5].material = upgradedMaterial;
            rampHeightIndicators[6].material = cantUpgradeMaterial;
            rampHeightIndicators[7].material = cantUpgradeMaterial;
            rampHeightIndicators[8].material = cantUpgradeMaterial;
            if (rampHeightPrices[rampController.height] <= gameController.money)
            {
                rampHeightIndicators[6].material = canUpgradeMaterial;
            }
        }
        else if (rampController.height == 7)
        {
            rampHeightIndicators[0].material = upgradedMaterial;
            rampHeightIndicators[1].material = upgradedMaterial;
            rampHeightIndicators[2].material = upgradedMaterial;
            rampHeightIndicators[3].material = upgradedMaterial;
            rampHeightIndicators[4].material = upgradedMaterial;
            rampHeightIndicators[5].material = upgradedMaterial;
            rampHeightIndicators[6].material = upgradedMaterial;
            rampHeightIndicators[7].material = cantUpgradeMaterial;
            rampHeightIndicators[8].material = cantUpgradeMaterial;
            if (rampHeightPrices[rampController.height] <= gameController.money)
            {
                rampHeightIndicators[7].material = canUpgradeMaterial;
            }
        }
        else if (rampController.height == 8)
        {
            rampHeightIndicators[0].material = upgradedMaterial;
            rampHeightIndicators[1].material = upgradedMaterial;
            rampHeightIndicators[2].material = upgradedMaterial;
            rampHeightIndicators[3].material = upgradedMaterial;
            rampHeightIndicators[4].material = upgradedMaterial;
            rampHeightIndicators[5].material = upgradedMaterial;
            rampHeightIndicators[6].material = upgradedMaterial;
            rampHeightIndicators[7].material = upgradedMaterial;
            rampHeightIndicators[8].material = cantUpgradeMaterial;
            if (rampHeightPrices[rampController.height] <= gameController.money)
            {
                rampHeightIndicators[8].material = canUpgradeMaterial;
            }
        }
        else if (rampController.height == 9)
        {
            rampHeightIndicators[0].material = upgradedMaterial;
            rampHeightIndicators[1].material = upgradedMaterial;
            rampHeightIndicators[2].material = upgradedMaterial;
            rampHeightIndicators[3].material = upgradedMaterial;
            rampHeightIndicators[4].material = upgradedMaterial;
            rampHeightIndicators[5].material = upgradedMaterial;
            rampHeightIndicators[6].material = upgradedMaterial;
            rampHeightIndicators[7].material = upgradedMaterial;
            rampHeightIndicators[8].material = upgradedMaterial;
        }
        ///////////////////////////////////////////////////////
        if (rampController.length >= 0 && rampController.length < 9)
        {
            rampLengthPriceDisplay.text = rampLengthPrices[rampController.length].ToString("F2") + " $";
        }
        if (rampController.length == 0)
        {
            rampLengthIndicators[0].material = cantUpgradeMaterial;
            rampLengthIndicators[1].material = cantUpgradeMaterial;
            rampLengthIndicators[2].material = cantUpgradeMaterial;
            rampLengthIndicators[3].material = cantUpgradeMaterial;
            rampLengthIndicators[4].material = cantUpgradeMaterial;
            rampLengthIndicators[5].material = cantUpgradeMaterial;
            rampLengthIndicators[6].material = cantUpgradeMaterial;
            rampLengthIndicators[7].material = cantUpgradeMaterial;
            rampLengthIndicators[8].material = cantUpgradeMaterial;
            if (rampHeightPrices[rampController.length] <= gameController.money)
            {
                rampLengthIndicators[0].material = canUpgradeMaterial;
            }
        }
        else if (rampController.length == 1)
        {
            rampLengthIndicators[0].material = upgradedMaterial;
            rampLengthIndicators[1].material = cantUpgradeMaterial;
            rampLengthIndicators[2].material = cantUpgradeMaterial;
            rampLengthIndicators[3].material = cantUpgradeMaterial;
            rampLengthIndicators[4].material = cantUpgradeMaterial;
            rampLengthIndicators[5].material = cantUpgradeMaterial;
            rampLengthIndicators[6].material = cantUpgradeMaterial;
            rampLengthIndicators[7].material = cantUpgradeMaterial;
            rampLengthIndicators[8].material = cantUpgradeMaterial;
            if (rampHeightPrices[rampController.length] <= gameController.money)
            {
                rampLengthIndicators[1].material = canUpgradeMaterial;
            }
        }
        else if (rampController.length == 2)
        {
            rampLengthIndicators[0].material = upgradedMaterial;
            rampLengthIndicators[1].material = upgradedMaterial;
            rampLengthIndicators[2].material = cantUpgradeMaterial;
            rampLengthIndicators[3].material = cantUpgradeMaterial;
            rampLengthIndicators[4].material = cantUpgradeMaterial;
            rampLengthIndicators[5].material = cantUpgradeMaterial;
            rampLengthIndicators[6].material = cantUpgradeMaterial;
            rampLengthIndicators[7].material = cantUpgradeMaterial;
            rampLengthIndicators[8].material = cantUpgradeMaterial;
            if (rampHeightPrices[rampController.length] <= gameController.money)
            {
                rampLengthIndicators[2].material = canUpgradeMaterial;
            }
        }
        else if (rampController.length == 3)
        {
            rampLengthIndicators[0].material = upgradedMaterial;
            rampLengthIndicators[1].material = upgradedMaterial;
            rampLengthIndicators[2].material = upgradedMaterial;
            rampLengthIndicators[3].material = cantUpgradeMaterial;
            rampLengthIndicators[4].material = cantUpgradeMaterial;
            rampLengthIndicators[5].material = cantUpgradeMaterial;
            rampLengthIndicators[6].material = cantUpgradeMaterial;
            rampLengthIndicators[7].material = cantUpgradeMaterial;
            rampLengthIndicators[8].material = cantUpgradeMaterial;
            if (rampHeightPrices[rampController.length] <= gameController.money)
            {
                rampLengthIndicators[3].material = canUpgradeMaterial;
            }
        }
        else if (rampController.length == 4)
        {
            rampLengthIndicators[0].material = upgradedMaterial;
            rampLengthIndicators[1].material = upgradedMaterial;
            rampLengthIndicators[2].material = upgradedMaterial;
            rampLengthIndicators[3].material = upgradedMaterial;
            rampLengthIndicators[4].material = cantUpgradeMaterial;
            rampLengthIndicators[5].material = cantUpgradeMaterial;
            rampLengthIndicators[6].material = cantUpgradeMaterial;
            rampLengthIndicators[7].material = cantUpgradeMaterial;
            rampLengthIndicators[8].material = cantUpgradeMaterial;
            if (rampHeightPrices[rampController.length] <= gameController.money)
            {
                rampLengthIndicators[4].material = canUpgradeMaterial;
            }
        }
        else if (rampController.length == 5)
        {
            rampLengthIndicators[0].material = upgradedMaterial;
            rampLengthIndicators[1].material = upgradedMaterial;
            rampLengthIndicators[2].material = upgradedMaterial;
            rampLengthIndicators[3].material = upgradedMaterial;
            rampLengthIndicators[4].material = upgradedMaterial;
            rampLengthIndicators[5].material = cantUpgradeMaterial;
            rampLengthIndicators[6].material = cantUpgradeMaterial;
            rampLengthIndicators[7].material = cantUpgradeMaterial;
            rampLengthIndicators[8].material = cantUpgradeMaterial;
            if (rampHeightPrices[rampController.length] <= gameController.money)
            {
                rampLengthIndicators[5].material = canUpgradeMaterial;
            }
        }
        else if (rampController.length == 6)
        {
            rampLengthIndicators[0].material = upgradedMaterial;
            rampLengthIndicators[1].material = upgradedMaterial;
            rampLengthIndicators[2].material = upgradedMaterial;
            rampLengthIndicators[3].material = upgradedMaterial;
            rampLengthIndicators[4].material = upgradedMaterial;
            rampLengthIndicators[5].material = upgradedMaterial;
            rampLengthIndicators[6].material = cantUpgradeMaterial;
            rampLengthIndicators[7].material = cantUpgradeMaterial;
            rampLengthIndicators[8].material = cantUpgradeMaterial;
            if (rampHeightPrices[rampController.length] <= gameController.money)
            {
                rampLengthIndicators[6].material = canUpgradeMaterial;
            }
        }
        else if (rampController.length == 7)
        {
            rampLengthIndicators[0].material = upgradedMaterial;
            rampLengthIndicators[1].material = upgradedMaterial;
            rampLengthIndicators[2].material = upgradedMaterial;
            rampLengthIndicators[3].material = upgradedMaterial;
            rampLengthIndicators[4].material = upgradedMaterial;
            rampLengthIndicators[5].material = upgradedMaterial;
            rampLengthIndicators[6].material = upgradedMaterial;
            rampLengthIndicators[7].material = cantUpgradeMaterial;
            rampLengthIndicators[8].material = cantUpgradeMaterial;
            if (rampHeightPrices[rampController.length] <= gameController.money)
            {
                rampLengthIndicators[7].material = canUpgradeMaterial;
            }
        }
        else if (rampController.length == 8)
        {
            rampLengthIndicators[0].material = upgradedMaterial;
            rampLengthIndicators[1].material = upgradedMaterial;
            rampLengthIndicators[2].material = upgradedMaterial;
            rampLengthIndicators[3].material = upgradedMaterial;
            rampLengthIndicators[4].material = upgradedMaterial;
            rampLengthIndicators[5].material = upgradedMaterial;
            rampLengthIndicators[6].material = upgradedMaterial;
            rampLengthIndicators[7].material = upgradedMaterial;
            rampLengthIndicators[8].material = cantUpgradeMaterial;
            if (rampHeightPrices[rampController.length] <= gameController.money)
            {
                rampLengthIndicators[8].material = canUpgradeMaterial;
            }
        }
        else if (rampController.length == 9)
        {
            rampLengthIndicators[0].material = upgradedMaterial;
            rampLengthIndicators[1].material = upgradedMaterial;
            rampLengthIndicators[2].material = upgradedMaterial;
            rampLengthIndicators[3].material = upgradedMaterial;
            rampLengthIndicators[4].material = upgradedMaterial;
            rampLengthIndicators[5].material = upgradedMaterial;
            rampLengthIndicators[6].material = upgradedMaterial;
            rampLengthIndicators[7].material = upgradedMaterial;
            rampLengthIndicators[8].material = upgradedMaterial;
        }
    }

    public void CheckAllButtons()
    {
        foreach(ShopButton button in shopButtons)
        {
            button.UnpressButton();
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
        if (boosterLevel < maxBoosterLevel)
        {
            if (gameController.money >= boosterPrices[boosterLevel])
            {
                return true;
            }
            else
            {
                return false;
            }
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
        if (wingsLevel < maxWingsLevel)
        {
            if (gameController.money >= wingsPrices[wingsLevel])
            {
                return true;
            }
            else
            {
                return false;
            }
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
        if (bodyLevel < maxBodyLevel)
        {
            if (gameController.money >= bodyPrices[bodyLevel])
            {
                return true;
            }
            else
            {
                return false;
            }
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
        if (boosterFuelLevel < maxBoosterFuelLevel)
        {
            if (gameController.money >= boosterFuelPrices[boosterFuelLevel])
            {
                return true;
            }
            else
            {
                return false;
            }
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
        if(rampController.height < maxRampHeightLevel)
        {
            if (gameController.money >= rampHeightPrices[rampController.height] )
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public void UpgradeRampLength()
    {
        if (canUpgradeRampLenght())
        {
            gameController.money -= rampLengthPrices[rampController.length];
            rampController.upgradeLength();
        }
    }
    public bool canUpgradeRampLenght()
    {
        if(rampController.length < maxRampLenghtLevel)
        {
            if (gameController.money >= rampLengthPrices[rampController.length] )
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
}
