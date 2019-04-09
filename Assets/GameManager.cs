using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float SwitchTime;

    private bool Player1Turn;

    void Start(){
        Player1Turn = true;

        InvokeRepeating("SwitchPlayerTurn", SwitchTime, SwitchTime);
    }

    // Update is called once per frame
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
