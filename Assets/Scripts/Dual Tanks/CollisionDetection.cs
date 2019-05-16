using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PhysicsLibrary;

public class CollisionDetection : MonoBehaviour
{
    [SerializeField] private string playerTag;
    [SerializeField] private string terrainTag;
    PhysicsClass customCollision;

    private GameObject[] players;
    private GameObject[] terrains;

    private Sprite selfSprite;
    private float radius;

    // Start is called before the first frame update
    void Start()
    {
        customCollision = new PhysicsClass();
        selfSprite = gameObject.GetComponent<SpriteRenderer>().sprite;
        radius = selfSprite.bounds.size.x / 2;
        players = GameObject.FindGameObjectsWithTag(playerTag);

        terrains = GameObject.FindGameObjectsWithTag(terrainTag);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf && players != null)
        {
            for (int p = 0; p < players.Length; p++)
            {
                if (gameObject.activeSelf && customCollision.CheckCollisionSqCc(players[p].transform.position, 
                    players[p].GetComponent<SpriteRenderer>().sprite, transform.position,selfSprite, radius) && 
                    gameObject.name != players[p].GetComponent<PlayerController>().GetBulletName())
                {
                    players[p].GetComponent<PlayerController>().OnClash();
                    gameObject.SetActive(false);
                    
                }
            }
        }
        if (gameObject.activeSelf && terrains != null)
        {
            for (int t = 0; t < terrains.Length; t++)
            {
                if (gameObject.activeSelf && customCollision.CheckCollisionSqCc(terrains[t].transform.position, 
                    terrains[t].GetComponent<SpriteRenderer>().sprite, 
                    transform.position, selfSprite, radius))
                {
                    Debug.Log(terrains[t].name);
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
