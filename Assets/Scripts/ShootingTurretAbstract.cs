using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShootingTurretAbstract : TurretAbstract
{
    protected virtual void Update()
    {
        counter += Time.deltaTime;
        if (counter > MINUTE / TurretValues.shotsPerMinute)
        {
            Shoot();
            counter = 0;
        }
    }

    protected void Shoot()
    {
        Instantiate(TurretValues.projectile, transform.position, transform.rotation);
    }
}
