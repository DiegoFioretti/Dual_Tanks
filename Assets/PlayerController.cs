using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private bool IsPlayer1;
    [SerializeField] private float Speed;
    [SerializeField] private GameObject Bullet;
    [SerializeField] private float AngleModifyRate;
    [SerializeField] private float SpeedModifyRate;
    [SerializeField] private float MaxBulletSpeed;

    Camera camara;
    Vector3 camarapos;
    float camheight;
    float camwidth;
    float camleft;
    float camright;

    float shootSpeed;
    float shootAngle;

    bool speedIncreasing;

    GameManager thegamemanager;
    CalculadorOblicuo calculadorBullet;

    void Start(){
        thegamemanager = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameManager>();
        camara = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        camarapos = GameObject.FindGameObjectWithTag("MainCamera").transform.position;
        camheight = 2f * camara.orthographicSize;
        camwidth = camheight * camara.aspect;
        camright = camarapos.x + (camwidth / 2);
        camleft = camarapos.x - (camwidth / 2);
        calculadorBullet = Bullet.GetComponent<CalculadorOblicuo>();
    }

    void Update(){
        if (thegamemanager.GetIfPlayer1Turn == IsPlayer1){
            if (Input.GetKey(KeyCode.RightArrow) && transform.position.x < camright && !Input.GetKey(KeyCode.LeftArrow))
                transform.Translate(Speed * Time.deltaTime, 0 , 0);
            if (Input.GetKey(KeyCode.LeftArrow) && transform.position.x > camleft && !Input.GetKey(KeyCode.RightArrow))
                transform.Translate(-Speed * Time.deltaTime, 0 , 0);
            if (Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow)){
                //With 0.01 gravity its good to increment at a 0.01 rate
                shootAngle += AngleModifyRate;
                if (shootAngle >= 0)
                    shootAngle = 0;
                calculadorBullet.ConfigurarAngulo = shootAngle;
            }
            if (Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow)){
                //With 0.01 gravity its good to decrease at a 0.01 rate
                shootAngle -= AngleModifyRate;
                if (shootAngle >= 0)
                    shootAngle = 0;
                calculadorBullet.ConfigurarAngulo = shootAngle;
            }
            if (Input.GetKey(KeyCode.Space)){
                //With 0.01 gravity its good to set a Max of 0.5
                if (speedIncreasing)
                    shootSpeed += SpeedModifyRate;
                if (!speedIncreasing)
                    shootSpeed -= SpeedModifyRate;
                if (shootSpeed <= 0)
                    speedIncreasing = true;
                if (shootSpeed >= MaxBulletSpeed)
                    speedIncreasing = false;
                calculadorBullet.ConfigurarAngulo = shootSpeed;
            }
            if (Input.GetKeyUp(KeyCode.Space)){
                //Shoot bullet
                calculadorBullet.OnShoot(transform.position);
            }
        }
    }

    public GameObject GetBullet()
    {
        return Bullet;
    }

    public void OnClash()
    {
        gameObject.SetActive(false);
    }
}
