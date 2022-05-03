using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    public int attackRange;
    public int attackDamage;

    public BattleManager battleManager;
    public Button attackButton;

    public PointAndClick pointAndClick;

    public bool canAttack;

    void Start()
    {
        pointAndClick = GetComponent<PointAndClick>();
    }

    public void isMyTurn()
    {
        attackButton.onClick.RemoveAllListeners();
        attackButton.onClick.AddListener(activateAttack);

        attackButton.interactable = false;
    }

    public void activateAttack()
    {
        pointAndClick.canAttack = true;
    }

    public void finishedMoving()//when you're done moving, it should make the attack button interactable
    {
        attackButton.interactable = true;
    }

    public void attack(TileInfo tile)
    {
        if (tile.canAttackonThisTile)
        {
            tile.currentEnemy.GetComponent<UniversalHealthSystem>().takeDamage(attackDamage);//deal damage to the enemy that is on the tile you clciked on
        }
        Debug.Log("did " + attackDamage + " damage to " + tile.currentEnemy);

        battleManager.AdvanceTurn();
    }
}
