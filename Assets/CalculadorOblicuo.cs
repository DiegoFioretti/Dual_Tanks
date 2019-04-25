using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PhysicsLibrary;

public class CalculadorOblicuo : MonoBehaviour
{
    [SerializeField] private float Angulodedisparo;
    [SerializeField] private float Velocidadinicial;
    [SerializeField] private float Gravedad;

    private float velocidadX;
    private float velocidadY;

    private PhysicsClass customPhysics;

    // Observation 3.14 = 180º, so logically 1.57 is 90º, also 0 shoots to the RIGHT so 3.14 shoots LEFT
    // This is because it calculates the angles in RADIANS instead of DEGREES
    // So when showing the degrees in the UI use Mathf.Rad2Deg to show the correct value
    // degree = radian * Mathf.Rad2Deg ---> show "degree"

    void Start(){
        customPhysics = new PhysicsClass();
        velocidadX = customPhysics.GetStartingXSpeed(Velocidadinicial, Angulodedisparo);
        velocidadY = customPhysics.GetStartingYSpeed(Velocidadinicial, Angulodedisparo, Gravedad);
        //VelocidadX = Velocidadinicial * Mathf.Cos(Angulodedisparo);
        //VelocidadY = Velocidadinicial * Mathf.Sin(Angulodedisparo) - Gravedad;
        //InvokeRepeating("ResetPosition", 1, 3);
    }

    void Update(){
        transform.Translate(customPhysics.Get2DMovement());
        //FOR TESTING ONLY
        /*if (transform.position.y <= -5)
        {
            ResetPosition();
        }*/
        //transform.Translate(VelocidadX,VelocidadY,0);
        //VelocidadY -= Gravedad;
    }

    public float ConfigurarVelocidad{
        get { return Velocidadinicial; }
        set {
            if (value < 0)
                Velocidadinicial = 0.0f;
            else
                Velocidadinicial = value;
        }
    }

    public float ConfigurarAngulo{
        get { return Angulodedisparo; }
        set{
            if (value < 0)
                Angulodedisparo = 0.0f;
            else if (value > 3.14)
                Angulodedisparo = 3.14f;
            else
                Angulodedisparo = value;
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
}
