using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowSize : MonoBehaviour {

    public int width = 512;
    public int height = 512;

    void Start()
    {
        Screen.SetResolution(width, height, false);
    }
}
