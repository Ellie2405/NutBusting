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
        }
    }

    private void FixedUpdate()
    {
        if (attacksHistorical >= TurretValues.shotsToDie)
        {
            DestroyTurret();
        }

    }

    protected void Shoot()
    {
        Instantiate(TurretValues.projectile, new(transform.position.x, TurretValues.projectileHeight, transform.position.z), transform.rotation);
        attacksHistorical++;
        counter = 0;
    }
}
