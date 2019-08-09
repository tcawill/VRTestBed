using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SteeringTask : MonoBehaviour
{

    public GameObject tunnel, LeftHand, RightHand;
    public TrailRenderer trailrendLeft, trailrendRight;

    private float[] amplitudeList = new float[] { 8f, 5f, 12f };
    private float[] widthList = new float[] { 0.5f, 1f, 3f };
    private int amplitudeItr = 0;
    private int widthItr = 0;
    private bool isDraggingLeft, isDraggingRight;

    // Start is called before the first frame update
    void Start()
    {

        LeftHand = GameObject.FindWithTag("Left Hand");
        RightHand = GameObject.FindWithTag("Right Hand");


        createTunnel();

        trailrendLeft.startWidth = 0.1f;
        trailrendLeft.enabled = false;

        trailrendRight.startWidth = 0.1f;
        trailrendRight.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {

        isMouseDown();
        if (isDraggingLeft)
        {
            trailrendLeft.startWidth = 0.1f;
            trailrendLeft.enabled = true;
            withinBounds();
        }
        if (isDraggingRight)
        {
            trailrendRight.startWidth = 0.1f;
            trailrendRight.enabled = true;
            withinBounds();
        }



    }


    void createTunnel()
    {
        tunnel = GameObject.FindWithTag("Tunnel");
        tunnel.transform.position = new Vector3(0, 0, 0);
        tunnel.GetComponent<Renderer>().material.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
        changeDimensions(tunnel, returnAmplitude());
        createTrail();
    }

    void createTrail()
    {
        LeftHand.AddComponent<TrailRenderer>();
        trailrendLeft = LeftHand.GetComponent<TrailRenderer>();
        trailrendLeft.time = 120.0f;

        RightHand.AddComponent<TrailRenderer>();
        trailrendRight = RightHand.GetComponent<TrailRenderer>();
        trailrendRight.time = 120.0f;
    }

    void isMouseDown()
    {

        if (OVRInput.GetDown(OVRInput.Button.Three)) //X
        {
            isDraggingLeft = true;
        }
        else if (OVRInput.GetUp(OVRInput.Button.Three))
        {
            if ((isDraggingRight || isDraggingLeft) && (amplitudeItr < amplitudeList.Length && widthItr < widthList.Length))
            {
                Reset();
                //if (OVRInput.GetUp(OVRInput.Button.One))
                //{
                //    Reset();
                //}
            }
            else
            {
                SwitchScenes();
                //if (OVRInput.GetUp(OVRInput.Button.One))
                //{
                //    SwitchScenes();
                //}
            }
                isDraggingLeft = false;
                isDraggingRight = false;
        }

        if (OVRInput.GetDown(OVRInput.Button.One)) // A
        {
            isDraggingRight = true;
        }
        else if (OVRInput.GetUp(OVRInput.Button.One))
        {
                if ((isDraggingRight || isDraggingLeft) && (amplitudeItr < amplitudeList.Length && widthItr < widthList.Length))
                {
                    Reset();
                    //if (OVRInput.GetUp(OVRInput.Button.Three))
                    //{
                    //    Reset();
                    //}
                }
                else
                {
                    SwitchScenes();
                    //if (OVRInput.GetUp(OVRInput.Button.Three))
                    //{
                    //    SwitchScenes();
                    //}
                }
                isDraggingLeft = false;
                isDraggingRight = false;
            }
    }

    void withinBounds()
    {
        var cursorBoundsLeft = LeftHand.GetComponent<Renderer>().bounds;
        var cursorBoundsRight = RightHand.GetComponent<Renderer>().bounds;
        var tunnelBounds = tunnel.GetComponent<Renderer>().bounds;

        if ((tunnelBounds.Intersects(cursorBoundsLeft) || tunnelBounds.Intersects(cursorBoundsRight)) && (isDraggingLeft || isDraggingRight))
        {
            tunnel.GetComponent<Renderer>().material.color = new Color(0f, 0f, 1f, 0.5f);

        }
        else
        {

            tunnel.GetComponent<Renderer>().material.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
        }
    }

    void changeDimensions(GameObject tunnel, float amplitude)
    {
        tunnel.transform.localScale = new Vector3(amplitude, returnWidth(), 1f);
    }

    float returnAmplitude()
    {
        if (amplitudeItr < amplitudeList.Length)
        {
            return amplitudeList[amplitudeItr];
        }

        return 0.0f;
    }

    float returnWidth()
    {
        if (widthItr < widthList.Length)
        {
            return widthList[widthItr];
        }
        return 0.0f;
    }

    void Reset()
    {
        trailrendLeft.Clear();
        trailrendRight.Clear();

        trailrendLeft.startWidth = 0.1f;
        trailrendLeft.enabled = false;

        trailrendRight.startWidth = 0.1f;
        trailrendRight.enabled = false;

        amplitudeItr++;
        widthItr++;
        tunnel.GetComponent<Renderer>().material.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
        changeDimensions(tunnel, returnAmplitude());
    }

    void SwitchScenes()
    {
        SceneManager.LoadScene("PointingTask");
    }
}

