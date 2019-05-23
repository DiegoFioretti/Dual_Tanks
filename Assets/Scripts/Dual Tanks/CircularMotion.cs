using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PhysicsLibrary;

public class CircularMotion : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float circleRadius;
    [SerializeField] private bool isGoingRight;

    PhysicsClass circularMotion;

    void Start()
    {
        circularMotion = new PhysicsClass();
        circularMotion.SetStartingRadiusConstants(circleRadius, speed, transform.localPosition ,isGoingRight);
    }
    
    void Update()
    {
        transform.Translate(circularMotion.GetRadialMovement());
    }
}
