using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "EnemyAbility", menuName = "EnemyAbility", order = 2)]
public class EnemyAbility : ScriptableObject
{
    // Start is called before the first frame update
    public string AbilityName;
    public string AbilityDescription;
    public int damage;
    
}
