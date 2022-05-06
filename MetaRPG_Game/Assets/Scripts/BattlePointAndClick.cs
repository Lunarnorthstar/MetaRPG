using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePointAndClick : MonoBehaviour
{
    public float playerSpeed;
    //public float battleSystemMaxMoveDistance;

    public Color tileDefaultColour;
    public Color tileSelectedRightColour;
    public Color tileSelectedWrongColour;
    public Color tileInRangeColour;
    public Color tileInRangeOfAttackColour;
    public Color tileEnemyColour;
    [Space]
    public Color enemyTileColour;
    public Color enemyDefaultColour;

    [Header("All of the characters in the game")]
    public List<PointAndClick> players = new List<PointAndClick>();
    public List<EnemyBehaviour> enemies = new List<EnemyBehaviour>();


    void Start()
    {
        PointAndClick[] players_ = FindObjectsOfType<PointAndClick>();

        foreach (var player_ in players_)
        {
            players.Add(player_);
        }

        EnemyBehaviour[] enemies_;

        enemies_ = FindObjectsOfType<EnemyBehaviour>();

        for (int i = 0; i < enemies_.Length; i++)
        {
            enemies.Add(enemies_[i]);
        }
    }

    private void FixedUpdate()
    {
        if (players.Count > 0)
        {
            for (int i = 0; i < players.Count; i++)
            {
                players[i].playerSpeed = playerSpeed;
                // players[i].battleSystemMaxMoveDistance = battleSystemMaxMoveDistance;

                players[i].tileDefaultColour = tileDefaultColour;
                players[i].tileSelectedRightColour = tileSelectedRightColour;
                players[i].tileSelectedWrongColour = tileSelectedWrongColour;
                players[i].tileInRangeColour = tileInRangeColour;
                players[i].tileInRangeOfAttackColor = tileInRangeOfAttackColour;
                players[i].tileEnemyColour = tileEnemyColour;
            }
        }

        if (enemies.Count > 0)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].tileColour = enemyTileColour;
                enemies[i].defaultColour = enemyDefaultColour;
            }
        }
    }
}
