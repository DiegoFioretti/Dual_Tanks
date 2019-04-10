using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculadorOblicuo : MonoBehaviour
{
    [SerializeField] private float Angulodedisparo;
    [SerializeField] private float Velocidadinicial;
    [SerializeField] private float Gravedad;

    private float VelocidadX;
    private float VelocidadY;

    // Observation 3.14 = 180º, so logically 1.57 is 90º, also 0 shoots to the RIGHT so 3.14 shoots LEFT
    // This is because it calculates the angles in RADIANS instead of DEGREES
    // So when showing the degrees in the UI use Mathf.Rad2Deg to show the correct value
    // degree = radian * Mathf.Rad2Deg ---> show "degree"

    void Start(){
        VelocidadX = Velocidadinicial * Mathf.Cos(Angulodedisparo);
        VelocidadY = Velocidadinicial * Mathf.Sin(Angulodedisparo) - Gravedad;
        InvokeRepeating("ResetPosition", 1, 2);
    }

    void Update(){
        transform.Translate(VelocidadX,VelocidadY,0);
        VelocidadY -= Gravedad;
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
}
