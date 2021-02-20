using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Look : MonoBehaviour
{
    public GameObject cube;
    public GameObject drone;
    private Visualize visualize;
    private float Compare;
    // Start is called before the first frame update
    void Start()
    {
        // cube = GameObject.Find("Cube");
        // drone = GameObject.Find("droneModel");
        // Compare = (drone.transform.position - cube.transform.position).magnitude;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        var vec = (drone.transform.position - cube.transform.position).normalized;
        float _distance = (drone.transform.position - cube.transform.position).magnitude;
        float magnification = _distance / Compare;
        this.transform.position = cube.transform.position + (vec * (magnification + 0.5f));
        */
        this.transform.position = Vector3.Lerp(drone.transform.position, cube.transform.position, 0.5f);
        // this.transform.localScale = new Vector3(magnification * 0.7f, magnification * 0.7f, magnification * 0.7f);


        // this.transform.localScale = new Vector3(12.5f * multiplication, 12.5f * multiplication, 12.5f * multiplication);

        // Vector3 Obstract = visualize.GetPosition();
    }
}
