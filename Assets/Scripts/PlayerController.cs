using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;
using Debug = UnityEngine.Debug; // add this for Input being able 

public class PlayerController : MonoBehaviour
{
    
    [SerializeField] private float m_speed = 1f;
    private Rigidbody m_playerRigidbody; 
    
    private float m_movementX;
    private float m_movementY;

    private int m_collectablesTotalCount;
    private int m_collectablesCounter;

    private Stopwatch m_stopwatch;
////private float m_localScaleMax;
    
    // Start is called before the first frame update
    void Start()
    {
        m_playerRigidbody = GetComponent<Rigidbody>();       // Unity checks if there is a Rigidbody Component added to our Gameobject this script is attached to (here Player). If so, it will add it to the Variable m_playerRigidbody  

        m_collectablesTotalCount = m_collectablesCounter = GameObject.FindGameObjectsWithTag("Collectable").Length;
        
        m_stopwatch = Stopwatch.StartNew();

////////m_localScaleMax = 1.2f;
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

            m_collectablesCounter--;

////////////if (transform.localScale.x >= m_localScaleMax)
////////////{
/////////////////gameObject.transform.localScale = m_localScaleMax;
////////////}
            
            //increase size of player when colliding with collectable
            gameObject.transform.localScale += new Vector3(.1f, .1f, .1f);


            if (m_collectablesCounter == 0)
        {
            Debug.Log("YOU WIN! CONGRATULATIONS!");
            Debug.Log($"It took you {m_stopwatch.Elapsed} to find all {m_collectablesTotalCount} collectables.");
                
#if UNITY_EDITOR
            UnityEditor.EditorApplication.ExitPlaymode();           
#endif  
        }
        else
        {
            Debug.Log(
                $"You've already found {m_collectablesTotalCount - m_collectablesCounter} of {m_collectablesTotalCount} collectables!");
        }
        }
        
        

        else if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("GAME OVER! MUAHAHA!");
            
#if UNITY_EDITOR
            UnityEditor.EditorApplication.ExitPlaymode();           
#endif        

        }

        //our toxic collectable gets deactivated when this method is called 
        if (other.gameObject.CompareTag("Toxic"))
        {
            other.gameObject.SetActive(false);
            Debug.Log("NOO, UGH, Doesn't taste good!");

        //decrease size of player when colliding with collectable
        gameObject.transform.localScale += new Vector3(-.1f, -.1f, -.1f);
        }

       //////////////if (gameObject.transform.localScale = m_localScaleMax)
        

    }
}
