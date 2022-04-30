using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "PlayerAbility", menuName = "PlayerAbility", order = 3)]
public class PlayerAbilities : ScriptableObject
{
    public string abilityName;
    public string abilityDesc;
    public int Mindamage;
    public int Maxdamage;
    
}
