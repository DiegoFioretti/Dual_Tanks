using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private bool IsPlayer1;
    [SerializeField] private float Speed;
    [SerializeField] private GameObject Bullet;

    Camera camara;
    Vector3 camarapos;
    float camheight;
    float camwidth;
    float camleft;
    float camright;

    float shootSpeed;
    float shootAngle;

    GameManager thegamemanager;

    void Start(){
        thegamemanager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        camara = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        camarapos = GameObject.FindGameObjectWithTag("MainCamera").transform.position;
        camheight = 2f * camara.orthographicSize;
        camwidth = camheight * camara.aspect;
        camright = camarapos.x + (camwidth / 2);
        camleft = camarapos.x - (camwidth / 2);
    }

    void Update(){
        if (thegamemanager.GetIfPlayer1Turn == IsPlayer1){
            if (Input.GetKey(KeyCode.RightArrow) && transform.position.x < camright && !Input.GetKey(KeyCode.LeftArrow))
                transform.Translate(Speed * Time.deltaTime, 0 , 0);
            if (Input.GetKey(KeyCode.LeftArrow) && transform.position.x > camleft && !Input.GetKey(KeyCode.RightArrow))
                transform.Translate(-Speed * Time.deltaTime, 0 , 0);
            if (Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow)){
                //Increase shootAngle
            }
            if (Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow)){
                //Increase shootAngle
            }
            if (Input.GetKey(KeyCode.Space)){
                //Increase shootSpeed
            }
            if (Input.GetKeyUp(KeyCode.Space)){
                //Shoot bullet
                Instantiate(Bullet, transform);
            }
        }
    }
}
