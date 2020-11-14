using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;         // add this for Input being able 

public class PlayerController : MonoBehaviour
{
    
    [SerializeField] private float m_speed = 1f;
    private Rigidbody m_playerRigidbody;
    
    private float m_movementX;
    private float m_movementY;
    
    // Start is called before the first frame update
    void Start()
    {
        m_playerRigidbody = GetComponent<Rigidbody>();       // Unity checks if there is a Rigidbody Component added to our Gameobject this script is attached to (here Player). If so, it will add it to the Variable m_playerRigidbody  
    }


    private void OnMove(InputValue inputValue)                // When a user input happens, we do the Vector2 movement by storing the input values in the variables
    {
        Vector2 movementVector = inputValue.Get<Vector2>();

        m_movementX = movementVector.x;
        m_movementY = movementVector.y;
    }


    private void FixedUpdate()                                     
    {
        Vector3 movement = new Vector3(m_movementX, 0f, m_movementY);     // Set like to Player movement
        
        m_playerRigidbody.AddForce(movement * m_speed);                // apply this Movement Vector to our Rigidbody
    }

    private void OnTriggerEnter(Collider other) // our collectable (cube) gets deactivated when this method is called 
    {
        if (other.gameObject.CompareTag("Collectable"))
        {
            other.gameObject.SetActive(false);
        }
    }
}
