using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public BattleManager battleManager;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (battleManager.isAlliedTurn)
            {
                attack();
            }
        }
    }


    public void attack()
    {

        if (battleManager.isAlliedTurn)
        {
            battleManager.hasAttacked = true;

            Debug.Log("Bam! just attacked for 999 damage");
        }

    }
}
