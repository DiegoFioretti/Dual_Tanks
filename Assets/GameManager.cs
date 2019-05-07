using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PhysicsLibrary;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float switchTime;
    [SerializeField] private float bulletRadius;
    [SerializeField] private string playersTag;
    [SerializeField] private string bulletsTag;
    [SerializeField] private string terrainsTag;
    private bool Player1Turn;
    private PhysicsClass customCollisions;

    private Sprite[] players;
    private Sprite[] bullets;
    private Sprite[] terrains;

    void Start() {
        customCollisions = new PhysicsClass();
        Player1Turn = false;
        GameObject[] playersGO;
        GameObject[] bulletsGO;
        GameObject[] terrainsGO;
        if (players != null)
        {
            playersGO = GameObject.FindGameObjectsWithTag(playersTag);
            for (int i = 0; i < playersGO.Length; i++)
            {
                players.SetValue(playersGO[i].GetComponent<Sprite>(), i);
            }
        }
        if (bullets != null)
        {
            bulletsGO = GameObject.FindGameObjectsWithTag(bulletsTag);
            for (int j = 0; j < bulletsGO.Length; j++)
            {
                players.SetValue(bulletsGO[j].GetComponent<Sprite>(), j);
            }
        }
        if (terrains != null)
        {
            terrainsGO = GameObject.FindGameObjectsWithTag(terrainsTag);
            for (int k = 0; k < terrainsGO.Length; k++)
            {
                players.SetValue(terrainsGO[k].GetComponent<Sprite>(), k);
            }
        }
        InvokeRepeating("SwitchPlayerTurn", 0.1f, switchTime);
    }
    
    void Update()
    {
        for (int p = 0; p < players.Length; p++)
        {
            for (int b = 0; b < bullets.Length; b++)
            {
                if (customCollisions.CheckCollisionSqCc(players[p],bullets[b],bulletRadius))
                {
                    
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

    private void BulletDestruction()
    {
        
    }
}
