using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Data", menuName = "Data/Enemy Data")]
public class EnemyData : ScriptableObject
{
    public string enemyName;
    public float health = 50f, 
                 damage = 10f,
                 defense = 10f,
                 critMultiplier = 1.2f;
}

public static class EnemyState
{
    public const string NEUTRAL = "NEUTRAL";
    public const string BREAK = "BREAK";
    public const string RAGE = "RAGE";
    public const string DEFENSIVE = "DEFENSIVE";
}