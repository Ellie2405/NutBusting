using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyMeleeData", menuName = "ScriptableObjects/EnemyDataScriptableObject/Melee", order = 1)]
public class EnemyMeleeScriptableObject : ScriptableObject
{
    public int maxHP;
    public int damage;
    public float movementSpeed;
    public float MaxSpeed;
    public int currencyDrop;
    public LayerMask playerObjectLayerMask;
    public LayerMask floorLayerMask;
    public GameObject HitVFX;
    public Sound SpawnSound;
    public Sound AttackSound;
    public Sound DeathSound;

    public float scanCD;
    public float attackCD;
    public float attackWindUp;
}
