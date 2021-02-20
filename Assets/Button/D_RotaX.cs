using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_RotaX : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject _drone;
    // var cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
    public void OnClick(){
        _drone.transform.Rotate(new Vector3(0,-0.5f,0));
    }
}
