using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UniversalHealthSystem : MonoBehaviour
{
    public Slider healthSlider;
    public Image healthSliderImage;

    PointAndClick pointAndClick;
    EnemyBehaviour enemyBehaviour;

    public TextMesh DamageDisplayText;
    public Vector2 offset;
    public float offsetVary;

    public bool isEnemy;

    public float currentHealth;
    public float MaxHealth;

    [Space]
    public float lerpSpeed;

    [Space]
    public int defence;

    public Gradient healthBarColors;

    void Start()
    {
        currentHealth = MaxHealth;

        if (isEnemy)
        {
            enemyBehaviour = GetComponent<EnemyBehaviour>();
        }
        else
        {
            pointAndClick = GetComponent<PointAndClick>();
        }
    }

    void die()
    {
        if (!isEnemy)//remove yourself from trhe list of players in the enemy behaviour script
        {
            EnemyBehaviour[] enemies = FindObjectsOfType<EnemyBehaviour>();

            foreach (var enemy in enemies)
            {
                enemy.players.Remove(transform);
            }

            BattlePointAndClick battlePointAndClick = FindObjectOfType<BattlePointAndClick>();
            battlePointAndClick.players.Remove(pointAndClick);

            FindObjectOfType<BattleManager>().characters.Remove(GetComponent<CharacterTurnManager>());
        }
        else//if youre an enemy, remove yourself from the list of enemies in the P&C, battle P&C script and the battle manager scripts
        {
            //point and click
            PointAndClick[] pointAndClicks = FindObjectsOfType<PointAndClick>();

            foreach (var pointAndClick in pointAndClicks)
            {
                pointAndClick.allEnemies.Remove(enemyBehaviour);
            }

            //battle point and click
            BattlePointAndClick battlePointAndClick = FindObjectOfType<BattlePointAndClick>();
            battlePointAndClick.enemies.Remove(enemyBehaviour);

            //battle manager
            BattleManager battleManager = FindObjectOfType<BattleManager>();
            battleManager.characters.Remove(GetComponent<CharacterTurnManager>());
        }

        Destroy(gameObject);
    }

    void FixedUpdate()
    {
        if (currentHealth <= 0)
        {
            die();
        }
        if (isEnemy)
        {
            if (enemyBehaviour.isMyTurn)
            {
                healthSlider.maxValue = MaxHealth;
                healthSlider.value = Mathf.Lerp(healthSlider.value, currentHealth, lerpSpeed);

                healthSliderImage.color = healthBarColors.Evaluate(healthSlider.normalizedValue);
            }
        }
        else
        {
            if (pointAndClick.isActiveCharacter)
            {
                healthSlider.maxValue = MaxHealth;
                healthSlider.value = Mathf.Lerp(healthSlider.value, currentHealth, lerpSpeed);

                healthSliderImage.color = healthBarColors.Evaluate(healthSlider.normalizedValue);
            }
        }

    }

    public void takeDamage(float amount)
    {
        float damageToRecive = amount - defence;

        if (damageToRecive > 0)
        {
            currentHealth -= amount;

            TextMesh textInstance = Instantiate(DamageDisplayText);

            float randomAmount = Random.Range(-offsetVary, offsetVary);
            Vector3 offset_ = offset + new Vector2(randomAmount, 0);

            textInstance.transform.position = transform.position + offset_;

            textInstance.text = damageToRecive.ToString("0");

            Destroy(textInstance, 5f);
        }
    }
}

