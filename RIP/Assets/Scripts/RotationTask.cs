using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationTask : MonoBehaviour
{
    public GameObject cubeTarget, LeftHand, RightHand;
    // Start is called before the first frame update
    void Start()
    {
        LeftHand = GameObject.FindWithTag("Left Hand");
        RightHand = GameObject.FindWithTag("Right Hand");
        cubeTarget = GameObject.FindWithTag("Cube");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
