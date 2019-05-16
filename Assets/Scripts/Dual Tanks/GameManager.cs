using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PhysicsLibrary;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float switchTime;
    private bool Player1Turn;

    private GameObject[] players;
    private GameObject[] bullets;
    private GameObject[] terrains;

    void Start() {
        Player1Turn = false;
        InvokeRepeating("SwitchPlayerTurn", 0.1f, switchTime);
    }
    
    void Update()
    {
            
    }

    void SwitchPlayerTurn() {
        if (Player1Turn == true)
            Player1Turn = false;
        else
            Player1Turn = true;
    }

    public bool GetIfPlayer1Turn{
        get { return Player1Turn; }
    }
}
