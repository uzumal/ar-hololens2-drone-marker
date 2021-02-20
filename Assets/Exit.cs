using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    // 終了関数
    private void OnApplicationQuit()
    {
        Material _obstacle = Resources.Load<Material>("default") as Material;
        _obstacle.shader = Shader.Find("Standard");
    }
}
