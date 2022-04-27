using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public bool isAlliedTurn;

    public bool hasAttacked;

    public Text turnText;

    [Header("Carousel parameters")]
    public Rigidbody2D carouselRB;
    public float carouselForce;

    //round will procees once you finish doing an attack.
    public void startTurn()
    {
        if (isAlliedTurn)
        {
            turnText.text = "Your Turn";
            turnText.GetComponent<Animator>().Play("Turn");

            hasAttacked = false;
        }
        else
        {
            turnText.text = "Enemy Turn";
            turnText.GetComponent<Animator>().Play("Turn");
        }
    }

    void Update()
    {
        if (hasAttacked)//this is where the check for if the turn has ended - put whatever you want in this if statement.
        {
            hasAttacked = false;
            endTurn();
        }
    }

    public void moveCarousel()
    {
        carouselRB.AddTorque(carouselForce);
    }

    public void endTurn()
    {
        if (isAlliedTurn)
        {
            isAlliedTurn = false;
        }
        else
        {
            isAlliedTurn = true;
        }
    }
}
