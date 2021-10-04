using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixFPS : MonoBehaviour
{
    private void Start()
    {
        Application.targetFrameRate = 60;
    }
}
