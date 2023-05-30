using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class RingFloor : MonoBehaviour
{
    const float SLICE = Mathf.PI / 6;

    [SerializeField] bool isControlled;
    [SerializeField] bool canHaveTurrets;
    [SerializeField] float distanceFromMiddle;
    [SerializeField] int spotToBuild;
    [SerializeField] bool[] spots = new bool[12];
    [SerializeField] float radRota;


    [SerializeField] GameObject testPrefab;
    GameObject testRef;
    bool testing = false;


    void Update()
    {
        if (isControlled)
        {

        }
#if UNITY_EDITOR
        if (testing)
            testRef.transform.position = new(testRef.transform.position.x, testRef.transform.position.y, distanceFromMiddle);
#endif
    }

    public void Rotate(int degrees)
    {
        transform.Rotate(0, degrees, 0);
        radRota = transform.eulerAngles.y * Mathf.Deg2Rad;
    }

    //if the ring can have turrets and the spot is free, build a turret, return result
    public bool BuildTurret(TurretAbstract turretType)
    {
        if (spotToBuild < 12 && spotToBuild >= 0)
        {
            if (canHaveTurrets)
            {
                if (!spots[spotToBuild])
                {
                    //get the selected radial position in radians relative to rotation
                    float radPosition = SLICE * spotToBuild + radRota;
                    var t = Instantiate(turretType,
                        new Vector3(distanceFromMiddle * Mathf.Sin(radPosition), 0.6f, distanceFromMiddle * Mathf.Cos(radPosition)),
                        quaternion.identity, this.transform);
                    t.transform.Rotate(0, radPosition * Mathf.Rad2Deg, 0);
                    spots[spotToBuild] = true;
                    spotToBuild += 3;
                    return true;
                }
                else Debug.Log("This turret spot is taken");
            }
            else Debug.Log("This ring can't have turrets");
        }
        else Debug.Log("Spot out of bounds");
        return false;
    }

    [ContextMenu("TestTurret")]
    public void TestTurret()
    {
        testing = true;
        testRef = Instantiate(testPrefab, this.transform);
    }
}
