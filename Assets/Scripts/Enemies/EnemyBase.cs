using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyScriptable", menuName = "ScriptableObjects/EnemyScriptable")]
public class EnemyBase : ScriptableObject
{
    [Header("Enemy Status")]
    public float hp;
    public float attackPower;
    public float speed;
}
