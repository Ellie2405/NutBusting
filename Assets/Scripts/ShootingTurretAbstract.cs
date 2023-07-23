using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public abstract class ShootingTurretAbstract : TurretAbstract
{
    protected override void Start()
    {
        base.Start();
        counter = 0.55f;
        TurretValues.BuildSound.Play();
    }

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
        TurretValues.FireSound.Play();
    }

    protected override void DestroyTurret()
    {
        TurretValues.BreakSound.Play();
        base.DestroyTurret();
    }
}
