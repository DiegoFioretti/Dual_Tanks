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
    private float TimeY;

    void Start(){
        VelocidadX = Velocidadinicial * Mathf.Cos(Angulodedisparo);
        TimeY = (Velocidadinicial * Mathf.Sin(Angulodedisparo)) / Gravedad;
        VelocidadY = Velocidadinicial * Mathf.Sin(Angulodedisparo) - Gravedad * TimeY;
    }

    void Update(){
        transform.Translate(VelocidadX,VelocidadY,0);
        VelocidadY -= Gravedad;
    }
}
