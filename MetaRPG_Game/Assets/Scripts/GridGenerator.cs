using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public List<GameObject> gridObjects = new List<GameObject>();

    GameObject tileParent;

    [Header("the width and height of the grid in amount of squares")]
    public int gridWidth;
    public int gridHeight;

    [Header("the spacing in unity default units (1m)")]
    public float gridSpacing;
    public float gridSquareSize;

    public GameObject gridObject;
    public Transform StartPos;

    int currentX;
    int currentY;

    private void Awake()
    {
        tileParent = new GameObject();
        tileParent.transform.position = Vector3.zero;
        tileParent.transform.rotation = Quaternion.identity;
        tileParent.name = "Tiles";

        generateTiles();

        // InvokeRepeating("generateTiles", 0, 1f);
    }

    public void generateTiles()
    {
        for (int i = 0; i < gridObjects.Count; i++)
        {
            Destroy(gridObjects[i]);
        }

        gridObjects.Clear();

        int amountToRight = Mathf.CeilToInt(gridWidth / 2);
        int amountToLeft = Mathf.FloorToInt(gridWidth - amountToRight);

        //x axis or width
        //start from 0, 0 (middle of screen) and then propogate to the right
        for (int x = 0; x <= gridWidth; x++)
        {
            for (int y = 0; y <= gridHeight; y++)
            {
                Vector3 position = StartPos.position + new Vector3(x * gridSpacing, y * gridSpacing, 0);

                GameObject objectInstance = Instantiate(gridObject, position, Quaternion.identity);

                objectInstance.transform.localScale = new Vector3(gridSquareSize, gridSquareSize, gridSquareSize);

                objectInstance.transform.parent = tileParent.transform;

                gridObjects.Add(objectInstance);
            }
        }

        /*
                // start from 0, 0 and propogate to the left
                for (int x = 0; x < amountToLeft; x++)
                {

                    for (int y = 0; y < Mathf.Floor(gridHeight / 2); y++)
                    {
                        Debug.Log("working");
                        Vector3 position = new Vector3(-x * gridSpacing - 1, -y * gridSpacing, 0);
                        gridObjects.Add(Instantiate(gridObject, position, Quaternion.identity));
                    }
                }
                */

    }

}
