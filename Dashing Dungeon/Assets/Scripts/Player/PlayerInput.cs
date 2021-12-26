using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
   
    public float horizontal{get; private set;}
    public float vertical{get; private set;}
    public Vector3 mousePosition{get; private set;}
    public bool isLeftMouseButtonPressed{get; private set;}
    public bool isRightMouseButtonPressed{get; private set;}
    public bool isMiddleMouseButtonPressed{get; private set;}

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        isLeftMouseButtonPressed = Input.GetMouseButtonDown(0);
        isRightMouseButtonPressed = Input.GetMouseButtonDown(1);
        isMiddleMouseButtonPressed = Input.GetMouseButtonDown(2);
    }
}
