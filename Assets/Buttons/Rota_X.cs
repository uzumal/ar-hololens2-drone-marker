using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rota_X : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject build;
    // var cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
    public void OnClick(){
        build.transform.Rotate(new Vector3(0,-5,0));
    }
}
