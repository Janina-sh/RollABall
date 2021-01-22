using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using extOSC;

//public class OSCDummy : MonoBehaviour
//{
//    public string Address = "/example/1";
//    public OSCReceiver Receiver;
//    public PlayerController playerController;

//    // Start is called before the first frame update
//    void Start()
//    {
//        Receiver.Bind(Address, ReceivedMessage);
//    }

//    private void ReceivedMessage(OSCMessage message)
//    {
//        Vector2 touch;
//        Debug.Log(message.ToVector2(out touch));

//        if (message.ToVector2(out touch) == true)
//        {
//            playerController.OnMoveVector2(touch);
//            Debug.Log(touch);
//        }

//        //Debug.LogFormat("Received: {0}", message);
//    }
//}

public class OSCDummy : MonoBehaviour
{
    public string Address = "/example/1";
    public OSCReceiver Receiver;
    public PlayerController playerController;
    public GameObject ground;

    // Start is called before the first frame update
    void Start()
    {
        Receiver.Bind(Address, ReceivedMessage);
    }

    private void ReceivedMessage(OSCMessage message)
    {
        Vector2 touch;
        Quaternion rotation;

        Vector3 tmp;


        //Debug.Log(message.ToVector2(out touch));

        if(message.ToQuaternion(out rotation))
        {
            //SEB: This should only pass a message to your GroundController . 
            // The GroundController needs to implement the rotation/interpolation behavior.
            ground.GetComponent<GroundController>().SetTargetRotation(rotation);
            Debug.Log(rotation);
        }
        

        //if (message.ToVector2(out touch) == true)
        //{
        //    playerController.OnMoveVector2(touch);
        //    //Debug.Log(touch);
        //}

        //Debug.LogFormat("Received: {0}", message);
    }
}
