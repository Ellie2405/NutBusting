using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainHero : TurretAbstract
{
    public static event Action<int> OnHeroDamaged;
    public static event Action OnCastleDestroyed;
    [SerializeField] Animator animator;
    

    protected override void Start()
    {
        base.Start();
        OnHeroDamaged?.Invoke(hp);
    }
    public override bool TakeDamage(int damage, EnemyBasic attacker)
    {
        bool isStillAlive = base.TakeDamage(damage, attacker);
        OnHeroDamaged.Invoke(hp);
        Retaliate(attacker);
        animator.SetTrigger("Hit");
        return isStillAlive;
    }

    void Retaliate(EnemyBasic attacker)
    {
        attacker.Die();
    }

    protected override void DestroyTurret()
    {
        Instantiate(TurretValues.DestroyedVFX, transform.position + Vector3.up, Quaternion.identity);
        OnCastleDestroyed.Invoke();
        Destroy(gameObject);
    }

}
