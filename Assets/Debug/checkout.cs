using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkout : MonoBehaviour
{
    // Start is called before the first frame update
    public  GameObject _build;



    public void OnClick(){
        //非表示
        _build.gameObject.SetActive(false);
    }
}
