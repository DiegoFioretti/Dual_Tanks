using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PhysicsLibrary;

public class CircularController : MonoBehaviour
{
    [SerializeField] private float angularSpeed;
    [SerializeField] private float yDrag;
    [SerializeField] private float lowerYLimit;
    [SerializeField] private float radius;

    PhysicsClass customController;
    Vector3 displacementVector;

    Camera camara;
    Vector3 camarapos;
    float camheight;
    float camwidth;
    float camleft;
    float camright;
    float camtop;
    float cambottom;

    void Start()
    {
        //--------------------------------------------------------------------------------
        camara = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        camarapos = GameObject.FindGameObjectWithTag("MainCamera").transform.position;
        camheight = 2f * camara.orthographicSize;
        camwidth = camheight * camara.aspect;
        camright = camarapos.x + (camwidth / 2);
        camleft = camarapos.x - (camwidth / 2);
        camtop = camarapos.y + (camheight / 2);
        cambottom = camarapos.y - (camheight / 2);
        //--------------------------------------------------------------------------------

        customController = new PhysicsClass();

        customController.SetStartingCircleConstants(radius, angularSpeed, yDrag, lowerYLimit);
        displacementVector.x = transform.position.x;
        displacementVector.y = transform.position.y;
        displacementVector.z = transform.position.z;
    }
    
    void Update()
    {
        customController.LeftInput(KeyCode.LeftArrow, transform.position.x, transform.position.y, Time.time);
        customController.RightInput(KeyCode.RightArrow, transform.position.x, transform.position.y, Time.time);

        displacementVector.x = customController.GetCircularXMovement(transform.position.x, Time.time);
        displacementVector.y = customController.GetCircularYMovement(transform.position.y, Time.time);

        if (displacementVector.x > camright)
        {
            displacementVector.x = camright;
        }
        if (displacementVector.x < camleft)
        {
            displacementVector.x = camright;
        }
        if (displacementVector.y > camtop)
        {
            displacementVector.y = camtop;
        }
        if (displacementVector.y < cambottom)
        {
            displacementVector.y = cambottom;
        }

        transform.position = displacementVector;
    }
}
