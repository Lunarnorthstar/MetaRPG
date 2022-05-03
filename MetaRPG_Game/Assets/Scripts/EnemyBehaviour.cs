using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public List<Transform> allTiles = new List<Transform>();
    public List<Transform> players = new List<Transform>();

    public Color tileColour;
    public Color defaultColour;

    public BattleManager battleManager;

    Vector3 targetPos;

    public float attackRange;
    public float attackDamage;
    public float moveDistance;

    public float damageRange;
    public float lerpSpeed = 0.01f;

    public bool isMyTurn;

    void Start()
    {
        GameObject[] allTiles_ = GameObject.FindGameObjectsWithTag("Tile");

        for (int i = 0; i < allTiles_.Length; i++)
        {
            allTiles.Add(allTiles_[i].transform);
        }

        PointAndClick[] players_ = GameObject.FindObjectsOfType<PointAndClick>();//list of all the players in the scene

        foreach (var Player in players_)
        {
            players.Add(Player.transform);//loop through and add their transforms into a list.
        }

        move(false);

        InvokeRepeating("checkDistanceToTiles", 0, 0.1f);
    }

    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, targetPos, lerpSpeed);
    }


    public void move(bool shouldAttack)
    {
        Transform targetPlayer = players[Random.Range(0, players.Count)];

        Vector2 movementVector = new Vector2
        (targetPlayer.position.x - transform.position.x,
        targetPlayer.position.y - transform.position.y);//get a vector that points in the direction of the player.

        movementVector.Normalize(); // set its length to 1.

        //create a new empty object for the sole purpose of moving where we want it to be
        GameObject tempPos = new GameObject();
        Transform tempPosTransform = tempPos.transform;
        tempPosTransform.position = transform.position;

        float moveVectorMagnitude = Random.Range(0f, moveDistance);//calculate a random amount to move the enemy by
        //float moveVectorMagnitude = moveDistance;

        //now this object is in a random position near one of the players. Snap it to the closest tile.

        tempPosTransform.Translate(movementVector * moveVectorMagnitude);

        Vector3 closestTile = Vector3.zero;  // empty closest tile object

        float minDistance = Mathf.Infinity;
        foreach (var tile in allTiles)//sort through each tile and check its distance to us. the closest one will be returned and we will snap to it.
        {
            float currentDistance = Vector3.Distance(tempPosTransform.position, tile.position);
            if (currentDistance < minDistance)
            {
                closestTile = tile.position;
                minDistance = currentDistance;
            }
        }

        tempPosTransform.position = closestTile;//sjap it

        targetPos = tempPosTransform.position;//set our target there for lerping

        Destroy(tempPos);//destroy that object

        if (shouldAttack)
            StartCoroutine(waitToAttack());
    }


    public void attack()
    {
        foreach (var player in players)//loop thru the players
        {
            if (Vector3.Distance(transform.position, player.position) < attackRange)//check the distance to each. boss will simply deal damage to multiple if needs be
            {
                player.GetComponent<UniversalHealthSystem>().takeDamage(attackDamage);
                Debug.Log("BOSS dealt " + attackDamage + " damage to " + player.name);
            }
        }

        isMyTurn = false;

        battleManager.AdvanceTurn();
    }

    public void checkDistanceToTiles()
    {
        if (isMyTurn)
        {
            foreach (var tile in allTiles)
            {
                SpriteRenderer spriteRend = tile.GetComponent<SpriteRenderer>();
                if (Mathf.Ceil((Vector3.Distance(transform.position, tile.position))) <= attackRange)//if this tile is in range
                {
                    spriteRend.color = tileColour;
                    //                    Debug.Log("hey");
                }
                else
                {
                    spriteRend.color = defaultColour;
                }
            }
        }
    }

    IEnumerator waitToAttack()
    {
        yield return new WaitForSeconds(Random.Range(2f, 7f));

        attack();
    }
}
