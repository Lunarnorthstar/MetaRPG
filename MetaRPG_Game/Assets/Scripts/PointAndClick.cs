using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using Cinemachine;

public class PointAndClick : MonoBehaviour
{
    [Header("Player Movement and Navigation")]
    public float playerSpeed;
    public float nextWayPointDistance = 3;

    [Header("Particle Systems")]
    public GameObject successfulClickParticle;
    public GameObject unsuccessfulClickParticle;

    [Header("Character Switching System")]
    public GameObject selectionArrow;
    public CinemachineVirtualCamera VirtualCam;
    public bool isActiveCharacter;

    //other navigation stuff--------

    bool reachedEndOfPath;
    int currentWayPointIndex;

    Vector2 mousepos;
    Vector3 navigationPoint;

    Rigidbody2D rb;
    Seeker seeker;
    Path path;
    Camera mainCam;

    void Start()
    {
        //Initialise Variables
        rb = GetComponent<Rigidbody2D>();
        seeker = GetComponent<Seeker>();
        mainCam = Camera.main;

        navigationPoint = transform.position;

        InvokeRepeating("updatePath", 0, 0.1f);

        if (isActiveCharacter)
        {
            selectionArrow.SetActive(true);

            VirtualCam.Follow = transform;
        }
        else
        {
            selectionArrow.SetActive(false);
        }
    }

    //this calculates the path to the next waypoint.
    //dont call it in update because its very expensive
    void updatePath()
    {
        if (isActiveCharacter)
        {
            seeker.StartPath(rb.position, navigationPoint, hasFinishedCalculating);
        }
    }

    //I dont really understand what this even does ngl
    void hasFinishedCalculating(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWayPointIndex = 0;
        }
    }

    //this does all the pathfinding to the place you clicked
    void FixedUpdate()
    {
        if (path == null)
            return;

        if (currentWayPointIndex >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 dir = ((Vector2)path.vectorPath[currentWayPointIndex] - rb.position).normalized;
        Vector2 force = dir * playerSpeed;

        rb.AddForce(force);

        float disance = Vector2.Distance(rb.position, path.vectorPath[currentWayPointIndex]);

        if (disance < nextWayPointDistance)
        {
            currentWayPointIndex++;
        }
    }

    public void swtichCharacter(bool isNowActiveCharacter)
    {
        if (isNowActiveCharacter)//if is now active character swicth its arrow and make the camera follow it.
        {
            isActiveCharacter = true;

            selectionArrow.SetActive(true);

            VirtualCam.Follow = transform;
        }
        else //otherwise, switch it off.
        {
            isActiveCharacter = false;
            selectionArrow.SetActive(false);
        }


    }

    void Update()
    {
        //get the position of the mouse in a vector2
        mousepos = new Vector2(
            (mainCam.ScreenToWorldPoint(Input.mousePosition).x),
            (mainCam.ScreenToWorldPoint(Input.mousePosition).y)
        );

        //if you click, it will store that position in a vector3 (so that it can be used in the waypoint system)
        if (isActiveCharacter)
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit2D hit = Physics2D.Raycast(mousepos, Vector2.zero);


                if (hit.collider != null)
                {
                    //have you clicked on a building (somewhere the player cannot move to)
                    if (hit.collider.gameObject.layer == 7)
                    {
                        //spawn in a particle effect and delete it after 2 seconds. Do nothing else.
                        Vector3 spawnPos = new Vector3(
                            mousepos.x,
                            mousepos.y,
                            transform.position.z
                        );

                        GameObject particleInstance = Instantiate(
                            unsuccessfulClickParticle,
                            spawnPos,
                            Quaternion.identity
                        );
                        Destroy(particleInstance, 2);
                    }
                    else
                    {
                        navigationPoint = new Vector3(mousepos.x, mousepos.y, transform.position.z);

                        GameObject particleInstance = Instantiate(
                            successfulClickParticle,
                            navigationPoint,
                            Quaternion.identity
                        );
                        Destroy(particleInstance, 2);
                    }
                }
            }
        }
    }
}
