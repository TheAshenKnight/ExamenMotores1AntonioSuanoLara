using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainCharacterController : MonoBehaviour
{
    private PlayerInput _playerInput;
    private CharacterController _characterController;
    private Rigidbody rb;
    
    public float movementSpeed = 2f;
    public float jumpHeight = 7f;
    public bool isGrounded;
    
    private bool isAlive;
    private bool isTriggered = false;
    
    //Get initial position of the player to use it later to reload his position if he dies.
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        isAlive = true;
        _playerInput = gameObject.GetComponent<PlayerInput>();
        _characterController = gameObject.GetComponent<CharacterController>();
        _playerInput.actions["Movement"].started += StartTriggering;
        _playerInput.actions["Movement"].canceled += StopTriggering;
    }

    private void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (isTriggered)
        {
            if (_playerInput.actions["Movement"].triggered)
            {
                Debug.Log("presionando la tecla");
                var input = _playerInput.actions["Movement"].ReadValue<Vector2>();
                var movement = new Vector3(input.x, 0, input.y);
                //_characterController.Move(transform.TransformDirection(movement * movementSpeed * Time.deltaTime));
                
            }
            
            if (isGrounded && _playerInput.actions["Jump"].triggered)
            {
                rb.AddForce(Vector3.up * jumpHeight);
            }
            
        }
        
    }
        
    

    private void StartTriggering(InputAction.CallbackContext ctx)
    {
        isTriggered = true;
    }
    
    private void StopTriggering(InputAction.CallbackContext ctx)
    {
        isTriggered = false;
    }
}
