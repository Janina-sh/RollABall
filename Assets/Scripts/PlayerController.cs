using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;
using Debug = UnityEngine.Debug; // add this for Input being able 
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    
    [SerializeField] private float m_speed = 1f;          //Current Speed
    [SerializeField] private float m_acceleration = 3f;   //Beschleunigung
    [SerializeField] private float m_speedMax = 20f;      //Max Speed
    
    [SerializeField] private float m_slowDown = 3f;       //Entschleunigung
    [SerializeField] private float m_speedMin = 10f;      //Min Speed

    private Rigidbody m_playerRigidbody; 
    
    private float m_movementX;
    private float m_movementY;

    private int m_collectablesTotalCount;
    private int m_collectablesCounter;

    private Stopwatch m_stopwatch;

    public Text scoreText;
    public GameObject gameOverText; //LevelCompleted
    public GameObject enemyGameOverText; //GameOvertext when Enemy hits player
    
    private float m_localScaleMax;
    private float m_localScaleMin;

    private float m_speedIncrease;

    //Level2
    public Transform teleportDestination_inside;
    public Transform teleportDestination_outside;
    
    //Level3
    
    //flowerfield to plattform
    public Transform Destination_Platform;
    public Transform DestinationBack;
    
    //falling
    public Transform Destination_Falling;

    //flowerfield garden to enemy platform
    public Transform DestinationEnemy;





    void Start()
    {
        // Unity checks if there is a Rigidbody Component added to our Gameobject this script is attached to (here Player).
        // If so, it will add it to the Variable m_playerRigidbody  
        m_playerRigidbody = GetComponent<Rigidbody>();
        
        
        m_collectablesTotalCount = m_collectablesCounter = GameObject.FindGameObjectsWithTag("Collectable").Length;
        scoreText.text = "There are " + m_collectablesTotalCount.ToString() + " christmas balls to collect! "; 
                         
        
        m_stopwatch = Stopwatch.StartNew();
        
        m_localScaleMax = 1.3f; 
        m_localScaleMin = 0.7f;
        m_speedIncrease = 25f;
        
        
    }



    // When a user input happens, we do the Vector2 movement by storing the input values in the variables
    private void OnMove(InputValue inputValue)
    {
        Vector2 movementVector = inputValue.Get<Vector2>();

        m_movementX = movementVector.x;
        m_movementY = movementVector.y;
    }

    public void OnMoveVector2(Vector2 input)
    {
        m_movementX = input.y;
        m_movementY = input.x;
    }


    private void FixedUpdate()
    {
        // Restartet das spiel, falls die y-position des players  -4 ist:
        if (transform.position.y < -4)
        {
            FindObjectOfType<AudioManager>().Play("playerFall");
            Debug.Log("GAME OVER! MUAHAHA!");
            StartCoroutine(EnemyGameOver());
            enemyGameOverText.SetActive(true);
        }

        Vector3 movement = new Vector3(m_movementX, 0f, m_movementY);     // Set like to Player movement

        m_playerRigidbody.AddForce(movement * m_speed);// apply this Movement Vector to our Rigidbody

    }


    // our collectable (cube) gets deactivated when this method is called
    private void OnTriggerEnter(Collider other)
    {


        if (other.gameObject.CompareTag("Collectable"))
        {
            FindObjectOfType<AudioManager>().Play("collectBall");
            other.gameObject.SetActive(false);

            m_collectablesCounter--;
            scoreText.text = m_collectablesCounter.ToString() + " christmas balls to go! ";

            
            if (m_collectablesCounter == 0)
            {
            Debug.Log("YOU WIN! CONGRATULATIONS!");
            gameOverText.SetActive(true);
            StartCoroutine(waitALittleBit());
            
            Debug.Log($"It took you {m_stopwatch.Elapsed} to find all {m_collectablesTotalCount} collectables.");
            
                
           
            }
            
            else
            {
                Debug.Log($"You've already found {m_collectablesTotalCount - m_collectablesCounter} of {m_collectablesTotalCount} collectables!");
            }
            
        }
        
        
        else if (other.gameObject.CompareTag("Enemy"))
        {
            FindObjectOfType<AudioManager>().Play("playerHit");
            Debug.Log("GAME OVER! MUAHAHA!");
            StartCoroutine(EnemyGameOver());
            enemyGameOverText.SetActive(true);
        }

        //our toxic collectable gets deactivated when this method is called 
        if (other.gameObject.CompareTag("Toxic"))
        {
            FindObjectOfType<AudioManager>().Play("collectPine");
            other.gameObject.SetActive(false);
            Debug.Log("NOO, UGH, Doesn't taste good!");
            
            //decrease size of playersphere until min-value
            if (gameObject.transform.localScale.x > m_localScaleMin)
                { 
                    gameObject.transform.localScale += new Vector3(-.1f, -.1f, -.1f);
                }
            
            //increase speed of playersphere when getting smaller
           
            m_speed += m_acceleration;
               
            if (m_speed > m_speedMax)
                m_speed = m_speedMax;
        }
        
        //our Bigger Collectable gets deactivated when this method is called
        if (other.gameObject.CompareTag("Bigger"))
        {
            FindObjectOfType<AudioManager>().Play("collectSnow");
            other.gameObject.SetActive(false);

            //increase size of playersphere until max-value
            if (gameObject.transform.localScale.x < m_localScaleMax)
            {
                gameObject.transform.localScale += new Vector3(.1f, .1f, .1f);
            }

            //decrease speed of playersphere when getting bigger

            m_speed -= m_slowDown;

            if (m_speed > m_speedMin)
                m_speed = m_speedMin;
        }
        
        //Teleport
        
        //Level2
        if (other.gameObject.CompareTag("Teleport"))
        {
            gameObject.transform.position = teleportDestination_inside.position;    
        }
        
        if (other.gameObject.CompareTag("TeleportOutside"))
        {
            gameObject.transform.position = teleportDestination_outside.position;    
        }
        
        //Level3
        
        //flowerfield to plattform
        if (other.gameObject.CompareTag("Teleport"))
        {
            gameObject.transform.position = Destination_Platform.position;    
        }
        
        if (other.gameObject.CompareTag("TeleportBack"))
        {
            gameObject.transform.position = DestinationBack.position;    
        }
        
        //falling
        if (other.gameObject.CompareTag("TeleportFalling"))
        {
            gameObject.transform.position = Destination_Falling.position;    
        }
        
        //flowerfield garden to enemy platform
        if (other.gameObject.CompareTag("TeleportEnemy"))
        {
            gameObject.transform.position = DestinationEnemy.position;      
        }
        

        
        
        if (other.gameObject.CompareTag("Collider"))
        {
            Debug.Log("COLLISIONNNNN");
            FindObjectOfType<AudioManager>().Play("PlayerDeath");
        }


    }

    //load Level2 when won
    public IEnumerator waitALittleBit()
    {
        yield return  new WaitForSeconds(4);
        //SceneManager.LoadScene("Level2");
        
       Scene currentScene = SceneManager.GetActiveScene ();
       string sceneName = currentScene.name;
        
        if (sceneName == "MainScene") 
         {
             SceneManager.LoadScene("Level2");
         }
         else if (sceneName == "Level2")
         {
             SceneManager.LoadScene("Level3");
         }
        else if (sceneName == "Level3")
        {
            SceneManager.LoadScene("Menu");
        }


    }
    
    public IEnumerator EnemyGameOver()
    {
        yield return new WaitForSeconds(4);

        Application.LoadLevel(Application.loadedLevel);
    }

}

