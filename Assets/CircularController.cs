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

    void Start()
    {
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

        transform.position = displacementVector;
    }
}
