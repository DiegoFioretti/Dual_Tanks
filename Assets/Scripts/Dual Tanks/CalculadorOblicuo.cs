using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PhysicsLibrary;

public class CalculadorOblicuo : MonoBehaviour
{
    [SerializeField] private float shootAngle;
    [SerializeField] private float startingSpeed;
    [SerializeField] private float gravity;

    private PhysicsClass customPhysics;
    private float startingTime;
    private Vector3 auxVector;
    
    // Observation 3.14 = 180º, so logically 1.57 is 90º, also 0 shoots to the RIGHT so 3.14 shoots LEFT
    // This is because it calculates the angles in RADIANS instead of DEGREES
    // So when showing the degrees in the UI use Mathf.Rad2Deg to show the correct value
    // degree = radian * Mathf.Rad2Deg ---> show "degree"

    void Start(){
        customPhysics = new PhysicsClass();
        gameObject.SetActive(false);
        //customPhysics.SetStartingAngledVariables(transform.position, startingSpeed, shootAngle, -gravity);
        auxVector.z = transform.position.z;
    }

    private void Awake()
    {
        startingTime = Time.time;
    }

    void Update(){
        auxVector.x = customPhysics.Get2DMovementX(Time.time - startingTime);
        auxVector.y = customPhysics.Get2DMovementY(Time.time - startingTime);
        transform.position = auxVector;
    }

    public float ConfigurarVelocidad{
        get { return startingSpeed; }
        set {
            if (value < 0)
                startingSpeed = 0.0f;
            else
                startingSpeed = value;
        }
    }

    public float ConfigurarAngulo{
        get { return shootAngle; }
        set{
            if (value < 0)
                shootAngle = 0.0f;
            else if (value > 3.14)
                shootAngle = 3.14f;
            else
                shootAngle = value;
        }
    }
    // FOR TESTING ONLY
    /*void ResetPosition()
    {
        transform.position = new Vector3(0, 0, 0);
        auxVector.x = transform.position.x;
        auxVector.y = transform.position.y;
        auxVector.z = transform.position.z;
        startingTime = Time.time;
        customPhysics.ResetAngledVariables();
        customPhysics.SetStartingAngledVariables(transform.position, startingSpeed, shootAngle, -gravity);
    }*/

    public void OnShoot(Vector3 position)
    {
        if (!gameObject.activeSelf)
        {
            startingTime = Time.time;
            transform.position = position;
            auxVector = transform.position;
            customPhysics.SetStartingAngledVariables(position, startingSpeed, shootAngle, -gravity);
            gameObject.SetActive(true);
        }
    }
}