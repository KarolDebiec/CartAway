using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveButton : MonoBehaviour
{
    public GameObject button;//referance to button in scene (collider have to be out of button so it doesnt jump around)
    public Vector3 pressedLocalPos;
    private Vector3 unpressedLocalPos;
    public bool canPress = true;
    public GameController gameController;
    public bool mode;// true - move to shop, false - move to cart
    void Start()
    {
        unpressedLocalPos = button.transform.localPosition;
    }


    public void PressButton()
    {
        if (canPress)
        {
            //button.transform.localPosition = pressedLocalPos;
            if (mode)
            {
                gameController.ChangeModeToShop();
            }
            else
            {
                gameController.ChangeModeToCart();
            }
            gameController.PlayButtonClickSound();
        }
    }
    public void UnpressButton()
    {
        //button.transform.localPosition = unpressedLocalPos;
    }
}
