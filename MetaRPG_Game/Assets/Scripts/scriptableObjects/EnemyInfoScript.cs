using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy", order = 1)]
public class EnemyInfoScript : ScriptableObject
{
    public string EnemyName;
    public string EnemyDescription;
    public int HP;
    public Sprite sprite;
    public EnemyAbility[] abilities;
}
