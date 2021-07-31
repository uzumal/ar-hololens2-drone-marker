using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Right : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject build;
    public GameObject _drone;
    // var cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
    public void OnClick(){
        // var cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        // Vector3 direction = cameraForward * 0 + Camera.main.transform.right * 0.1f;
        build.transform.position += new Vector3(0.05f, 0, 0);
        _drone.transform.position += new Vector3(0.05f, 0, 0);
    }
}
