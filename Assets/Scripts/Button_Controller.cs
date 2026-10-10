using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Button_Controller : MonoBehaviour
{

    private SpriteRenderer button;
    public Sprite RegularButton; // normal state
    public Sprite PressedButton; // pressed state

     [SerializeField] private InputAction keyAction = new InputAction("Button"); // input action for the button
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        button = GetComponent<SpriteRenderer>();
    }

    void OnEnable()
    {
        keyAction.Enable();
    }

    void OnDisable()
    {
        keyAction.Disable();
    }

    // Update is called once per frame
    void Update()
    {

        if (keyAction.WasPressedThisFrame())
        {
            button.sprite = PressedButton;
        }
        if (keyAction.WasReleasedThisFrame())
        {
            button.sprite = RegularButton;
        }
    }
}
