using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePointAndClick : MonoBehaviour
{
    public float playerSpeed;
    public float battleSystemMaxMoveDistance;

    public Color tileDefaultColour;
    public Color tileSelectedRightColour;
    public Color tileSelectedWrongColour;
    public Color tileInRangeColour;
    [Space]
    public Color enemyTileColour;

    [Header("All of the characters in the game")]
    public PointAndClick[] players;
    public EnemyBehaviour[] enemies;

    void Start()
    {
        players = FindObjectsOfType<PointAndClick>();

        enemies = FindObjectsOfType<EnemyBehaviour>();
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < players.Length; i++)
        {
            players[i].playerSpeed = playerSpeed;
            players[i].battleSystemMaxMoveDistance = battleSystemMaxMoveDistance;

            players[i].tileDefaultColour = tileDefaultColour;
            players[i].tileSelectedRightColour = tileSelectedRightColour;
            players[i].tileSelectedWrongColour = tileSelectedWrongColour;
            players[i].tileInRangeColour = tileInRangeColour;
        }

        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].tileColour = enemyTileColour;
        }
    }
}
