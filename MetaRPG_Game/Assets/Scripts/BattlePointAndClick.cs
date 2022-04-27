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

    public PointAndClick[] players;

    void Start()
    {
        players = FindObjectsOfType<PointAndClick>();
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
    }
}
