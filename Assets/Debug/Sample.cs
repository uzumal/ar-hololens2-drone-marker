using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Sample : MonoBehaviour
{
    private GameObject untitled;
    private GameObject arrow;
    private GameObject Button;
    // Start is called before the first frame update
    void Start()
    {
        //見つける
        untitled = GameObject.Find("untitled");
        arrow = GameObject.Find("arrow");
        Button = GameObject.Find("Button");
    }

    // Update is called once per frame
    void Update()
    {
        Button.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + 0.5f);
        arrow.transform.position = new Vector3(this.transform.position.x + 0.1f, this.transform.position.y, this.transform.position.z + 0.2f);
    }
}
