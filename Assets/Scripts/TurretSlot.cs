using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class TurretSlot : MonoBehaviour
{
    [SerializeField] int slotNum;
    [SerializeField] RingFloor parentFloor;
    [SerializeField] TurretAbstract occupyingTurret;

    public void Build(TurretAbstract turretAbstract)
    {
        parentFloor.BuildTurret(turretAbstract);
    }

    public TurretSlot SelectSlot()
    {
        parentFloor.SelectSpotToBuild(slotNum);
        return this;
    }
}
