using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ScalingTask : MonoBehaviour
{
    private bool isDraggingLeft, isDraggingRight;
    public GameObject LeftHand, RightHand, innerSphere;

    // Start is called before the first frame update
    void Start()
    {
        LeftHand = GameObject.FindWithTag("Left Hand");
        RightHand = GameObject.FindWithTag("Right Hand");

        innerSphere = GameObject.FindWithTag("Sphere 2");

    }

    // Update is called once per frame
    void Update()
    {
        isMouseDown();
        if (isDraggingLeft)
        {
            stretchTargetLeft();
        }
            
        if (isDraggingRight)
        {
            stretchTargetRight();
        }
    }

    void isMouseDown()
    {
        if (OVRInput.GetDown(OVRInput.Button.Three)) //X
        {
            isDraggingLeft = true;
        }
        else if (OVRInput.GetUp(OVRInput.Button.Three))
        {   if(isDraggingLeft == true)
            {
                SwitchScenes();
            }
            isDraggingLeft = false;
        }

        if (OVRInput.GetDown(OVRInput.Button.One)) //A
        {
            isDraggingRight = true;
        }
        else if (OVRInput.GetUp(OVRInput.Button.One))
        {
            if (isDraggingRight == true)
            {
                SwitchScenes();
            }
            isDraggingRight = false;
        }
    }

    void stretchTargetLeft()
    {
        innerSphere.transform.localScale = LeftHand.transform.position;
    }
    void stretchTargetRight()
    {
        innerSphere.transform.localScale = RightHand.transform.position;
    }

    void SwitchScenes()
    {
        SceneManager.LoadScene("PointingTask");
    }
}
