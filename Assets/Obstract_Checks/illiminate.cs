using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class illiminate : MonoBehaviour
{
    void OnTriggerEnter(Collider other){
        // Debug.Log(other.gameObject.tag);
        // Debug.Log(other.gameObject.name);
        if(other.gameObject.tag == "Building"){
            // Debug.Log("illiminate");
            Destroy(this.gameObject);
        }
    }
    void Start(){
        Invoke("exit_dot",3);
    }

    void exit_dot(){
        Destroy(this.gameObject);
    }

    // void OnTriggerExit(){
    //     Destroy(this.gameObject);
    // }
}
