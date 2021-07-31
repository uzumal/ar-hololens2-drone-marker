using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject _arrow;
    public GameObject _rota;
    public  GameObject _Cube;
    // public GameObject _windows;
    public GameObject _drone;
    public GameObject _sphere;
    // private Renderer _mesh;
    private int check = 0;
    

    public void OnClick(){
        //非表示
        this.gameObject.SetActive(false);
        _arrow.gameObject.SetActive(false);
        _rota.gameObject.SetActive(false);
        // 表示
        _sphere.gameObject.SetActive(true);
        //スクリプトON
        _Cube.GetComponent<Obst_Clear>().enabled = true;
        // _Cube.GetComponent<Surface_Clear>().enabled = true;
        _drone.GetComponent<pre_Receive>().enabled = false;
        _drone.GetComponent<Receive>().enabled = true;

        // _sphere.gameObject.GetComponent<SphereCollider>().enabled = true;

        //シェーダーchange
        // Material _obstacle = Resources.Load<Material>("default") as Material;
        // _obstacle.shader = Shader.Find("MaskedObject");
        // _windows.GetComponent<Renderer>().material.shader = Shader.Find("Window");
        check = 1;
    }
}
