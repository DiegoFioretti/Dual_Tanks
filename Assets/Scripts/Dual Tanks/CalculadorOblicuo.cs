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
    //private float currentTime;
    // Observation 3.14 = 180º, so logically 1.57 is 90º, also 0 shoots to the RIGHT so 3.14 shoots LEFT
    // This is because it calculates the angles in RADIANS instead of DEGREES
    // So when showing the degrees in the UI use Mathf.Rad2Deg to show the correct value
    // degree = radian * Mathf.Rad2Deg ---> show "degree"

    void Start(){
        startingTime = Time.deltaTime;
        customPhysics = new PhysicsClass();
        gameObject.SetActive(false);
        customPhysics.SetStartingAngledVariables(transform.position, startingSpeed, shootAngle, gravity);
        //velocidadX = customPhysics.SetStartingXSpeed(Velocidadinicial, Angulodedisparo);
        //velocidadY = customPhysics.SetStartingYSpeed(Velocidadinicial, Angulodedisparo, Gravedad);
        //VelocidadX = Velocidadinicial * Mathf.Cos(Angulodedisparo);
        //VelocidadY = Velocidadinicial * Mathf.Sin(Angulodedisparo) - Gravedad;
        //InvokeRepeating("ResetPosition", 1, 3);
    }

    void Update(){
        //currentTime = startingTime + Time.deltaTime;
        transform.position += customPhysics.Get2DMovement(startingTime + Time.deltaTime);
        //timeCounter += Time.deltaTime;
        //FOR TESTING ONLY
        /*if (transform.position.y <= -5)
        {
            ResetPosition();
        }*/
        //transform.Translate(VelocidadX,VelocidadY,0);
        //VelocidadY -= Gravedad;
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
    /*
    void ResetPosition()
    {
        transform.position = new Vector3(0, 0, -10);
        velocidadX = customPhysics.GetStartingXSpeed(Velocidadinicial, Angulodedisparo);
        velocidadY = customPhysics.GetStartingYSpeed(Velocidadinicial, Angulodedisparo, Gravedad);
        Debug.Log(customPhysics.GetLocalXSpeed());
        Debug.Log(customPhysics.GetLocalYSpeed());
        Debug.Log(customPhysics.GetLocalGravity());
    }*/

    public void OnShoot(Vector3 position)
    {
        if (!gameObject.activeSelf)
        {
            startingTime = Time.deltaTime;
            transform.position = position;
            customPhysics.SetStartingAngledVariables(transform.position, startingSpeed, shootAngle, gravity);
            gameObject.SetActive(true);
        }
    }
}