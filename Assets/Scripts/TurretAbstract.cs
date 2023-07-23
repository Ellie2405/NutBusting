using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TurretAbstract : MonoBehaviour
{
    protected const int MINUTE = 60;

    [SerializeField] protected TPBasicScriptableObject TurretValues;
    [SerializeField] protected AudioSource audioSource;
    protected int hp;
    RingFloor parentRing;
    int ringPosition;

    protected float counter = 0;
    protected float attacksHistorical;

    protected virtual void Start()
    {
        hp = TurretValues.maxHP;
        if (TurretValues.shotsPerMinute <= 0) TurretValues.shotsPerMinute = 60;
    }

    public int GetPrice()
    {
        return TurretValues.price;
    }

    public void AssignPosition(RingFloor ring, int position)
    {
        parentRing = ring;
        ringPosition = position;
    }

    //return true if still alive
    public virtual bool TakeDamage(int damage, EnemyBasic attacker)
    {
        hp -= damage;
        return CheckHP();
    }

    bool CheckHP()
    {
        if (hp < 1)
        {
            DestroyTurret();
            return false;
        }
        return true;
    }

    protected virtual void DestroyTurret()
    {
        parentRing.FreeUpSpot(ringPosition);
        Destroy(gameObject, 0.5f);
    }

    virtual protected void OnDestroy()
    {
        Instantiate(TurretValues.DestroyedVFX, transform.position, Quaternion.identity);
        
    }
}
