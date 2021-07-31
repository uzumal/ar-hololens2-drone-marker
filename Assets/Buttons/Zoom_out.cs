using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoom_out : MonoBehaviour
{
    public GameObject build;
    public GameObject _drone;
    public void OnClick(){
        // var cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        // Vector3 direction = cameraForward * 0 + Camera.main.transform.right * -0.1f;
        // build.transform.position += direction;
        build.transform.position += new Vector3(0, 0, 0.05f);
        _drone.transform.position += new Vector3(0, 0, 0.05f);
    }
}
