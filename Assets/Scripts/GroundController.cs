using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour {

    //SEB: This is to control the rotation strenghth with a slider in the editor
    [Range(0.0f, 1.0f)]
    public float rotationStrenght;
    //SEB: This is the rotation that the controller interpolates towards on every frame
    private Quaternion targetRotation;

    private void Update(){
        //SEB: This get's called each frame and interpolates...
        TiltGround();
    }    

    private void TiltGround(){
        //SEB: This is to keep the code clean and clear
        var step = rotationStrenght * Time.deltaTime;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, target.rotation, step);
    }

    public void SetTargetRotation(Quaternion to){
        //SEB: This is you public interface to you OSC Dummy
        targetRotation = to;
    }
}