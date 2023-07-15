using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainHero : TurretAbstract
{
    public static event Action<int> OnHeroDamaged;

    protected override void Start()
    {
        base.Start();
        OnHeroDamaged.Invoke(hp);
    }
    public override bool TakeDamage(int damage, EnemyBasic attacker)
    {
        bool isStillAlive = base.TakeDamage(damage, attacker);
        OnHeroDamaged.Invoke(hp);
        Retaliate(attacker);
        Debug.Log(damage);
        return isStillAlive;
    }

    void Retaliate(EnemyBasic attacker)
    {
        attacker.Die();
    }

    protected override void DestroyTurret()
    {
        Destroy(gameObject);

    }

}
