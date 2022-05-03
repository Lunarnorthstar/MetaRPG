using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script just sets a character up when it's its turn to go
public class CharacterTurnManager : MonoBehaviour
{
    public bool isPlayer;
    public int characterSwitchIndex;

    public CharacterSwitcher characterSwitcher;
    public PlayerAttack playerAttack;
    PointAndClick pointAndClick;
    EnemyBehaviour enemyBehaviour;

    void Awake()
    {
        if (isPlayer)
        {
            playerAttack = GetComponent<PlayerAttack>();
            pointAndClick = GetComponent<PointAndClick>();
        }
        else
        {
            enemyBehaviour = GetComponent<EnemyBehaviour>();
        }
    }

    public void switchTurn()
    {
        characterSwitcher.swtichCharacter(characterSwitchIndex);
        if (isPlayer)
        {
            playerAttack.isMyTurn();
            pointAndClick.canMove = true;
            pointAndClick.canAttack = false;
        }
        else
        {
            enemyBehaviour.isMyTurn = true;
            enemyBehaviour.move(true);
        }
    }
}
