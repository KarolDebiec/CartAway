using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveButton : MonoBehaviour
{
    public GameObject button;//referance to button in scene (collider have to be out of button so it doesnt jump around)
    public Vector3 pressedLocalPos;
    private Vector3 unpressedLocalPos;
    public bool canPress;
    void Start()
    {
        unpressedLocalPos = button.transform.localPosition;
    }

    public void PressButton()
    {
        if (canPress)
        {
            button.transform.localPosition = pressedLocalPos;
            
        }
    }
    public void UnpressButton()
    {
        button.transform.localPosition = unpressedLocalPos;
    }
}
