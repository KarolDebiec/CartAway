using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopButton : MonoBehaviour
{
    public GameObject button;//referance to button in scene (collider have to be out of button so it doesnt jump around)
    public Vector3 pressedLocalPos;
    private Vector3 unpressedLocalPos;
    public ShopController shopController;
    public Material canPressMaterial;
    public Material cannotPressMaterial;
    public Material pressedMaterial;
    public bool oneTimeButton = true;
    private bool canPress = true; // if above is true then changes to false after first press
    public MeshRenderer meshRenderer;

    public GameController gameController;
    public List<bool> buttonTypes;// all button types in the shop 0-upgrade ramp height , 1-upgrade ramp length, 2- booster, 3-wings,4- body,5- fuel
    void Start()
    {
        meshRenderer = button.GetComponent<MeshRenderer>();
        unpressedLocalPos = button.transform.localPosition;
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    public void PressButton()
    {
        if (canPress)
        {
            button.transform.localPosition = pressedLocalPos;
            meshRenderer.material = pressedMaterial;
            if (buttonTypes[0] == true)
            {
                shopController.UpgradeRampHeight();
            }
            else if (buttonTypes[1] == true)
            {
                shopController.UpgradeRampLength();
            }
            else if (buttonTypes[2] == true)
            {
                shopController.UpgradeBooster();
            }
            else if (buttonTypes[3] == true)
            {
                shopController.UpgradeWings();
            }
            else if (buttonTypes[4] == true)
            {
                shopController.UpgradeBody();
            }
            else if (buttonTypes[5] == true)
            {
                shopController.UpgradeBoosterFuel();
            }
            shopController.CheckAllDisplays();
            gameController.PlayButtonClickSound();
        }
    }
    public void UnpressButton()
    {
        button.transform.localPosition = unpressedLocalPos;
        if (buttonTypes[0] == true && shopController.canUpgradeRampHeight())
        {
            SetAvailable();
        }
        else if (buttonTypes[1] == true && shopController.canUpgradeRampLenght())
        {
            SetAvailable();
        }
        else if (buttonTypes[2] == true && shopController.canUpgradeBooster())
        {
            SetAvailable();
        }
        else if (buttonTypes[3] == true && shopController.canUpgradeWings())
        {
            SetAvailable();
        }
        else if (buttonTypes[4] == true && shopController.canUpgradeBody())
        {
            SetAvailable();
        }
        else if (buttonTypes[5] == true && shopController.canUpgradeBoosterFuel())
        {
            SetAvailable();
        }
        else
        {
            SetUnavailable();
        }

    }
    public void SetAvailable()
    {
        canPress = true;
        meshRenderer.material = canPressMaterial;
    }
    public void SetUnavailable()
    {
        canPress = false;
        meshRenderer.material = cannotPressMaterial;
    }
}
