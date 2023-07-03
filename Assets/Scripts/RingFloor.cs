using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class RingFloor : MonoBehaviour
{
    [SerializeField] Transform parentTransform;
    [SerializeField] bool canHaveTurrets;
    [SerializeField] float distanceFromMiddle;
    [SerializeField] int spotToBuild;
    [SerializeField] bool[] spots = new bool[12];


    [SerializeField] GameObject testPrefab;
    GameObject testRef;
    bool testing = false;

    //rotation variables
    [SerializeField] float rotationSpeed;
    bool inPosition = true;
    int rotationDirection;
    int mousePressPos;
    int mouseReleasePos;
    int targetPosition;
    int errorRange;


    void Update()
    {

#if UNITY_EDITOR
        if (testing)
            testRef.transform.position = new(testRef.transform.position.x, testRef.transform.position.y, distanceFromMiddle);
#endif
    }

    private void FixedUpdate()
    {
        if (!inPosition)
        {
            RotateSlowly();
            CheckPosition();
        }

    }

    public void StoreA(Vector3 a)
    {
        mousePressPos = Vector3Angle(a);
        //Debug.Log($"{mousePressPos} stored");
    }
    public void StoreB(Vector3 b)
    {
        mouseReleasePos = Vector3Angle(b);
        CalculateRotation();
    }

    void CalculateRotation()
    {
        //calculate difference
        int rotation = mouseReleasePos - mousePressPos;
        //if rotation is close to 0, leave the function;
        if (rotation > -5 && rotation < 5) return;
        //round difference to multiples of 10
        rotation = Round10(rotation);
        //check rotation length and make sure it takes the quickest route
        if (rotation > 180)
            rotation -= 360;
        else if (rotation < -180)
            rotation += 360;

        targetPosition = Round10((int)parentTransform.eulerAngles.y) - rotation;
        if (targetPosition < 0) targetPosition += 360;
        if (targetPosition > 360) targetPosition -= 360;
        //set the direction of the rotation
        if (rotation < 0)
        {
            rotationDirection = Constants.CLOCKWISE;
            if (targetPosition == 360) targetPosition = 0;
            errorRange = targetPosition + (int)Constants.MIN_ROTATION;
        }
        else
        {
            rotationDirection = Constants.COUNTERCLOCKWISE;
            if (targetPosition == 0) targetPosition = 360;
            errorRange = targetPosition - (int)Constants.MIN_ROTATION;
        }
        //set target rotation
        inPosition = false;
    }

    //happens every tick to check if the rotation is complete
    void CheckPosition()
    {
        float parentRotation = parentTransform.eulerAngles.y;
        //clockwise check
        if (rotationDirection == Constants.CLOCKWISE)
        {
            //please come up with something better for this
            if (parentRotation >= targetPosition && parentRotation < errorRange)
            {
                parentTransform.eulerAngles.Set(0, targetPosition, 0);
                inPosition = true;
            }
        }

        //countclockwise check
        else if (rotationDirection == Constants.COUNTERCLOCKWISE)
        {
            //please come up with something better for this
            if (parentRotation <= targetPosition && parentRotation > errorRange)
            {
                parentTransform.eulerAngles.Set(0, targetPosition, 0);
                inPosition = true;
            }
        }
    }

    public void Rotate(int degrees)
    {
        parentTransform.Rotate(0, degrees, 0);
    }

    public void RotateSlowly()
    {
        parentTransform.Rotate(0, rotationDirection * rotationSpeed * Time.fixedDeltaTime, 0);

    }

    void GetRotationTarget(Vector2 a, Vector2 b)
    {
        //turn a position into a radial position
        float radA = Mathf.Asin(a.x);
    }

    //gets a vector 3 and returns the radial position
    int Vector3Angle(Vector3 input)
    {
        return Mathf.RoundToInt(Mathf.Atan2(input.z, input.x) * Mathf.Rad2Deg);
    }

    int Round10(int input)
    {
        //checks for negative numbers
        int negativeMultiplier = 1;
        if (input < 0)
        {
            negativeMultiplier = -1;
            input *= -1;
        }
        //logic
        if (input % 10 < 5)
        {
            return (input - (input % 10)) * negativeMultiplier;
        }
        return (input + (10 - input % 10)) * negativeMultiplier;
    }

    public void SelectSpotToBuild(int spotNum)
    {
        spotToBuild = spotNum;
    }

    public void FreeUpSpot(int spotNum)
    {
        spots[spotNum] = false;
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
                    float radPosition = Constants.TURRET_SPACING * spotToBuild + parentTransform.eulerAngles.y * Mathf.Deg2Rad;
                    var t = Instantiate(turretType,
                        new Vector3(distanceFromMiddle * Mathf.Sin(radPosition), 0, distanceFromMiddle * Mathf.Cos(radPosition)),
                        quaternion.identity, parentTransform);
                    //rotate the turret to face outside
                    t.transform.Rotate(0, radPosition * Mathf.Rad2Deg, 0);
                    //mark turret spot as taken
                    t.AssignPosition(this, spotToBuild);
                    spots[spotToBuild] = true;
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
