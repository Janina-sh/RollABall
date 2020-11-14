using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelloWorld : MonoBehaviour
{
    [SerializeField] private int m_threshold = 10;    // put it into inspector
    [SerializeField] private string m_message = "Hello World";

    // Start is called before the first frame update
    void Start()
    {
        LogHelloWorld(m_message);  //print Hello World when in Game Mode
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LogHelloWorld(string message)  // void means here nothing will return, it only executes
    {
        if (m_threshold > 10)
        Debug.Log(message);   // Ausführen von Log.message
    }
}
