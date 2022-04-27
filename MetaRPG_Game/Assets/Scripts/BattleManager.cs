using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public bool isAlliedTurn;

    public bool hasAttacked;


    //round will procees once you finish doing an attack.
    public void startTurn()
    {
        if (isAlliedTurn)
        {
            hasAttacked = false;
        }
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
