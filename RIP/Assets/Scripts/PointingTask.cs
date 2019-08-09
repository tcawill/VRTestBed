using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PointingTask : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> sphereList = new List<GameObject>();
    public GameObject nextTarget;
    public GameObject sphere, LeftHandAnchor, RightHandAnchor;
    public AudioSource m_AudioSource;
    private int targetItr = 0;
    private int sizeItr = 0;
    private int amplitudeItr = 0;
    private int numSpheres = 7;
    private float length, width, height;
    private float[] sphereSizes = new float[] { 0.5f, 0.7f, 0.1f };
    private float[] amplitudeList = new float[] { 0.5f, 1f, 1.5f };
    // Start is called before the first frame update
    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
        setSphereLocation();

        sphereList = Shuffle(sphereList);
        sphereList.Insert(0, GameObject.FindWithTag("Sphere 0"));
        nextTarget = sphereList[0];

        LeftHandAnchor = GameObject.FindWithTag("Left Hand");
        RightHandAnchor = GameObject.FindWithTag("Right Hand");

        LeftHandAnchor.gameObject.transform.localScale += new Vector3(0.1f, 0.091067f, 500f);
        RightHandAnchor.gameObject.transform.localScale += new Vector3(0.1f, 0.091067f, 500f);

    }

    // Update is called once per frame
    void Update()
    {
        isMouseDown();

    }

    void setSphereLocation()
    {
        float radius = 1f;
        for (int i = 0; i < numSpheres; i++)
        {
            if (i == 0)
            {
                sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                sphere.transform.position = new Vector3(0, 0, 0);
            }
            else
            {
                float angle = i * Mathf.PI * 2f / (numSpheres - 1);
                Vector3 newPos = new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0);
                sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                sphere.transform.position = newPos;
            }
            sphere.tag = "Sphere " + i;
            setStart(); 
        }
        setSphereSize();
    }

    void setSphereSize()
    {
        if (sizeItr < sphereSizes.Length && amplitudeItr < amplitudeList.Length)
        {
            length = sphereSizes[sizeItr];
            width = sphereSizes[sizeItr];
            height = sphereSizes[sizeItr];
            GameObject[] foundObjects = FindObjectsOfType<GameObject>();
            foreach (var obj in foundObjects)
                obj.transform.localScale = new Vector3(length, width, height);
            sizeItr++;
        }
        else if (sizeItr == sphereSizes.Length && amplitudeItr < amplitudeList.Length)
        {
            sizeItr = 0;
            amplitudeItr++;
            selectAmplitude();
            Reset();
        }
        else
        {
            SwitchScenes();
        }
    }

    void setStart()
    {
        if (sphere.gameObject.tag == "Sphere 0")
        {
            sphere.GetComponent<Renderer>().material.color = Color.blue;
        }
        else
        {
            sphereList.Add(sphere);
        }
    }

    void changeAmplitude(GameObject sphereObj, float amplitude)
    {
        Vector3 dist = sphereObj.transform.position - sphereList[0].transform.position;
        Vector3 temp = new Vector3(dist.x * amplitude, dist.y * amplitude, 0);
        sphereObj.transform.position += temp;
    }

    void selectAmplitude()
    {
        for (int i = 0; i < sphereList.Count; i++) // should randomize at some point
        {
            changeAmplitude(sphereList[i], returnAmplitude());
        }
    }

    float returnAmplitude()
    {
        if (amplitudeItr != amplitudeList.Length)
        {
            return amplitudeList[amplitudeItr];
        }

        return 0.0f;
    }

    void isMouseDown()
    {
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            withinBounds();
        }
    }

    void changeTarget()
    {
        targetItr++;
        if (targetItr != sphereList.Count)
        {
            nextTarget = sphereList[targetItr];
            changeObjColor(nextTarget, Color.blue);
        }
        else
        {
            Reset();
        }
    }

    void changeObjColor(GameObject sphere, Color c)
    {
        sphere.GetComponent<Renderer>().material.color = c;
    }

    void Reset()
    {
        foreach (var sphere in sphereList)
            changeObjColor(sphere, Color.white);
        setSphereSize();
        changeObjColor(sphereList[0], Color.blue);
        targetItr = 0;
        nextTarget = sphereList[0];
        LeftHandAnchor.gameObject.transform.localScale += new Vector3(0.1f, 0.091067f, 500f);
        RightHandAnchor.gameObject.transform.localScale += new Vector3(0.1f, 0.091067f, 500f);
    }

    void SwitchScenes()
    {
        SceneManager.LoadScene("SteeringTask"); //randomize later to counterbalance scenes
    }

    List<GameObject> Shuffle(List<GameObject> inputList) // this here shuffles the ordering for the spheres to be highlighted for each trial.
    {
        List<GameObject> shuffledList = new List<GameObject>();
        int randomIndex = 0;
        while (inputList.Count > 0)
        {
            randomIndex = Random.Range(0, inputList.Count); //Choose a random object in the list
            shuffledList.Add(inputList[randomIndex]); //add it to the new, random list
            inputList.RemoveAt(randomIndex); //remove to avoid duplicates
        }

        return shuffledList; //return the new random list
    }


    void withinBounds() //figure out :/
    {

        var leftRayBound = LeftHandAnchor.GetComponent<Renderer>().bounds;
        var rightRayBound = RightHandAnchor.GetComponent<Renderer>().bounds;
        var sphereBound = nextTarget.GetComponent<Renderer>().bounds.center;

        if ((leftRayBound.Contains(sphereBound)) || (rightRayBound.Contains(sphereBound)))
        {
            changeObjColor(nextTarget.transform.gameObject, Color.white);
            m_AudioSource.Play();
            changeTarget();
       }
    }
} 
