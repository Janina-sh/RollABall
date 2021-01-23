using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour
{

    //SEB: This is to control the rotation strenghth with a slider in the editor
    [Range(0.0f, 360.0f)]
    public float rotationStrenght;
    //SEB: This is the rotation that the controller interpolates towards on every frame
    public Vector3 targetRotation;

    private void Update()
    {
        //SEB: This get's called each frame and interpolates...
        TiltGround();
    }

    private void TiltGround()
    {
        //SEB: This is to keep the code clean and clear
        var step = rotationStrenght * Time.deltaTime;

        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(
            Mathf.Clamp(targetRotation.x, -25, 25),
            Mathf.Clamp(targetRotation.y, 0, 0),
            Mathf.Clamp(targetRotation.z, -25, 25)), step);
    }

    public void SetTargetRotation(Quaternion to)
    {
        //SEB: This is you public interface to you OSC Dummy
        targetRotation = to.eulerAngles;
    }
}


//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class GroundController : MonoBehaviour
//{


//    //SEB: This is to control the rotation strenghth with a slider in the editor
//    [Range(0.0f, 50.0f)]
//    public float rotationStrenght;
//    //SEB: This is the rotation that the controller interpolates towards on every frame
//    private Quaternion targetRotation;

//    private void Update()
//    {
//        //SEB: This get's called each frame and interpolates...
//        TiltGround();
//    }

//    private void TiltGround()
//    {
//        //SEB: This is to keep the code clean and clear
//        var step = rotationStrenght * Time.deltaTime;
//        Debug.Log(" ROTATION " + targetRotation);
//        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, step);
//        Debug.Log(" GAMEOBJECT_TRANSFORM_ROTATION " + transform.rotation);
//    }

//    public void SetTargetRotation(Quaternion to)
//    {

//        Vector3 tmp;
//        tmp = to.eulerAngles;
//        float x_rotation = Mathf.Clamp(tmp.x,-45,45);
//       // float y_rotation = Mathf.Clamp(tmp.y, -45, 45);
//        float z_rotation = Mathf.Clamp(tmp.z, -100, 100);


//        //SEB: This is you public interface to you OSC Dummy
//        targetRotation = Quaternion.Euler(tmp.x, 0.0f, tmp.z);
//        Debug.Log( "MESSAGE_RECIEVED" );
//    }
//}