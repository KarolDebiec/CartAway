using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public GameObject button;//referance to button in scene (collider have to be out of button so it doesnt jump around)
    public Vector3 pressedLocalPos;
    private Vector3 unpressedLocalPos;
    public GameController gameController;
    public Material canPressMaterial;
    public Material cannotPressMaterial;
    public Material pressedMaterial;
    public bool oneTimeButton = true;
    private bool canPress = true; // if above is true then changes to false after first press
    public MeshRenderer meshRenderer;

    private bool isPressed = false;
    public List<bool> buttonTypes;// all button types 0-launch , 1-reset, 2-start booster, 3-stop booster
    void Start()
    {
        meshRenderer = button.GetComponent<MeshRenderer>();
        unpressedLocalPos = button.transform.localPosition;
    }

    public void PressButton()
    {
        if(canPress)
        {
            button.transform.localPosition = pressedLocalPos;
            meshRenderer.material = pressedMaterial;
            if (buttonTypes[0] == true)
            {
                gameController.StartLaunch();
            }
            else if (buttonTypes[1] == true)
            {
                gameController.SetupStart();
            }
            else if (buttonTypes[2] == true)
            {
                gameController.StartBoost();
            }
            else if (buttonTypes[3] == true)
            {
                gameController.StopBoostTemporary();
            }
            gameController.PlayButtonClickSound();
            isPressed = true;
        }
    }
    public void UnpressButton()
    {
        if(isPressed)
        {
            button.transform.localPosition = unpressedLocalPos;
            if (oneTimeButton)
            {
                SetUnavailable();
            }
            else
            {
                SetAvailable();
            }
            isPressed = false;
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
