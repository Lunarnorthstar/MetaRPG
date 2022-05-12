using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using Cinemachine;
using UnityEngine.EventSystems;

public class PointAndClick : MonoBehaviour
{
    public bool isInBattle;
    public bool canMove = true;
    public bool canAttack;

    public Transform sprite;

    [Header("Player Movement and Navigation")]
    public float playerSpeed;
    public float nextWayPointDistance = 3;
    public float nonActiveModifer = 0.5f;


    [Header("Battle Information")]
    public bool isGoblin;
    public List<GameObject> allTiles = new List<GameObject>();
    public List<EnemyBehaviour> allEnemies = new List<EnemyBehaviour>();

    public BattleManager battleManager; //DAVID  = PlayerAbilities = VALORANT THERE4 DAVE IZ ABIG POG GAMER!! N THAZ A FACT!!!!  This is 
    public float battleSystemMaxMoveDistance;
    public PlayerAttack playerAttack;

    public Color tileDefaultColour;
    public Color tileSelectedRightColour;
    public Color tileSelectedWrongColour;
    public Color tileInRangeColour;
    public Color tileInRangeOfAttackColor;
    public Color tileEnemyColour;

    [Header("Particle Systems")]
    public GameObject successfulClickParticle;
    public GameObject unsuccessfulClickParticle;

    [Header("Character Switching System")]
    public GameObject selectionArrow;
    public CinemachineVirtualCamera VirtualCam;
    public bool isActiveCharacter;

    public Transform nonActiveFollowPoint;
    //other navigation stuff--------

    bool reachedEndOfPath;
    int currentWayPointIndex;

    Vector2 mousepos;
    [SerializeField] Vector3 navigationPoint;
    Vector3 altPoint;

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

        PlayerPrefs.SetInt("canMove", 1); //alow player to move

        sprite = transform.GetChild(0);

