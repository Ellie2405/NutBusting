using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyMeleeData", menuName = "ScriptableObjects/EnemyDataScriptableObject/Melee", order = 1)]
public class EnemyMeleeScriptableObject : ScriptableObject
{
    public int maxHP;
    public int damage;
    public float movementSpeed;
    public int currencyDrop;
    public LayerMask playerObjectLayerMask;

}
