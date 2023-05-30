using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TurretAbstract : MonoBehaviour
{
    protected const int MINUTE = 60;

    [SerializeField] protected TPBasicScriptableObject TurretValues;
    [SerializeField] protected int level;

    protected float counter = 0;

    protected virtual void Start()
    {
        if (TurretValues.shotsPerMinute <= 0) TurretValues.shotsPerMinute = 60;
    }

    public int GetPrice()
    {
        return TurretValues.price;
    }
}
