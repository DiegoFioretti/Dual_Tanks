using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PhysicsLibrary;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float switchTime;
    [SerializeField] private string playersTag;
    [SerializeField] private string bulletsTag;
    [SerializeField] private string terrainsTag;
    private bool Player1Turn;
    private PhysicsClass customCollisions;

    private GameObject[] players;
    private GameObject[] bullets;
    private GameObject[] terrains;

    void Start() {
        customCollisions = new PhysicsClass();
        if (customCollisions != null)
        {
            Debug.Log("collisions not null");
        }
        Player1Turn = false;
        if (players == null)
        {
            players = GameObject.FindGameObjectsWithTag(playersTag);
        }
        if (bullets == null)
        {
            bullets = GameObject.FindGameObjectsWithTag(bulletsTag);
        }
        if (terrains == null)
        {
            terrains = GameObject.FindGameObjectsWithTag(terrainsTag);
        }
        InvokeRepeating("SwitchPlayerTurn", 0.1f, switchTime);
    }
    
    void Update()
    {
        for (int b = 0; b < bullets.Length; b++)
        {
            for (int p = 0; p < players.Length; p++)
            {
                if (bullets[b] != 
                    players[p].GetComponent<PlayerController>().GetBullet())
                {
                    if (customCollisions.CheckCollisionSqCc(players[p].GetComponent<Sprite>(), bullets[b].GetComponent<Sprite>(), bullets[b].GetComponent<CalculadorOblicuo>().GetRadius()))
                    {
                        players[p].GetComponent<PlayerController>().OnClash();
                        bullets[b].GetComponent<CalculadorOblicuo>().OnClash();
                    }
                    
                }
            }
            if (bullets[b].activeSelf)
            {
                for (int t = 0; t < terrains.Length; t++)
                {
                    if (customCollisions.CheckCollisionSqCc(terrains[t].GetComponent<Sprite>(),
                        bullets[b].GetComponent<Sprite>(),
                        bullets[b].GetComponent<CalculadorOblicuo>().GetRadius()))
                    {
                        bullets[b].GetComponent<CalculadorOblicuo>().OnClash();
                    }
                }
            }
        }
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