        //non battle stuff
        if (!isInBattle)
        {
            InvokeRepeating("updatePath", 0, 0.1f);
            navigationPoint = transform.position;
        }
        else //battle initialisation
        {
            //make a list of all the tiles in the scene
            GameObject[] alltiles_ = GameObject.FindGameObjectsWithTag("Tile");

            for (int i = 0; i < alltiles_.Length; i++)
            {
                allTiles.Add(alltiles_[i]);
                alltiles_[i].GetComponent<SpriteRenderer>().color = tileDefaultColour;
            }

            //make a list of all enemies in scene.
            EnemyBehaviour[] allEnemies_ = FindObjectsOfType<EnemyBehaviour>();

            for (int i = 0; i < allEnemies_.Length; i++)
            {
                allEnemies.Add(allEnemies_[i]);
            }

            InvokeRepeating("checkDistanceToCubes", 0, 0.1f);

            playerAttack = GetComponent<PlayerAttack>();

            //float randomPos = Random.Range(-20f, 20f);
            //navigationPoint = new Vector3(randomPos, randomPos, 0);

        }
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
        else
        {

            seeker.StartPath(rb.position, nonActiveFollowPoint.position, hasFinishedCalculating);
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
        if (navigationPoint.x < transform.position.x)
        {
            sprite.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            sprite.localScale = new Vector3(1, 1, 1);
        }
        if (!isInBattle)
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
            Vector2 force;

            if (isActiveCharacter)
            {
                force = dir * playerSpeed;
            }
            else
            {
                force = dir * playerSpeed * nonActiveModifer;
            }

            rb.AddForce(force);
            float disance = Vector2.Distance(rb.position, path.vectorPath[currentWayPointIndex]);

            if (disance < nextWayPointDistance)
            {
                currentWayPointIndex++;
            }
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, navigationPoint, playerSpeed);
        }

    }

    /// <summary>
    /// this deals with the colours you see in each of the tiles, going one by one through
    // them to check for differnt information to determine what colour it will be (eg if an enemy is on a tile
    // and you can attack then make it orange
    /// </summary>
    void checkDistanceToCubes()
    {
        if (isActiveCharacter)
        {
            if (!canAttack) // if you cacnt attack, display movement range
            {
                for (int i = 0; i < allTiles.Count; i++)
                {
                    if ((Mathf.Ceil(Vector3.Distance(allTiles[i].transform.position, transform.position)) > battleSystemMaxMoveDistance)
                    || allTiles[i].GetComponent<TileInfo>().hasPlayerOnIt)
                    {
                        allTiles[i].GetComponent<SpriteRenderer>().color = tileDefaultColour;
                    }
                    else
                    {
                        allTiles[i].GetComponent<SpriteRenderer>().color = tileInRangeColour;
                    }
                }
            }
            else //if you can attack, display attack range
            {
                for (int i = 0; i < allTiles.Count; i++)
                {
                    if (isGoblin)//if its the goblin, then it should only attack in 6 by 6 lines around it.
                    {
                        //if the tile is on the same x as us and the tile is within the minimum range, then turn it's colour.
                        //get the difference between our x values and then if it rounds to 0 then yh we on the same x
                        float xDistance = Mathf.RoundToInt(Mathf.Abs(transform.position.x - allTiles[i].transform.position.x));
                        float yDistance = Mathf.RoundToInt(Mathf.Abs(transform.position.y - allTiles[i].transform.position.y));

                        if (xDistance == 0
                        && Mathf.Ceil(Vector3.Distance(allTiles[i].transform.position, transform.position)) < playerAttack.attackRange)
                        {
                            allTiles[i].GetComponent<SpriteRenderer>().color = tileInRangeOfAttackColor;
                            allTiles[i].GetComponent<TileInfo>().isTileInRange = true;

                        }//similarly, if the tile is on the same y as us and yh
                        else if (yDistance == 0
                             && Mathf.Ceil(Vector3.Distance(allTiles[i].transform.position, transform.position)) < playerAttack.attackRange)
                        {
                            allTiles[i].GetComponent<SpriteRenderer>().color = tileInRangeOfAttackColor;
                            allTiles[i].GetComponent<TileInfo>().isTileInRange = true;
                        }
                        else
                        {
                            allTiles[i].GetComponent<SpriteRenderer>().color = tileDefaultColour;
                            allTiles[i].GetComponent<TileInfo>().isTileInRange = false;
                        }
                    }
                    else //othereise attack like normal (r u within distance?)
                    {
                        if (Mathf.Ceil(Vector3.Distance(allTiles[i].transform.position, transform.position)) > playerAttack.attackRange)
                        {
                            allTiles[i].GetComponent<SpriteRenderer>().color = tileDefaultColour;
                            allTiles[i].GetComponent<TileInfo>().isTileInRange = false;
                        }
                        else
                        {
                            allTiles[i].GetComponent<SpriteRenderer>().color = tileInRangeOfAttackColor;
                            allTiles[i].GetComponent<TileInfo>().isTileInRange = true;
                        }
                    }

                    //for each tile, check the distance to each enemy, and if it is less than that enemy's
                    //damage range (the range at which it may be damaged) then make that tile orange or smth
                    //only do this if it is within the range
                    //what this does is highlights the area around the enemy within your range that can be attacked.
                    foreach (var enemy in allEnemies)
                    {
                        if (Vector3.Distance(allTiles[i].transform.position, enemy.transform.position)
                         < enemy.damageRange)//distabce check
                        {
                            if (allTiles[i].GetComponent<TileInfo>().isTileInRange)//check if it's in range.
                            {
                                //set it's colour and then tell the tile that is has an enemy on it (this is for attack purposes)
                                allTiles[i].GetComponent<SpriteRenderer>().color = tileEnemyColour;
                                allTiles[i].GetComponent<TileInfo>().hasEnemyOnIt = true;
                                allTiles[i].GetComponent<TileInfo>().currentEnemy = enemy;
                            }
                        }
                        else
                        {
                            allTiles[i].GetComponent<TileInfo>().hasEnemyOnIt = false;
                            allTiles[i].GetComponent<TileInfo>().currentEnemy = null;
                        }
                    }
                }
            }
        }
    }

    public void makeMove(Vector3 pos)
    {
        if (isActiveCharacter)
            navigationPoint = pos;
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
        // if (isActiveCharacter)//do nothing of this if you arent active
        // {
        if (isInBattle)//if you are in battle, then it will do tile detection and nothing else
        {
            RaycastHit2D hit = Physics2D.Raycast(mousepos, Vector2.zero);//see if theres anything where the mouse is

            if (hit.collider != null && !EventSystem.current.IsPointerOverGameObject())//did ya hhit something?
            {
                GameObject objectYouHit = hit.collider.gameObject; //store the hit object in a local variable

                //Debug.Log(objectYouHit.name);

                if (objectYouHit.CompareTag("Tile"))//is it a tile?
                {
                    if (Input.GetMouseButtonDown(0))//did you click this frame?
                    {
                        if (canMove && PlayerPrefs.GetInt("canMove") == 1)//if you clicked on a tile you can click on and you can move AND the delay has been met
                        {
                            if (Mathf.Floor(Vector3.Distance(transform.position, objectYouHit.transform.position))
                             < battleSystemMaxMoveDistance)//is the tile u clicked on within movement range?
                            {
                                makeMove(objectYouHit.transform.position);//move the person
                                battleManager.checkInfo(true, false);//update the manager with this info
                                canMove = false;

                                playerAttack.finishedMoving();
                            }
                        }
                        else//did you click on a tile but cant move (indicates that ur attacking)
                        {
                            if (Mathf.Floor(Vector3.Distance(transform.position, objectYouHit.transform.position))
                             < playerAttack.attackRange)//is the tile u clicked on within attack range?
                            {
                                if (canAttack)
                                {
                                    playerAttack.attack(objectYouHit.GetComponent<TileInfo>());//ATTACK

                                    canAttack = false;

                                    StartCoroutine(waitToMove());
                                }
                            }
                        }
                    }
                    else
                    {
                        if (Mathf.Floor(Vector3.Distance(transform.position, objectYouHit.transform.position))
                         < battleSystemMaxMoveDistance)//if its outside the range then it shall be red
                        {
                            objectYouHit.GetComponent<SpriteRenderer>().color = tileSelectedRightColour;
                        }
                        else
                        {
                            objectYouHit.GetComponent<SpriteRenderer>().color = tileSelectedWrongColour;
                        }
                    }
                }
            }
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(mousepos, Vector2.zero);
            if (hit.collider != null)
            {
                GameObject objectYouHit = hit.collider.gameObject;

                //have you clicked on a building (somewhere the player cannot move to)
                if (objectYouHit.layer == 7)
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


    IEnumerator waitToMove()
    {
        PlayerPrefs.SetInt("canMove", 0);
        yield return new WaitForSeconds(0.1f);
        PlayerPrefs.SetInt("canMove", 1);
    }
}

