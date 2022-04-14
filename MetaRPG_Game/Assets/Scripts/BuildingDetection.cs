using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingDetection : MonoBehaviour
{
    [Header("Entering Building Stuff")]
    public float interactabilityDistance = 7f;

    public BoxCollider2D[] enterableBuildings;
    public Animator enterBuildingAni;
    public Text enterBuildingText;

    int buildingsCloseTo;
    bool isClose;

    void Start()
    {
        InvokeRepeating("checkDistance", 0, 0.2f);
    }

    // Update is called once per frame
    void checkDistance()
    {
        enterBuildingAni.SetBool("Enter", false);
        for (int i = 0; i < enterableBuildings.Length; i++)
        {
            // Debug.Log(Vector3.Distance(transform.position, enterableBuildings[i].bounds.ClosestPoint(transform.position))); 
            if (Vector3.Distance(transform.position, enterableBuildings[i].bounds.ClosestPoint(transform.position)) < interactabilityDistance)
            {
                enterBuildingAni.SetBool("Enter", true);
                enterBuildingText.text = ("Enter " + enterableBuildings[i].name);

                buildingsCloseTo++;
            }
        }
    }
}
