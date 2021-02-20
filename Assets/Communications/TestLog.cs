using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLog : MonoBehaviour
{
    void Start()
    {
        Debug.Log("!!! Test Debug Log Message !!!");
        Debug.LogError("!!! Test Error Log Message !!!");
    }
}