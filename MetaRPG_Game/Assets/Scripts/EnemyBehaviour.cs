using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public List<Transform> allTiles = new List<Transform>();

    public Color tileColour;

    void Start()
    {
        GameObject[] allTiles_ = GameObject.FindGameObjectsWithTag("Tile");

        for (int i = 0; i < allTiles_.Length; i++)
        {
            allTiles.Add(allTiles_[i].transform);
        }
    }

    //public void
}
