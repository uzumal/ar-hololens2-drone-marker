using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private GameObject build;
    private GameObject mainCam;
    private GameObject Arrow;
    private GameObject Button;
    private MyScript B_script;
    // Start is called before the first frame update
    void Start()
    {
        build = GameObject.Find("untitled");
        // Arrow = GameObject.Find("arrow");
        // Button = GameObject.Find("Button");
        mainCam = Camera.main.gameObject;
        // B_script = Button.GetComponent<MyScript>();
    }

    // Update is called once per frame
    void Update()
    {
        build.transform.parent = mainCam.transform;
        // Arrow.transform.parent = mainCam.transform;
        // Button.transform.parent = mainCam.transform;
        
        // int _check = B_script.check;
        // if(_check == 1){
        //     build.transform.parent = null;
        // }
    }
}
