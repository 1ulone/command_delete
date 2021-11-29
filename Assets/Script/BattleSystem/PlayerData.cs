using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Data/Player Data")]
public class PlayerData : ScriptableObject
{
    public string playername = "Player";

    public float damage = 10f,
                 health = 100f,
                 critMultiplier = 1.5f,
                 defense = 10f;
    public int luck = 5;
}

public static class AttackType 
{
    public const string BREAKER = "BREAKER";
    public const string SWIFT = "SWIFT";
    public const string BUFF = "BUFF";
}
