using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using extOSC;

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
        //Vector2 touch;
        Quaternion rotation;

        //Vector3 tmp;

        if (message.ToQuaternion(out rotation))
        {
            //SEB: This should only pass a message to your GroundController . 
            // The GroundController needs to implement the rotation/interpolation behavior.
            ground.GetComponent<GroundController>().SetTargetRotation(rotation);
            Debug.Log(rotation);
        }



    }
}

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


//public class OSCDummy : MonoBehaviour
//{
//    public string Address = "/example/1";
//    public OSCReceiver Receiver;
//    public PlayerController playerController;
//    public GameObject ground;

//    // Start is called before the first frame update
//    void Start()
//    {
//        Receiver.Bind(Address, ReceivedMessage);
//    }

//    private void ReceivedMessage(OSCMessage message)
//    {
//        Vector2 touch;
//        Vector4 rotation;

//        Vector3 tmp;


//        //Debug.Log(message.ToVector2(out touch));

//        if (message.ToVector4(out rotation)==true)
//        {
//            //tmp = rotation.eulerAngles; //changing the quaternion to euler rotation (rotation = quaternion)

//            //ground.transform.rotation = Quaternion.Euler(-tmp.x,tmp.z,-tmp.y); //creating a quaternion out of this euler rotation
//            //ground.GetComponent<Rigidbody>().AddTorque(-tmp.x, tmp.z, -tmp.y);
//            playerController.OnMoveVector4(rotation);
//            //Debug.Log(rotation);
//        }

//         if (message.ToVector2(out touch) == true)
//                {
//                    playerController.OnMoveVector2(touch);
//                     //Debug.Log(touch);
//                 }
//    }
//}
//public class OSCDummy : MonoBehaviour
//{
//    public string Address = "/example/1";
//    public OSCReceiver Receiver;
//    public PlayerController playerController;
//    public GameObject ground;

//    // Start is called before the first frame update
//    void Start()
//    {
//        Receiver.Bind(Address, ReceivedMessage);
//    }

//    private void ReceivedMessage(OSCMessage message)
//    {
//        Vector2 touch;
//        Quaternion rotation;

//        Vector3 tmp;


//        //Debug.Log(message.ToVector2(out touch));

//        if(message.ToQuaternion(out rotation)==true)

//        {
//            playerController.OnMoveVector2(rotation);
//            //Debug.Log(touch);
//        }

//        if (message.ToQuaternion(out rotation))
//        {
//            tmp = rotation.eulerAngles; //changing the quaternion to euler rotation (rotation = quaternion)

//            ground.transform.rotation = Quaternion.RotateTowards(-tmp.x, tmp.z, -tmp.y); //creating a quaternion out of this euler rotation


//            Debug.Log(rotation);
//        }

//{
//    // The object whose rotation we want to match.
//    Transform target;

//    // Angular speed in degrees per sec.
//    float speed;

//    void Update()
//    {
//        // The step size is equal to speed times frame time.
//        var step = speed * Time.deltaTime;

//        // Rotate our transform a step closer to the target's.
//        transform.rotation = Quaternion.RotateTowards(transform.rotation, target.rotation, step);
//    }


//    //tmp = rotation.eulerAngles; //changing the quaternion to euler rotation (rotation = quaternion)

//    //ground.transform.rotation = Quaternion.RotateTowards(-tmp.x,tmp.z,-tmp.y); //creating a quaternion out of this euler rotation
//    //ground.GetComponent<Rigidbody>().AddRelativeTorque(-tmp.x, tmp.z, -tmp.y);

//    //Debug.Log(rotation);
//}


//        if (message.ToVector2(out touch) == true)
//        {
//            playerController.OnMoveVector2(touch);
//            //Debug.Log(touch);
//        }

//        //Debug.LogFormat("Received: {0}", message);
//    }
//}
//{
//    //Make sure you attach a Rigidbody in the Inspector of this GameObject
//    Rigidbody m_Rigidbody;
//    Vector3 m_EulerAngleVelocity;

//    void Start()
//    {
//        //Set the axis the Rigidbody rotates in (100 in the y axis)
//        m_EulerAngleVelocity = new Vector3(0, 100, 0);

//        //Fetch the Rigidbody from the GameObject with this script attached
//        m_Rigidbody = GetComponent<Rigidbody>();
//    }

//    void FixedUpdate()
//    {
//        Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.deltaTime);
//        m_Rigidbody.MoveRotation(m_Rigidbody.rotation * deltaRotation);
//    }
//}

