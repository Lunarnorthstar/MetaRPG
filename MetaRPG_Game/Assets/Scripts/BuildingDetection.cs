using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingDetection : MonoBehaviour
{
    [Header("Entering Building Stuff")]
    public float interactabilityDistance = 7f;

    public Transform enterableBuilding;
    public Animator enterBuildingAni;
    public Text enterBuildingText;

    public PointAndClick pointAndClick;

    int buildingsCloseTo;
    bool isClose;

    void Start()
    {
        InvokeRepeating("checkDistance", 0, 0.2f);

        pointAndClick = GetComponent<PointAndClick>();
    }

    // Update is called once per frame
    void checkDistance()
    {
        if (pointAndClick.isActiveCharacter)
        {
            //Debug.Log(Vector3.Distance(transform.position, enterableBuilding.position));
            if (Vector3.Distance(transform.position, enterableBuilding.position) < interactabilityDistance)
            {
                enterBuildingAni.SetBool("Enter", true);
                enterBuildingText.text = ("Fight Boss!");
            }
            else
            {
                enterBuildingAni.SetBool("Enter", false);
            }
        }
        // //enterBuildingAni.SetBool("Enter", false);
        // for (int i = 0; i < enterableBuildings.Length; i++)
        // {
        //     // Debug.Log(Vector3.Distance(transform.position, enterableBuildings[i].bounds.ClosestPoint(transform.position))); 
        //     if (Vector3.Distance(transform.position, enterableBuildings[i].bounds.ClosestPoint(transform.position)) < interactabilityDistance)
        //     {
        //         enterBuildingAni.SetBool("Enter", true);
        //         enterBuildingText.text = ("Fight " + enterableBuildings[i].name);

        //         buildingsCloseTo++;
        //     }
        //     else
        //     {
        //         enterBuildingAni.SetBool("Enter", false);
        //     }
        // }
    }
}
