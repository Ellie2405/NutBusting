using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TurretData", menuName = "ScriptableObjects/TurretDataScriptableObject", order = 1)]
public class TPBasicScriptableObject : ScriptableObject
{
    public int maxHP;
    public int damage;
    public float projectileSpeed;
    public int shotsPerMinute;
    public GameObject projectile;
    public float projectileHeight;
    public int price;
    public int shotsToDie;

}
