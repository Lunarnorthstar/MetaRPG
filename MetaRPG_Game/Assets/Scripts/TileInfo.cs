using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInfo : MonoBehaviour
{
    public bool hasEnemyOnIt;
    public bool isTileInRange;// this specifies wether this tile is ready for an attack (in range)
    public EnemyBehaviour currentEnemy;

    public bool canAttackonThisTile;

    void FixedUpdate()
    {
        if (hasEnemyOnIt && isTileInRange)
        {
            canAttackonThisTile = true;
        }
    }
}
