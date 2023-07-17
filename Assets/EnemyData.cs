using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "EnemyData")]
public class EnemyData : ScriptableObject
{
    public int Health;
    public int enemySpeed;
    public int enemyDamage;
    public int enemyPower;
}

