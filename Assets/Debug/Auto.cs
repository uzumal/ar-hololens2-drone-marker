using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Auto : MonoBehaviour
{
    private float speed;
    private Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        speed = 0.36f;
    }

    // Update is called once per frame
    void Update()
    {
        pos = this.transform.position;
        pos.y += 5.3f;
        this.transform.position = Vector3.MoveTowards(this.transform.position, pos, speed * Time.deltaTime);
        
    }
}
