using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainHero : TurretAbstract
{
    public static event Action<int> OnHeroDamaged;
    int HeroHP = 3;

    protected override void Start()
    {
        base.Start();
        OnHeroDamaged.Invoke(HeroHP);
    }
    public override bool TakeDamage(int damage, EnemyBasic attacker)
    {
        Retaliate(attacker);
        return base.TakeDamage(damage, attacker);
    }

    void Retaliate(EnemyBasic attacker)
    {
        attacker.TakeDamage(99);
    }

    protected override void DestroyTurret()
    {
        HeroHP--;
        OnHeroDamaged.Invoke(HeroHP);
        CheckHeroHP();
    }

    void CheckHeroHP()
    {
        if (HeroHP < 1)
        {
            Destroy(gameObject);
        }
    }

}
