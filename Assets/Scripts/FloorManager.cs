using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    public static FloorManager Instance;
    int rotationDegrees = 10;
    int selectedFloor = 1;
    [SerializeField] List<RingFloor> rings = new List<RingFloor>();
    [SerializeField] TurretAbstract turret;



    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //Ring controls
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            selectedFloor--;
            if (selectedFloor < 0) selectedFloor = 0;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            selectedFloor++;
            if (selectedFloor > rings.Count - 1) selectedFloor = rings.Count - 1;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            rings[selectedFloor].Rotate(rotationDegrees);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            rings[selectedFloor].Rotate(-rotationDegrees);
        }

        //Turret Building
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OrderTurret(turret);
        }
    }

    void OrderTurret(TurretAbstract turretToBuild)
    {
        //check if the player has enough currency
        if (Inventory.Instance.CanAfford(turretToBuild.GetPrice()))
        {
            //build the turret if conditions are met
            if (rings[selectedFloor].BuildTurret(turretToBuild))
            {
                //pay currency
                Inventory.Instance.Pay(turretToBuild.GetPrice());
                Debug.Log($"Turret built succesfully, {turretToBuild}, costed {turretToBuild.GetPrice()}");

            }
        }
        else Debug.Log("Not enough currency");

    }
}
