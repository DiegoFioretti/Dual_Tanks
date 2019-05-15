using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UniversalUI : MonoBehaviour
{
    [SerializeField] private GameObject gameManagerGO;
    [SerializeField] private GameObject player1GO;
    [SerializeField] private GameObject player2GO;
    [SerializeField] private bool isPower;
    [SerializeField] private bool isTurn;
    [SerializeField] private bool isAngle;

    GameManager gameManager;
    PlayerController player1;
    PlayerController player2;
    Text selfText;

    void Start()
    {
        gameManager = gameManagerGO.GetComponent<GameManager>();
        player1 = player1GO.GetComponent<PlayerController>();
        player2 = player2GO.GetComponent<PlayerController>();
        selfText = gameObject.GetComponent<Text>();
    }
    

    void Update()
    {
        if (isPower)
        {
            ShowPower();
        }
        if (isAngle)
        {
            ShowAngle();
        }
        if (isTurn)
        {
            ShowPlayerTurn();
        }
    }

    private void ShowPower()
    {
        if (gameManager.GetIfPlayer1Turn)
        {
            selfText.text = "POWER: " + (int) PowerPercentage(player1.GetPower()) + "%";
        }
        else
        {
            selfText.text = "POWER: " + (int) PowerPercentage(player2.GetPower()) + "%";
        }
    }

    private void ShowAngle()
    {
        if (gameManager.GetIfPlayer1Turn)
        {
            selfText.text = "ANGLE: " + (int) AngleConverter(player1.GetAngle());
        }
        else
        {
            selfText.text = "ANGLE: " + (int) AngleConverter(player2.GetAngle());
        }
    }

    private void ShowPlayerTurn()
    {
        if (gameManager.GetIfPlayer1Turn)
        {
            selfText.text = "PLAYER 1 TURN";
        }
        else
        {
            selfText.text = "PLAYER 2 TURN";
        }
    }

    private float AngleConverter(float angle)
    {
        return (angle * 180) / 3.14f;
    }

    private float PowerPercentage(float power)
    {
        return (power * 100) / 0.3f;
    }
}
